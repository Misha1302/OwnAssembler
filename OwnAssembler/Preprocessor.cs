using System.Text.RegularExpressions;

namespace OwnAssembler;

public static class Preprocessor
{
    public static string Preprocess(string code)
    {
        code = code.ToLower();
        code = Regex.Replace(code, ";.*", "");
        return code;
    }
}