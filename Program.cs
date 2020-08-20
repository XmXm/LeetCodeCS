using System;
using System.Collections.Generic;
public class TreeNode
{
    public int val;
    public TreeNode left;
    public TreeNode right;
    public TreeNode(int x) { val = x; }
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
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var s = new Solution();
            var inputArr = new object[] { 3, 9, 20, null, null, 15, 7 };
            var root = MakeTreeNode(inputArr);
            Console.WriteLine($"input: {string.Join(",", inputArr)}  MaxDepth: {s.MaxDepth(root)}");

            Console.WriteLine($"input: {string.Join(",", inputArr)}  IsBalanced: {s.IsBalanced(root)}");
            inputArr = new object[] {1,2,2,3,3,null,null,4,4 };
            root = MakeTreeNode(inputArr);
            Console.WriteLine($"input: {string.Join(",", inputArr)}  IsBalanced: {s.IsBalanced(root)}");

            inputArr = new object[]{1,2,3};
            root = MakeTreeNode(inputArr);
            Console.WriteLine($"input: {string.Join(",", inputArr)}  MaxPathSum: {s.MaxPathSum(root)}");
            inputArr = new object[]{-10,9,20,null,null,15,7};
            root = MakeTreeNode(inputArr);
            Console.WriteLine($"input: {string.Join(",", inputArr)}  MaxPathSum: {s.MaxPathSum(root)}");
        }
    }
}
