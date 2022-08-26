namespace OwnAssembler;

public class Token
{
    public readonly string Text;
    public readonly Kind TokenKind;
    public readonly object? Value;

    public Token(Kind tokenKind, string text, object? value = null)
    {
        TokenKind = tokenKind;
        Text = text;
        Value = value;
    }
    public Token(Kind tokenKind, char text, object? value = null)
    {
        TokenKind = tokenKind;
        Text = text.ToString();
        Value = value;
    }
}