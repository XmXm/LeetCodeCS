/// <summary>
/// https://leetcode-cn.com/problems/remove-duplicates-from-sorted-list-ii/
/// </summary>
public partial class Solution
{
    public ListNode DeleteDuplicates2(ListNode head)
    {
        ListNode newHead = null;
        ListNode newTail = null;
        ListNode prev = null;
        var p = head;
        while (p != null)
        {
            if ((prev == null || p.val != prev.val) && (p.next == null || p.val != p.next.val))
            {
                var newNode = new ListNode(p.val);
                if (newHead == null)
                {
                    newHead = newNode;
                    newTail = newHead;
                }
                else
                {
                    newTail.next = newNode;
                    newTail = newNode;
                }
            }
            prev = p;
            p = p.next;
        }
        return newHead;
    }

    public ListNode DeleteDuplicates3(ListNode head)
    {
        var dummy = new ListNode(0);
        dummy.next = head;
        var p = dummy;
        while (p.next != null && p.next.next != null)
        {
            if (p.next.val == p.next.next.val)
            {
                var rmVal = p.next.val;
                while (p.next != null && p.next.val == rmVal)
                {
                    p.next = p.next.next;
                }
            }
            else
            {
                p = p.next;
            }
        }

        return dummy.next;
    }
}