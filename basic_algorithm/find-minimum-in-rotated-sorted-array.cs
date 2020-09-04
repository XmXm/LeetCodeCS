/// <summary>
/// https://leetcode-cn.com/problems/find-minimum-in-rotated-sorted-array/
/// </summary>
public partial class Solution
{
    public int FindMin(int[] nums)
    {
        if(nums.Length == 0){
            return 0;
        }
        var left = 0;
        var right = nums.Length - 1;
        while (left < right)
        {
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