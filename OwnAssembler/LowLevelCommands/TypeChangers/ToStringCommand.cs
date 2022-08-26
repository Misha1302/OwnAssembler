namespace OwnAssembler.LowLevelCommands.TypeChangers;

public class ToStringCommand : ICommand
{
    public void Execute(EditedStack stack, ref int currentCommandIndex)
    {
        var value = stack.Peek();
        if (value is double d) value = d.ToString("0." + new string('#', 324));
        else value = value.ToString();
        
        stack.Push(value);
        currentCommandIndex++;
    }

    public void Dump()
    {
        Console.Write("convert to string");
    }
}