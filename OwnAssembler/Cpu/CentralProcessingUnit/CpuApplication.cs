using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using Connector;

namespace Cpu.CentralProcessingUnit;

public class CpuApplication
{
    private readonly int _applicationIndex;
    private readonly IReadOnlyList<ICommand> _commands;
    private readonly CpuStack _cpuStack;
    private readonly bool _debugMode;

    private int _commandIndex;
    private Stopwatch _stopwatch = new();

    public int ApplicationPriority = 1;

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public CpuApplication(ByteCode byteCode, int applicationIndex, bool debugMode)
    {
        _commands = byteCode.Commands;
        _cpuStack = new CpuStack();
        _applicationIndex = applicationIndex;
        _debugMode = debugMode;
    }


    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    private void OnApplicationExit()
    {
        _stopwatch.Stop();

        Console.WriteLine($"Execution time: {_stopwatch.ElapsedMilliseconds / 1000f} sec");
        Cpu.KillApplication(_applicationIndex);
    }


    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public void OnApplicationStart()
    {
        _stopwatch = Stopwatch.StartNew();
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public void ApplicationTakeOneStep()
    {
        for (var i = 0; i < ApplicationPriority; i++)
        {
            if (_debugMode) ApplicationTakeOneDebugStep(_commands);
            else ApplicationTakeOneRealiseStep(_commands);

            if (_commandIndex != -1) continue;

            OnApplicationExit();
            return;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    private void ApplicationTakeOneRealiseStep(IReadOnlyList<ICommand> commands)
    {
        commands[_commandIndex].Execute(_cpuStack, ref _commandIndex, _applicationIndex);
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    private void ApplicationTakeOneDebugStep(IReadOnlyList<ICommand> commands)
    {
        WriteCommandDebug(commands);

        WriteLogs();

        commands[_commandIndex].Execute(_cpuStack, ref _commandIndex, _applicationIndex);
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


    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    private void WriteLogs()
    {
        var stackLogString = new StringBuilder(512);
        var ramLogString = new StringBuilder(512);

        stackLogString.Append($"{_commandIndex}".PadRight(4) + "| ");
        ramLogString.Append($"{_commandIndex}".PadRight(4) + "| ");

        for (var i = 0; i < _cpuStack.Count; i++)
            stackLogString.Append($"{i}: {Regex.Escape(_cpuStack[i]?.ToString() ?? string.Empty)}".PadRight(20));
        foreach (var pair in Ram.RamDictionary)
            stackLogString.Append($"{pair.Key}: {Regex.Escape(pair.Value?.ToString() ?? string.Empty)}".PadRight(20));

        using (var fs = File.AppendText("stackLog.txt"))
        {
            fs.WriteLine(stackLogString);
        }

        using (var fs = File.AppendText("ramLog.txt"))
        {
            fs.WriteLine(ramLogString);
        }
    }
}