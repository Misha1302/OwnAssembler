using OwnAssembler.LowLevelCommands;

namespace OwnAssembler.Ram;

public class RamReadCommand : ICommand
{
    private readonly string _address;
    private readonly int _registerIndexToSave;

    public RamReadCommand(string address, int registerIndexToSave)
    {
        _address = address;
        _registerIndexToSave = registerIndexToSave;
    }

    public void Execute(int[] registers, ref int currentCommandIndex)
    {
        registers[_registerIndexToSave] = Ram.Read(_address);
        currentCommandIndex++;
    }

    public void Dump()
    {
        Console.Write($"{_registerIndexToSave} = {Ram.Read(_address)}:{_address}");
    }
    
    
}