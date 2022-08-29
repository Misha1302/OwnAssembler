using System.Runtime.CompilerServices;

namespace Connector;

public interface ICommand
{
    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    void Execute(CpuStack stack, ref int currentCommandIndex);

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    void Dump();
}