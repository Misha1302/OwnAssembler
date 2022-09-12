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
        CreateVariablesFromParameters(parameters, out var needsCompilation, out var exitWhenFinished,
            out var assemblerCodePath, out var byteCodeSavePath, out var byteCodeReadPath, out var debugMode);

        var commands = new List<ICommand>(64);
        var code = File.ReadAllText(assemblerCodePath);
        var byteCode = new ByteCode(commands);


        if (needsCompilation)
            if (Compile(code, commands, byteCodeSavePath, byteCode))
                return;

        StartLauncher(debugMode, byteCodeReadPath, exitWhenFinished);
    }

    private static void StartLauncher(bool debugMode, string byteCodeRead, bool exitWhenFinished)
    {
        var filePath = GetLauncherPath();
        var arguments = $"-debug {debugMode} -bytecoderead \"{byteCodeRead}\" -exitwhenfinished {exitWhenFinished}";

        var launcherStartInfo = new ProcessStartInfo
        {
            FileName = filePath,
            Arguments = arguments,
            UseShellExecute = true
        };

        var launcher = new Process();
        launcher.StartInfo = launcherStartInfo;
        launcher.Start();
    }

    private static bool Compile(string code, List<ICommand> commands, string byteCodeSave, ByteCode byteCode)
    {
        var lexer = new Lexer(code);
        var tokens = lexer.GetTokens();
        if (CheckForSyntaxErrors(tokens)) return true;

        CompilerToBytecode.Compile(commands, tokens);
        SerializeByteCode(byteCodeSave, byteCode);
        return false;
    }

    private static string GetLauncherPath()
    {
        var allText = File.ReadAllText("C:\\Users\\Public\\Launcher.url");
        var index = allText.IndexOf("URL=", StringComparison.Ordinal) + 4;
        var length = allText.IndexOf(".exe", StringComparison.Ordinal) + 4;
        var filePath = allText[index..length];
        return filePath;
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


    // there is no point in worrying about security in this context
    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    private static void SerializeByteCode(string byteCodePath, ByteCode byteCode)
    {
        var binaryFormatter = new SoapFormatter();
        File.WriteAllText(byteCodePath, "");
        using var fs = new FileStream(byteCodePath, FileMode.OpenOrCreate);
        binaryFormatter.Serialize(fs, byteCode);
    }
    
    
    private static void CreateVariablesFromParameters(IReadOnlyDictionary<string, object> parameters,
        out bool needsCompilation,
        out bool exitWhenFinished, out string assemblerCodePath, out string byteCodeSavePath,
        out string byteCodeReadPath, out bool debugMode)
    {
        debugMode = (bool)parameters["-debug"];
        needsCompilation = (bool)parameters["-compile"];
        exitWhenFinished = (bool)parameters["-exitwhenfinished"];
        assemblerCodePath = (string)parameters["-codepath"];
        byteCodeSavePath = (string)parameters["-bytecodesave"];
        byteCodeReadPath = (string)parameters["-bytecoderead"];
    }
}