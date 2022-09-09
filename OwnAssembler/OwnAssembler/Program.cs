using System.Runtime;

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
        var parameters = Compiler.GetParameters(args);
        Compiler.StartNewApplication(parameters);
    }

    private static void OptimizeApplication()
    {
        Thread.CurrentThread.Priority = ThreadPriority.Highest;
        ProfileOptimization.SetProfileRoot(Directory.GetCurrentDirectory());
        ProfileOptimization.StartProfile("MainProfile");
    }
}