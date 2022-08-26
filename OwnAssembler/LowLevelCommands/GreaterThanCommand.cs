namespace OwnAssembler.LowLevelCommands;

public class GreaterThanCommand : BaseBinaryCommand
{
    public GreaterThanCommand() : base("gt")
    {
    }

    protected override object? ExecuteBinaryCommand(object leftValue, object rightValue)
    {
        return Convert.ToDouble(leftValue) > Convert.ToDouble(rightValue);
    }
}