/// <summary>
/// https://leetcode-cn.com/problems/single-number-ii/
/// </summary>
public partial class Solution
{
    public int SingleNumberII(int[] nums)
    {
        var once = 0;
        var twice = 0;
        for (var i = 0; i < nums.Length; i++)
        {
            once = ~twice & (nums[i] ^ once);
            twice = ~once & (nums[i] ^ twice);
        }
        return once;
    }
}