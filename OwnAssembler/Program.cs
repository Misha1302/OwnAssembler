using System.Text;
using OwnAssembler;
using OwnAssembler.HighLevelCommands;
using OwnAssembler.LowLevelCommands;

Main();

void Main()
{
    Console.OutputEncoding = Encoding.Unicode;

    const int registersCount = 25;

    var registers = new int[registersCount];
    for (var i = 0; i < registersCount; i++) registers[i] = int.MinValue;

    var commands = new List<ICommand>(32);

    const string code = @"
put 0 5
put 1 5
equals 0

if
    put 1 48
    add 0
    output 0 1 ; 1
endIf

output 0 1
";

    commands = GetCommands();

    for (var index = 0; index < commands.Count;)
    {
        var command = commands[index];
        command.Dump();

        Console.CursorLeft = 30;
        Console.Write("| ");
        command.Execute(registers, ref index);

        Console.WriteLine();
        if (command is BaseBinaryCommand) Console.WriteLine();
    }


    List<ICommand> GetCommands()
    {
        var lexer = new Lexer(code);
        var list = lexer.GetTokens();
        var line = 0;


        var ifClause = false;
        var ifClauseStartIndex = 0;
        var ifClauseEndIndex = 0;
        var elseClause = false;
        var elseClauseStartIndex = 0;
        var elseClauseEndIndex = 0;

        for (var index = 0; index < list.Count; index++)
        {
            // try
            // {
            int arg2;
            int arg1;

            switch (list[index].TokenKind)
            {
                case Kind.Output:
                    arg1 = list[index + 1].Value ?? throw new ArgumentNullException($"line: {line}");
                    arg2 = list[index + 2].Value ?? throw new ArgumentNullException($"line: {line}");
                    commands.Add(new OutputCommand(arg1, arg2));
                    index += 2;
                    break;
                case Kind.Put:
                    arg1 = list[index + 1].Value ?? throw new ArgumentNullException($"line: {line}");
                    arg2 = list[index + 2].Value ?? throw new ArgumentNullException($"line: {line}");
                    commands.Add(new PutConstantToRegister(arg1, arg2));
                    index += 2;
                    break;
                case Kind.Add:
                    arg1 = list[index + 1].Value ?? throw new ArgumentNullException($"line: {line}");
                    commands.Add(new AddCommand(arg1));
                    index++;
                    break;
                case Kind.ReadKey:
                    arg1 = list[index + 1].Value ?? throw new ArgumentNullException($"line: {line}");
                    commands.Add(new ReadKeyCommand(arg1));
                    index++;
                    break;
                case Kind.Equals:
                    arg1 = list[index + 1].Value ?? throw new ArgumentNullException($"line: {line}");
                    commands.Add(new EqualsCommand(arg1));
                    index++;
                    break;
                case Kind.Gt:
                    arg1 = list[index + 1].Value ?? throw new ArgumentNullException($"line: {line}");
                    commands.Add(new GreaterThanCommand(arg1));
                    index++;
                    break;
                case Kind.Jmp:
                    commands.Add(new JumpCommand());
                    break;
                case Kind.Lt:
                    arg1 = list[index + 1].Value ?? throw new ArgumentNullException($"line: {line}");
                    commands.Add(new LessThanCommand(arg1));
                    index++;
                    break;
                case Kind.Sub:
                    arg1 = list[index + 1].Value ?? throw new ArgumentNullException($"line: {line}");
                    commands.Add(new SubtractCommand(arg1));
                    index++;
                    break;
                case Kind.If:
                    ifClauseStartIndex = commands.Count;
                    ifClause = true;
                    index++;
                    break;
                case Kind.Else:
                    ifClauseEndIndex = commands.Count;
                    elseClauseStartIndex = commands.Count;
                    elseClause = true;
                    index++;
                    break;
                case Kind.EndIf:
                    elseClauseEndIndex = commands.Count;
                    if (!elseClause)
                    {
                        ifClauseEndIndex = commands.Count;
                        elseClauseStartIndex = commands.Count;
                    }

                    ifClause = false;
                    elseClause = false;

                    var ifClauseArray = commands
                        .GetRange(ifClauseStartIndex, ifClauseEndIndex - ifClauseStartIndex).ToArray();
                    var elseClauseArray = commands
                        .GetRange(elseClauseStartIndex, elseClauseEndIndex - elseClauseStartIndex).ToArray();

                    var ifCompileIEnumerator = new IfCommand(ifClauseArray, elseClauseArray).Compile();

                    commands.RemoveRange(ifClauseStartIndex, elseClauseEndIndex - ifClauseStartIndex);


                    commands.AddRange(ifCompileIEnumerator);


                    ifClause = false;
                    ifClauseStartIndex = 0;
                    ifClauseEndIndex = 0;
                    elseClause = false;
                    elseClauseStartIndex = 0;
                    elseClauseEndIndex = 0;


                    index++;
                    break;
                case Kind.NewLine:
                    line++;
                    break;
                default:
                    throw new MissingMethodException(list[index].TokenKind.ToString());
            }
            // }
            // catch (ArgumentOutOfRangeException)
            // {
            //     throw new ArgumentNullException($"line: {line}");
            // }
        }

        return commands;
    }
}


Console.ReadKey();