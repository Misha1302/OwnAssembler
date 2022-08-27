using System.Runtime.CompilerServices;
using OwnAssembler.Assembler.LowLevelCommands;
using OwnAssembler.CentralProcessingUnit;

namespace OwnAssembler.Assembler.HighLevelCommands;

public class GetTimeInMillisecondsCommand : ICommand
{
    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public void Execute(CpuStack stack, ref int currentCommandIndex)
    {
        stack.Push(DateTimeOffset.Now.ToUnixTimeMilliseconds());
        currentCommandIndex++;
    }

    
    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public void Dump()
    {
        Console.Write("getting time in milliseconds");
    }
}