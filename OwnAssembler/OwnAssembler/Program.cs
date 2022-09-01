using System.Runtime;

namespace OwnAssembler;

public static class Program
{
    public static void Main(string[] args)
    {
        // TODO: and compiler optimization
        // optimizations:
        // var % 2 != 0 -> var & 1 == 1
        // 2 * 32 -> 2 << 5
        // fixed
        OptimizeApplication();
        var parameters = Compiler.GetParameters(args);
        Compiler.StartNewApplication(parameters);
        Compiler.StartNewApplication(parameters);
        Compiler.StartNewApplication(parameters);
    }
    
    private static void OptimizeApplication()
    {
        Thread.CurrentThread.Priority = ThreadPriority.Highest;
        ProfileOptimization.SetProfileRoot(Directory.GetCurrentDirectory());
        ProfileOptimization.StartProfile("MainProfile");
    }
}