using OwnAssembler.LowLevelCommands;
using OwnAssembler.LowLevelCommands.MathematicalOperations;
using OwnAssembler.LowLevelCommands.TypeChangers;
using OwnAssembler.Ram;

namespace OwnAssembler.HighLevelCommands;

public class IfCommand
{
    private readonly ICommand[] _elseClause;
    private readonly ICommand[] _ifClause;
    
    private readonly string _addressNameOne = DateTimeOffset.Now.ToUnixTimeMilliseconds().ToString() + '1';
    private readonly string _addressNameZero = DateTimeOffset.Now.ToUnixTimeMilliseconds().ToString() + '0';

    public IfCommand(ICommand[] ifClause, ICommand[] elseClause)
    {
        _ifClause = ifClause;
        _ifClause = IfClause().ToArray();

        _elseClause = elseClause;
        _elseClause = ElseClause().ToArray();
    }

    public IEnumerable<ICommand> Compile()
    {
        yield return new RamWriteCommand(_addressNameOne);
        yield return new RamWriteCommand(_addressNameZero);

        yield return new ToInt32Command();

        yield return new PushCommand(1);
        yield return new AddCommand();
        yield return new JumpCommand();

        yield return new PushCommand(_ifClause.Length + 3);
        yield return new PushCommand(1);
        yield return new AddCommand();
        yield return new JumpCommand();

        foreach (var ifClause in _ifClause) yield return ifClause;

        yield return new PushCommand(_elseClause.Length + 1);
        yield return new JumpCommand();

        foreach (var elseClause in _elseClause) yield return elseClause;


        yield return new RamReadCommand(_addressNameOne);
        yield return new RamReadCommand(_addressNameZero);
    }

    private IEnumerable<ICommand> ElseClause()
    {
        yield return new RamReadCommand(_addressNameOne);
        yield return new RamReadCommand(_addressNameZero);

        foreach (var command in _elseClause) yield return command;

        yield return new RamWriteCommand(_addressNameOne);
        yield return new RamWriteCommand(_addressNameZero);
    }

    private IEnumerable<ICommand> IfClause()
    {
        yield return new RamReadCommand(_addressNameOne);
        yield return new RamReadCommand(_addressNameZero);

        foreach (var command in _ifClause) yield return command;

        yield return new RamWriteCommand(_addressNameOne);
        yield return new RamWriteCommand(_addressNameZero);
    }
}