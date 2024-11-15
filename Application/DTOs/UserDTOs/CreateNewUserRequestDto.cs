namespace Application.DTOs.UserDTOs;

public class CreateNewUserRequestDto
{
    public int? UserId { get; set; }
    public required NameElementDto Name { get; set; }
    public required DateTime Dob { get; set; }
    public required string Email { get; set; }
    public required string ContactNUmberCountryCode { get; set; }
    public required string ContactNumber { get; set; }
}