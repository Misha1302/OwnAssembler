using System.Runtime.CompilerServices;

namespace Connector;

[Serializable]
public class ByteCode
{
    public readonly List<ICommand> Commands;

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public ByteCode(List<ICommand> commands)
    {
        Commands = commands;
    }

    public ByteCode()
    {
    }
}