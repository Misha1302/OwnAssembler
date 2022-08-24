namespace OwnAssembler.LowLevelCommands;

public class GreaterThanCommand : BaseBinaryCommand
{
    private readonly int _registerIndexForResult;

    public GreaterThanCommand(int registerIndexForResult) : base(registerIndexForResult, "gt")
    {
        _registerIndexForResult = registerIndexForResult;
    }

    protected override int ExecuteBinaryCommand(int leftValue, int rightValue)
    {
        return Convert.ToInt32(leftValue > rightValue);
    }
}