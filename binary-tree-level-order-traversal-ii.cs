using System.Collections.Generic;
/// <summary>
/// https://leetcode-cn.com/problems/binary-tree-level-order-traversal-ii/
/// </summary>
public partial class Solution
{
    public IList<IList<int>> LevelOrderBottom(TreeNode root)
    {
        var depthList = new List<IList<int>>();
        CollectLevelVals(root, depthList, 0);
        depthList.Reverse();
        return depthList;
    }
}