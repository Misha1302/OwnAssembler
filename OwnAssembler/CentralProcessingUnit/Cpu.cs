using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using OwnAssembler.Assembler.LowLevelCommands;

namespace OwnAssembler.CentralProcessingUnit;

public static class Cpu
{
    static Cpu()
    {
        Console.OutputEncoding = Encoding.Unicode;
        Console.InputEncoding = Encoding.Unicode;
    }

    public static void StartNewApplication(string asmCodePath)
    {
        var cpuApplication = new CpuApplication();

        var commands = new List<ICommand>(64);
        var code = File.ReadAllText(asmCodePath);

        commands = CompilerToBytecode.Compile(code, commands, cpuApplication);
        
        var applicationThread = new Thread(() => { cpuApplication.Execute(commands); });
        cpuApplication.ThisThread = applicationThread;
        applicationThread.Start();
    }
}

public class CpuApplication
{
    public Thread ThisThread;
    public readonly CpuStack Stack;
    private Stopwatch _stopwatch = new();

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public CpuApplication()
    {
        Stack = new CpuStack();
    }


    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public void ApplicationExit()
    {
        _stopwatch.Stop();

        Console.WriteLine();
        Console.WriteLine($"Execution time: {_stopwatch.ElapsedMilliseconds} ms");
        Console.ReadKey();
        ThisThread.Interrupt();
    }


    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public void Execute(IReadOnlyList<ICommand> commands, bool debugMode = false)
    {
        _stopwatch = Stopwatch.StartNew();

        if (debugMode) DebugExecuteInternal(commands, Stack);
        else RealiseExecuteInternal(commands, Stack);

        Stack.Clear();
    }

    #region ExecuteInternal

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    private static void RealiseExecuteInternal(IReadOnlyList<ICommand> commands, CpuStack stack)
    {
        for (var index = 0; index < commands.Count;)
        {
            var command = commands[index];
            command.Execute(stack, ref index);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    private void DebugExecuteInternal(IReadOnlyList<ICommand> commands, CpuStack stack)
    {
        for (var index = 0; index < commands.Count;)
        {
            var command = commands[index];
            command.Dump();

            Console.CursorLeft = 70;
            Console.Write("| ");
            Console.Write(index);
            Console.CursorLeft = 77;
            Console.Write("| ");
            command.Execute(stack, ref index);

            Console.WriteLine();
            if (command is BaseBinaryCommand) Console.WriteLine();
        }
    }

    #endregion
}