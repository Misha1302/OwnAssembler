using System.Runtime.CompilerServices;
using Connector;
using OwnAssembler.Assembler.HighLevelCommands;
using OwnAssembler.Assembler.LowLevelCommands;
using OwnAssembler.Assembler.LowLevelCommands.Dlls;
using OwnAssembler.Assembler.LowLevelCommands.MathematicalOperations;
using OwnAssembler.Assembler.LowLevelCommands.TypeChangers;

namespace OwnAssembler.Assembler;

public static class CompilerToBytecode
{
    private static readonly string RamMarkName = DateTimeOffset.Now.ToUnixTimeMilliseconds().ToString();


    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public static void Compile(string code, List<ICommand> commands)
    {
        var lexer = new Lexer(code);
        var tokens = lexer.GetTokens();


        var ifClauseStartIndex = 0;
        var ifClauseEndIndex = 0;
        var elseClauseBool = false;
        var elseClauseStartIndex = 0;

        for (var index = 0; index < tokens.Count; index++)
        {
            // try
            // {
            object arg1;

            switch (tokens[index].TokenKind)
            {
                case Kind.Import:
                    commands.Add(new ImportDllCommand());
                    index += 2;
                    break;
                case Kind.Invoke:
                    commands.Add(new InvokeCommand(commands));
                    index += 2;
                    break;
                case Kind.Output:
                    commands.Add(new OutputCommand());
                    index += 2;
                    break;
                case Kind.Add:
                    commands.Add(new AddCommand());
                    index++;
                    break;
                case Kind.GetTimeInMilliseconds:
                    commands.Add(new GetTimeInMillisecondsCommand());
                    index++;
                    break;
                case Kind.SetPriority:
                    commands.Add(new SetApplicationPriorityCommand());
                    index++;
                    break;
                case Kind.Nop:
                    commands.Add(new NopCommand());
                    index++;
                    break;
                case Kind.ReadKey:
                    commands.Add(new ReadKeyCommand());
                    index++;
                    break;
                case Kind.ReadLine:
                    commands.Add(new ReadLineCommand());
                    index++;
                    break;
                case Kind.Equals:
                    commands.Add(new EqualsCommand());
                    index++;
                    break;
                case Kind.Gt:
                    commands.Add(new GreaterThanCommand());
                    index++;
                    break;
                case Kind.Jmp:
                    commands.Add(new JumpCommand());
                    break;
                case Kind.Lt:
                    commands.Add(new LessThanCommand());
                    index++;
                    break;
                case Kind.Sub:
                    commands.Add(new SubtractCommand());
                    index++;
                    break;
                case Kind.Goto:
                    commands.Add(new GotoCommand(commands));
                    index++;
                    break;
                case Kind.RamRead:
                    arg1 = tokens[index + 1].Value!;
                    commands.Add(new RamReadCommand((string)arg1));
                    index++;
                    break;
                case Kind.RamWrite:
                    arg1 = tokens[index + 1].Value!;
                    commands.Add(new RamWriteCommand((string)arg1));
                    index++;
                    break;
                case Kind.Clear:
                    commands.Add(new ClearStackCommand());
                    index++;
                    break;
                case Kind.Copy:
                    commands.Add(new CopyCommand());
                    index++;
                    break;
                case Kind.ConvertToDouble:
                    commands.Add(new ToDoubleCommand());
                    index++;
                    break;
                case Kind.ConvertToBool:
                    commands.Add(new ToBoolCommand());
                    index++;
                    break;
                case Kind.ConvertToInt:
                    commands.Add(new ToInt32Command());
                    index++;
                    break;
                case Kind.ConvertToString:
                    commands.Add(new ToStringCommand());
                    index++;
                    break;
                case Kind.ConvertToChar:
                    commands.Add(new ToCharCommand());
                    index++;
                    break;
                case Kind.Push:
                    arg1 = tokens[index + 1].Value!;
                    commands.Add(new PushCommand(arg1));
                    index++;
                    break;
                case Kind.Pop:
                    commands.Add(new PopCommand());
                    index++;
                    break;
                case Kind.Call:
                    arg1 = tokens[++index].Value!;
                    var startIndex = index;
                    var markName = DateTimeOffset.Now.ToUnixTimeMilliseconds().ToString();

                    commands.Add(new PushCommand(markName));
                    commands.Add(new RamWriteCommand(RamMarkName));
                    commands.Add(new PopCommand());

                    commands.Add(new PushCommand(arg1));
                    commands.Add(new GotoCommand(commands));
                    commands.Add(new GotoMark(markName));

                    index = startIndex + 1;
                    break;
                case Kind.Ret:
                    commands.Add(new RamReadCommand(RamMarkName));
                    commands.Add(new GotoCommand(commands));
                    index++;
                    break;
                case Kind.If:
                    ifClauseStartIndex = commands.Count;
                    index++;
                    break;
                case Kind.Else:
                    ifClauseEndIndex = commands.Count;
                    elseClauseStartIndex = commands.Count;
                    elseClauseBool = true;
                    index++;
                    break;
                case Kind.EndIf:
                    AddIfCommand(commands, ref elseClauseBool, ref ifClauseEndIndex,
                        ref elseClauseStartIndex, ref ifClauseStartIndex, ref index);
                    break;
                case Kind.Exit:
                    commands.Add(new ExitCommand());
                    index++;
                    break;
                case Kind.SetMark:
                    arg1 = tokens[index + 1].Value!;
                    commands.Add(new GotoMark((string)arg1));
                    index++;
                    break;
                case Kind.NewLine:
                case Kind.String:
                    break;
                default:
                    throw new MissingMethodException(tokens[index].TokenKind.ToString());
            }
            // }
            // catch (ArgumentOutOfRangeException)
            // {
            //     throw new ArgumentNullException($"line: {line}");
            // }
        }

        commands.Add(new ExitCommand());
    }

    private static void AddIfCommand(List<ICommand> commands, ref bool elseClause, ref int ifClauseEndIndex,
        ref int elseClauseStartIndex, ref int ifClauseStartIndex, ref int index)
    {
        var elseClauseEndIndex = commands.Count;
        if (!elseClause)
        {
            ifClauseEndIndex = commands.Count;
            elseClauseStartIndex = commands.Count;
        }

        var ifClauseArray = commands.GetRange(ifClauseStartIndex, ifClauseEndIndex - ifClauseStartIndex);
        var elseClauseArray = commands.GetRange(elseClauseStartIndex, elseClauseEndIndex - elseClauseStartIndex);

        var ifCompileIEnumerator =
            new IfCommand(ifClauseArray, elseClauseArray).Compile();

        commands.RemoveRange(ifClauseStartIndex, elseClauseEndIndex - ifClauseStartIndex);

        commands.AddRange(ifCompileIEnumerator);


        ifClauseStartIndex = 0;
        ifClauseEndIndex = 0;
        elseClause = false;
        elseClauseStartIndex = 0;


        index++;
    }
}