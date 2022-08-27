using System.Collections;
using System.Runtime.CompilerServices;
using OwnAssembler.Assembler.LowLevelCommands;
using OwnAssembler.Assembler.LowLevelCommands.MathematicalOperations;
using OwnAssembler.Assembler.LowLevelCommands.TypeChangers;
using OwnAssembler.CentralProcessingUnit;

namespace OwnAssembler.Assembler.HighLevelCommands;

public class IfCommand
{
    private readonly ICommand[] _elseClause;
    private readonly ICommand[] _ifClause;

    private readonly string _addressNameStack = DateTimeOffset.Now.ToUnixTimeMilliseconds().ToString() + "Stack";

    
    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public IfCommand(ICommand[] ifClause, ICommand[] elseClause)
    {
        _ifClause = ifClause;
        _elseClause = elseClause;
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public IEnumerable<ICommand> Compile(CpuStack stack)
    {
        yield return new PushCommand(stack.Stack);
        yield return new RamWriteCommand(_addressNameStack);
        yield return new PopCommand();
        
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
        
        yield return new RamReadCommand(_addressNameStack);
        yield return new SetStack();
    }
}

public class SetStack : ICommand
{
    public void Execute(CpuStack stack, ref int currentCommandIndex)
    {
        stack.Stack = new ArrayList((object?[])stack.Pop());
        currentCommandIndex++;
    }

    public void Dump()
    {
        Console.Write("set stack (virtual machine)");
    }
}