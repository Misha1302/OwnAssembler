using System.Runtime.CompilerServices;

namespace OwnAssembler.Assembler.LowLevelCommands.Operations.MathematicalOperations;

[Serializable]
public class DivisionCommand : BaseBinaryCommand
{
    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public DivisionCommand() : base("dev")
    {
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    protected override object? ExecuteBinaryCommand(object leftValue, object rightValue)
    {
        unchecked
        {
            return (int)leftValue / (int)rightValue;
        }
    }
}