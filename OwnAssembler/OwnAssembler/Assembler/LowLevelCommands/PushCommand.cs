using System.Runtime.CompilerServices;
using Connector;

namespace OwnAssembler.Assembler.LowLevelCommands;

[Serializable]
public class PushCommand : ICommand
{
    private readonly int[] _constantInts;

    public PushCommand(params int[] constantInts)
    {
        _constantInts = constantInts;
    }


    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public void Execute(CpuStack stack, ref int currentCommandIndex, int applicationIndex)
    {
        foreach (var constantInt in _constantInts) stack.Push(constantInt);
        currentCommandIndex++;
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public void Dump()
    {
        Console.Write("push");
    }
}