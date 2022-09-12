using System.Runtime.CompilerServices;
using Connector;

namespace OwnAssembler.Assembler.LowLevelCommands;

[Serializable]
public class RamWriteCommand : ICommand
{
    private readonly string _address;

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public RamWriteCommand(string address)
    {
        _address = address;
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public void Execute(CpuStack stack, ref int currentCommandIndex)
    {
        Ram.Write(_address, stack.Pop());
        currentCommandIndex++;
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public void Dump()
    {
        Console.Write($"ram write: {_address}");
    }
}