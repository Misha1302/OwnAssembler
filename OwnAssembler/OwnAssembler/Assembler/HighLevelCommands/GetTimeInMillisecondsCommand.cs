using System.Runtime.CompilerServices;
using Connector;

namespace OwnAssembler.Assembler.HighLevelCommands;

[Serializable]
public class GetTimeInMillisecondsCommand : ICommand
{
    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public void Execute(CpuStack stack, ref int currentCommandIndex, int applicationIndex)
    {
        var timeMilliseconds = DateTimeOffset.Now.ToUnixTimeMilliseconds();
        var a = (int)(timeMilliseconds / 100_000_000);
        var b = (int)(timeMilliseconds % 100_000_000);
        stack.Push(a);
        stack.Push(b);
        currentCommandIndex++;
    }


    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public void Dump()
    {
        Console.Write("getting time in milliseconds");
    }
}