/// <summary>
/// https://leetcode-cn.com/problems/validate-binary-search-tree/
/// </summary>
public partial class Solution
{
    struct BSTTreeNodeRecorder
    {
        public bool IsValid;
        public TreeNode Min;
        public TreeNode Max;
    }

    private BSTTreeNodeRecorder CheckBSTTreeNode(TreeNode root)
    {
        if (root == null)
        {
            return new BSTTreeNodeRecorder { IsValid = true };
        }
        var valid = (root.left == null || root.left.val < root.val) && (root.right == null || root.right.val > root.val);
        if (!valid)
        {
            return new BSTTreeNodeRecorder { IsValid = false }; ;
        }
        var left = CheckBSTTreeNode(root.left);
        var right = CheckBSTTreeNode(root.right);
        var ret = new BSTTreeNodeRecorder { IsValid = left.IsValid && right.IsValid };
        if (!ret.IsValid)
        {
            return ret;
        }
        if ((left.Max != null && left.Max.val >= root.val) || (right.Min != null && right.Min.val <= root.val))
        {
            ret.IsValid = false;
            return ret;
        }
        if (left.Min != null)
        {
            ret.Min = left.Min;
            ret.IsValid = ret.Min.val < root.val;
        }
        else
        {
            ret.Min = root;
        }
        if (right.Max != null)
        {
            ret.Max = right.Max;
            ret.IsValid = ret.IsValid && ret.Max.val > root.val;
        }
        else
        {
            ret.Max = root;
        }
        return ret;
    }
    public bool IsValidBST(TreeNode root)
    {
        return CheckBSTTreeNode(root).IsValid;
    }
}