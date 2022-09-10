using System.Runtime.CompilerServices;
using Connector;

namespace OwnAssembler.Assembler.HighLevelCommands;

[Serializable]
public class ExitCommand : ICommand
{
    // ReSharper disable once RedundantAssignment
    // We can't make this method static
#pragma warning disable CA1822
    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public void Execute(CpuStack stack, ref int currentCommandIndex)
    {
        currentCommandIndex = -1;
    }
#pragma warning restore CA1822


    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public void Dump()
    {
        Console.Write("exit");
    }
}