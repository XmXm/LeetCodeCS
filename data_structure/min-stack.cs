using System.Collections.Generic;
public class MinStack
{
    private List<int> _stack = new List<int>();
    private List<int> _min = new List<int>();
    /** initialize your data structure here. */
    public MinStack()
    {

    }

    public override string ToString()
    {
        var copyList = new List<int>(_stack);
        copyList.Reverse();
        var str = copyList.ToListString();

        if (_stack.Count > 0)
        {
            str = $"{str} min:{GetMin()}"; 
        }
        return str;
    }
    public void Push(int x)
    {
        _stack.Add(x);
        if (_min.Count == 0)
        {
            _min.Add(x);
        }
        else
        {
            var min = GetMin();
            if (x < min)
            {
                _min.Add(x);
            }
            else
            {
                _min.Add(min);
            }
        }
    }

    public void Pop()
    {
        _stack.RemoveAt(_stack.Count - 1);
        _min.RemoveAt(_min.Count - 1);
    }

    public int Top()
    {
        return _stack[_stack.Count - 1];
    }

    public int GetMin()
    {
        return _min[_min.Count - 1];
    }
}
