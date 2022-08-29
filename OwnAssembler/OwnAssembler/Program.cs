using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters.Binary;
using Connector;
using OwnAssembler.Assembler;
using Processor = Cpu.CentralProcessingUnit.Cpu;

namespace OwnAssembler;

public static class Program
{
    public static void Main(string[] args)
    {
        Start(GetParameters(args));
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    private static Dictionary<string, object> GetParameters(IReadOnlyList<string> args)
    {
        var parameters = new Dictionary<string, object>();

        var defaultParameters = new List<(string key, object value)>
        {
            ("-codepath", "Code.asmEasy"),
            ("-compile", true),
            ("-bytecoderead", "byteCode.dat"),
            ("-bytecodesave", "byteCode.dat")
        };

        for (var index = 0; index < args.Count - 1; index++)
        {
            var arg = args[index].ToLower();

            index++;

            object value = arg != "-compile" ? args[index] : args[index].ToLower() == "true";
            parameters.Add(arg, value);
        }

        foreach (var pair in defaultParameters.Where(pair => !parameters.ContainsKey(pair.key)))
            parameters.Add(pair.key, pair.value);

        return parameters;
    }


    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    private static void Start(IReadOnlyDictionary<string, object> parameters)
    {
        var commands = new List<ICommand>(64);
        var code = File.ReadAllText((string)parameters["-codepath"]);
        var byteCode = new ByteCode(commands);

        var needsCompilation = (bool)parameters["-compile"];
        var byteCodeSave = (string)parameters["-bytecodesave"];
        var byteCodeRead = (string)parameters["-bytecoderead"];

        if (needsCompilation)
        {
            CompilerToBytecode.Compile(code, commands);
            SerializeByteCode(byteCodeSave, byteCode);
        }

        DeserializeByteCode(byteCodeRead, out byteCode);


        Processor.StartNewApplication(byteCode);
    }


    // there is no point in worrying about security in this context
#pragma warning disable SYSLIB0011
    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    private static void SerializeByteCode(string byteCodePath, ByteCode byteCode)
    {
        var formatter = new BinaryFormatter();
        using var fs = new FileStream(byteCodePath, FileMode.OpenOrCreate);
        formatter.Serialize(fs, byteCode);
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    private static void DeserializeByteCode(string byteCodePath, out ByteCode byteCode)
    {
        var formatter = new BinaryFormatter();
        using var fs = new FileStream(byteCodePath, FileMode.OpenOrCreate);
        byteCode = (ByteCode)formatter.Deserialize(fs);
    }
#pragma warning restore SYSLIB0011
}