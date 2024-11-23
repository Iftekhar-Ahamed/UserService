namespace Application.DTOs.UserDTOs;

public class LogInResponseDto
{
    public string AccessToken { get; set; } = default!;
    public string RefreshToken { get; set; } = default!;
}