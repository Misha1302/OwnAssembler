using System.Runtime.CompilerServices;
using OwnAssembler.CentralProcessingUnit;

namespace OwnAssembler.Assembler.LowLevelCommands;

public abstract class BaseBinaryCommand : ICommand
{
    private readonly string _commandName;

    protected BaseBinaryCommand(string commandName)
    {
        _commandName = commandName;
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public void Execute(CpuStack stack, ref int currentCommandIndex)
    {
        var b = stack[^2];
        var a = stack[^1];
        
        stack.Push(ExecuteBinaryCommand(a, b));
        
        
        currentCommandIndex++;
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public void Dump()
    {
        Console.Write($"{_commandName}");
    }

    protected abstract object? ExecuteBinaryCommand(object leftValue, object rightValue);
}