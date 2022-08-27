using OwnAssembler.CentralProcessingUnit;

namespace OwnAssembler.Assembler.LowLevelCommands.Dlls;

public class InvokeCommand : ICommand
{
    private readonly List<ICommand> _commands;

    public InvokeCommand(List<ICommand> commands)
    {
        _commands = commands;
    }

    public void Execute(CpuStack stack, ref int currentCommandIndex)
    {
        var split = ((string)stack.Peek()).Split('.');
        var dllName = split[0];
        var methodName = split[1];

        var method = Dlls.GetMethodFromDll(dllName, methodName);
        var args = new object?[] { stack, new RefCurrentCommandIndex { CurrentCommandIndex = currentCommandIndex } };
        method.Invoke(null, args);
    }

    public void Dump()
    {
        Console.Write("invoke method");
    }
}

public class RefCurrentCommandIndex
{
    public int CurrentCommandIndex;
}