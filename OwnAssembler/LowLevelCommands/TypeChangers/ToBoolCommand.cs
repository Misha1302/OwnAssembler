namespace OwnAssembler.LowLevelCommands.TypeChangers;

public class ToBoolCommand : ICommand
{
    public void Execute(EditedStack stack, ref int currentCommandIndex)
    {
        stack.Push(bool.Parse(stack.Peek().ToString()));
        currentCommandIndex++;
    }

    public void Dump()
    {
        Console.Write("convert to bool");
    }
}