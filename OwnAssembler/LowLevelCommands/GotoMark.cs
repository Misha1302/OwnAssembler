namespace OwnAssembler.LowLevelCommands;

public class GotoMark : ICommand
{
    public readonly string MarkName;

    public GotoMark(string markName)
    {
        MarkName = markName;
    }

    public void Execute(EditedStack stack, ref int currentCommandIndex)
    {
        currentCommandIndex++;
    }

    public void Dump()
    {
        Console.Write($"goto mark {MarkName}");
    }
}