using System.Runtime.CompilerServices;

namespace OwnAssembler.Assembler.LowLevelCommands;

[Serializable]
public class EqualsCommand : BaseBinaryCommand
{
    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public EqualsCommand() : base("eq")
    {
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    protected override int ExecuteBinaryCommand(int leftValue, int rightValue)
    {
        return Convert.ToInt32(leftValue.Equals(rightValue));
    }
}