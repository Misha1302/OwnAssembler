namespace OwnAssembler;

public static class RegisterExtension
{
    public static void Dump(this int[] registers)
    {
        Console.Write("Reg: ");
        for (var i = 0; i < registers.Length; i++)
        {
            var registerValue = registers[i];
            if (registerValue != int.MinValue)
                Console.Write($"{$"r{i}:{registerValue}",8}");
        }
    }
}