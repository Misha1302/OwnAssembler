using System.Text;
using OwnAssembler.LowLevelCommands;

namespace OwnAssembler.HighLevelCommands;

public class OutputCommand : ICommand
{
    private readonly int _endIndex;
    private readonly int _startIndex;

    public OutputCommand(int startIndex = 0, int length = 1)
    {
        _startIndex = startIndex;
        _endIndex = startIndex + length;
    }

    public void Execute(int[] registers, ref int currentCommandIndex)
    {
        var stringBuilder = new StringBuilder(_endIndex - _startIndex);

        for (var i = _startIndex; i < _endIndex; i++) stringBuilder.Append((char)registers[i]);

        Console.Write(stringBuilder.ToString());
        currentCommandIndex++;
    }

    public void Dump()
    {
        Console.Write($"output {_startIndex} {_endIndex - _startIndex}");
    }
}