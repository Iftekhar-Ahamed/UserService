namespace User.Core.DTOs.UserDTOs;

public class SearchUserResultResponseDto
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;
    public string Avatar { get; set; } = string.Empty;
    public bool IsActive {get; set; }
}