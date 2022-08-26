using System.Text;
using OwnAssembler.LowLevelCommands;

namespace OwnAssembler.HighLevelCommands;

public class OutputCommand : ICommand
{
    public void Execute(EditedStack stack, ref int currentCommandIndex)
    {
        var length = (int)stack.Pop();

        var stringBuilder = new StringBuilder(length);

        var list = new List<object?>();
        for (var i = 0; i < length; i++) list.Add(stack.Pop());

        foreach (var obj in list) stack.Push(obj);

        foreach (var item in list.Select(x => x?.ToString()).Reverse()) stringBuilder.Append(item);

        Console.Write(stringBuilder.ToString());
        currentCommandIndex++;
    }

    public void Dump()
    {
        Console.Write("output");
    }
}