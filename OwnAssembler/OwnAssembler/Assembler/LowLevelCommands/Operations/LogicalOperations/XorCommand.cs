using System.Runtime.CompilerServices;

namespace OwnAssembler.Assembler.LowLevelCommands.Operations.LogicalOperations;

[Serializable]
public class XorCommand : BaseBinaryCommand
{
    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public XorCommand() : base("xor")
    {
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    protected override object? ExecuteBinaryCommand(object leftValue, object rightValue)
    {
        return (int)leftValue ^ (int)rightValue;
    }
}