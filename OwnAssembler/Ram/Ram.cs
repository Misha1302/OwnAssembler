namespace OwnAssembler.Ram;

public static class Ram
{
    private static readonly Dictionary<string, int> RamDictionary = new();

    public static int Read(string address)
    {
        return RamDictionary[address];
    }

    public static void Write(string address, int value)
    {
        RamDictionary[address] = value;
    }

    public static void Dump()
    {
        Console.Write("RAM: ");
        foreach (var ramPair in RamDictionary) Console.Write($"{ramPair.Key}={ramPair.Value}  ");
    }
}