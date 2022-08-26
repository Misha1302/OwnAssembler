﻿using OwnAssembler.LowLevelCommands;

namespace OwnAssembler.HighLevelCommands;

public class ReadKeyCommand : ICommand
{
    public void Execute(EditedStack stack, ref int currentCommandIndex)
    {
        stack.Push(Console.ReadKey().KeyChar);
        currentCommandIndex++;
    }

    public void Dump()
    {
        Console.Write("readKey");
    }
}