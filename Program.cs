using System.Text;
using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Reflection;

public class TreeNode
{
    public int val;
    public TreeNode left;
    public TreeNode right;
    public TreeNode(int x) { val = x; }
    public object[] ToArray()
    {
        var arr = new List<object>();
        var queue = new Queue<TreeNode>();
        queue.Enqueue(this);
        while (queue.Count > 0)
        {
            var len = queue.Count;
            for (var i = 0; i < len; i++)
            {
                var node = queue.Dequeue();
                if (node != null)
                {
                    arr.Add(node.val);
                    queue.Enqueue(node.left);
                    queue.Enqueue(node.right);
                }
                else
                {
                    arr.Add(null);
                }
            }
        }
        int nullIndex;
        for (nullIndex = arr.Count; nullIndex > 0; nullIndex--)
        {
            if (arr[nullIndex - 1] != null)
            {
                break;
            }
        }
        arr.RemoveRange(nullIndex, arr.Count - nullIndex);
        return arr.ToArray();
    }
    public TreeNode(object[] arr) : this((int)arr[0])
    {
        var treeQueue = new Queue<TreeNode>();
        treeQueue.Enqueue(this);
        for (var i = 1; i < arr.Length; i = i + 2)
        {
            var cur = treeQueue.Dequeue();
            var val = arr[i];
            if (val != null)
            {
                cur.left = new TreeNode((int)val);
                treeQueue.Enqueue(cur.left);
            }
            val = arr[i + 1];
            if (val != null)
            {
                cur.right = new TreeNode((int)val);
                treeQueue.Enqueue(cur.right);
            }
        }
    }

    public static TreeNode Parse(string str)
    {
        var objs = str.Replace("[", "").Replace("]", "").Split(',', StringSplitOptions.RemoveEmptyEntries).Select(m => int.TryParse(m, out var num) ? num : null as object).ToArray();
        if (objs.Length == 0)
        {
            return null;
        }
        return new TreeNode(objs);
    }
    public override string ToString()
    {
        var str = new StringBuilder();
        var objs = ToArray();
        for (int i = 0; i < objs.Length; i++)
        {
            object o = objs[i];
            if (o == null)
            {
                str.Append("null");
            }
            else
            {
                str.Append(o.ToString());
            }
            if (i < objs.Length - 1)
            {
                str.Append(',');
            }
        }
        return str.ToString();
    }
}


public class ListNode
{
    public int val;
    public ListNode next;
    public ListNode(int x) { val = x; }
    public ListNode(int[] array) : this(array[0])
    {
        var p = this;
        for (var i = 1; i < array.Length; i++)
        {
            p.next = new ListNode(array[i]);
            p = p.next;
        }
    }
    public static ListNode Parse(string str, int cyclePos = -1)
    {
        var objs = str.Replace("[", "").Replace("]", "").Split(',', StringSplitOptions.RemoveEmptyEntries).Select(m => int.Parse(m)).ToArray();
        if (objs.Length == 0)
        {
            return null;
        }
        var head = new ListNode(objs);
        if (cyclePos >= 0)
        {
            var cyclePoint = head;
            for (var i = 1; i <= cyclePos; i++)
            {
                cyclePoint = cyclePoint.next;
                if (cyclePoint == null)
                {
                    return head;
                }
            }
            var tail = head;
            while (tail.next != null)
            {
                tail = tail.next;
            }
            tail.next = cyclePoint;
        }
        return head;
    }
    public int[] ToArray(out int cyclePos)
    {
        var list = new List<int>();
        var p = this;
        cyclePos = -1;
        var eachs = new Dictionary<ListNode, int>();
        while (p != null)
        {
            if (eachs.TryGetValue(p, out cyclePos))
            {
                break;
            }
            eachs.Add(p, list.Count);
            list.Add(p.val);
            p = p.next;
        }
        return list.ToArray();
    }
    public override string ToString()
    {
        var arrStr = ToArray(out var cyclePos).ToListString();
        // if (cyclePos >= 0)
        // {
        //     arrStr = $"{arrStr} tail connects to node index:{cyclePos}";
        // }
        return arrStr;
    }
}

