using System.Collections.Generic;
/// <summary>
/// https://leetcode-cn.com/problems/binary-tree-level-order-traversal/
/// </summary>
public partial class Solution
{
    private void CollectLevelVals(TreeNode root, IList<IList<int>> collection, int depth)
    {
        if (root == null)
        {
            return;
        }
        if (depth == collection.Count)
        {
            collection.Add(new List<int>());
        }
        var clist = collection[depth];
        clist.Add(root.val);
        CollectLevelVals(root.left, collection, depth + 1);
        CollectLevelVals(root.right, collection, depth + 1);
    }
    public IList<IList<int>> LevelOrder(TreeNode root)
    {
        var depthList = new List<IList<int>>();
        CollectLevelVals(root, depthList, 0);
        return depthList;
    }

    public IList<IList<int>> LevelOrderUseQueue(TreeNode root)
    {
        var queue = new Queue<TreeNode>();
        var depthList = new List<IList<int>>();
        if (root != null)
        {
            queue.Enqueue(root);
        }
        while (queue.Count > 0)
        {
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
        }
        return depthList;
    }
}