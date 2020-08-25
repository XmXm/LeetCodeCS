public partial class Solution
{
    /// <summary>
    /// O(1) 内存
    /// </summary>
    /// <param name="head"></param>
    /// <returns></returns>
    public bool HasCycle(ListNode head)
    {
        if (head == null)
        {
            return false;
        }
        var slow = head;
        var fast = slow.next;
        while (fast != null && fast.next != null)
        {
            if (slow == fast)
            {
                return true;
            }
            slow = slow.next;
            fast = fast.next.next;
        }
        return false;
    }
}