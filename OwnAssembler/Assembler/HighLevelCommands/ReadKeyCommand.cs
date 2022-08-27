using System.Runtime.CompilerServices;
using OwnAssembler.Assembler.LowLevelCommands;
using OwnAssembler.CentralProcessingUnit;

namespace OwnAssembler.Assembler.HighLevelCommands;

public class ReadKeyCommand : ICommand
{
    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public void Execute(CpuStack stack, ref int currentCommandIndex)
    {
        stack.Push(Console.ReadKey().KeyChar);
        currentCommandIndex++;
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public void Dump()
    {
        Console.Write("readKey");
    }
}