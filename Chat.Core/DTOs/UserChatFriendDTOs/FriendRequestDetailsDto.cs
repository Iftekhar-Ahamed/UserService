using Domain.Enums;
using User.Core.DTOs.UserDTOs;

namespace Chat.Core.DTOs.UserChatFriendDTOs;

public class FriendRequestDetailsDto
{
    public long ChatId { get; set; }
    public NameElementDto Name { get; set; } = null!;
    public string? ImageUrl { get; set; }
    public FriendshipStatus FriendshipStatus { get; set; }
}