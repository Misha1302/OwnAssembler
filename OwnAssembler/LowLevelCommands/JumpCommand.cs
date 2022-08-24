namespace OwnAssembler.LowLevelCommands;

public class JumpCommand : ICommand
{ 
    public void Execute(int[] registers, ref int currentCommandIndex)
    {
        currentCommandIndex += registers[0];
    }

    public void Dump()
    {
        Console.Write("jmp");
    }
}