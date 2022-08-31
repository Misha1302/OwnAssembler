using System.Runtime.CompilerServices;

namespace OwnAssembler.Assembler.LowLevelCommands.Operations.MathematicalOperations;

[Serializable]
public class ShiftRightCommand : BaseBinaryCommand
{
    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public ShiftRightCommand() : base("shr")
    {
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    protected override object? ExecuteBinaryCommand(object leftValue, object rightValue)
    {
        return (int)rightValue >> (int)leftValue;
    }
}