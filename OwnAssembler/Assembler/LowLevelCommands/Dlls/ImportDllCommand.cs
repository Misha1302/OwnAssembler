using OwnAssembler.CentralProcessingUnit;

namespace OwnAssembler.Assembler.LowLevelCommands.Dlls;

public class ImportDllCommand : ICommand
{
    public void Execute(CpuStack stack, ref int currentCommandIndex)
    {
        Dlls.AddDll((string)stack.Peek());
    }

    public void Dump()
    {
        Console.Write("import dll");
    }
}