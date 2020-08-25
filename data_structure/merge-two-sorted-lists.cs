/// <summary>
/// https://leetcode-cn.com/problems/merge-two-sorted-lists/
/// </summary>
public partial class Solution
{
    public ListNode MergeTwoLists(ListNode l1, ListNode l2)
    {
        var p = l1;
        var q = l2;
        var dummy = new ListNode(0);
        var head = dummy;
        while (p != null || q != null)
        {
            if (p != null && (q == null || p.val < q.val))
            {
                head.next = p;
                p = p.next;
            }
            else
            {
                head.next = q;
                q = q.next;
            }
            head = head.next;
        }
        return dummy.next;
    }
}