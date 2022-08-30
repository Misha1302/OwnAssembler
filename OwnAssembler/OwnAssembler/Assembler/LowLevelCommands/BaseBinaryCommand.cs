using System.Runtime.CompilerServices;
using Connector;

namespace OwnAssembler.Assembler.LowLevelCommands;

[Serializable]
public abstract class BaseBinaryCommand : ICommand
{
    private readonly string _commandName;

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    protected BaseBinaryCommand(string commandName)
    {
        _commandName = commandName;
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public void Execute(CpuStack stack, ref int currentCommandIndex, int applicationIndex)
    {
        stack.Push(ExecuteBinaryCommand(stack.Pop()!, stack.Pop()!));

        currentCommandIndex++;
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public void Dump()
    {
        Console.Write($"{_commandName}");
    }

    protected abstract object? ExecuteBinaryCommand(object leftValue, object rightValue);
}