using System.Text;
using System.Collections.Generic;
/// <summary>
/// https://leetcode-cn.com/problems/decode-string/
/// </summary>
public partial class Solution
{
    public string DecodeString(string s)
    {
        var strBuild = new StringBuilder();
        var stack = new Stack<char>();
        var cList = new List<char>();
        for (var i = 0; i < s.Length; i++)
        {
            var ch = s[i];
            if (ch == ']')
            {
                cList.Clear();
                ch = stack.Pop();
                while (ch != '[')
                {
                    cList.Add(ch);
                    ch = stack.Pop();
                }
                //解析数字
                strBuild.Clear();
                while (stack.Count > 0 && stack.Peek() >= '0' && stack.Peek() <= '9')
                {
                    ch = stack.Pop();
                    strBuild.Insert(0, ch);
                }
                var num = int.Parse(strBuild.ToString());
                for (var j = 0; j < num; j++)
                {
                    for (var w = cList.Count - 1; w >= 0; w--)
                    {
                        stack.Push(cList[w]);
                    }
                }
            }
            else
            {
                stack.Push(ch);
            }
        }
        strBuild.Clear();
        while (stack.Count > 0)
        {
            strBuild.Insert(0, stack.Pop());
        }
        return strBuild.ToString();
    }
}