/// <summary>
/// https://leetcode-cn.com/problems/reverse-linked-list/
/// </summary>
public partial class Solution
{
    /// <summary>
    /// 递归实现
    /// </summary>
    /// <param name="head"></param>
    /// <returns></returns>
    public ListNode ReverseList(ListNode head)
    {
        if (head?.next == null)
        {
            return head;
        }
        var p = head.next;
        head.next = null;
        var prev = ReverseList(p);
        p.next = head;
        return prev;
    }

    /// <summary>
    /// 迭代实现
    /// </summary>
    /// <param name="head"></param>
    /// <returns></returns>
    public ListNode ReverseList2(ListNode head)
    {
        ListNode prev = null;
        while (head != null)
        {
            var tmp = head.next;
            head.next = prev;
            prev = head;
            head = tmp;
        }
        return prev;
    }
}