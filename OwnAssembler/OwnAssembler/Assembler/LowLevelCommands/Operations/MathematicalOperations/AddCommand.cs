using System.Runtime.CompilerServices;

namespace OwnAssembler.Assembler.LowLevelCommands.Operations.MathematicalOperations;

[Serializable]
public class AddCommand : BaseBinaryCommand
{
    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public AddCommand() : base("add")
    {
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    protected override object? ExecuteBinaryCommand(object leftValue, object rightValue)
    {
        unchecked
        {
            return leftValue switch
            {
                int leftInt when rightValue is int rightInt => leftInt + rightInt,
                double leftDouble when rightValue is double rightDouble => leftDouble + rightDouble,
                string leftStr when rightValue is string rightStr => leftStr + rightStr,
                _ => (long)leftValue + (long)rightValue
            };
        }
    }
}