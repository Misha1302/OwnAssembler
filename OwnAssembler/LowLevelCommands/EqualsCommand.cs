namespace OwnAssembler.LowLevelCommands;

public class EqualsCommand : BaseBinaryCommand
{
    public EqualsCommand() : base("eq")
    {
    }

    protected override object? ExecuteBinaryCommand(object leftValue, object rightValue)
    {
        return Convert.ToInt32(leftValue == rightValue);
    }
}