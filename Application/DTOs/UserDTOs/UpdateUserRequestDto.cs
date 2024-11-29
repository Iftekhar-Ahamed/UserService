namespace Application.DTOs.UserDTOs;

public class UpdateUserRequestDto
{
    public int UserId { get; set; }
    public  NameElementDto? Name { get; set; }
    public  DateOnly? Dob { get; set; }
    public  string? Email { get; set; }
    public  string? ContactNumberCountryCode { get; set; }
    public  string? ContactNumber { get; set; }
    public string? Password { get; set; }
}