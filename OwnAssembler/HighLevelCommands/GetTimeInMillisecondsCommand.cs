using OwnAssembler.LowLevelCommands;

namespace OwnAssembler.HighLevelCommands;

public class GetTimeInMillisecondsCommand : ICommand
{
    public void Execute(EditedStack stack, ref int currentCommandIndex)
    {
        stack.Push(DateTimeOffset.Now.ToUnixTimeMilliseconds());
        currentCommandIndex++;
    }

    public void Dump()
    {
        Console.Write("getting time in milliseconds");
    }
}