namespace Domain.DTOs;

public class ChatUserSearchResultDto
{
    public long Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string MiddleName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Avatar { get; set; } = string.Empty;
    public int ApproveStatus { get; set; }
}