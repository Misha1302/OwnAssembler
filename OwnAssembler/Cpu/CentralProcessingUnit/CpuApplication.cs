using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
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

        Console.WriteLine($"\nExecution time: {_stopwatch.ElapsedMilliseconds / 1000f} sec");
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

        stackLogString.Append($"{_applicationIndex}::{_commandIndex}".PadRight(7) + "| ");
        ramLogString.Append($"{_applicationIndex}::{_commandIndex}".PadRight(7) + "| ");

        WriteStackLogs(stackLogString);
        WriteRamLogs(ramLogString);
    }

    private static void WriteRamLogs(StringBuilder ramLogString)
    {
        foreach (var pair in Ram.RamDictionary)
        {
            var value = ((pair.Value ?? "null").ToString() ?? string.Empty).ToLiteral();
            ramLogString.Append($"{pair.Key}: {value}::{pair.Value?.GetType().Name}".PadRight(20) + " ");
        }

        using var fs = File.AppendText("ramLog.txt");
        fs.WriteLine(ramLogString);
    }

    private void WriteStackLogs(StringBuilder stackLogString)
    {
        for (var i = 0; i < _cpuStack.Count; i++)
        {
            var value = ((_cpuStack[i] ?? "null").ToString() ?? string.Empty).ToLiteral();
            stackLogString.Append($"{i}: {value}::{_cpuStack[i]?.GetType().Name}".PadRight(20) + " ");
        }

        using var fs = File.AppendText("stackLog.txt");
        fs.WriteLine(stackLogString);
    }
}