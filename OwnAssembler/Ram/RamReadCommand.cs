using OwnAssembler.LowLevelCommands;

namespace OwnAssembler.Ram;

public class RamReadCommand : ICommand
{
    private readonly string _address;

    public RamReadCommand(string address)
    {
        _address = address;
    }

    public void Execute(EditedStack stack, ref int currentCommandIndex)
    {
        stack.Push(Ram.Read(_address));
        currentCommandIndex++;
    }

    public void Dump()
    {
        Console.Write($"ram read: a:{_address} v:{Ram.Read(_address)}");
    }
}