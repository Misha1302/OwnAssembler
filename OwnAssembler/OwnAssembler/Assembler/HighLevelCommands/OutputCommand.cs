using System.Collections;
using System.Runtime.CompilerServices;
using System.Text;
using Connector;

namespace OwnAssembler.Assembler.HighLevelCommands;

[Serializable]
public class OutputCommand : ICommand
{
    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public void Execute(CpuStack stack, ref int currentCommandIndex, int applicationIndex)
    {
        Console.Write(stack.GetString());
        currentCommandIndex++;
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public void Dump()
    {
        Console.Write("output");
    }
}