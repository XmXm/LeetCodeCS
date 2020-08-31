/// <summary>
/// https://leetcode-cn.com/problems/counting-bits/
/// </summary>
public partial class Solution
{
    public int[] CountBits(int num)
    {
        var result = new int[num + 1];
        for(var i = 0; i <= num; i++){
            result[i] = HammingWeight((uint)i);
        }
        return result;
    }
    //todo 使用动态规划来解
}