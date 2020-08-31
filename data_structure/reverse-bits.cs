/// <summary>
/// https://leetcode-cn.com/problems/reverse-bits/
/// </summary>
public partial class Solution
{
    public uint reverseBits(uint n)
    {
        var pow = 31;
        uint res = 0;
        while (n != 0)
        {
            res += (n & 1) << pow;
            n >>= 1;
            pow--;
        }
        return res;
    }
}