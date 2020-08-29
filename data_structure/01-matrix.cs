using System;
using System.Collections.Generic;
/// <summary>
/// https://leetcode-cn.com/problems/01-matrix/
/// </summary>
public partial class Solution
{
    private struct MatrixPoint
    {
        public int Row;
        public int Col;
        public MatrixPoint(int i, int j)
        {
            Row = i;
            Col = j;
        }
        public MatrixPoint Left => new MatrixPoint(Row - 1, Col);
        public MatrixPoint Right => new MatrixPoint(Row + 1, Col);
        public MatrixPoint Up => new MatrixPoint(Row, Col - 1);
        public MatrixPoint Down => new MatrixPoint(Row, Col + 1);
    }
    public int[][] UpdateMatrix(int[][] matrix)
    {
        var queue = new Queue<MatrixPoint>();
        for (var i = 0; i < matrix.Length; i++)
        {
            var row = matrix[i];
            for (var j = 0; j < row.Length; j++)
            {
                if (row[j] != 1)
                {
                    continue;
                }
                queue.Enqueue(new MatrixPoint(i, j));
                var index = 0;
                while (queue.Count > 0)
                {
                    var len = queue.Count;
                    for (var w = 0; w < len; w++)
                    {
                        var p = queue.Dequeue();
                        if (p.Row < 0 || p.Col < 0 || p.Row >= matrix.Length || p.Col >= matrix[i].Length)
                        {
                            continue;
                        }
                        if (matrix[p.Row][p.Col] == 0)
                        {
                            row[j] = index;
                            queue.Clear();
                            break;
                        }
                        //上下左右加入队列
                        queue.Enqueue(p.Left);
                        queue.Enqueue(p.Right);
                        queue.Enqueue(p.Up);
                        queue.Enqueue(p.Down);
                    }
                    index++;
                }
            }
        }
        return matrix;
    }
}