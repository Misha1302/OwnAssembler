using System.Runtime.CompilerServices;
using Connector;

namespace OwnAssembler.Assembler.LowLevelCommands.Dlls;

[Serializable]
public class ImportDllCommand : ICommand
{
    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public void Execute(CpuStack stack, ref int currentCommandIndex)
    {
        Dlls.AddDll((string)stack.Pop()!);
        currentCommandIndex++;
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public void Dump()
    {
        Console.Write("import dll");
    }
}