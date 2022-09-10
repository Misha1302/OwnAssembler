using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;

namespace OwnAssembler.Assembler.FrontEnd;

public class Lexer
{
    private static readonly IReadOnlyDictionary<string, Kind> commands = new Dictionary<string, Kind>
    {
        { "add", Kind.Add },
        { "equals", Kind.Equals },
        { "gt", Kind.Gt },
        { "lt", Kind.Lt },
        { "sub", Kind.Sub },
        { "jmp", Kind.Jmp },
        { "clear", Kind.Clear },
        { "getTimeMs", Kind.GetTimeInMilliseconds },
        { "copy", Kind.Copy },
        { "nop", Kind.Nop },
        { "exit", Kind.Exit },

        { "toStr", Kind.ConvertToString },
        { "toInt", Kind.ConvertToInt },
        { "toDouble", Kind.ConvertToDouble },
        { "toChar", Kind.ConvertToChar },

        { "output", Kind.Output },
        { "readKey", Kind.ReadKey },
        { "readline", Kind.ReadLine },

        { "and", Kind.And },
        { "or", Kind.Or },
        { "not", Kind.Not },
        { "xor", Kind.Xor },
        { "dev", Kind.Division },
        { "mul", Kind.Multiplication },
        { "shr", Kind.ShiftRight },
        { "shl", Kind.ShiftLeft },
        { "mod", Kind.Mod },

        { "push", Kind.Push },
        { "pop", Kind.Pop },

        { "if", Kind.If },
        { "else", Kind.Else },
        { "endif", Kind.EndIf },

        { "ramRead", Kind.RamRead },
        { "ramWrite", Kind.RamWrite },

        { "setMark", Kind.SetMark },
        { "goto", Kind.Goto },

        { "call", Kind.Call },
        { "ret", Kind.Ret },

        { "import", Kind.Import },
        { "invoke", Kind.Invoke },


        { "#define", Kind.Define }
    };

    private readonly string _code;

    private int _position = -1;


    static Lexer()
    {
        commands = commands.Select(x => (x.Key.ToLower(), x.Value)).ToDictionary(x => x.Item1, x => x.Value);
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public Lexer(string code)
    {
        _code = Preprocessor.Preprocess(code);
    }

    /// <summary>
    ///     Gets all tokens from code<br />
    ///     Eof - end of file
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public List<Token> GetTokens()
    {
        var token = new Token(Kind.Unknown, "");
        var tokens = new List<Token>();

        while (token.TokenKind != Kind.Eof)
        {
            token = GetNextToken();
            tokens.Add(token);
        }

        UnityUnknownTokens(tokens);

        tokens = Preprocessor.PreprocessTokens(tokens);

        tokens.RemoveAt(tokens.Count - 1); // delete eof

        return tokens;
    }

    private static void UnityUnknownTokens(IList<Token> tokens)
    {
        for (var i = 0; i < tokens.Count; i++)
        {
            var startPosition = i;
            if (tokens[i].TokenKind != Kind.Unknown) continue;

            i++;
            while (tokens[i].TokenKind == Kind.Unknown)
            {
                tokens[startPosition].Text += tokens[i].Text;
                tokens.RemoveAt(i);
            }
        }
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
                    var endIndex = Regex.Match(_code[(_position + 1)..], "(?<!(\\\\))\"").Index + _position + 1;
                    var value = _code[(_position + 1)..endIndex];
                    _position += value.Length + 1;
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

            foreach (var commandPair in commands.Where(commandPair =>
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
        const int NUMBER_BASE = 16;
        var validCharacters = new[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f' };
        return GetNextNumberToken(NUMBER_BASE, validCharacters);
    }

    private Token GetNextBinaryToken()
    {
        const int NUMBER_BASE = 2;
        var validCharacters = new[] { '0', '1' };
        return GetNextNumberToken(NUMBER_BASE, validCharacters);
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
        const int NUMBER_BASE = 10;
        var validCharacters = new[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        return GetNextNumberToken(NUMBER_BASE, validCharacters);
    }
}

public enum NumberType
{
    Decimal,
    Hex,
    Binary
}