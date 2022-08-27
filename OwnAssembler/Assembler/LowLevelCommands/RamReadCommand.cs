﻿using System.Runtime.CompilerServices;
using OwnAssembler.CentralProcessingUnit;

namespace OwnAssembler.Assembler.LowLevelCommands;

public class RamReadCommand : ICommand
{
    private readonly string _address;

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public RamReadCommand(string address)
    {
        _address = address + Thread.CurrentThread.ManagedThreadId;
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public void Execute(CpuStack stack, ref int currentCommandIndex)
    {
        stack.Push(Ram.Read(_address));
        currentCommandIndex++;
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public void Dump()
    {
        Console.Write($"ram read: {_address}");
    }
}