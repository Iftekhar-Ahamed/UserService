namespace Domain.Models;

public partial class Tbluserinformation
{
    public long Userid { get; set; }

    public string Title { get; set; } = null!;

    public string Firstname { get; set; } = null!;

    public string? Middlename { get; set; }

    public string Lastname { get; set; } = null!;

    public DateOnly Dob { get; set; }

    public string Email { get; set; } = null!;

    public string Contactnumbercountrycode { get; set; } = null!;

    public string Contactnumber { get; set; } = null!;

    public bool Isactive { get; set; }

    public DateOnly Creationdatetime { get; set; }

    public DateOnly Lastmodifieddate { get; set; }
}
