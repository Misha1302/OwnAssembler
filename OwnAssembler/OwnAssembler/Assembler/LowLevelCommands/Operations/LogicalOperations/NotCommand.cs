using System.Runtime.CompilerServices;
using Connector;

namespace OwnAssembler.Assembler.LowLevelCommands.Operations.LogicalOperations;

[Serializable]
public class NotCommand : ICommand
{
    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public void Execute(CpuStack stack, ref int currentCommandIndex, int applicationIndex)
    {
        stack.Push(Convert.ToInt32(stack.Pop()) == 1 ? 0 : 1);
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public void Dump()
    {
        Console.Write("not");
    }
}