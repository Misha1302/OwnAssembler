using OwnAssembler.LowLevelCommands;

namespace OwnAssembler.HighLevelCommands;

public class ExitCommand : ICommand
{
    private readonly EventHandler _onExitEvent = VirtualMachine.OnExit;

    public void Execute(EditedStack stack, ref int currentCommandIndex)
    {
        var exitCode = 0;
        if (stack.Peek() is int i) exitCode = i;

        _onExitEvent.Invoke(null, null);

        Environment.Exit(exitCode);
    }

    public void Dump()
    {
        Console.Write("--- Exit ---");
    }
}