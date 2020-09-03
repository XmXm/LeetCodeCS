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
            val = i < arr.Length - 1 ? arr[i + 1] : null;
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
namespace LinkList
{
    public class Node
    {
        public int val;
        public Node next;
        public Node random;
        public Node(int _val)
        {
            val = _val;
            next = null;
            random = null;
        }

        public override string ToString()
        {
            var list = new List<int[]>();
            var p = this;
            var dict = new Dictionary<Node, int>();
            var index = 0;
            while (p != null)
            {
                dict.Add(p, index++);
                p = p.next;
            }
            p = this;
            while (p != null)
            {
                list.Add(new int[] { p.val, p.random != null ? dict[p.random] : -1 });
                p = p.next;
            }
            return list.ToListString((o) => ((int)o) == -1 ? "null" : null);
        }
        public static Node Parse(string str)
        {
            //[[3,null [3,0 [3,null]]
            var arr = str.Split("],", 256, StringSplitOptions.RemoveEmptyEntries);
            var listNodes = new List<Node>();
            var randomIndexs = new List<int>();
            for (var i = 0; i < arr.Length; i++)
            {
                var objArr = arr[i].Replace("[", "").Replace("]", "").Split(',', StringSplitOptions.RemoveEmptyEntries);
                if (objArr.Length == 2)
                {
                    var node = new Node(int.Parse(objArr[0]));
                    listNodes.Add(node);
                    if (objArr[1] == "null")
                    {
                        randomIndexs.Add(-1);
                    }
                    else
                    {
                        randomIndexs.Add(int.Parse(objArr[1]));
                    }
                }

            }
            if (listNodes.All(m => m == null))
            {
                return null;
            }
            for (var i = 0; i < listNodes.Count; i++)
            {
                if (i < listNodes.Count - 1)
                {
                    listNodes[i].next = listNodes[i + 1];
                }
                var randomIndex = randomIndexs[i];
                if (randomIndex >= 0)
                {
                    listNodes[i].random = listNodes[randomIndex];
                }
            }
            return listNodes[0];
        }
    }
}

namespace Graph
{
    public class Node
    {
        public int val;
        public IList<Node> neighbors;

        public Node()
        {
            val = 0;
            neighbors = new List<Node>();
        }

        public Node(int _val)
        {
            val = _val;
            neighbors = new List<Node>();
        }

        public Node(int _val, List<Node> _neighbors)
        {
            val = _val;
            neighbors = _neighbors;
        }

        public void CollectNode(ICollection<Node> sets)
        {
            if (sets.Contains(this))
            {
                return;
            }
            sets.Add(this);
            foreach (var item in neighbors)
            {
                item.CollectNode(sets);
            }
        }

