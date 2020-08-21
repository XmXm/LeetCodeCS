/// <summary>
/// https://leetcode-cn.com/problems/lowest-common-ancestor-of-a-binary-tree/
/// </summary>
public partial class Solution
{
    // private int CountInputNode(TreeNode root, TreeNode p, TreeNode q, ref TreeNode result)
    // {
    //     if (root == null || result != null)
    //     {
    //         return 0;
    //     }
    //     var count = 0;
    //     if (root.val == p.val)
    //     {
    //         count++;
    //     }
    //     else if (root.val == q.val)
    //     {
    //         count++;
    //     }
    //     count += CountInputNode(root.left, p, q, ref result);
    //     count += CountInputNode(root.right, p, q, ref result);
    //     if (count == 2 && result == null)
    //     {
    //         result = root;
    //     }
    //     return count;
    // }
    // public TreeNode LowestCommonAncestor(TreeNode root, TreeNode p, TreeNode q)
    // {
    //     TreeNode result = null;
    //     CountInputNode(root, p, q, ref result);
    //     return result;
    // }
    public TreeNode LowestCommonAncestor(TreeNode root, TreeNode p, TreeNode q)
    {
        if (root == null)
        {
            return null;
        }
        if (root.val == p.val || root.val == q.val)
        {
            return root;
        }
        var left = LowestCommonAncestor(root.left, p, q);
        var right = LowestCommonAncestor(root.right, p, q);
        if (left != null && right != null)
        {
            return root;
        }
        if (left != null)
        {
            return left;
        }
        if (right != null)
        {
            return right;
        }
        return null;
    }
}