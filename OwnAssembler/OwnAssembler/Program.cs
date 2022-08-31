using System.Runtime;

namespace OwnAssembler;

public static class Program
{
    public static void Main(string[] args)
    {
        // TODO: syntactical analyzer
        OptimizeApplication();
        Compiler.StartNewApplication(Compiler.GetParameters(args));
    }
    
    private static void OptimizeApplication()
    {
        Thread.CurrentThread.Priority = ThreadPriority.Highest;
        ProfileOptimization.SetProfileRoot(Directory.GetCurrentDirectory());
        ProfileOptimization.StartProfile("MainProfile");
    }
}