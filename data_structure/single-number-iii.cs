/// <summary>
/// https://leetcode-cn.com/problems/single-number-iii/
/// </summary>
public partial class Solution
{
    public int[] SingleNumberIII(int[] nums)
    {
        var mask = 0;
        for (var i = 0; i < nums.Length; i++)
        {
            mask ^= nums[i];
        }
        var diff = mask & (-mask);
        var x = mask;
        var y = mask;
        for (var i = 0; i < nums.Length; i++)
        {
            //数组分组， 把不同的2个数分成2组做异或
            if ((diff & nums[i]) == 0)
            {
                x ^= nums[i];
            }
            else
            {
                y ^= nums[i];
            }
        }
        return new int[] { x, y };
    }
}