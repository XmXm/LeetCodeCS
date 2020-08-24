﻿using System.Text;
using System;
using System.Collections.Generic;
using System.Collections;

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
}

namespace LeetCodeCS
{
    class Program
    {
        static TreeNode MakeTreeNode(object[] arr)
        {
            var treeQueue = new Queue<TreeNode>();
            var ret = new TreeNode((int)arr[0]);
            treeQueue.Enqueue(ret);
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
            return ret;
        }

        static string TreeArrayToString(object[] objs)
        {
            var str = new StringBuilder();
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
        static string ListToString(IList l)
        {
            var str = new StringBuilder();
            str.Append('[');
            for (var i = 0; i < l.Count; i++)
            {
                var o = l[i];
                if (o is IList subL)
                {
                    str.Append(ListToString(subL));
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
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var s = new Solution();
            var inputArr = new object[] { 3, 9, 20, null, null, 15, 7 };
            var root = MakeTreeNode(inputArr);
            Console.WriteLine($"input: {TreeArrayToString(inputArr)}  MaxDepth: {s.MaxDepth(root)}");

            Console.WriteLine($"input: {TreeArrayToString(inputArr)}  IsBalanced: {s.IsBalanced(root)}");
            inputArr = new object[] { 1, 2, 2, 3, 3, null, null, 4, 4 };
            root = MakeTreeNode(inputArr);
            Console.WriteLine($"input: {TreeArrayToString(inputArr)}  IsBalanced: {s.IsBalanced(root)}");

            inputArr = new object[] { 1, 2, 3 };
            root = MakeTreeNode(inputArr);
            Console.WriteLine($"input: {TreeArrayToString(inputArr)}  MaxPathSum: {s.MaxPathSum(root)}");
            inputArr = new object[] { -10, 9, 20, null, null, 15, 7 };
            root = MakeTreeNode(inputArr);
            Console.WriteLine($"input: {TreeArrayToString(inputArr)}  MaxPathSum: {s.MaxPathSum(root)}");

            inputArr = new object[] { 3, 5, 1, 6, 2, 0, 8, null, null, 7, 4 };
            root = MakeTreeNode(inputArr);
            Console.WriteLine($"input: {TreeArrayToString(inputArr)}  LowestCommonAncestor: {s.LowestCommonAncestor(root, new TreeNode(5), new TreeNode(1))?.val}");
            Console.WriteLine($"input: {TreeArrayToString(inputArr)}  LowestCommonAncestor: {s.LowestCommonAncestor(root, new TreeNode(4), new TreeNode(7))?.val}");

            inputArr = new object[] { 3, 9, 20, null, null, 15, 7 };
            root = MakeTreeNode(inputArr);
            Console.WriteLine($"input: {TreeArrayToString(inputArr)}  LevelOrder: {ListToString(s.LevelOrder(root) as IList)}");
            Console.WriteLine($"input: {TreeArrayToString(inputArr)}  LevelOrderBottom: {ListToString(s.LevelOrderBottom(root) as IList)}");


            inputArr = new object[] { 3, 9, 20, null, null, 15, 7 };
            root = MakeTreeNode(inputArr);
            Console.WriteLine($"input: {TreeArrayToString(inputArr)}  ZigzagLevelOrder: {ListToString(s.ZigzagLevelOrder(root) as IList)}");

            inputArr = new object[] { 3, 1, 5, 0, 2, 4, 6, null, null, null, 3 };
            root = MakeTreeNode(inputArr);
            Console.WriteLine($"input: {TreeArrayToString(inputArr)}  ZigzagLevelOrder: {ListToString(s.ZigzagLevelOrder(root) as IList)}");

            inputArr = new object[] { 2, 1, 3 };
            root = MakeTreeNode(inputArr);
            Console.WriteLine($"input: {TreeArrayToString(inputArr)}  IsValidBST: {s.IsValidBST(root)}");

            inputArr = new object[] { 5, 1, 4, null, null, 3, 6 };
            root = MakeTreeNode(inputArr);
            Console.WriteLine($"input: {TreeArrayToString(inputArr)}  IsValidBST: {s.IsValidBST(root)}");

            inputArr = new object[] { 10, 5, 15, null, null, 6, 20 };
            root = MakeTreeNode(inputArr);
            Console.WriteLine($"input: {TreeArrayToString(inputArr)}  IsValidBST: {s.IsValidBST(root)}");

            inputArr = new object[] { 10, 5, 15, null, null, 13, 20 };
            root = MakeTreeNode(inputArr);
            Console.WriteLine($"input: {TreeArrayToString(inputArr)}  IsValidBST: {s.IsValidBST(root)}");

            inputArr = new object[] { 3, 1, 5, 0, 2, 4, 6, null, null, null, 3 };
            root = MakeTreeNode(inputArr);
            Console.WriteLine($"input: {TreeArrayToString(inputArr)}  IsValidBST: {s.IsValidBST(root)}");

            inputArr = new object[] { 4, 2, 7, 1, 3 };
            root = MakeTreeNode(inputArr);
            Console.WriteLine($"input: {TreeArrayToString(inputArr)}  InsertIntoBST: {TreeArrayToString(s.InsertIntoBST(root, 5).ToArray())}");
        }
    }
}
