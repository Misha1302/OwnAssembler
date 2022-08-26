namespace OwnAssembler.Ram;

public static class Ram
{
    private static readonly Dictionary<string, object?> RamDictionary = new();

    public static object? Read(string address)
    {
        return RamDictionary[address];
    }

    public static void Write(string address, object? value)
    {
        RamDictionary[address] = value;
    }

    public static void Dump()
    {
        Console.Write("RAM: ");
        foreach (var ramPair in RamDictionary) Console.Write($"{ramPair.Key}={ramPair.Value}  ");
    }
}