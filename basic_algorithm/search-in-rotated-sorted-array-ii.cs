/// <summary>
/// https://leetcode-cn.com/problems/search-in-rotated-sorted-array-ii/
/// </summary>
public partial class Solution
{
    public bool SearchIII(int[] nums, int target)
    {
        if (nums.Length == 0)
        {
            return false;
        }
        var left = 0;
        var right = nums.Length - 1;
        while (left + 1 < right)
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
            if (nums[mid] == target)
            {
                return true;
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
        return nums[left] == target || nums[right] == target;
    }
}