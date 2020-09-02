using System;
using System.Collections.Generic;
/// <summary>
/// https://leetcode-cn.com/problems/largest-rectangle-in-histogram/
/// </summary>
public partial class Solution
{
    public int LargestRectangleArea(int[] heights)
    {
        var hStack = new Stack<int>();
        int max = 0;
        for (var i = 0; i <= heights.Length; i++)
        {
            var cur = i == heights.Length ? 0 : heights[i];
            while (hStack.Count > 0 && cur <= heights[hStack.Peek()])
            {
                var h = heights[hStack.Pop()];
                //宽
                var w = i;
                if (hStack.Count > 0)
                {
                    w = i - hStack.Peek() - 1;
                }
                //面积=长x宽
                max = Math.Max(h * w, max);
            }
            hStack.Push(i);
        }
        return max;
    }
}