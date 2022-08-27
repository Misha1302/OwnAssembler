using System.Runtime.CompilerServices;

namespace OwnAssembler.Assembler.LowLevelCommands;

public class WhileCommand
{
    private readonly ICommand[] _whileBody;

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public WhileCommand(ICommand[] whileBody)
    {
        _whileBody = whileBody;
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public void Compile()
    {
        // TODO: do while cycle compile
        throw new NotImplementedException();
    }
}