﻿using System.Runtime.CompilerServices;

namespace OwnAssembler.Assembler.LowLevelCommands.MathematicalOperations;

[Serializable]
public class SubtractCommand : BaseBinaryCommand
{
    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public SubtractCommand() : base("sub")
    {
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    protected override object? ExecuteBinaryCommand(object leftValue, object rightValue)
    {
        unchecked
        {
            return leftValue switch
            {
                double leftDouble when rightValue is double rightDouble => leftDouble - rightDouble,
                _ => Convert.ToInt32(leftValue) - Convert.ToInt32(rightValue)
            };
        }
    }
}