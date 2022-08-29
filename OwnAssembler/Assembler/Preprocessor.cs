using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;

namespace OwnAssembler.Assembler;

public static class Preprocessor
{
    /// <summary>
    ///     Prepares the code for splitting into tokens. <br />
    ///     Doesn't remove single line comments!
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public static string Preprocess(string code)
    {
        var list = FindChars(code, new[] { '"', '\'' });

        var splitted = code.Split('"', '\'');

        for (var i = 0; i < splitted.Length; i += 2) code = code.ToLower();

        var result = new StringBuilder(string.Join('☻', splitted));

        foreach (var pair in list) result[pair.position] = pair.ch;

        result = result.Replace(new string('\0', 1), "");
        result.Append('\0');

        var returnValue = result.ToString();

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
}