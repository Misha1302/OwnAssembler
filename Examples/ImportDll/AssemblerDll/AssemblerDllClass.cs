using OwnAssembler.Assembler.HighLevelCommands;
using OwnAssembler.Assembler.LowLevelCommands;
using OwnAssembler.Assembler.LowLevelCommands.Dlls;
using OwnAssembler.CentralProcessingUnit;

namespace AssemblerDllNamespace;

public static class AssemblerDllClass
{
    public static void HelloMethod(CpuStack stack, List<ICommand> commands, RefCurrentCommandIndex currentCommandIndex)
    {
        commands.InsertRange(currentCommandIndex.CurrentCommandIndex, new ICommand[]
        {
            new PushCommand("Привет!"),
            new PushCommand(1),
            new OutputCommand()
        });
    }
}