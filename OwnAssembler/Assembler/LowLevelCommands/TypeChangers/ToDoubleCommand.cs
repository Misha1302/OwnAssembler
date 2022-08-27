using System.Runtime.CompilerServices;
using OwnAssembler.CentralProcessingUnit;

namespace OwnAssembler.Assembler.LowLevelCommands.TypeChangers;

public class ToDoubleCommand : ICommand
{
    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public void Execute(CpuStack stack, ref int currentCommandIndex)
    {
        stack.Push(double.Parse(stack.Peek().ToString()));
        currentCommandIndex++;
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public void Dump()
    {
        Console.Write("convert to double");
    }
}