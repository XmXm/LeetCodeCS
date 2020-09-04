/// <summary>
/// https://leetcode-cn.com/problems/search-in-rotated-sorted-array/
/// </summary>
public partial class Solution
{
    public int SearchII(int[] nums, int target)
    {
        var left = 0;
        var right = nums.Length - 1;
        while (left + 1 < right)
        {
            var mid = left + (right - left) / 2;
            if (nums[mid] == target)
            {
                return mid;
            }
            if (nums[left] < nums[mid])
            {
                if (target >= nums[left] && target <= nums[mid])
                {
                    right = mid;
                }
                else
                {
                    left = mid;
                }
            }
            else if (nums[right] > nums[mid])
            {
                if (target >= nums[mid] && target <= nums[right])
                {
                    left = mid;
                }
                else
                {
                    right = mid;
                }
            }
        }
        if (nums[left] == target)
        {
            return left;
        }
        if (nums[right] == target)
        {
            return right;
        }
        return -1;
    }
}