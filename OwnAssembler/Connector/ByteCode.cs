using System.Runtime.CompilerServices;

namespace Connector;

[Serializable]
public class ByteCode
{
    public readonly IReadOnlyList<ICommand> Commands;

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public ByteCode(IReadOnlyList<ICommand> commands)
    {
        Commands = commands;
    }
}