namespace Domain.ViewModels;

public class ChatUserSearchResultViewModel
{
    public long Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string MiddleName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string ImageUrl { get; set; } = string.Empty;
    public int ActionBy { get; set; }
    public int ApproveStatus { get; set; }
}