using System.Runtime.CompilerServices;

namespace OwnAssembler.Assembler.LowLevelCommands.Operations.MathematicalOperations;

[Serializable]
public class SubtractCommand : BaseBinaryCommand
{
    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public SubtractCommand() : base("sub")
    {
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    protected override object? ExecuteBinaryCommand(object leftValue, object rightValue)
    {
        unchecked
        {
            return leftValue switch
            {
                int leftInt when rightValue is int rightInt => leftInt - rightInt,
                double leftDouble when rightValue is double rightDouble => leftDouble - rightDouble,
                _ => (long)leftValue - (long)rightValue
            };
        }
    }
}