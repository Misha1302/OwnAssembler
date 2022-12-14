using System.Runtime.CompilerServices;
using Connector;
using OwnAssembler.Assembler.LowLevelCommands;
using OwnAssembler.Assembler.LowLevelCommands.Operations.MathematicalOperations;

namespace OwnAssembler.Assembler.HighLevelCommands;

[Serializable]
public class IfCommand
{
    private readonly List<ICommand> _elseClause;
    private readonly List<ICommand> _ifClause;


    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public IfCommand(List<ICommand> ifClause, List<ICommand> elseClause)
    {
        ifClause.Insert(0, new PopCommand());
        elseClause.Insert(0, new PopCommand());
        _ifClause = ifClause;
        _elseClause = elseClause;
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public IEnumerable<ICommand> Compile()
    {
        yield return new CopyCommand();
        yield return new PushCommand(1);
        yield return new AddCommand();
        yield return new JumpCommand();

        yield return new PushCommand(_ifClause.Count + 3);
        yield return new PushCommand(0);
        yield return new AddCommand();
        yield return new JumpCommand();

        foreach (var ifClause in _ifClause) yield return ifClause;

        yield return new PushCommand(_elseClause.Count + 1);
        yield return new JumpCommand();

        foreach (var elseClause in _elseClause) yield return elseClause;
    }
}