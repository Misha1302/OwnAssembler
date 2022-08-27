using System.Runtime.CompilerServices;
using OwnAssembler.Assembler.LowLevelCommands;
using OwnAssembler.CentralProcessingUnit;

namespace OwnAssembler.Assembler.HighLevelCommands;

public class ReadLineCommand : ICommand
{
    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public void Execute(CpuStack stack, ref int currentCommandIndex)
    {
        stack.Push(Console.ReadLine());
        currentCommandIndex++;
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public void Dump()
    {
        Console.Write("readLine");
    }
}