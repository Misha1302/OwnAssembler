namespace OwnAssembler.LowLevelCommands;

public class AddCommand : BaseBinaryCommand
{
    private readonly int _registerIndexForResult;

    public AddCommand(int registerIndexForResult) : base(registerIndexForResult, "add")
    {
        _registerIndexForResult = registerIndexForResult;
    }

    protected override int ExecuteBinaryCommand(int leftValue, int rightValue)
    {
        return leftValue + rightValue;
    }
}