using System.Text;
using OwnAssembler;
using OwnAssembler.LowLevelCommands;

Main();

void Main()
{
    Console.OutputEncoding = Encoding.Unicode;
    Console.InputEncoding = Encoding.Unicode;

    var commands = new List<ICommand>(32);

    var code = File.ReadAllText("Code.asmEasy");

    commands = CompilerToBytecode.GetCommands(code, commands);
    VirtualMachine.Execute(commands);
}