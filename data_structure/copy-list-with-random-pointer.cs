using System.Collections.Generic;
/// <summary>
/// https://leetcode-cn.com/problems/copy-list-with-random-pointer/
/// </summary>
public partial class Solution
{
    public Node CopyRandomList2(Node head)
    {
        if (head == null)
        {
            return null;
        }
        var map = new Dictionary<Node, int>();
        var index = 0;
        while (head != null)
        {
            map.Add(head, index);
            head = head.next;
            index++;
        }
        var cloneMap = new Dictionary<int, Node>();
        foreach (var entry in map)
        {
            var node = entry.Key;

            if (!cloneMap.TryGetValue(entry.Value, out var cloneNode))
            {
                cloneNode = new Node(node.val);
                cloneMap.Add(entry.Value, cloneNode);
            }
            if (node.next != null)
            {
                var nextIndex = map[node.next];
                if (!cloneMap.TryGetValue(nextIndex, out var cloneNextNode))
                {
                    cloneNextNode = new Node(node.next.val);
                    cloneMap.Add(nextIndex, cloneNextNode);
                }
                cloneNode.next = cloneNextNode;
            }
            if (node.random != null)
            {
                var nextIndex = map[node.random];
                if (!cloneMap.TryGetValue(nextIndex, out var cloneRandomNode))
                {
                    cloneRandomNode = new Node(node.random.val);
                    cloneMap.Add(nextIndex, cloneRandomNode);
                }
                cloneNode.random = cloneRandomNode;
            }
        }
        return cloneMap[0];
    }
    public Node CopyRandomList(Node head)
    {
        if (head == null)
        {
            return null;
        }
        //链表内哈希
        var p = head;
        while (p != null)
        {
            var tmp = p.next;
            p.next = new Node(p.val);
            p.next.next = tmp;
            p = tmp;
        }

        p = head;
        //步长为2时，永远存在p.next
        while (p != null )
        {
            if (p.random != null)
            {
                p.next.random = p.random.next;
            }
            p = p.next.next;
        }
        p = head;
        var copyNode = head.next;

        while (p != null && p.next != null)
        {
            var tmp = p.next;
            p.next = p.next.next;
            p = tmp;
        }
        return copyNode;
    }
}