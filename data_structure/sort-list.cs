/// <summary>
/// https://leetcode-cn.com/problems/sort-list/
/// </summary>
public partial class Solution
{
    /// <summary>
    /// 1 2 3 4 5 6
    /// 1 2    3
    /// 2 3 4  5 6
    /// </summary>
    /// <param name="head"></param>
    /// <returns></returns>
    private ListNode FindMiddle(ListNode head)
    {
        var slow = head;
        ListNode fast = slow.next;
        while (fast != null && fast.next != null)
        {
            slow = slow.next;
            fast = fast.next.next;
        }
        return slow;
    }
    /// <summary>
    /// 分治法， 使用步长不一致的指针来确定中心元素
    /// </summary>
    /// <param name="head"></param>
    /// <returns></returns>
    public ListNode SortList(ListNode head)
    {
        if (head?.next == null)
        {
            return head;
        }
        if (head.next != null && head.next.next == null)
        {
            //2个元素开始排序
            if (head.val <= head.next.val)
            {
                return head;
            }
            var newHead = head.next;
            newHead.next = head;
            head.next = null;
            return newHead;
        }
        var middle = FindMiddle(head);
        var tail = middle.next;
        middle.next = null;
        var left = SortList(head);
        var right = SortList(tail);
        //合并链表
        return MergeTwoLists(left, right);
    }
}