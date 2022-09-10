using Connector;

namespace OwnAssembler.Assembler.LowLevelCommands;

[Serializable]
public class NopCommand : ICommand
{
    public void Execute(CpuStack stack, ref int currentCommandIndex)
    {
        currentCommandIndex++;
    }

    public void Dump()
    {
        Console.Write("nop");
    }
}