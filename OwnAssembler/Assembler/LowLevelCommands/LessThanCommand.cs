using System.Runtime.CompilerServices;

namespace OwnAssembler.Assembler.LowLevelCommands;

public class LessThanCommand : BaseBinaryCommand
{
    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public LessThanCommand() : base("lt")
    {
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    protected override object? ExecuteBinaryCommand(object leftValue, object rightValue)
    {
        return Convert.ToDouble(leftValue) < Convert.ToDouble(rightValue);
    }
}