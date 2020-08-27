using System.Collections.Generic;
/// <summary>
/// https://leetcode-cn.com/problems/binary-tree-inorder-traversal/
/// </summary>
public partial class Solution
{
    /// <summary>
    /// 递归实现
    /// </summary>
    /// <param name="root"></param>
    /// <param name="ret"></param>
    private void InorderTraversal(TreeNode root, IList<int> ret)
    {
        if (root == null)
        {
            return;
        }
        InorderTraversal(root.left, ret);
        ret.Add(root.val);
        InorderTraversal(root.right, ret);
    }
    public IList<int> InorderTraversal(TreeNode root)
    {
        var ret = new List<int>();
        var stack = new Stack<TreeNode>();
        while (stack.Count > 0 || root != null)
        {
            while (root != null)
            {
                stack.Push(root);
                root = root.left;
            }
            var node = stack.Pop();
            ret.Add(node.val);
            root = node.right;
        }

        return ret;
    }
}