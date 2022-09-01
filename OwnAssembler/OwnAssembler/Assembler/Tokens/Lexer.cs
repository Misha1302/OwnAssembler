using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;

namespace OwnAssembler.Assembler.Tokens;

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

        { "converttostring", Kind.ConvertToString },
        { "converttoint", Kind.ConvertToInt },
        { "converttodouble", Kind.ConvertToDouble },
        { "converttochar", Kind.ConvertToChar },

        { "output", Kind.Output },
        { "readkey", Kind.ReadKey },
        { "readline", Kind.ReadLine },

        { "and", Kind.And },
        { "or", Kind.Or },
        { "not", Kind.Not },
        { "xor", Kind.Xor },
        { "dev", Kind.Division },
        { "mul", Kind.Multiplication },
        { "shr", Kind.ShiftRight },
        { "shl", Kind.ShiftLeft },

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
        var token = new Token(Kind.Unknown, "");
        var tokens = new List<Token>();

        ignoredTokenKinds ??= new[] { Kind.Whitespace, Kind.Eof };

        while (token.TokenKind != Kind.Eof)
        {
            token = GetNextToken();
            if (!ignoredTokenKinds.Contains(token.TokenKind))
                tokens.Add(token);
        }

        return tokens;
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    private Token GetNextToken()
    {
        while (true)
        {
            _position++;

            if (_code.Length <= _position || _code[_position] == '\0') return new Token(Kind.Eof, '\0');

            var currentChar = _code[_position];

            if (currentChar == '\n') return new Token(Kind.NewLine, "\n");
            if (char.IsWhiteSpace(currentChar)) return new Token(Kind.Whitespace, currentChar);

            switch (currentChar)
            {
                case '"':
                {
                    var value = _code[(_position + 1)..(_code[(_position + 1)..].IndexOf('"') + _position + 1)];
                    _position += value.Length + 1;
                    value = Regex.Unescape(value);
                    return new Token(Kind.String, value, value);
                }
                case '\'':
                {
                    _position++;
                    var ch = _code[_position];
                    _position++;
                    return new Token(Kind.Char, ch, ch);
                }
                case ';':
                {
                    while (_code[_position] != '\n')
                    {
                        _position++;
                        if (_code[_position] == '\0') return new Token(Kind.Eof, '\0');
                    }

                    _position--;
                    continue;
                }
            }

            if (currentChar == '-' || char.IsNumber(currentChar))
                return GetNextNumberToken();

            foreach (var commandPair in _commands.Where(commandPair =>
                         _code[_position..].IndexOf(commandPair.Key, StringComparison.Ordinal) == 0))
            {
                if (_code.Length > _position + commandPair.Key.Length + 1)
                {
                    if (Regex.IsMatch(_code[_position + commandPair.Key.Length].ToString(), "[a-zA-Z]")) continue;

                    _position += commandPair.Key.Length - 1;

                    return new Token(commandPair.Value, commandPair.Key);
                }

                _position += commandPair.Key.Length - 1;
                return new Token(commandPair.Value, commandPair.Key);
            }

            return new Token(Kind.Unknown, currentChar);
        }
    }

    private Token GetNextNumberToken()
    {
        switch (_code[_position + 1])
        {
            case 'x':
                _position += 2;
                return GetNextHexToken();
            case 'b':
                _position += 2;
                return GetNextBinaryToken();
            default:
                return GetNextDecimalToken();
        }
    }

    private Token GetNextHexToken()
    {
        const int numberBase = 16;
        var validCharacters = new[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f' };
        return GetNextNumberToken(numberBase, validCharacters);
    }

    private Token GetNextBinaryToken()
    {
        const int numberBase = 2;
        var validCharacters = new[] { '0', '1' };
        return GetNextNumberToken(numberBase, validCharacters);
    }

    private Token GetNextNumberToken(int numberBase, char[] validCharacters)
    {
        var number = new StringBuilder();
        var ch = _code[_position];

        do
        {
            number.Append(ch);
            _position++;
        } while (_code.Length > _position && validCharacters.Contains(ch = _code[_position]));

        _position--;

        var numberStr = number.ToString();
        return new Token(Kind.Int, numberStr, Convert.ToInt32(numberStr, numberBase));
    }

    private Token GetNextDecimalToken()
    {
        const int numberBase = 10;
        var validCharacters = new[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        return GetNextNumberToken(numberBase, validCharacters);
    }
}

public enum NumberType
{
    Decimal,
    Hex,
    Binary
}