using System.Runtime.CompilerServices;
using OwnAssembler.Assembler.LowLevelCommands;
using OwnAssembler.CentralProcessingUnit;

namespace OwnAssembler.Assembler.HighLevelCommands;

public class ExitCommand : ICommand
{
    private readonly CpuApplication cpuApplication;
    
    public ExitCommand(CpuApplication cpuApplication)
    {
        this.cpuApplication = cpuApplication;
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public void Execute(CpuStack stack, ref int currentCommandIndex)
    {
        currentCommandIndex = int.MaxValue;
        cpuApplication.ApplicationExit();
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public void Dump()
    {
        Console.Write("--- Exit ---");
    }
}