using OwnAssembler.LowLevelCommands;

namespace OwnAssembler.HighLevelCommands;

public class ReadKeyCommand : ICommand
{
    private readonly int _registerSaveIndex;

    public ReadKeyCommand(int registerSaveIndex)
    {
        _registerSaveIndex = registerSaveIndex;
    }
    
    public void Execute(int[] registers, ref int currentCommandIndex)
    {
        registers[_registerSaveIndex] = Console.ReadKey().KeyChar;
        currentCommandIndex++;
    }

    public void Dump()
    {
        Console.Write("output");
    }
}