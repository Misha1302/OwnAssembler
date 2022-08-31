using System.Runtime.CompilerServices;

namespace OwnAssembler.Assembler.LowLevelCommands.Operations.MathematicalOperations;

[Serializable]
public class ShiftLeftCommand : BaseBinaryCommand
{
    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public ShiftLeftCommand() : base("shl")
    {
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    protected override object? ExecuteBinaryCommand(object leftValue, object rightValue)
    {
        return (int)rightValue << (int)leftValue;
    }
}