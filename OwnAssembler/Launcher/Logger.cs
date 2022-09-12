using System.Runtime.CompilerServices;
using System.Text;
using Connector;

namespace Launcher;

public static class Logger
{
    private const string RAM_LOG_FILE_NAME = "ramLog.txt";
    private const string STACK_LOG_FILE_NAME = "stackLog.txt";
    private const string IF_VALUE_IS_NULL = "null";

    static Logger()
    {
        File.WriteAllText(RAM_LOG_FILE_NAME, "");
        File.WriteAllText(STACK_LOG_FILE_NAME, "");
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public static void WriteLogs(int commandIndex, CpuStack cpuStack)
    {
        var stackLogString = new StringBuilder(64);
        var ramLogString = new StringBuilder(64);

        stackLogString.Append($"{commandIndex}".PadLeft(4) + " | ");
        ramLogString.Append($"{commandIndex}".PadLeft(4) + " | ");

        WriteStackLogs(stackLogString, cpuStack);
        WriteRamLogs(ramLogString);
    }

    private static void WriteRamLogs(StringBuilder ramLogString)
    {
        foreach (var pair in Ram.RamDictionary)
        {
            var value = ((pair.Value ?? IF_VALUE_IS_NULL).ToString() ?? string.Empty).ToLiteral();
            value = $"\"{value}\"";
            ramLogString.Append($"{pair.Key}: {value}::{pair.Value?.GetType().Name}".PadRight(20) + " ");
        }

        using var fs = File.AppendText(RAM_LOG_FILE_NAME);
        fs.WriteLine(ramLogString);
    }

    private static void WriteStackLogs(StringBuilder stackLogString, CpuStack cpuStack)
    {
        for (var i = 0; i < cpuStack.Count; i++)
        {
            var value = ((cpuStack[i] ?? IF_VALUE_IS_NULL).ToString() ?? string.Empty).ToLiteral();
            value = $"\"{value}\"";
            stackLogString.Append($"{i}: {value}::{cpuStack[i]?.GetType().Name}".PadRight(20) + " ");
        }

        using var fs = File.AppendText(STACK_LOG_FILE_NAME);
        fs.WriteLine(stackLogString);
    }
}