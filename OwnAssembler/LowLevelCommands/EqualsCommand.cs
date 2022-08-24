namespace OwnAssembler.LowLevelCommands;

public class EqualsCommand : BaseBinaryCommand
{
    private readonly int _registerIndexForResult;

    public EqualsCommand(int registerIndexForResult) : base(registerIndexForResult, "eq")
    {
        _registerIndexForResult = registerIndexForResult;
    }

    protected override int ExecuteBinaryCommand(int leftValue, int rightValue)
    {
        return Convert.ToInt32(leftValue == rightValue);
    }
}