using System.Runtime.CompilerServices;

namespace OwnAssembler.Assembler.LowLevelCommands.MathematicalOperations;

[Serializable]
public class SubtractCommand : BaseBinaryCommand
{
    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public SubtractCommand() : base("sub")
    {
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    protected override int ExecuteBinaryCommand(int leftValue, int rightValue)
    {
        unchecked
        {
            return leftValue - leftValue;
        }
    }
}