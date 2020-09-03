/// <summary>
/// https://leetcode-cn.com/problems/search-a-2d-matrix/
/// </summary>
public partial class Solution
{
    public bool SearchMatrix(int[][] matrix, int target)
    {
        if (matrix.Length == 0)
        {
            return false;
        }
        var row = matrix.Length;
        var col = matrix[0].Length;
        var left = 0;
        var right = row * col - 1;
        while (left <= right)
        {
            var mid = left + (right - left) / 2;
            var i = mid / col;
            var j = mid % col;
            if (matrix[i][j] == target)
            {
                return true;
            }
            if (matrix[i][j] < target)
            {
                left = mid + 1;
            }
            else
            {
                right = mid - 1;
            }
        }
        return false;
    }
}