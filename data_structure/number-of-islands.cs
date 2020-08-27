/// <summary>
/// https://leetcode-cn.com/problems/number-of-islands/
/// </summary>
public partial class Solution
{
    private int DFSIslands(char[][] grid, int i, int j)
    {
        if (i < 0 || i >= grid.Length || j < 0 || j >= grid[i].Length)
        {
            return 0;
        }
        if (grid[i][j] == '1')
        {
            //访问过则标记为0
            grid[i][j] = '0';
            //周围4格
            return DFSIslands(grid, i - 1, j) + DFSIslands(grid, i + 1, j) + DFSIslands(grid, i, j - 1) + +DFSIslands(grid, i, j + 1) + 1;
        }
        return 0;
    }
    public int NumIslands(char[][] grid)
    {
        var count = 0;
        for (var i = 0; i < grid.Length; i++)
        {
            for (var j = 0; j < grid[i].Length; j++)
            {
                if (grid[i][j] == '1' && DFSIslands(grid, i, j) > 0)
                {
                    count++;
                }
            }
        }
        return count;
    }
}