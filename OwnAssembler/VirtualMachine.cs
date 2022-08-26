using System.Diagnostics;
using OwnAssembler.LowLevelCommands;

namespace OwnAssembler;

internal static class VirtualMachine
{
    private static Stopwatch _stopwatch = new();

    public static void Execute(IReadOnlyList<ICommand> commands, bool debugMode = false)
    {
        var stack = new EditedStack(32);

        _stopwatch = Stopwatch.StartNew();

        if (debugMode) DebugExecuteInternal(commands, stack);
        else RealiseExecuteInternal(commands, stack);
    }

    // method OnExit used in ExitCommand when closing the application
    public static void OnExit(object? sender, EventArgs? e)
    {
        _stopwatch.Stop();
        
        Console.WriteLine();
        Console.WriteLine($"Execution time: {_stopwatch.ElapsedMilliseconds} ms");
        Console.ReadKey();
    }

    private static void RealiseExecuteInternal(IReadOnlyList<ICommand> commands, EditedStack stack)
    {
        for (var index = 0; index < commands.Count;)
        {
            var command = commands[index];
            command.Execute(stack, ref index);
        }
    }

    private static void DebugExecuteInternal(IReadOnlyList<ICommand> commands, EditedStack stack)
    {
        for (var index = 0; index < commands.Count;)
        {
            var command = commands[index];
            command.Dump();

            Console.CursorLeft = 40;
            Console.Write("| ");
            command.Execute(stack, ref index);

            Console.WriteLine();
            if (command is BaseBinaryCommand) Console.WriteLine();
        }
    }
}