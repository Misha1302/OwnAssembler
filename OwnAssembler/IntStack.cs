namespace OwnAssembler;

public class EditedStack
{
    private readonly Stack<object?> _stack;

    public EditedStack(int capacity = 0)
    {
        _stack = new Stack<object?>(capacity);
    }

    public int Count => _stack.Count;


    public void Push(object? value)
    {
        _stack.Push(value);
    }

    public object? Peek(bool throwExceptions = false)
    {
        if (throwExceptions) return _stack.Peek();

        _stack.TryPeek(out var result);
        return result;
    }

    public object? Pop(bool throwExceptions = false)
    {
        if (throwExceptions) return _stack.Pop();

        _stack.TryPop(out var result);
        return result;
    }

    public void Clear(bool throwExceptions = false)
    {
        _stack.Clear();
    }
}