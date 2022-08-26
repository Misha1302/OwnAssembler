using OwnAssembler.LowLevelCommands;

namespace OwnAssembler.Ram;

public class RamWriteCommand : ICommand
{
    private readonly string _address;

    public RamWriteCommand(string address)
    {
        _address = address;
    }

    public void Execute(EditedStack stack, ref int currentCommandIndex)
    {
        Ram.Write(_address, stack.Peek());
        currentCommandIndex++;
    }

    public void Dump()
    {
        Console.Write($"ram write: {_address}");
    }
}