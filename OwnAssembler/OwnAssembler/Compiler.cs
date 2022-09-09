using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters.Binary;
using Connector;
using OwnAssembler.Assembler;
using OwnAssembler.Assembler.FrontEnd;
using OwnAssembler.Assembler.SyntacticalAnalyzer;
using Processor = Cpu.CentralProcessingUnit.Cpu;

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
        else
        {
            DeserializeByteCode(byteCodeRead, out byteCode);
        }

        Processor.StartNewApplication(byteCode, debugMode);
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
            ("-codepath", "Code.asmEasy"),
            ("-bytecoderead", "byteCode.dat"),
            ("-bytecodesave", "byteCode.dat")
        };

        var booleanParameters = new[]
        {
            "-compile",
            "-debug"
        };

        for (var index = 0; index < args.Count - 1; index++)
        {
            var arg = args[index].ToLower();

            index++;

            object value = !booleanParameters.Contains(arg) ? args[index] : args[index].ToLower() == "true";
            parameters.Add(arg, value);
        }

        foreach (var pair in defaultParameters.Where(pair => !parameters.ContainsKey(pair.key)))
            parameters.Add(pair.key, pair.value);

        return parameters;
    }


    // there is no point in worrying about security in this context
#pragma warning disable SYSLIB0011
#warning if you need the security of the stored bytecode, then don't forget to change the serialization and deserialization
    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    private static void SerializeByteCode(string byteCodePath, ByteCode byteCode)
    {
        var binaryFormatter = new BinaryFormatter();
        using var fs = new FileStream(byteCodePath, FileMode.OpenOrCreate);
        binaryFormatter.Serialize(fs, byteCode);
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    private static void DeserializeByteCode(string byteCodePath, out ByteCode byteCode)
    {
        var binaryFormatter = new BinaryFormatter();
        using var fs = new FileStream(byteCodePath, FileMode.Open);
        byteCode = (ByteCode)binaryFormatter.Deserialize(fs);
    }
#pragma warning restore SYSLIB0011
}