namespace Chat.Core.DTOs.UserChatFriendDTOs;

public class AddNewChatFriendRequestDto
{
    public long SelfUserId { get; set; }
    public long RequestedUserId { get; set; }
}