using System.Runtime.CompilerServices;

namespace OwnAssembler.Assembler.FrontEnd;

public class Token
{
    public string Text;
    public readonly Kind TokenKind;
    public readonly object? Value;

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public Token(Kind tokenKind, string text, object? value = null)
    {
        TokenKind = tokenKind;
        Text = text;
        Value = value;
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public Token(Kind tokenKind, char text, object? value = null)
    {
        TokenKind = tokenKind;
        Text = text.ToString();
        Value = value;
    }
}