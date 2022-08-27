using System.Runtime.CompilerServices;
using OwnAssembler.CentralProcessingUnit;

namespace OwnAssembler.Assembler.LowLevelCommands;

public class RamWriteCommand : ICommand
{
    private readonly string _address;

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public RamWriteCommand(string address)
    {
        _address = address + Thread.CurrentThread.ManagedThreadId;
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public void Execute(CpuStack stack, ref int currentCommandIndex)
    {
        Ram.Write(_address, stack.Peek());
        currentCommandIndex++;
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public void Dump()
    {
        Console.Write($"ram write: {_address}");
    }
}