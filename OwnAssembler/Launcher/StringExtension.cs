using System.Text;

namespace Launcher;

public static class StringExtension
{
    public static string ToLiteral(this string input)
    {
        var literal = new StringBuilder(input.Length * 2);

        ToLiteralInternal(input, literal);

        return literal.ToString();
    }

    private static void ToLiteralInternal(string input, StringBuilder literal)
    {
        foreach (var c in input)
            switch (c)
            {
                case '\"':
                    literal.Append("\\\"");
                    break;
                case '\\':
                    literal.Append(@"\\");
                    break;
                case '\0':
                    literal.Append(@"\0");
                    break;
                case '\a':
                    literal.Append(@"\a");
                    break;
                case '\b':
                    literal.Append(@"\b");
                    break;
                case '\f':
                    literal.Append(@"\f");
                    break;
                case '\n':
                    literal.Append(@"\n");
                    break;
                case '\r':
                    literal.Append(@"\r");
                    break;
                case '\t':
                    literal.Append(@"\t");
                    break;
                case '\v':
                    literal.Append(@"\v");
                    break;
                default:
                    // ASCII printable character
                    if (c >= 0x20 && c <= 0x7e)
                    {
                        literal.Append(c);
                    }
                    // As UTF16 escaped character
                    else
                    {
                        literal.Append(@"\u");
                        literal.Append(((int)c).ToString("x4"));
                    }

                    break;
            }
    }
}