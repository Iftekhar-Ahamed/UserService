namespace Domain.Models;

public partial class TblUserInformation
{
    public long UserId { get; set; }

    public string Title { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string? MiddleName { get; set; }

    public string LastName { get; set; } = null!;

    public DateOnly Dob { get; set; }

    public string Email { get; set; } = null!;

    public string ContactNumberCountryCode { get; set; } = null!;

    public string ContactNumber { get; set; } = null!;

    public bool IsActive { get; set; }

    public DateTime CreationDateTime { get; set; }

    public DateTime LastModifiedDateTime { get; set; }
}
