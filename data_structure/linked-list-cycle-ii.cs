/// <summary>
/// https://leetcode-cn.com/problems/linked-list-cycle-ii/
/// </summary>
public partial class Solution
{
    /// <summary>
    /// 1 2 3 4 5 -> 2
    /// 1-2 2-4 3-2 4-4
    /// 1-5 2-2
    /// </summary>
    /// <param name="head"></param>
    /// <returns></returns>
    public ListNode DetectCycle(ListNode head)
    {
        if (head == null)
        {
            return null;
        }
        var slow = head;
        var fast = slow.next;
        while (fast != null && fast.next != null)
        {
            if (slow == fast)
            {
                //步调一致，再次相交则为入环点
                fast = head;
                slow = slow.next;
                while (fast != slow)
                {
                    fast = fast.next;
                    slow = slow.next;
                }
                return slow;
            }
            slow = slow.next;
            fast = fast.next.next;
        }
        return null;
    }
}