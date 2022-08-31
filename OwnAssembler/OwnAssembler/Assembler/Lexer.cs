using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using Connector;

namespace OwnAssembler.Assembler;

public class Lexer
{
    private readonly string _code;

    private readonly Dictionary<string, Kind> _commands = new()
    {
        { "add", Kind.Add },
        { "equals", Kind.Equals },
        { "gt", Kind.Gt },
        { "lt", Kind.Lt },
        { "sub", Kind.Sub },
        { "jmp", Kind.Jmp },
        { "clear", Kind.Clear },
        { "gettimems", Kind.GetTimeInMilliseconds },
        { "copy", Kind.Copy },
        { "setpriority", Kind.SetPriority },
        { "nop", Kind.Nop },
        { "exit", Kind.Exit },

        { "output", Kind.Output },
        { "readkey", Kind.ReadKey },
        { "readline", Kind.ReadLine },

        { "push", Kind.Push },
        { "pop", Kind.Pop },

        { "if", Kind.If },
        { "else", Kind.Else },
        { "endif", Kind.EndIf },

        { "ramread", Kind.RamRead },
        { "ramwrite", Kind.RamWrite },

        { "setmark", Kind.SetMark },
        { "goto", Kind.Goto },

        { "call", Kind.Call },
        { "ret", Kind.Ret },

        { "import", Kind.Import },
        { "invoke", Kind.Invoke }
    };

    private int _position = -1;


    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public Lexer(string code)
    {
        _code = Preprocessor.Preprocess(code);
    }

    /// <summary>
    ///     Gets all tokens from code<br />
    ///     Eof - end of file (char '\0')
    /// </summary>
    /// <param name="ignoredTokenKinds">default value = { Kind.Whitespace, Kind.Eof }</param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public List<Token> GetTokens(Kind[]? ignoredTokenKinds = null)
    {
        ignoredTokenKinds ??= new[] { Kind.Whitespace, Kind.Eof };

        var returnValue= GetTokensInternal().TakeWhile(token => token.TokenKind != Kind.Eof)
            .Where(token => !ignoredTokenKinds.Contains(token.TokenKind)).ToList();
        
        return returnValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    private IEnumerable<Token> GetTokensInternal()
    {
        while (true)
        {
            whilePoint:
            _position++;

            if (_code.Length <= _position || _code[_position] == '\0')
            {
                yield return new Token(Kind.Eof, '\0');
                continue;
            }

            var currentChar = _code[_position];

            if (currentChar == '\n')
            {
                yield return new Token(Kind.NewLine, "\n");
                continue;
            }

            if (char.IsWhiteSpace(currentChar))
            {
                yield return new Token(Kind.Whitespace, currentChar);
                continue;
            }

            switch (currentChar)
            {
                case '"':
                {
                    var value = _code[(_position + 1)..(_code[(_position + 1)..].IndexOf('"') + _position + 1)];
                    _position += value.Length + 1;
                    value = Regex.Unescape(value);
                    var values = CpuStack.GetIntsFromString(value);
                    foreach (var v in values) yield return new Token(Kind.Int, v.ToString(), v);
                    goto whilePoint;
                }
                case '\'':
                {
                    _position++;
                    var ch = _code[_position];
                    _position++;
                    yield return new Token(Kind.Char, ch, ch);
                    goto whilePoint;
                }
                case ';':
                {
                    while (_code[_position] != '\n')
                    {
                        _position++;
                        if (_code[_position] == '\0') yield return new Token(Kind.Eof, '\0');
                    }

                    _position--;
                    goto whilePoint;
                }
            }

            if (currentChar == '-' || char.IsNumber(currentChar))
            {
                var number = new StringBuilder();

                var ch = _code[_position];
                do
                {
                    if (ch != '_') number.Append(ch);

                    _position++;
                } while (_code.Length > _position && (char.IsNumber(ch = _code[_position]) || ch is '_' or '.'));

                _position--;
                var numberStr = number.ToString();
                yield return new Token(Kind.Int, numberStr, Convert.ToInt32(numberStr));
                goto whilePoint;
            }

            foreach (var commandPair in _commands.Where(commandPair =>
                         _code[_position..].IndexOf(commandPair.Key, StringComparison.Ordinal) == 0))
            {
                if (_code.Length > _position + commandPair.Key.Length + 1)
                {
                    if (Regex.IsMatch(_code[_position + commandPair.Key.Length].ToString(), "[a-zA-Z]")) continue;

                    _position += commandPair.Key.Length - 1;

                    yield return new Token(commandPair.Value, commandPair.Key);
                    goto whilePoint;
                }

                _position += commandPair.Key.Length - 1;
                yield return new Token(commandPair.Value, commandPair.Key);
                goto whilePoint;
            }

            yield return new Token(Kind.Unknown, currentChar);
            goto whilePoint;
        }
    }
}