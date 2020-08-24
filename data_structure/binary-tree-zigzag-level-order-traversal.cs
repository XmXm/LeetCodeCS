using System.Collections.Generic;
/// <summary>
/// https://leetcode-cn.com/problems/binary-tree-zigzag-level-order-traversal/
/// </summary>
public partial class Solution
{
    public IList<IList<int>> ZigzagLevelOrder(TreeNode root)
    {
        var queue = new Queue<TreeNode>();
        var depthList = new List<IList<int>>();
        if (root != null)
        {
            queue.Enqueue(root);
        }
        var left2Right = false;
        while (queue.Count > 0)
        {
            left2Right = !left2Right;
            var l = queue.Count;
            var clist = new List<int>();
            depthList.Add(clist);
            for (var i = 0; i < l; i++)
            {
                var node = queue.Dequeue();
                clist.Add(node.val);
                if (node.left != null)
                {
                    queue.Enqueue(node.left);
                }
                if (node.right != null)
                {
                    queue.Enqueue(node.right);
                }
            }
            if (!left2Right)
            {
                clist.Reverse();
            }
        }
        return depthList;
    }
}