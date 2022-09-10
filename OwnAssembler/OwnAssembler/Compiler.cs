using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters.Soap;
using Connector;
using OwnAssembler.Assembler;
using OwnAssembler.Assembler.FrontEnd;
using OwnAssembler.Assembler.SyntacticalAnalyzer;

namespace OwnAssembler;

public static class Compiler
{
    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public static void StartNewApplication(IReadOnlyDictionary<string, object> parameters)
    {
        var debugMode = (bool)parameters["-debug"];
        var needsCompilation = (bool)parameters["-compile"];
        var assemblerCodePath = (string)parameters["-codepath"];
        var byteCodeSave = (string)parameters["-bytecodesave"];
        var byteCodeRead = (string)parameters["-bytecoderead"];

        var commands = new List<ICommand>(64);
        var code = File.ReadAllText(assemblerCodePath);
        var byteCode = new ByteCode(commands);


        if (needsCompilation)
        {
            var lexer = new Lexer(code);
            var tokens = lexer.GetTokens();
            if (CheckForSyntaxErrors(tokens)) return;

            CompilerToBytecode.Compile(commands, tokens);
            SerializeByteCode(byteCodeSave, byteCode);
        }

        var allText = File.ReadAllText("C:\\Users\\Public\\Launcher.url");
        var index = allText.IndexOf("URL=", StringComparison.Ordinal) + 4;
        var length = allText.IndexOf(".exe", StringComparison.Ordinal) + 4;
        var fileName = allText[index..length];

        var launcher = new Process();
        launcher.StartInfo.FileName = fileName;
        launcher.StartInfo.Arguments = $"-debug {debugMode} -bytecoderead \"{byteCodeRead}\"";
        launcher.StartInfo.UseShellExecute = true;
        launcher.Start();
    }

    private static bool CheckForSyntaxErrors(IReadOnlyList<Token> tokens)
    {
        var syntaxErrors = SyntacticalAnalyzer.CheckForSyntaxErrors(tokens);

        if (syntaxErrors.Count == 0)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Compiled successfully");
            Console.ResetColor();
            return false;
        }

        Console.ForegroundColor = ConsoleColor.Red;
        foreach (var syntaxError in syntaxErrors) Console.WriteLine(syntaxError.ErrorMessage);
        Console.WriteLine("Compilation failed");
        return true;
    }


    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public static Dictionary<string, object> GetParameters(IReadOnlyList<string> args)
    {
        var parameters = new Dictionary<string, object>();

        var defaultParameters = new (string key, object value)[]
        {
            ("-compile", true),
            ("-debug", false),
            ("-codepath", $"{Directory.GetCurrentDirectory()}\\Code.asmEasy"),
            ("-bytecoderead", $"{Directory.GetCurrentDirectory()}\\byteCode.abcf"),
            ("-bytecodesave", $"{Directory.GetCurrentDirectory()}\\byteCode.abcf")
        };

        var booleanParameters = new[]
        {
            "-compile",
            "-debug"
        };

        var pathParameters = new[]
        {
            "-bytecoderead",
            "-bytecodesave",
            "-codepath"
        };

        for (var index = 0; index < args.Count - 1; index++)
        {
            var arg = args[index].ToLower();

            index++;

            object value = !booleanParameters.Contains(arg) ? args[index] : args[index].ToLower() == "true";
            if (pathParameters.Contains(arg))
            {
                var str = value as string ?? throw new Exception("Parameter is not a path (string)");
                if (str[1] != ':') str = Directory.GetCurrentDirectory() + "\\" + str;
                value = str;
            }

            parameters.Add(arg, value);
        }

        foreach (var pair in defaultParameters.Where(pair => !parameters.ContainsKey(pair.key)))
            parameters.Add(pair.key, pair.value);

        return parameters;
    }


    // there is no point in worrying about security in this context
    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    private static void SerializeByteCode(string byteCodePath, ByteCode byteCode)
    {
        var binaryFormatter = new SoapFormatter();
        File.WriteAllText(byteCodePath, "");
        using var fs = new FileStream(byteCodePath, FileMode.OpenOrCreate);
        binaryFormatter.Serialize(fs, byteCode);
    }
}