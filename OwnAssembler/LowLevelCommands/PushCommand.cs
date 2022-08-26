namespace OwnAssembler.LowLevelCommands;

public class PushCommand : ICommand
{
    private readonly object _constant;
    
    public PushCommand(object constant)
    {
        _constant = constant;
        if (constant is int.MinValue)
            throw new Exception($"You cannot put a constant equal to {int.MinValue}. Use {int.MinValue + 1}");
    }

    public void Execute(EditedStack stack, ref int currentCommandIndex)
    {
        stack.Push(_constant);
        currentCommandIndex++;
    }

    public void Dump()
    {
        Console.Write($"push {_constant}");
    }
}