namespace OwnAssembler.LowLevelCommands;

public class PopCommand : ICommand
{
    public void Execute(EditedStack stack, ref int currentCommandIndex)
    {
        stack.Pop();
        currentCommandIndex++;
    }

    public void Dump()
    {
        Console.Write("pop");
    }
}