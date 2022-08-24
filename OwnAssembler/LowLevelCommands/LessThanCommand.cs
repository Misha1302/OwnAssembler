namespace OwnAssembler.LowLevelCommands;

public class LessThanCommand : BaseBinaryCommand
{
    private readonly int _registerIndexForResult;

    public LessThanCommand(int registerIndexForResult) : base(registerIndexForResult, "lt")
    {
        _registerIndexForResult = registerIndexForResult;
    }

    protected override int ExecuteBinaryCommand(int leftValue, int rightValue)
    {
        return Convert.ToInt32(leftValue < rightValue);
    }
}