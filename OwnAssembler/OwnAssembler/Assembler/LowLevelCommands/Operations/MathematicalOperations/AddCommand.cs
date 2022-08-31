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
            return (int)leftValue + (int)rightValue;
        }
    }
}