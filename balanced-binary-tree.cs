/// <summary>
/// https://leetcode-cn.com/problems/balanced-binary-tree/
/// </summary>
public partial class Solution
{
    private System.Tuple<bool, int> GetBalancedInfo(TreeNode root)
    {
        if (root == null)
        {
            return new System.Tuple<bool, int>(true, 0);
        }
        var leftData = GetBalancedInfo(root.left);
        var rightData = GetBalancedInfo(root.right);

        var leftDepth = leftData.Item2;
        var rightDepth = rightData.Item2;
        var depth =  System.Math.Max(leftDepth, rightDepth) + 1;
        return new System.Tuple<bool, int>(leftData.Item1 && rightData.Item1 && System.Math.Abs(leftData.Item2 - rightData.Item2) < 2, depth);
    }
    public bool IsBalanced(TreeNode root)
    {
        return GetBalancedInfo(root).Item1;
    }
}