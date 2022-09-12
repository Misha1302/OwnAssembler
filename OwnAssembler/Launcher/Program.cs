namespace Launcher;

public static class Program
{
    public static void Main(string[] args)
    {
        MainInternal(args);
    }

    private static void MainInternal(IReadOnlyList<string> args)
    {
        Helper.OptimizeApplication();

        if (!Helper.CreateLnk()) throw new Exception("Error creating lnk");

        var parameters = Helper.GetParameters(args);
        var path = (string)parameters["-bytecoderead"];
        var isDebug = (bool)parameters["-debug"];
        var exitWhenFinished = (bool)parameters["-exitwhenfinished"];

        Helper.DeserializeByteCode(path, out var byteCode);
        var application = new Application(byteCode, isDebug, exitWhenFinished);
        application.Start();
    }
}