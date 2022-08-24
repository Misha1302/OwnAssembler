namespace OwnAssembler.LowLevelCommands;

public abstract class BaseBinaryCommand : ICommand
{
    private readonly int _registerIndexForResult;
    private readonly string _commandName;

    protected BaseBinaryCommand(int registerIndexForResult, string commandName)
    {
        _registerIndexForResult = registerIndexForResult;
        _commandName = commandName;
    }

    public void Execute(int[] registers, ref int currentCommandIndex)
    {
        registers[_registerIndexForResult] = ExecuteBinaryCommand(registers[0], registers[1]);
        currentCommandIndex++;
    }

    public void Dump()
    {
        Console.Write($"{_commandName} r{_registerIndexForResult}");
    }

    protected abstract int ExecuteBinaryCommand(int leftValue, int rightValue);
}