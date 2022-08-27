using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace OwnAssembler.Assembler;

public static class Preprocessor
{
    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public static string Preprocess(string code)
    {
        code = code.ToLower();
        code = Regex.Replace(code, ";.*", "");
        return code;
    }
}