
using System.Collections.Generic;
/// <summary>
/// https://leetcode-cn.com/problems/implement-queue-using-stacks/
/// </summary>
public class MyQueue
{

    private Stack<int> _stack = new Stack<int>();
    private Stack<int> _back = new Stack<int>();
    /** Initialize your data structure here. */
    public MyQueue()
    {

    }

    /** Push element x to the back of queue. */
    public void Push(int x)
    {
        while (_stack.Count > 0)
        {
            _back.Push(_stack.Pop());
        }
        _stack.Push(x);
        while (_back.Count > 0)
        {
            _stack.Push(_back.Pop());
        }
    }

    /** Removes the element from in front of queue and returns that element. */
    public int Pop()
    {
        return _stack.Pop();
    }

    /** Get the front element. */
    public int Peek()
    {
        return _stack.Peek();
    }

    /** Returns whether the queue is empty. */
    public bool Empty()
    {
        return _stack.Count == 0;
    }

    public override string ToString()
    {
        return _stack.ToArray().ToListString();
    }
}

/**
 * Your MyQueue object will be instantiated and called as such:
 * MyQueue obj = new MyQueue();
 * obj.Push(x);
 * int param_2 = obj.Pop();
 * int param_3 = obj.Peek();
 * bool param_4 = obj.Empty();
 */