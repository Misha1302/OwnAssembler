using System.Collections;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using Connector;

namespace OwnAssembler.Assembler.HighLevelCommands;

[Serializable]
public class OutputCommand : ICommand
{
    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public void Execute(CpuStack stack, ref int currentCommandIndex)
    {
        var length = (int)stack.Pop()!;
        var stringBuilder = new StringBuilder(32);
        var list = new ArrayList(length);

        var count = stack.Count;

        for (var i = count; i > count - length; i--) list.Add(stack.Pop());

        foreach (var item in list.ToArray().Reverse().Select(x => x?.ToString())) stringBuilder.Append(item);

        Console.Write(Regex.Unescape(stringBuilder.ToString()));
        currentCommandIndex++;
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public void Dump()
    {
        Console.Write("output");
    }
}