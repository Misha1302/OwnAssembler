using OwnAssembler.LowLevelCommands;

namespace OwnAssembler.HighLevelCommands;

public class ReadLineCommand : ICommand
{
    public void Execute(EditedStack stack, ref int currentCommandIndex)
    {
        stack.Push(Console.ReadLine());
        currentCommandIndex++;
    }

    public void Dump()
    {
        Console.Write("readLine");
    }
}