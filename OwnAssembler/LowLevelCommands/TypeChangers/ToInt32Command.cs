namespace OwnAssembler.LowLevelCommands.TypeChangers;

public class ToInt32Command : ICommand
{
    public void Execute(EditedStack stack, ref int currentCommandIndex)
    {
        stack.Push(int.Parse(stack.Peek().ToString()));
        currentCommandIndex++;
    }

    public void Dump()
    {
        Console.Write("convert to Int32");
    }
}