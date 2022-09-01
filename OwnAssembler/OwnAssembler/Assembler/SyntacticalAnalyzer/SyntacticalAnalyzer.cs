using OwnAssembler.Assembler.Tokens;

namespace OwnAssembler.Assembler.SyntacticalAnalyzer;

public static class SyntacticalAnalyzer
{
    public static IReadOnlyList<SyntaxError> CheckForSyntaxErrors(IReadOnlyList<Token> tokens)
    {
        var errors = new List<SyntaxError>();

        var invalidCommandArgumentsErrors = InvalidCommandArgumentsAnalyzer.GetInvalidCommandArgumentsErrors(tokens);
        var invalidLinkedWordsErrors = InvalidLinkedWordsAnalyzer.GetInvalidLinkedWordsErrors(tokens);

        errors.AddRange(invalidCommandArgumentsErrors);
        errors.AddRange(invalidLinkedWordsErrors);

        return errors;
    }
}