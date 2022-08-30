using Connector;
using Processor = Cpu.CentralProcessingUnit.Cpu;

namespace OwnAssembler.Assembler.LowLevelCommands;

[Serializable]
public class SetApplicationPriorityCommand : ICommand
{
    public void Execute(CpuStack stack, ref int currentCommandIndex, int applicationIndex)
    {
        var priority = (int)stack.Pop()!;
        Processor.Applications[applicationIndex].ApplicationPriority = priority switch
        {
            < 1 => throw new Exception("You can't set priority less than 1"),
            > 5 => throw new Exception("You can't set priority more than 5"),
            _ => priority
        };
        currentCommandIndex++;
    }

    public void Dump()
    {
        Console.Write("set priority");
    }
}