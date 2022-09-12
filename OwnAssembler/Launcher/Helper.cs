using System.Runtime;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters.Soap;
using Connector;

namespace Launcher;

public static class Helper
{
    public static bool CreateLnk()
    {
        const string PATH = "C:\\Users\\Public\\Launcher.url";
        var app = Environment.ProcessPath;
        if (app == null) return false;

        File.WriteAllText(PATH, "");

        using var writer = new StreamWriter(PATH);

        writer.WriteLine("[InternetShortcut]");
        writer.WriteLine("URL=file:///" + app);
        writer.WriteLine("IconIndex=0");
        var icon = app.Replace('\\', '/');
        writer.WriteLine("IconFile=" + icon);
        writer.Flush();

        return true;
    }

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
        var defaultParameters = new (string key, object value)[]
        {
            ("-debug", false),
            ("-exitwhenfinished", true),
            ("-bytecoderead", "byteCode.abcf") // assembler byte code file
        };

        var booleanParameters = new[]
        {
            "-debug",
            "-exitwhenfinished"
        };

        var pathParameters = Array.Empty<string>();

        var parameters = ArgumentsParser.GetParameters(args, defaultParameters, booleanParameters, pathParameters);

        return parameters;
    }
}