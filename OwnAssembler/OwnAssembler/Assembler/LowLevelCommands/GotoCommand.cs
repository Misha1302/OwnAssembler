using System.Runtime.CompilerServices;
using Connector;

namespace OwnAssembler.Assembler.LowLevelCommands;

[Serializable]
public class GotoCommand : ICommand
{
    private readonly List<ICommand> _commands;

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public GotoCommand(List<ICommand> commands)
    {
        _commands = commands;
    }

    // ReSharper disable once RedundantAssignment
    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public void Execute(CpuStack stack, ref int currentCommandIndex, int applicationIndex)
    {
        var gotoMark = (string)stack.Pop()!;
        var gotoPos = _commands.FindIndex(x => (x as GotoMark)?.MarkName == gotoMark);

        currentCommandIndex = gotoPos;
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public void Dump()
    {
        Console.Write("goto");
    }
}