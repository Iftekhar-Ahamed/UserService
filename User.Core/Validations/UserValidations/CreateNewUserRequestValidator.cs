using User.Core.DTOs.UserDTOs;
using FluentValidation;

namespace User.Core.Validations.UserValidations;

public class CreateNewUserRequestValidator : AbstractValidator<CreateNewUserRequestDto>
{
    public CreateNewUserRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotNull().WithMessage("Name is required.");

        RuleFor(x => x.Dob)
            .LessThan(DateOnly.FromDateTime(DateTime.Now)).WithMessage("Date of birth must be in the past.")
            .GreaterThan(DateOnly.FromDateTime(DateTime.Now.AddYears(-120))).WithMessage("Date of birth must be within a realistic range.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Email is not valid.");

        RuleFor(x => x.ContactNumberCountryCode)
            .Matches(@"^\+\d+$")
            .When(x => !string.IsNullOrEmpty(x.ContactNumberCountryCode)) 
            .WithMessage("Country code must be in the format '+<digits>'.");

        RuleFor(x => x.ContactNumber)
            .Matches(@"^\d+$")
            .When(x => !string.IsNullOrEmpty(x.ContactNumber))
            .WithMessage("Contact number must only contain digits.")
            .MinimumLength(7).WithMessage("Contact number must be at least 7 digits.")
            .MaximumLength(15).WithMessage("Contact number must not exceed 15 digits.");

        
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters")
            .MaximumLength(30).WithMessage("Password must be between 8 and 30 characters")
            .Matches(@"[!@#$%^&*(),.?""':{}|<>]").WithMessage("Password must contain at least one special character")
            .Matches(@"[0123456789]").WithMessage("Password must contain at least one number");
    }
}