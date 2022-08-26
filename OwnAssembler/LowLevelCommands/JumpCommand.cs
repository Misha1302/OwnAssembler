namespace OwnAssembler.LowLevelCommands;

public class JumpCommand : ICommand
{ 
    public void Execute(EditedStack stack, ref int currentCommandIndex)
    {
        currentCommandIndex += (int)(stack.Peek() ?? throw new TypeAccessException("Jump only works with int values"));
    }

    public void Dump()
    {
        Console.Write("jmp");
    }
}