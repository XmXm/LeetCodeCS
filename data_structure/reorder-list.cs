/// <summary>
/// https://leetcode-cn.com/problems/reorder-list/
/// </summary>
public partial class Solution
{
    public void ReorderList(ListNode head)
    {
        if (head == null)
        {
            return;
        }
        var middle = FindMiddle(head);
        var tail = middle.next;
        middle.next = null;
        tail = ReverseList(tail);
        //head 链表元素数量永远大于等于tail
        while (tail != null)
        {
            var p = head.next;
            var q = tail.next;
            head.next = tail;
            tail.next = p;
            head = p;
            tail = q;
        }
    }
}