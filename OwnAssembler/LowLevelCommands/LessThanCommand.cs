namespace OwnAssembler.LowLevelCommands;

public class LessThanCommand : BaseBinaryCommand
{
    public LessThanCommand() : base("lt")
    {
    }

    protected override object? ExecuteBinaryCommand(object leftValue, object rightValue)
    {
        return Convert.ToDouble(leftValue) < Convert.ToDouble(rightValue);
    }
}