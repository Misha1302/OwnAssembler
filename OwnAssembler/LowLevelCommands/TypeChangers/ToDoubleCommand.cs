namespace OwnAssembler.LowLevelCommands.TypeChangers;

public class ToDoubleCommand : ICommand
{
    public void Execute(EditedStack stack, ref int currentCommandIndex)
    {
        stack.Push(double.Parse(stack.Peek().ToString()));
        currentCommandIndex++;
    }

    public void Dump()
    {
        Console.Write("convert to double");
    }
}