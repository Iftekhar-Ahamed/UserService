namespace User.Core.DTOs.UserDTOs;

public class NameElementDto
{
    public required string  Title  { get; set; }
    public required string FirstName  { get; set; }
    public string?  MiddleName  { get; set; }
    public required string  LastName  { get; set; }
}