using System.Text;

namespace OwnAssembler;

public class Lexer
{
    private readonly string _code;

    private readonly Dictionary<string, Kind> _commands = new()
    {
        { "put", Kind.Put },
        { "add", Kind.Add },
        { "equals", Kind.Equals },
        { "gt", Kind.Gt },
        { "lt", Kind.Lt },
        { "sub", Kind.Sub },
        { "jmp", Kind.Jmp },
        { "output", Kind.Output },
        { "readkey", Kind.ReadKey },
        { "if", Kind.If },
        { "else", Kind.Else },
        { "endif", Kind.EndIf },
    };

    private int _position = -1;


    public Lexer(string code)
    {
        _code = Preprocessor.Preprocess(code);
    }

    /// <summary>
    ///     Gets all tokens from code
    /// </summary>
    /// <param name="ignoredTokenKinds">default value = { Kind.Whitespace, Kind.Eof }</param>
    /// <returns></returns>
    public List<Token> GetTokens(Kind[]? ignoredTokenKinds = null)
    {
        var token = new Token(Kind.Unknown);
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

    private Token GetNextToken()
    {
        _position++;

        if (_code.Length <= _position || _code[_position] == '\0') return new Token(Kind.Eof);

        var currentChar = _code[_position];

        if (currentChar == '\n') return new Token(Kind.NewLine);
        if (char.IsWhiteSpace(currentChar)) return new Token(Kind.Whitespace);

        if (char.IsNumber(currentChar) || currentChar == '-')
        {
            var number = new StringBuilder();

            do
            {
                number.Append(_code[_position]);
                _position++;
            } while (_code.Length > _position && char.IsNumber(_code[_position]));

            _position--;
            return new Token(Kind.Int, Convert.ToInt32(number.ToString()));
        }

        foreach (var commandPair in _commands.Where(commandPair =>
                     _code[_position..].IndexOf(commandPair.Key, StringComparison.Ordinal) == 0))
        {
            _position += commandPair.Key.Length - 1;
            return new Token(commandPair.Value, isCommand: false);
        }

        return new Token(Kind.Unknown);
    }
}