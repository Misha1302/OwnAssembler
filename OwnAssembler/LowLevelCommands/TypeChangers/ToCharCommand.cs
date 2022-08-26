namespace OwnAssembler.LowLevelCommands.TypeChangers;

public class ToCharCommand : ICommand
{
    public void Execute(EditedStack stack, ref int currentCommandIndex)
    {
        stack.Push(char.Parse(stack.Peek().ToString()));
        currentCommandIndex++;
    }

    public void Dump()
    {
        Console.Write("convert to char");
    }
}