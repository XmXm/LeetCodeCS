using System;
/// <summary>
/// https://leetcode-cn.com/problems/binary-tree-maximum-path-sum/
/// </summary>
public partial class Solution
{
    //最大路径和、单边路径和
    private Tuple<int, int> GetPathSum(TreeNode root)
    {
        if (root == null)
        {
            return new Tuple<int, int>(int.MinValue, 0);
        }
        var left = GetPathSum(root.left);
        var right = GetPathSum(root.right);
        var singlePathNum = Math.Max(left.Item2, right.Item2) + root.val;
        singlePathNum = Math.Max(singlePathNum, 0);
        //最大路径和与单边路径和无关
        var maxPathNum = Math.Max(left.Item1, right.Item1);
        maxPathNum = Math.Max(maxPathNum, left.Item2 + right.Item2 + root.val);
        return new Tuple<int, int>(maxPathNum, singlePathNum);
    }
    public int MaxPathSum(TreeNode root)
    {
        return GetPathSum(root).Item1;
    }
}