using System.Collections.Generic;
using Graph;
/// <summary>
/// https://leetcode-cn.com/problems/clone-graph/
/// </summary>
public partial class Solution
{
    private Node CloneGraph(Node node, IDictionary<Node, Node> visisted)
    {
        if (node == null)
        {
            return null;
        }
        if (visisted.TryGetValue(node, out var newNode))
        {
            return newNode;
        }
        newNode = new Node(node.val);
        visisted.Add(node, newNode);
        newNode.neighbors = new Node[node.neighbors.Count];
        for (int i = 0; i < node.neighbors.Count; i++)
        {
            newNode.neighbors[i] = CloneGraph(node.neighbors[i], visisted);
        }
        return newNode;
    }
    public Node CloneGraph(Node node)
    {
        var visisted = new Dictionary<Node, Node>();
        return CloneGraph(node, visisted);
    }
}