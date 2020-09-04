/// <summary>
/// https://leetcode-cn.com/problems/find-minimum-in-rotated-sorted-array-ii/
/// </summary>
public partial class Solution
{
    public int FindMinII(int[] nums)
    {
        if (nums.Length == 0)
        {
            return 0;
        }
        var left = 0;
        var right = nums.Length - 1;
        while (left < right)
        {
            //跳过重复元素
            while (left < right && nums[left] == nums[left + 1])
            {
                left++;
            }
            while (left < right && nums[right] == nums[right - 1])
            {
                right--;
            }
            var mid = left + (right - left) / 2;
            if (nums[mid] > nums[right])
            {
                left = mid + 1;
            }
            else
            {
                right = mid;
            }
        }
        return nums[left];
    }
}