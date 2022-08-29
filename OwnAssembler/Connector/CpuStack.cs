using System.Collections;
using System.Runtime.CompilerServices;

namespace Connector;

public class CpuStack
{
    public ArrayList Stack;

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public CpuStack(int capacity = 64)
    {
        Stack = new ArrayList(capacity);
    }

    public int Count => Stack.Count;

    public object? this[int index] => Stack[index];


    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public void Push(object? value)
    {
        Stack.Add(value);
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public object? Peek()
    {
        return Count > 0 ? Stack[^1] : null;
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public object? Pop()
    {
        if (Count == 0) return null;

        var returnValue = Stack[^1];
        Stack.RemoveAt(Count - 1);
        return returnValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public void Clear()
    {
        Stack.Clear();
    }
}