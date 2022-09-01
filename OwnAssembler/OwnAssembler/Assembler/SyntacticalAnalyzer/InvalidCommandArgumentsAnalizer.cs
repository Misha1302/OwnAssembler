using OwnAssembler.Assembler.Tokens;

namespace OwnAssembler.Assembler.SyntacticalAnalyzer;

public static class InvalidCommandArgumentsAnalyzer
{
    private static readonly IReadOnlyList<Kind> ArgumentTypes = new List<Kind>
    {
        Kind.String,
        Kind.Int,
        Kind.Char,
        Kind.Double
    };
    
    
    /// <summary>
    ///     An array of commands and valid arguments to them. <br />
    ///     Arguments are passed as Kind[] and the type of the argument (string, number, etc.). <br />
    ///     If the command is followed by an argument of any type, then instead of the type, you can put the Null value <br />
    /// </summary>
    private static readonly IReadOnlyDictionary<Kind, object?> ValidCommandsArguments =
        new Dictionary<Kind, object?>
        {
            { Kind.Add, null },
            { Kind.And, null },
            { Kind.Call, null },
            { Kind.Clear, null },
            { Kind.Copy, null },
            { Kind.Division, null },
            { Kind.Else, null },
            { Kind.Equals, null },
            { Kind.Exit, null },
            { Kind.Goto, null },
            { Kind.Gt, null },
            { Kind.If, null },
            { Kind.Import, null },
            { Kind.Invoke, null },
            { Kind.Jmp, null },
            { Kind.Lt, null },
            { Kind.Multiplication, null },
            { Kind.Nop, null },
            { Kind.Not, null },
            { Kind.Or, null },
            { Kind.Output, null },
            { Kind.Pop, null },
            { Kind.Push, new[] { (object?)null } },
            { Kind.Ret, null },
            { Kind.Sub, null },
            { Kind.Xor, null },
            { Kind.EndIf, null },
            { Kind.RamRead, new[] { Kind.String } },
            { Kind.RamWrite, new[] { Kind.String } },
            { Kind.ReadKey, null },
            { Kind.ReadLine, null },
            { Kind.SetMark, new[] { Kind.String } },
            { Kind.SetPriority, null },
            { Kind.ShiftLeft, null },
            { Kind.ShiftRight, null },
            { Kind.ConvertToChar, null },
            { Kind.ConvertToDouble, null },
            { Kind.ConvertToInt, null },
            { Kind.ConvertToString, null },

            { Kind.NewLine, null },
            { Kind.Whitespace, null }
        };
    
    public static IEnumerable<SyntaxError> GetInvalidCommandArgumentsErrors(IReadOnlyList<Token> tokens)
    {
        var errors = new List<SyntaxError>();
        var line = 0;

        for (var index = 0; index < tokens.Count; index++)
        {
            var token = tokens[index];
            if (token.TokenKind == Kind.NewLine) line++;

            if (!ValidCommandsArguments.ContainsKey(token.TokenKind))
            {
                errors.Add(new SyntaxError($"{line}. Unknown token t:{token.Text} k:{token.TokenKind}"));
                continue;
            }

            var temp = ValidCommandsArguments[token.TokenKind];
            if (temp == null) continue;
            var args = (object?[])temp;

            var startIndex = index;
            index = CheckMethodArguments(tokens, index, args, startIndex, errors, line);
        }

        return errors;
    }

    private static int CheckMethodArguments(IReadOnlyList<Token> tokens, int index, IReadOnlyList<object?> args, int startIndex,
        ICollection<SyntaxError> errors, int line)
    {
        for (++index; index < args.Count + startIndex + 1; index++)
        {
            if (tokens.Count <= index)
            {
                errors.Add(new SyntaxError($"{line}. Not enough argument"));
                continue;
            }

            var arg = tokens[index];
            var argumentIndex = index - startIndex - 1;
            var argType = args[argumentIndex];
            if (argType == null)
            {
                if (!ArgumentTypes.Contains(arg.TokenKind))
                    errors.Add(
                        new SyntaxError($"{line}. Argument number {argumentIndex} must be of type {argType}"));

                continue;
            }

            argType = (Kind)argType;
            if (arg.TokenKind != (Kind)argType)
                errors.Add(new SyntaxError($"{line}. Argument number {argumentIndex} must be of type {argType}"));
        }

        return index;
    }
}