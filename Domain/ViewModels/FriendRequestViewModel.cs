namespace Domain.ViewModels;

public class FriendRequestViewModel
{
    public long ChatId { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string? MiddleName { get; set; }
    public string ImageUrl { get; set; } = null!;
    public int FriendshipStatus { get; set; }
}