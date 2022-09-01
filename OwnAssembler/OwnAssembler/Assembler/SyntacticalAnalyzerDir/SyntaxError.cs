namespace OwnAssembler.Assembler.SyntacticalAnalyzerDir;

public readonly struct SyntaxError
{
    public readonly string ErrorMessage;

    public SyntaxError(string errorMessage)
    {
        ErrorMessage = errorMessage;
    }
}