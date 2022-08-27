using System.Runtime.CompilerServices;
using OwnAssembler.CentralProcessingUnit;

namespace OwnAssembler.Assembler.LowLevelCommands;

public class GotoCommand : ICommand
{
    private readonly List<ICommand> _commands;

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public GotoCommand(List<ICommand> commands)
    {
        _commands = commands;
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public void Execute(CpuStack stack, ref int currentCommandIndex)
    {
        var gotoMark = (string)stack.Peek() ?? throw new IndexOutOfRangeException("mark null not found");
        
        var numberOfPasses = 0;
        while (true)
        {
            currentCommandIndex++;

            if (_commands.Count <= currentCommandIndex)
            {
                currentCommandIndex = 0;

                if (++numberOfPasses == 2) throw new IndexOutOfRangeException($"mark {gotoMark} not found");
            }

            var command = _commands[currentCommandIndex];

            if (command is not GotoMark mark) continue;

            if (mark.MarkName == gotoMark) break;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public void Dump()
    {
        Console.Write("goto");
    }
}