namespace OwnAssembler;

public class Token
{
    public readonly Kind TokenKind;
    public readonly int? Value;
    public readonly bool IsCommand;

    public Token(Kind tokenKind, int? value=null, bool isCommand = false)
    {
        TokenKind = tokenKind;
        IsCommand = isCommand;
        Value = value;
    }
}