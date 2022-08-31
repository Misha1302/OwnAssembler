using System.Runtime.CompilerServices;

namespace Cpu.CentralProcessingUnit;

// heap analogue
public static class Ram
{
    public static readonly Dictionary<string, int> RamDictionary = new();

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public static int Read(string address)
    {
        var result = RamDictionary[address];
        return result;
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public static void Write(string address, int value)
    {
        RamDictionary[address] = value;
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public static void Dump()
    {
        Console.Write("RAM: ");
        foreach (var ramPair in RamDictionary) Console.Write($"{ramPair.Key}={ramPair.Value}  ");
    }
}