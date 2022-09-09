using System.Collections;
using System.Runtime.CompilerServices;

namespace Connector;

public class CpuStack
{
    private readonly ArrayList _stack;

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public CpuStack(int capacity = 64)
    {
        _stack = new ArrayList(capacity);
    }

    public int Count
    {
        get { return _stack.Count; }
    }

    /// <summary>
    /// only use when needed as Cpu Stack is a stack not an array <br/>
    /// try not to use it
    /// </summary>
    public object? this[int index]
    {
        get { return _stack[index]; }
        set { _stack[index] = value; }
    }


    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public void Push(object? value)
    {
        _stack.Add(value);
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public object? Peek()
    {
        return Count > 0 ? _stack[^1] : null;
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public object? Pop()
    {
        if (Count == 0) return null;

        var returnValue = _stack[^1];
        _stack.RemoveAt(Count - 1);
        return returnValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public void Clear()
    {
        _stack.Clear();
    }
}