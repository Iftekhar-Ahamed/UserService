using Domain.Enums;
namespace Domain.DTOs;

public class FriendRequestDetailsDto
{
    public long ChatId { get; set; }
    public string FirstName { get; set; } = null!;
    public string? MiddleName { get; set; }
    public string LastName { get; set; } = null!;
    public string? ImageUrl { get; set; }
    public FriendshipStatus FriendshipStatus { get; set; }
}