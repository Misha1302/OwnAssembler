using Connector;
using Processor = Cpu.CentralProcessingUnit.Cpu;

namespace OwnAssembler.Assembler.LowLevelCommands;

[Serializable]
public class SetApplicationPriorityCommand : ICommand
{
    private const int MinimumPriority = 1;
    private const int MaximumPriority = 5;

    public void Execute(CpuStack stack, ref int currentCommandIndex, int applicationIndex)
    {
        var priority = (int)stack.Pop()!;
        Processor.Applications[applicationIndex].ApplicationPriority = priority switch
        {
            < MinimumPriority => throw new Exception($"You can't set priority less than {MinimumPriority}"),
            > MaximumPriority => throw new Exception($"You can't set priority more than {MaximumPriority}"),
            _ => priority
        };
        currentCommandIndex++;
    }

    public void Dump()
    {
        Console.Write("set priority");
    }
}