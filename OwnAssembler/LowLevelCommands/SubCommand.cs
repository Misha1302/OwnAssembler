namespace OwnAssembler.LowLevelCommands;

public class SubtractCommand : BaseBinaryCommand
{
    private readonly int _registerIndexForResult;

    public SubtractCommand(int registerIndexForResult) : base(registerIndexForResult, "sub")
    {
        _registerIndexForResult = registerIndexForResult;
    }

    protected override int ExecuteBinaryCommand(int leftValue, int rightValue)
    {
        return leftValue - rightValue;
    }
}