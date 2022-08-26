namespace OwnAssembler.LowLevelCommands;

public class ClearStackCommand : ICommand
{
    public void Execute(EditedStack stack, ref int currentCommandIndex)
    {
        stack.Clear();
        currentCommandIndex++;
    }

    public void Dump()
    {
        Console.Write("Clear stack");
    }
}