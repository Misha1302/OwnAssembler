﻿using System.Runtime.CompilerServices;
using OwnAssembler.CentralProcessingUnit;

namespace OwnAssembler.Assembler.LowLevelCommands.TypeChangers;

public class ToBoolCommand : ICommand
{
    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public void Execute(CpuStack stack, ref int currentCommandIndex)
    {
        stack.Push(bool.Parse(stack.Peek().ToString()));
        currentCommandIndex++;
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public void Dump()
    {
        Console.Write("convert to bool");
    }
}