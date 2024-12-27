namespace Chat.Core.DTOs.UserChatFriendDTOs;

public class SearchChatUserResultResponseDto
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;
    public string Avatar { get; set; } = string.Empty;
    public int FriendshipStatus {get; set; }
    public bool UserActive {get; set; }
}