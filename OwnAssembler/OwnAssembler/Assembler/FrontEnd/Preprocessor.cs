using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;

namespace OwnAssembler.Assembler.FrontEnd;

public static class Preprocessor
{
    /// <summary>
    ///     Prepares the code for splitting into tokens. <br />
    ///     Doesn't remove single line comments!
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public static string Preprocess(string code)
    {
        var timeNow0 = DateTimeOffset.Now.ToUnixTimeMilliseconds().ToString();
        var timeNow1 = (DateTimeOffset.Now.ToUnixTimeMilliseconds() + 1).ToString();

        code = code.Replace("\\\"", timeNow0);
        code = code.Replace("\\'", timeNow1);


        var list = FindChars(code, new[] { '"', '\'' });

        var split = code.Split('"', '\'');

        for (var i = 0; i < split.Length; i += 2) split[i] = split[i].ToLower();

        var result = new StringBuilder(string.Join('☻', split));

        foreach (var pair in list) result[pair.position] = pair.ch;

        result = result.Replace("\0", "");
        result.Append('\0');

        var returnValue = result.ToString();
        returnValue = returnValue.Replace(timeNow0, "\\\"");
        returnValue = returnValue.Replace(timeNow1, "\\\'");

        return returnValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    private static List<(int position, char ch)> FindChars(string code, char[] chars)
    {
        var list = new List<(int position, char ch)>();
        for (var i = 0; i < code.Length; i++)
        {
            var ch = code[i];
            if (chars.Contains(ch)) list.Add((i, ch));
        }

        return list;
    }

    /// <summary>
    ///     executes preprocessor directives
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public static List<Token> PreprocessTokens(IEnumerable<Token> tokens, bool removeWhitespaces = true)
    {
        var tokensInternal = new List<Token>(tokens);

        if (removeWhitespaces)
            tokensInternal.RemoveAll(token1 => token1.TokenKind == Kind.Whitespace);

        ReplaceDefine(tokensInternal);

        return tokensInternal;
    }

    private static void ReplaceDefine(List<Token> tokensInternal)
    {
        for (var i = 0; i < tokensInternal.Count; i++)
            if (tokensInternal[i].TokenKind == Kind.Define)
            {
                var text = tokensInternal[i + 1].Text.ToLower();
                var lexer = new Lexer(Regex.Unescape(tokensInternal[i + 2].Text));
                var value = lexer.GetTokens();
                tokensInternal.RemoveRange(i, 3);

                for (var j = 0; j < tokensInternal.Count; j++)
                    if (tokensInternal[j].Text == text)
                    {
                        tokensInternal.RemoveAt(j);
                        tokensInternal.InsertRange(j, value);
                    }
            }
    }
}