public static class ExtentionMethod
{
    public static string ToListString(this IList l)
    {
        var str = new StringBuilder();
        str.Append('[');
        for (var i = 0; i < l.Count; i++)
        {
            var o = l[i];
            if (o is IList subL)
            {
                str.Append(subL.ToListString());
            }
            else
            {
                str.Append(o.ToString());
            }
            if (i < l.Count - 1)
            {
                str.Append(',');
            }
        }
        str.Append(']');
        return str.ToString();
    }
}
namespace LeetCodeCS
{
    class Program
    {
        private static readonly Dictionary<Type, MethodInfo[]> sCacheStaticMethods = new Dictionary<Type, MethodInfo[]>();
        private static readonly Dictionary<Type, MethodInfo[]> sCacheInstanceMethods = new Dictionary<Type, MethodInfo[]>();
        public static object InvokeStaticMethod(Type t, string methodName, params object[] args)
        {
            if (!sCacheStaticMethods.TryGetValue(t, out var methodInfos))
            {
                methodInfos = t.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
                sCacheStaticMethods.Add(t, methodInfos);
            }
            var ms = methodInfos.FirstOrDefault(m =>
            {
                if (m.Name != methodName)
                {
                    return false;
                }

                var ps = m.GetParameters();
                return !ps.Where((t1, i) => !t1.ParameterType.IsAssignableFrom(args[i]?.GetType())).Any();
            });
            return ms?.Invoke(null, args);
        }
        public static object InvokeInstanceMethod(object o, string methodName, params object[] args)
        {
            var t = o.GetType();
            if (!sCacheInstanceMethods.TryGetValue(t, out var methodInfos))
            {
                methodInfos = o.GetType().GetMethods(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
                sCacheInstanceMethods.Add(t, methodInfos);
            }

            var ms = methodInfos.FirstOrDefault(m =>
            {
                if (m.Name != methodName)
                {
                    return false;
                }

                var ps = m.GetParameters();
                return ps.Where((t1, i) => (t1.ParameterType.IsClass && args[i] == null) || t1.ParameterType.IsAssignableFrom(args[i]?.GetType())).Any();
            });
            return ms?.Invoke(o, args);
        }
        public static string Object2String(object o)
        {
            if (o is IList list)
            {
                return list.ToListString();
            }
            return o?.ToString();
        }
        private static Solution sSolution = new Solution();
        static void NewCase(string methodName, params object[] objs)
        {
            Console.WriteLine($"###############{methodName}###############");
            Console.WriteLine("Input:");
            foreach (var o in objs)
            {
                Console.WriteLine(Object2String(o));
            }
            var result = InvokeInstanceMethod(sSolution, methodName, objs);
            if (result == null)
            {
                result = objs[0];
            }
            Console.WriteLine("Output:");
            Console.WriteLine(Object2String(result));
            Console.WriteLine("---------------------------------------------------");
        }
        static void Main(string[] args)
        {
            var s = new Solution();
            var root = TreeNode.Parse("[3, 9, 20, null, null, 15, 7]");
            Console.WriteLine($"input: {root}  MaxDepth: {s.MaxDepth(root)}");
            //二叉树
            NewCase(nameof(Solution.MaxDepth), TreeNode.Parse("[3, 9, 20, null, null, 15, 7]"));
            NewCase(nameof(Solution.IsBalanced), TreeNode.Parse("[3, 9, 20, null, null, 15, 7]"));
            NewCase(nameof(Solution.IsBalanced), TreeNode.Parse("[1, 2, 2, 3, 3, null, null, 4, 4]"));
            NewCase(nameof(Solution.MaxPathSum), TreeNode.Parse("[1, 2, 3]"));
            NewCase(nameof(Solution.MaxPathSum), TreeNode.Parse("[-10, 9, 20, null, null, 15, 7]"));
            NewCase(nameof(Solution.LowestCommonAncestor), TreeNode.Parse("[3, 5, 1, 6, 2, 0, 8, null, null, 7, 4]"), new TreeNode(5), new TreeNode(1));
            NewCase(nameof(Solution.LowestCommonAncestor), TreeNode.Parse("[3, 5, 1, 6, 2, 0, 8, null, null, 7, 4]"), new TreeNode(4), new TreeNode(7));
            NewCase(nameof(Solution.LevelOrder), TreeNode.Parse("[3, 9, 20, null, null, 15, 7]"));
            NewCase(nameof(Solution.LevelOrderBottom), TreeNode.Parse("[3, 9, 20, null, null, 15, 7]"));
            NewCase(nameof(Solution.ZigzagLevelOrder), TreeNode.Parse("[3, 9, 20, null, null, 15, 7]"));
            NewCase(nameof(Solution.ZigzagLevelOrder), TreeNode.Parse("[3, 1, 5, 0, 2, 4, 6, null, null, null, 3]"));
            NewCase(nameof(Solution.IsValidBST), TreeNode.Parse("[2, 1, 3]"));
            NewCase(nameof(Solution.IsValidBST), TreeNode.Parse("[5, 1, 4, null, null, 3, 6]"));
            NewCase(nameof(Solution.IsValidBST), TreeNode.Parse("[10, 5, 15, null, null, 6, 20]"));
            NewCase(nameof(Solution.IsValidBST), TreeNode.Parse("[10, 5, 15, null, null, 13, 20]"));
            NewCase(nameof(Solution.IsValidBST), TreeNode.Parse("[3, 1, 5, 0, 2, 4, 6, null, null, null, 3]"));
            NewCase(nameof(Solution.InsertIntoBST), TreeNode.Parse("[4, 2, 7, 1, 3]"), 5);

            //链表
            NewCase(nameof(Solution.DeleteDuplicates), ListNode.Parse("[1, 1, 2]"));
            NewCase(nameof(Solution.DeleteDuplicates), ListNode.Parse("[1, 1, 2, 3, 3]"));
            NewCase(nameof(Solution.DeleteDuplicates2), ListNode.Parse("[1, 2, 3, 3]"));
            NewCase(nameof(Solution.DeleteDuplicates3), ListNode.Parse("[1, 1, 2, 3, 3, 4, 5, 6, 7, 7, 8, 8, 9, 9]"));
            NewCase(nameof(Solution.ReverseList), ListNode.Parse("[1,2,3,4,5,5,99,6]"));
            NewCase(nameof(Solution.ReverseList2), ListNode.Parse("[1,2,3,4,5,5,99,6]"));
            NewCase(nameof(Solution.ReverseBetween), ListNode.Parse("[1,2,3,4,5,5,99,6]"), 2, 4);
            NewCase(nameof(Solution.ReverseBetween), ListNode.Parse("[1,2,3,4,5,5,99,6]"), 1, 3);
            NewCase(nameof(Solution.MergeTwoLists), ListNode.Parse("[1,2,4,6]"), ListNode.Parse("[1,3,4,4,5]"));
            NewCase(nameof(Solution.Partition), ListNode.Parse("[1,4,3,2,5,2]"), 4);
            NewCase(nameof(Solution.SortList), ListNode.Parse("[4,2,1,3]"));
            NewCase(nameof(Solution.SortList), ListNode.Parse("[-1,5,3,4,0]"));
            NewCase(nameof(Solution.ReorderList), ListNode.Parse("[1,2,3,4,5]"));
            NewCase(nameof(Solution.HasCycle), ListNode.Parse("[1,2,3,4,5]", 2));
            NewCase(nameof(Solution.DetectCycle), ListNode.Parse("[1,2,3,4,5,6]", 2));


        }
    }
}
