using System.Diagnostics;
using System.Runtime.CompilerServices;
using Connector;

namespace Launcher;

public class Application
{
    private readonly IReadOnlyList<ICommand> _commands;
    private readonly CpuStack _cpuStack;
    private readonly bool _debugMode;
    private readonly bool _exitWhenFinished;

    private int _commandIndex;
    private bool _isExited;
    private Stopwatch? _stopwatch;

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public Application(ByteCode byteCode, bool debugMode, bool exitWhenFinished, Stopwatch? stopwatch = null)
    {
        _cpuStack = new CpuStack();

        _commands = byteCode.Commands;
        _debugMode = debugMode;
        _exitWhenFinished = exitWhenFinished;

        if (stopwatch != null) _stopwatch = stopwatch;
    }


    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    private void OnApplicationExit()
    {
        _stopwatch?.Stop();
        _isExited = true;

        Console.WriteLine($"\nExecution time: {_stopwatch?.ElapsedMilliseconds / 1000f} sec");
        if (!_exitWhenFinished) Console.ReadKey();
    }


    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    private void OnApplicationStart()
    {
        _stopwatch = Stopwatch.StartNew();
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    private void ApplicationTakeOneStep()
    {
        if (_debugMode) ApplicationTakeOneDebugStep(_commands);
        else ApplicationTakeOneRealiseStep(_commands);

        if (_commandIndex == -1) OnApplicationExit();
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    private void ApplicationTakeOneRealiseStep(IReadOnlyList<ICommand> commands)
    {
        commands[_commandIndex].Execute(_cpuStack, ref _commandIndex);
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    private void ApplicationTakeOneDebugStep(IReadOnlyList<ICommand> commands)
    {
        WriteCommandDebug(commands);

        Logger.WriteLogs(_commandIndex, _cpuStack);

        commands[_commandIndex].Execute(_cpuStack, ref _commandIndex);
        Console.WriteLine();
    }


    private void WriteCommandDebug(IReadOnlyList<ICommand> commands)
    {
        commands[_commandIndex].Dump();
        Console.CursorLeft = 40;
        Console.Write("| ");
        Console.Write(_commandIndex);
        Console.Write(" | ");
    }

    public void Start()
    {
        OnApplicationStart();
        while (!_isExited) ApplicationTakeOneStep();
    }
}