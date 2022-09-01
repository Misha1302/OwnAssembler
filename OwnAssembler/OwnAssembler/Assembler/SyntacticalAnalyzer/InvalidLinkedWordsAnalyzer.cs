using OwnAssembler.Assembler.Tokens;

namespace OwnAssembler.Assembler.SyntacticalAnalyzer;

public static class InvalidLinkedWordsAnalyzer
{
    private static readonly IReadOnlyList<(Kind start, Kind end)> LinkingWords = new List<(Kind start, Kind end)>
    {
        (Kind.If, Kind.EndIf)
    };
    
    public static IEnumerable<SyntaxError> GetInvalidLinkedWordsErrors(IEnumerable<Token> tokens)
    {
        var errors = new List<SyntaxError>();

        var linkingWordsStart = LinkingWords.Select(x => x.start).ToArray();
        var linkingWordsEnd = LinkingWords.Select(x => x.end).ToArray();

        const int line = 0;
        CheckForLinkedWordsInternal(tokens, line, linkingWordsStart, linkingWordsEnd, errors);
        
        return errors;
    }

    private static void CheckForLinkedWordsInternal(IEnumerable<Token> tokens, int line,
        Kind[] linkingWordsStart, IReadOnlyList<Kind> linkingWordsEnd, List<SyntaxError> errors)
    {
        var unclosedWords = new List<(int line, string text)>();
        var expectedWords = new List<Kind>();

        foreach (var token in tokens)
        {
            if (token.TokenKind == Kind.NewLine) line++;

            if (linkingWordsStart.Contains(token.TokenKind))
            {
                unclosedWords.Add((line, token.Text));
                expectedWords.Add(linkingWordsEnd[unclosedWords.Count - 1]);
            }

            if (!linkingWordsEnd.Contains(token.TokenKind)) continue;

            if (unclosedWords.Count == 0)
            {
                errors.Add(new SyntaxError($"{line}. Nothing to close"));
                continue;
            }

            unclosedWords.RemoveAt(unclosedWords.Count - 1);
            expectedWords.RemoveAt(expectedWords.Count - 1);
        }
        
        var errorsDueToUnclosedWords = unclosedWords.Select(unclosedWord =>
            new SyntaxError($"{unclosedWord.line}. Not closed word: {unclosedWord.text}"));
        errors.AddRange(errorsDueToUnclosedWords);
    }
}