using OwnAssembler.LowLevelCommands;
using OwnAssembler.Ram;

namespace OwnAssembler.HighLevelCommands;

public class IfCommand
{
    private readonly ICommand[] _elseClause;
    private readonly ICommand[] _ifClause;

    public IfCommand(ICommand[] ifClause, ICommand[] elseClause)
    {
        _ifClause = ifClause;
        _ifClause = IfClause().ToArray();

        _elseClause = elseClause;
        _elseClause = ElseClause().ToArray();
    }

    public IEnumerable<ICommand> Compile()
    {
        yield return new RamWriteCommand("1", 1);
        yield return new RamWriteCommand("0", 0);
        
        
        yield return new PutConstantToRegister(1, 1);
        yield return new AddCommand(0);
        yield return new JumpCommand();

        yield return new PutConstantToRegister(1, _ifClause.Length + 3);
        yield return new PutConstantToRegister(0, 0);
        yield return new AddCommand(0);
        yield return new JumpCommand();

        foreach (var ifClause in _ifClause) yield return ifClause;
        
        yield return new PutConstantToRegister(0, _elseClause.Length + 1);
        yield return new JumpCommand();

        foreach (var elseClause in _elseClause) yield return elseClause;
        
        
        yield return new RamReadCommand("0", 0);
        yield return new RamReadCommand("1", 1);
    }

    private IEnumerable<ICommand> ElseClause()
    {
        yield return new RamReadCommand("0", 0);
        yield return new RamReadCommand("1", 1);
        
        foreach (var command in _elseClause) yield return command;
        
        yield return new RamWriteCommand("0", 0);
        yield return new RamWriteCommand("1", 1);
    }
    
    private IEnumerable<ICommand> IfClause()
    {
        yield return new RamReadCommand("1", 1);
        yield return new RamReadCommand("0", 0);

        foreach (var command in _ifClause) yield return command;
        
        yield return new RamWriteCommand("0", 0);
        yield return new RamWriteCommand("1", 1);
    }
}