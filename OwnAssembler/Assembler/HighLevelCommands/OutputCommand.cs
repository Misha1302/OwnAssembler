using System.Collections;
using System.Runtime.CompilerServices;
using System.Text;
using OwnAssembler.Assembler.LowLevelCommands;
using OwnAssembler.CentralProcessingUnit;

namespace OwnAssembler.Assembler.HighLevelCommands;

public class OutputCommand : ICommand
{
    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public void Execute(CpuStack stack, ref int currentCommandIndex)
    {
        var length = (int)stack.Pop();
        var stringBuilder = new StringBuilder(512);
        var list = new ArrayList(length);

        for (var i = stack.Count - length; i < stack.Count; i++) list.Add(stack[i]);

        foreach (var item in list.ToArray().Select(x => x?.ToString())) stringBuilder.Append(item);

        Console.Write(stringBuilder.ToString());
        currentCommandIndex++;
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public void Dump()
    {
        Console.Write("output");
    }
}