/// <summary>
/// https://leetcode-cn.com/problems/number-of-1-bits/
/// </summary>
public partial class Solution
{
    public int HammingWeight(uint n)
    {
        var count = 0;
        while (n != 0)
        {
            n = n & (n - 1);
            count++;
        }
        return count;
    }
}