/// <summary>
/// https://leetcode-cn.com/problems/search-insert-position/
/// </summary>
public partial class Solution
{
    public int SearchInsert(int[] nums, int target)
    {
        var left = 0;
        var right = nums.Length - 1;
        while (left + 1 < right)
        {
            var mid = left + (right - left) / 2;
            if (nums[mid] < target)
            {
                left = mid;
            }
            else
            {
                right = mid;
            }
        }
        if (target <= nums[left])
        {
            return left;
        }
        else if (target <= nums[right])
        {
            return right;
        }
        else
        {
            return right + 1;
        }
    }
}