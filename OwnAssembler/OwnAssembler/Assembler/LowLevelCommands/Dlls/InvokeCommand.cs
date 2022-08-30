using System.Runtime.CompilerServices;
using Connector;

namespace OwnAssembler.Assembler.LowLevelCommands.Dlls;

[Serializable]
public class InvokeCommand : ICommand
{
    private readonly List<ICommand> _commands;

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public InvokeCommand(List<ICommand> commands)
    {
        _commands = commands;
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public void Execute(CpuStack stack, ref int currentCommandIndex, int applicationIndex)
    {
        var split = ((string)stack.Pop()!).Split('.');
        var dllName = split[0];
        var methodName = split[1];

        var method = Dlls.GetMethodFromDll(dllName, methodName);

        currentCommandIndex++;
        var refCurrentCommandIndex = new RefCurrentCommandIndex { CurrentCommandIndex = currentCommandIndex };
        var args = new object?[] { stack, _commands, refCurrentCommandIndex };
        method.Invoke(null, args);
        currentCommandIndex = refCurrentCommandIndex.CurrentCommandIndex;
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public void Dump()
    {
        Console.Write("invoke method");
    }
}