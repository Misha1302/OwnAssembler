using OwnAssembler.LowLevelCommands;

namespace OwnAssembler.Ram;

public class RamWriteCommand : ICommand
{
    private readonly string _address;
    private readonly int _registerIndexToSave;

    public RamWriteCommand(string address, int registerIndexToSave)
    {
        _address = address;
        _registerIndexToSave = registerIndexToSave;
    }

    public void Execute(int[] registers, ref int currentCommandIndex)
    {
        Ram.Write(_address, registers[_registerIndexToSave]);
        currentCommandIndex++;
    }

    public void Dump()
    {
        Console.Write($"{_address}=r{_registerIndexToSave}");
    }
}