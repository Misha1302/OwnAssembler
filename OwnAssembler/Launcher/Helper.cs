using System.Runtime;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters.Soap;
using Connector;

namespace Launcher;

public static class Helper
{
    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public static void DeserializeByteCode(string byteCodePath, out ByteCode byteCode)
    {
        var binaryFormatter = new SoapFormatter();
        using var fs = new FileStream(byteCodePath, FileMode.Open);
        byteCode = (ByteCode)binaryFormatter.Deserialize(fs);
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public static void OptimizeApplication()
    {
        Thread.CurrentThread.Priority = ThreadPriority.Highest;
        ProfileOptimization.SetProfileRoot(Directory.GetCurrentDirectory());
        ProfileOptimization.StartProfile("MainProfile");
    }

    public static Dictionary<string, object> GetParameters(IReadOnlyList<string> args)
    {
        var parameters = new Dictionary<string, object>();

        var defaultParameters = new (string key, object value)[]
        {
            ("-debug", false),
            ("-bytecoderead", "byteCode.abcf") // assembler byte code file
        };

        var booleanParameters = new[]
        {
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
}