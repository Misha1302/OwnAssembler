using System.Runtime.CompilerServices;

namespace OwnAssembler.Assembler.LowLevelCommands;

public class EqualsCommand : BaseBinaryCommand
{
    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public EqualsCommand() : base("eq")
    {
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    protected override object? ExecuteBinaryCommand(object leftValue, object rightValue)
    {
        return Convert.ToInt32(leftValue.Equals(rightValue));
    }
}