using System;
/// <summary>
/// https://leetcode-cn.com/problems/insert-into-a-binary-search-tree/
/// </summary>
public partial class Solution
{
    public TreeNode InsertIntoBST(TreeNode root, int val)
    {
        if (root == null)
        {
            return new TreeNode(val);
        }
        if (val < root.val)
        {
            //左子树
            root.left = InsertIntoBST(root.left, val);
        }
        else if (val > root.val)
        {
            //右子树
            root.right = InsertIntoBST(root.right, val);
        }
        return root;
    }
}