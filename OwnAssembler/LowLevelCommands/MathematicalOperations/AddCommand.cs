namespace OwnAssembler.LowLevelCommands.MathematicalOperations;

public class AddCommand : BaseBinaryCommand
{
    public AddCommand() : base("add")
    {
    }

    protected override object? ExecuteBinaryCommand(object leftValue, object rightValue)
    {
        if (leftValue is double || rightValue is double)
            return Convert.ToDouble(leftValue) + Convert.ToDouble(rightValue);
        return Convert.ToInt32(leftValue) + Convert.ToInt32(rightValue);
    }
}