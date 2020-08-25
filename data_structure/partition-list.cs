/// <summary>
/// https://leetcode-cn.com/problems/partition-list/
/// </summary>
public partial class Solution
{
    public ListNode Partition(ListNode head, int x)
    {
        var small = new ListNode(0);
        var bigger = new ListNode(0);
        var bp = bigger;
        var sp = small;
        while (head != null)
        {
            if (head.val >= x)
            {
                bp.next = head;
                bp = head;
            }
            else
            {
                sp.next = head;
                sp = head;
            }
            head = head.next;
            bp.next = null;
            sp.next = null;
        }
        sp.next = bigger.next;
        return small.next;
    }
}