using System.Runtime.CompilerServices;

namespace OwnAssembler.Assembler.LowLevelCommands.MathematicalOperations;

[Serializable]
public class AddCommand : BaseBinaryCommand
{
    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public AddCommand() : base("add")
    {
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    protected override int ExecuteBinaryCommand(int leftValue, int rightValue)
    {
        unchecked
        {
            return leftValue + leftValue;
        }
    }
}