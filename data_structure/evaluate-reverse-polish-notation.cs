using System.Collections.Generic;
/// <summary>
/// https://leetcode-cn.com/problems/evaluate-reverse-polish-notation/
/// </summary>
public partial class Solution
{
    public int EvalRPN(string[] tokens)
    {
        var stack = new Stack<int>();
        for (var i = 0; i < tokens.Length; i++)
        {
            var str = tokens[i];
            if (int.TryParse(str, out var num))
            {
                stack.Push(num);
            }
            else
            {
                var n2 = stack.Pop();
                var n1 = stack.Pop();
                if (str == "+")
                {
                    stack.Push(n1 + n2);
                }
                else if (str == "-")
                {
                    stack.Push(n1 - n2);
                }
                else if (str == "*")
                {
                    stack.Push(n1 * n2);
                }
                else if (str == "/")
                {
                    stack.Push(n1 / n2);
                }
            }
        }
        return stack.Pop();
    }
}