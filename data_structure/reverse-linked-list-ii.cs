/// <summary>
/// https://leetcode-cn.com/problems/reverse-linked-list-ii/
/// </summary>
public partial class Solution
{
    public ListNode ReverseBetween(ListNode head, int m, int n)
    {
        var p = head;
        var index = 1;
        ListNode headFirst = null;
        while (index < m)
        {
            headFirst = p;
            p = p.next;
            index++;
        }
        ListNode prev = null;
        ListNode reverseTail = p;
        while (index >= m && index <= n)
        {
            var tmp = p.next;
            p.next = prev;
            prev = p;
            index++;
            p = tmp;
        }
        reverseTail.next = p;
        if (headFirst != null)
        {
            headFirst.next = prev;
            return head;
        }
        else
        {
            return prev;
        }
    }
}