namespace Launcher;

public static class Program
{
    public static void Main(string[] args)
    {
        CreateLnk();
        Helper.OptimizeApplication();

        var parameters = Helper.GetParameters(args);
        var path = (string)parameters["-bytecoderead"];
        var isDebug = (bool)parameters["-debug"];

        Helper.DeserializeByteCode(path, out var byteCode);
        var application = new Application(byteCode, isDebug);
        application.Start();
    }

    private static void CreateLnk()
    {
        const string PATH = "C:\\Users\\Public\\Launcher.url";
        File.WriteAllText(PATH, "");
        
        using var writer = new StreamWriter(PATH);
        var app = Environment.ProcessPath;
        writer.WriteLine("[InternetShortcut]");
        writer.WriteLine("URL=file:///" + app);
        writer.WriteLine("IconIndex=0");
        var icon = app.Replace('\\', '/');
        writer.WriteLine("IconFile=" + icon);
        writer.Flush();
    }
}