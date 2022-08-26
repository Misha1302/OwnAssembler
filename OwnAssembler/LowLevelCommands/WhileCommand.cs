namespace OwnAssembler.LowLevelCommands;

public class WhileCommand
{
    private readonly ICommand[] _whileBody;

    public WhileCommand(ICommand[] whileBody)
    {
        _whileBody = whileBody;
    }

    public void Compile()
    {
        
    }
}