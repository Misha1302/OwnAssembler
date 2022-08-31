using System.Runtime.CompilerServices;

namespace OwnAssembler.Assembler.LowLevelCommands;

[Serializable]
public class GreaterThanCommand : BaseBinaryCommand
{
    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public GreaterThanCommand() : base("gt")
    {
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    protected override int ExecuteBinaryCommand(int leftValue, int rightValue)
    {
        return leftValue > rightValue ? 1 : 0;
    }
}