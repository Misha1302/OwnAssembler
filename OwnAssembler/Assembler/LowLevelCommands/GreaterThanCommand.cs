using System.Runtime.CompilerServices;

namespace OwnAssembler.Assembler.LowLevelCommands;

public class GreaterThanCommand : BaseBinaryCommand
{
    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public GreaterThanCommand() : base("gt")
    {
    }
    
    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    protected override object? ExecuteBinaryCommand(object leftValue, object rightValue)
    {
        return Convert.ToDouble(leftValue) > Convert.ToDouble(rightValue);
    }
}