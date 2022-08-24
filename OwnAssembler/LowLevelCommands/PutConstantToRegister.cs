namespace OwnAssembler.LowLevelCommands;

public class PutConstantToRegister : ICommand
{
    private readonly int _constant;
    private readonly int _registerIndexToWrite;


    public PutConstantToRegister(int registerIndexToWrite, int constant)
    {
        _registerIndexToWrite = registerIndexToWrite;
        _constant = constant;
        if (constant == int.MinValue)
            throw new Exception($"You cannot put a constant equal to {int.MinValue}. Use {int.MinValue + 1}");
    }

    public void Execute(int[] registers, ref int currentCommandIndex)
    {
        registers[_registerIndexToWrite] = _constant;
        currentCommandIndex++;
    }

    public void Dump()
    {
        Console.Write($"put r{_registerIndexToWrite} {_constant}");
    }
}