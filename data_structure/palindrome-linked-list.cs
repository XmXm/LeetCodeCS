/// <summary>
/// https://leetcode-cn.com/problems/palindrome-linked-list/
/// </summary>
public partial class Solution
{
    public bool IsPalindrome(ListNode head)
    {
        if (head == null)
        {
            return true;
        }
        var middle = FindMiddle(head);
        var tail = middle.next;
        tail = ReverseList(tail);
        while (tail != null)
        {
            if (tail.val != head.val)
            {
                return false;
            }
            tail = tail.next;
            head = head.next;
        }
        return true;
    }
}