using System.Runtime;
using Connector;

namespace OwnAssembler;

public static class Program
{
    public static void Main(string[] args)
    {
        MainInternal(args);
    }

    private static void MainInternal(IReadOnlyList<string> args)
    {
        OptimizeApplication();
        var parameters = GetParameters(args);
        Compiler.StartNewApplication(parameters);
    }

    private static void OptimizeApplication()
    {
        Thread.CurrentThread.Priority = ThreadPriority.Highest;
        ProfileOptimization.SetProfileRoot(Directory.GetCurrentDirectory());
        ProfileOptimization.StartProfile("MainProfile");
    }


    private static Dictionary<string, object> GetParameters(IReadOnlyList<string> args)
    {
        var defaultParameters = new (string key, object value)[]
        {
            ("-compile", true),
            ("-debug", false),
            ("-exitwhenfinished", true),
            ("-codepath", Path.GetFullPath("Code.asmEasy")),
            ("-bytecoderead", Path.GetFullPath("byteCode.abcf")),
            ("-bytecodesave", Path.GetFullPath("byteCode.abcf"))
        };

        var booleanParameters = new[]
        {
            "-exitwhenfinished",
            "-compile",
            "-debug"
        };

        var pathParameters = new[]
        {
            "-bytecoderead",
            "-bytecodesave",
            "-codepath"
        };


        var parameters = ArgumentsParser.GetParameters(args, defaultParameters, booleanParameters, pathParameters);
        return parameters;
    }
}