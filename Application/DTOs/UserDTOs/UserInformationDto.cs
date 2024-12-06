namespace Application.DTOs.UserDTOs;

public class UserInformationDto
{
    public int UserId { get; set; }
    public  NameElementDto? Name { get; set; }
    public  DateOnly? Dob { get; set; }
    public  string? Email { get; set; }
    public  string? ContactNumberCountryCode { get; set; }
    public  string? ContactNumber { get; set; }
    public string? Password { get; set; }
    public bool? IsActive { get; set; }
}