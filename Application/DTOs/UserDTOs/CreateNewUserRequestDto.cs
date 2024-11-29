namespace Application.DTOs.UserDTOs;

public class CreateNewUserRequestDto
{
    public int? UserId { get; set; }
    public required NameElementDto Name { get; set; }
    public required DateOnly Dob { get; set; }
    public required string Email { get; set; }
    public required string ContactNumberCountryCode { get; set; }
    public required string ContactNumber { get; set; }
    public string? Password { get; set; }
}