        public override string ToString()
        {
            var visited = new HashSet<Node>();
            CollectNode(visited);
            var list = visited.ToList();
            list.Sort((x, y) => x.val.CompareTo(y.val));
            return list.ToListString(o => ((Node)o).neighbors.Select(n => n.val).ToList().ToListString());
        }
        public static Node Parse(string str)
        {
            var arr = str.Split("],", 256, StringSplitOptions.RemoveEmptyEntries);
            var nodes = new Node[arr.Length];
            for (var i = 0; i < arr.Length; i++)
            {
                nodes[i] = new Node(i + 1);
            }
            for (var i = 0; i < arr.Length; i++)
            {
                var objArr = arr[i].Replace("[", "").Replace("]", "").Split(',', StringSplitOptions.RemoveEmptyEntries);
                foreach (var item in objArr)
                {
                    nodes[i].neighbors.Add(nodes[int.Parse(item) - 1]);
                }
            }
            return nodes[0];
        }
    }
}
public static class ExtentionMethod
{
    // public static object Deserialize(string str)
    // {
    //     throw new NotImplementedException();
    // }
    public static string ToListString(this IList l, Func<object, string> predicate = null)
    {
        var str = new StringBuilder();
        str.Append('[');
        for (var i = 0; i < l.Count; i++)
        {
            var o = l[i];
            if (o is IList subL)
            {
                str.Append(subL.ToListString(predicate));
            }
            else
            {
                var oStr = predicate?.Invoke(o);
                if (string.IsNullOrEmpty(oStr))
                {
                    oStr = o.ToString();
                }
                str.Append(oStr);
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
        private static readonly Dictionary<Type, MethodInfo[]> sCacheInstanceMethods = new Dictionary<Type, MethodInfo[]>();
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
                if (ps.Length == 0 && args.Length == 0)
                {
                    return true;
                }
                if (ps.Length == 1 && ps[0].ParameterType.IsArray)
                {
                    return true;
                }
                if (ps.Length != args.Length)
                {
                    return false;
                }
                return ps.Where((t1, i) =>
                {
                    if (t1.ParameterType.IsClass && args[i] == null)
                    {
                        return true;
                    }
                    var t = args[i]?.GetType();
                    if (t1.ParameterType.IsAssignableFrom(t))
                    {
                        return true;
                    }
                    if (t.IsPrimitive && t1.ParameterType.IsPrimitive)
                    {
                        return true;
                    }
                    return false;
                }).Any();
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

        public enum OutputOption
        {
            UseFirstParam,
            UseInstance
        }
        private static Stack<OutputOption> sOutputOptions = new Stack<OutputOption>();
        static void NewCaseArrayInput(object instance, string methodName, object obj)
        {
            NewCase(instance, methodName, new object[] { obj });
        }
        class DisposeOutputOption : IDisposable
        {
            public DisposeOutputOption(OutputOption opt)
            {
                sOutputOptions.Push(opt);
            }
            public void Dispose()
            {
                sOutputOptions.Pop();
            }
        }
        static IDisposable UsingOutput(OutputOption opt)
        {
            return new DisposeOutputOption(opt);
        }
        static void NewCase(object instance, string methodName, params object[] objs)
        {
            Console.WriteLine($"###############{instance.GetType().Name}.{methodName}###############");
            Console.WriteLine("Input:");
            foreach (var o in objs)
            {
                Console.WriteLine(Object2String(o));
            }
            var result = InvokeInstanceMethod(instance, methodName, objs);
            if (sOutputOptions.Count == 0)
            {
                sOutputOptions.Push(OutputOption.UseFirstParam);
            }
            if (result == null)
            {
                if (objs.Length > 0 && sOutputOptions.Peek() == OutputOption.UseFirstParam)
                {
                    result = objs[0];
                }
                else if (sOutputOptions.Peek() == OutputOption.UseInstance)
                {
                    result = instance;
                }
            }
            Console.WriteLine("Output:");
            Console.WriteLine(Object2String(result));
            Console.WriteLine("---------------------------------------------------");
        }
        private static Solution sSolution = new Solution();
        static void Main(string[] args)
        {
            var s = new Solution();
            //二叉树
            NewCase(s, nameof(Solution.MaxDepth), TreeNode.Parse("[3, 9, 20, null, null, 15, 7]"));
            NewCase(s, nameof(Solution.IsBalanced), TreeNode.Parse("[3, 9, 20, null, null, 15, 7]"));
            NewCase(s, nameof(Solution.IsBalanced), TreeNode.Parse("[1, 2, 2, 3, 3, null, null, 4, 4]"));
            NewCase(s, nameof(Solution.MaxPathSum), TreeNode.Parse("[1, 2, 3]"));
            NewCase(s, nameof(Solution.MaxPathSum), TreeNode.Parse("[-10, 9, 20, null, null, 15, 7]"));
            NewCase(s, nameof(Solution.LowestCommonAncestor), TreeNode.Parse("[3, 5, 1, 6, 2, 0, 8, null, null, 7, 4]"), new TreeNode(5), new TreeNode(1));
            NewCase(s, nameof(Solution.LowestCommonAncestor), TreeNode.Parse("[3, 5, 1, 6, 2, 0, 8, null, null, 7, 4]"), new TreeNode(4), new TreeNode(7));
            NewCase(s, nameof(Solution.LevelOrder), TreeNode.Parse("[3, 9, 20, null, null, 15, 7]"));
            NewCase(s, nameof(Solution.LevelOrderBottom), TreeNode.Parse("[3, 9, 20, null, null, 15, 7]"));
            NewCase(s, nameof(Solution.ZigzagLevelOrder), TreeNode.Parse("[3, 9, 20, null, null, 15, 7]"));
            NewCase(s, nameof(Solution.ZigzagLevelOrder), TreeNode.Parse("[3, 1, 5, 0, 2, 4, 6, null, null, null, 3]"));
            NewCase(s, nameof(Solution.IsValidBST), TreeNode.Parse("[2, 1, 3]"));
            NewCase(s, nameof(Solution.IsValidBST), TreeNode.Parse("[5, 1, 4, null, null, 3, 6]"));
            NewCase(s, nameof(Solution.IsValidBST), TreeNode.Parse("[10, 5, 15, null, null, 6, 20]"));
            NewCase(s, nameof(Solution.IsValidBST), TreeNode.Parse("[10, 5, 15, null, null, 13, 20]"));
            NewCase(s, nameof(Solution.IsValidBST), TreeNode.Parse("[3, 1, 5, 0, 2, 4, 6, null, null, null, 3]"));
            NewCase(s, nameof(Solution.InsertIntoBST), TreeNode.Parse("[4, 2, 7, 1, 3]"), 5);

            //链表
            NewCase(s, nameof(Solution.DeleteDuplicates), ListNode.Parse("[1, 1, 2]"));
            NewCase(s, nameof(Solution.DeleteDuplicates), ListNode.Parse("[1, 1, 2, 3, 3]"));
            NewCase(s, nameof(Solution.DeleteDuplicates2), ListNode.Parse("[1, 2, 3, 3]"));
            NewCase(s, nameof(Solution.DeleteDuplicates3), ListNode.Parse("[1, 1, 2, 3, 3, 4, 5, 6, 7, 7, 8, 8, 9, 9]"));
            NewCase(s, nameof(Solution.ReverseList), ListNode.Parse("[1,2,3,4,5,5,99,6]"));
            NewCase(s, nameof(Solution.ReverseList2), ListNode.Parse("[1,2,3,4,5,5,99,6]"));
            NewCase(s, nameof(Solution.ReverseBetween), ListNode.Parse("[1,2,3,4,5,5,99,6]"), 2, 4);
            NewCase(s, nameof(Solution.ReverseBetween), ListNode.Parse("[1,2,3,4,5,5,99,6]"), 1, 3);
            NewCase(s, nameof(Solution.MergeTwoLists), ListNode.Parse("[1,2,4,6]"), ListNode.Parse("[1,3,4,4,5]"));
            NewCase(s, nameof(Solution.Partition), ListNode.Parse("[1,4,3,2,5,2]"), 4);
            NewCase(s, nameof(Solution.SortList), ListNode.Parse("[4,2,1,3]"));
            NewCase(s, nameof(Solution.SortList), ListNode.Parse("[-1,5,3,4,0]"));
            NewCase(s, nameof(Solution.ReorderList), ListNode.Parse("[1,2,3,4,5]"));
            NewCase(s, nameof(Solution.HasCycle), ListNode.Parse("[1,2,3,4,5]", 2));
            NewCase(s, nameof(Solution.DetectCycle), ListNode.Parse("[1,2,3,4,5,6]", 2));
            NewCase(s, nameof(Solution.IsPalindrome), ListNode.Parse("[1,2,3,2,1]"));
            NewCase(s, nameof(Solution.CopyRandomList), LinkList.Node.Parse("[[7,null],[13,0],[11,4],[10,2],[1,0]]"));


            //栈
            using (UsingOutput(OutputOption.UseInstance))
            {
                MinStack minStack = new MinStack();
                NewCase(minStack, nameof(MinStack.Push), -2);
                NewCase(minStack, nameof(MinStack.Push), 0);
                NewCase(minStack, nameof(MinStack.Push), -3);
                NewCase(minStack, nameof(MinStack.GetMin));
                NewCase(minStack, nameof(MinStack.Pop));
                NewCase(minStack, nameof(MinStack.Pop));
                NewCase(minStack, nameof(MinStack.GetMin));
            }


            NewCaseArrayInput(s, nameof(Solution.EvalRPN), new string[] { "2", "1", "+", "3", "*" });
            NewCaseArrayInput(s, nameof(Solution.EvalRPN), new string[] { "4", "13", "5", "/", "+" });
            NewCaseArrayInput(s, nameof(Solution.EvalRPN), new string[] { "10", "6", "9", "3", "+", "-11", "*", "/", "*", "17", "+", "5", "+" });

            NewCase(s, nameof(Solution.DecodeString), "3[a]2[bc]");
            NewCase(s, nameof(Solution.DecodeString), "2[abc]3[cd]ef");
            NewCase(s, nameof(Solution.DecodeString), "3[a2[c]]");
            NewCase(s, nameof(Solution.DecodeString), "abc3[cd]xyz");
            NewCase(s, nameof(Solution.InorderTraversal), TreeNode.Parse("[1,null,2,3]"));
            NewCase(s, nameof(Solution.CloneGraph), Graph.Node.Parse("[[2,4],[1,3],[2,4],[1,3]]"));
            NewCase(s, nameof(Solution.LargestRectangleArea), new int[] { 2, 1, 5, 6, 2, 3 });


            using (UsingOutput(OutputOption.UseInstance))
            {
                MyQueue queue = new MyQueue();
                NewCase(queue, nameof(MyQueue.Push), 1);
                NewCase(queue, nameof(MyQueue.Push), 2);
                NewCase(queue, nameof(MyQueue.Peek));
                NewCase(queue, nameof(MyQueue.Pop));
                NewCase(queue, nameof(MyQueue.Empty));
            }
            var matrix = new int[][]
            {
                new int[]{0, 0, 0},
                new int[]{0, 1, 0},
                new int[]{0, 0, 0},
            };
            NewCaseArrayInput(s, nameof(Solution.UpdateMatrix), matrix);
            matrix = new int[][]
            {
                new int[]{0,0,0},
                new int[]{0,1,0},
                new int[]{1,1,1},
            };
            NewCaseArrayInput(s, nameof(Solution.UpdateMatrix), matrix);
            NewCase(s, nameof(Solution.SingleNumber), new int[] { 4, 1, 2, 1, 2 });
            NewCase(s, nameof(Solution.SingleNumberII), new int[] { 4, 1, 1, 2, 2, 1, 2 });
            NewCase(s, nameof(Solution.SingleNumberIII), new int[] { 1, 2, 1, 3, 2, 5 });
            NewCase(s, nameof(Solution.HammingWeight), (uint)0b11111111111111111111111111111101);
            NewCase(s, nameof(Solution.CountBits), 5);
            NewCase(s, nameof(Solution.reverseBits), (uint)0b00000010100101000001111010011100);
            NewCase(s, nameof(Solution.RangeBitwiseAnd), 0, 1);
            NewCase(s, nameof(Solution.Search), new int[]{5},9);
            NewCase(s, nameof(Solution.SearchInsert), new int[]{1,3,5,7},2);
        }
    }
}
