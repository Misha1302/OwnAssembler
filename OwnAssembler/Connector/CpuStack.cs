using System.Runtime.CompilerServices;
using System.Text;

namespace Connector;

public class CpuStack
{
    private readonly List<int> _stack;

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public CpuStack(int capacity = 32)
    {
        _stack = new List<int>(capacity);
    }

    public int Count => _stack.Count;

    public int this[int index]
    {
        get
        {
            try
            {
                return _stack[index];
            }
            catch
            {
                return -1;
            }
        }
    }


    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public void Push(int value)
    {
        _stack.Add(value);
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public int Peek()
    {
        return _stack[^1];
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public int Pop()
    {
        var returnValue = Peek();
        _stack.RemoveAt(Count - 1);
        return returnValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public void Clear()
    {
        _stack.Clear();
    }

    /// <summary>
    ///     reads numbers until it encounters a '$' character
    /// </summary>
    /// <returns></returns>
    public string GetString()
    {
        var stringBuilder = new StringBuilder(512);
        var list = new List<int>(32);

        while (this[^2] != '\\' && Peek() != '$') list.Add(Pop());

        foreach (var item in list.ToArray().Select(x => (char)(uint)x)) stringBuilder.Append(item);

        return stringBuilder.ToString();
    }

    /// <summary>
    ///     saves the string as the order of the numbers. <br />
    ///     string -> symbols -> numbers <br />
    ///     the last character of the string is stored last <br />
    /// </summary>
    /// <param name="str"></param>
    public void PushString(string str)
    {
        foreach (var ch in GetIntsFromString(str)) Push(ch);
    }
    
    /// <summary>
    ///     saves the string as the order of the numbers. <br />
    ///     string -> symbols -> numbers <br />
    ///     the last character of the string is stored last <br />
    /// </summary>
    /// <param name="str"></param>
    public static int[] GetIntsFromString(string str)
    {
        var stringChars = new int[str.Length];
        for (var index = 0; index < str.Length; index++)
        {
            var ch = str[index];
            stringChars[^(index + 1)] = ch;
        }

        return stringChars;
    }
}