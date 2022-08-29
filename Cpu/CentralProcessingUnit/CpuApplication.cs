using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using Connector;

namespace Cpu.CentralProcessingUnit;

public class CpuApplication
{
    private readonly ByteCode _byteCode;
    private readonly CpuStack _cpuStack;
    private int _commandIndex;
    private Stopwatch _stopwatch = new();

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public CpuApplication(ByteCode byteCode)
    {
        _byteCode = byteCode;
        _cpuStack = new CpuStack();
    }


    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public void OnApplicationExit()
    {
        _stopwatch.Stop();

        Console.WriteLine();
        Console.WriteLine($"Execution time: {_stopwatch.ElapsedMilliseconds} ms");
    }


    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public void OnApplicationStart()
    {
        _stopwatch = Stopwatch.StartNew();
    }

    public void ApplicationTakeOneStep(bool debugMode = false)
    {
        if (_commandIndex == -1) return;

        var commands = _byteCode.Commands;

        if (debugMode) ApplicationTakeOneDebugStep(commands);
        else ApplicationTakeOneRealiseStep(commands);
    }

    private void ApplicationTakeOneRealiseStep(IReadOnlyList<ICommand> commands)
    {
        commands[_commandIndex].Execute(_cpuStack, ref _commandIndex);
    }

    private void ApplicationTakeOneDebugStep(IReadOnlyList<ICommand> commands)
    {
        commands[_commandIndex].Dump();
        Console.CursorLeft = 40;
        Console.Write("| ");
        Console.WriteLine(_commandIndex);

        var stackLogString = new StringBuilder(512);
        for (var i = 0; i < _cpuStack.Count; i++) stackLogString.Append($"{i}:{_cpuStack[i]}");
        stackLogString.Append('\n');
        
        File.WriteAllText("stackLog.txt", File.ReadAllText("stackLog.txt") + stackLogString);

        commands[_commandIndex].Execute(_cpuStack, ref _commandIndex);
    }
}