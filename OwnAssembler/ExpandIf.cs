using OwnAssembler.HighLevelCommands;
using OwnAssembler.LowLevelCommands;

namespace OwnAssembler;

public class ExpandIf : ICommand
{
    private readonly List<ICommand> _commands;
    private readonly ICommand[] _elseClause;
    private readonly ICommand[] _ifClause;

    public ExpandIf(ICommand[] ifClause, ICommand[] elseClause, List<ICommand> commands)
    {
        _ifClause = ifClause;
        _elseClause = elseClause;
        _commands = commands;
    }

    public void Execute(int[] registers, ref int currentCommandIndex)
    {
        _commands.AddRange(new IfCommand(_ifClause, _elseClause).Compile().ToArray());
        currentCommandIndex++;
    }

    public void Dump()
    {
    }
}