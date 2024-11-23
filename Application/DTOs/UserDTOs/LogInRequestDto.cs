namespace Application.DTOs.UserDTOs;

public class LogInRequestDto
{
    public string UserName { get; set; } = default!;
    public string Password { get; set; } = default!;
}