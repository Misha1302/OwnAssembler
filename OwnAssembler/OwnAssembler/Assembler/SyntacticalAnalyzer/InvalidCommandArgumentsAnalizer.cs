using OwnAssembler.Assembler.FrontEnd;

namespace OwnAssembler.Assembler.SyntacticalAnalyzer;

public static class InvalidCommandArgumentsAnalyzer
{
    private static readonly IReadOnlyList<Kind> argumentTypes = new List<Kind>
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
    private static readonly IReadOnlyDictionary<Kind, object?[]?> validCommandsArguments =
        new Dictionary<Kind, object?[]?>
        {
            { Kind.Define, new object?[] { Kind.String, Kind.String } },
            { Kind.Add, null },
            { Kind.And, null },
            { Kind.Call, new object?[] { Kind.String } },
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
            { Kind.RamRead, new object?[] { Kind.String } },
            { Kind.RamWrite, new object?[] { Kind.String } },
            { Kind.ReadKey, null },
            { Kind.ReadLine, null },
            { Kind.SetMark, new object?[] { Kind.String } },
            { Kind.ShiftLeft, null },
            { Kind.Mod, null },
            { Kind.ShiftRight, null },
            { Kind.ConvertToChar, null },
            { Kind.ConvertToDouble, null },
            { Kind.ConvertToInt, null },
            { Kind.ConvertToString, null },
            { Kind.GetTimeInMilliseconds, null },

            { Kind.NewLine, null },
            { Kind.Whitespace, null }
        };

    public static IEnumerable<SyntaxError> GetInvalidCommandArgumentsErrors(IReadOnlyList<Token> tokens)
    {
        var errors = new List<SyntaxError>();
        var line = 1;

        for (var index = 0; index < tokens.Count; index++)
        {
            var token = tokens[index];
            if (token.TokenKind == Kind.NewLine) line++;

            if (!validCommandsArguments.ContainsKey(token.TokenKind))
            {
                errors.Add(new SyntaxError($"{line}. Unknown token t:{token.Text} k:{token.TokenKind}"));
                continue;
            }

            var arguments = validCommandsArguments[token.TokenKind];
            if (arguments == null) continue;

            var startIndex = index;
            index = CheckMethodArguments(tokens, index, arguments, startIndex, errors, line);
        }

        return errors;
    }

    private static int CheckMethodArguments(IReadOnlyList<Token> tokens, int index, IReadOnlyList<object?> args,
        int startIndex,
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
                if (!argumentTypes.Contains(arg.TokenKind))
                    errors.Add(
                        new SyntaxError($"{line}. Argument number {argumentIndex} must be of type {argType ?? "\"any\""}"));

                continue;
            }

            var argKind = (Kind)argType;
            if (arg.TokenKind != argKind)
                errors.Add(new SyntaxError($"{line}. Argument number {argumentIndex} must be of type {argKind}"));
        }

        return index;
    }
}