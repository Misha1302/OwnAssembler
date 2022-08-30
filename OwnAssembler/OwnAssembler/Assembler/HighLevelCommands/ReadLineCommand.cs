using System.Runtime.CompilerServices;
using Connector;

namespace OwnAssembler.Assembler.HighLevelCommands;

[Serializable]
public class ReadLineCommand : ICommand
{
    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public void Execute(CpuStack stack, ref int currentCommandIndex, int applicationIndex)
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