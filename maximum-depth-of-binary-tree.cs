 /// <summary>
 /// https://leetcode-cn.com/problems/maximum-depth-of-binary-tree/
 /// </summary>
public partial class Solution
{
    public int MaxDepth(TreeNode root)
    {
        if (root == null)
        {
            return 0;
        }
        var leftDepth = MaxDepth(root.left);
        var rightDepth = MaxDepth(root.right);
        return System.Math.Max(leftDepth, rightDepth) + 1;
    }
}