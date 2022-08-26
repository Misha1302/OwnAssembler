namespace OwnAssembler.LowLevelCommands;

public abstract class BaseBinaryCommand : ICommand
{
    private readonly string _commandName;

    protected BaseBinaryCommand(string commandName)
    {
        _commandName = commandName;
    }

    public void Execute(EditedStack stack, ref int currentCommandIndex)
    {
        var b = stack.Pop();
        var a = stack.Peek();
        
        stack.Push(a);
        
        stack.Push(ExecuteBinaryCommand(a, b));
        
        
        currentCommandIndex++;
    }

    public void Dump()
    {
        Console.Write($"{_commandName}");
    }

    protected abstract object? ExecuteBinaryCommand(object leftValue, object rightValue);
}