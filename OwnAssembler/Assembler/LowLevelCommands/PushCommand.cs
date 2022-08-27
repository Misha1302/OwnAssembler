using System.Collections;
using System.Runtime.CompilerServices;
using OwnAssembler.CentralProcessingUnit;

namespace OwnAssembler.Assembler.LowLevelCommands;

public class PushCommand : ICommand
{
    private object _constant;
    
    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public PushCommand(object constant)
    {
        _constant = constant;
        if (constant is int.MinValue)
            throw new Exception($"You cannot put a constant equal to {int.MinValue}. Use {int.MinValue + 1}");
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public void Execute(CpuStack stack, ref int currentCommandIndex)
    {
        if (_constant is ArrayList arrayList) _constant = arrayList.ToArray();
        stack.Push(_constant);
        currentCommandIndex++;
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public void Dump()
    {
        Console.Write($"push {_constant}");
    }
}