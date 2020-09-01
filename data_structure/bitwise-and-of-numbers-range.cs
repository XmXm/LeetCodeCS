/// <summary>
/// https://leetcode-cn.com/problems/bitwise-and-of-numbers-range/
/// 给定范围 [m, n]，其中 0 <= m <= n <= 2147483647，返回此范围内所有数字的按位与（包含 m, n 两端点）
/// </summary>
public partial class Solution
{
    public int RangeBitwiseAnd(int m, int n)
    {
        while (m < n)
        {
            //抹掉末端的1
            n = n & (n - 1);
        }
        return n;
    }
}