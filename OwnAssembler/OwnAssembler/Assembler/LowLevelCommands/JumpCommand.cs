using System.Runtime.CompilerServices;
using Connector;

namespace OwnAssembler.Assembler.LowLevelCommands;

[Serializable]
public class JumpCommand : ICommand
{
    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public void Execute(CpuStack stack, ref int currentCommandIndex, int applicationIndex)
    {
        currentCommandIndex += stack.Pop();
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public void Dump()
    {
        Console.Write("jmp");
    }
}