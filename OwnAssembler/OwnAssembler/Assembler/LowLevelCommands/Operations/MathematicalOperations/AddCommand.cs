﻿using System.Runtime.CompilerServices;

namespace OwnAssembler.Assembler.LowLevelCommands.Operations.MathematicalOperations;

[Serializable]
public class AddCommand : BaseBinaryCommand
{
    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public AddCommand() : base("add")
    {
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    protected override object? ExecuteBinaryCommand(object leftValue, object rightValue)
    {
        unchecked
        {
            if (leftValue is double leftDouble && rightValue is double rightDouble)
                return leftDouble + rightDouble;
            if (leftValue is string leftStr && rightValue is string rightStr)
                return leftStr + rightStr;
            return (int)leftValue + (int)rightValue;
        }
    }
}