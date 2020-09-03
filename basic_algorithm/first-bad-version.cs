public partial class Solution
{
    public int BadVersion { get; set; }
    private bool IsBadVersion(int version) => version >= BadVersion;
    public int FirstBadVersion(int n)
    {
        var left = 0;
        var right = n;
        while (left + 1 < right)
        {
            var mid = left + (right - left) / 2;
            var isBad = IsBadVersion(mid);
            if (isBad)
            {
                right = mid;
            }
            else
            {
                left = mid;
            }
        }
        return IsBadVersion(left) ? left : right;
    }
}