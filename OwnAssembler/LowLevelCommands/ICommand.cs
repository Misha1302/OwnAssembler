namespace OwnAssembler.LowLevelCommands;

public interface ICommand
{
    void Execute(EditedStack stack, ref int currentCommandIndex);
    void Dump();
}