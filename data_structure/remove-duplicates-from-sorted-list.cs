/// <summary>
/// https://leetcode-cn.com/problems/remove-duplicates-from-sorted-list/
/// /// </summary>
public partial class Solution
{
    public ListNode DeleteDuplicates(ListNode head)
    {
        var p = head;
        while (p != null)
        {
            while (p.next != null && p.val == p.next.val)
            {
                p.next = p.next.next;
            }
            p = p.next;
        }
        return head;
    }
}