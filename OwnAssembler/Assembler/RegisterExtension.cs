using System.Runtime.CompilerServices;

namespace OwnAssembler.Assembler;

public static class RegisterExtension
{
    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
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