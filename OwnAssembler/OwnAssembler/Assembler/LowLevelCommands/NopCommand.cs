using Connector;

namespace OwnAssembler.Assembler.LowLevelCommands;

public class NopCommand : ICommand
{
    public void Execute(CpuStack stack, ref int currentCommandIndex, int applicationIndex)
    {
    }

    public void Dump()
    {
        Console.Write("nop");
    }
}