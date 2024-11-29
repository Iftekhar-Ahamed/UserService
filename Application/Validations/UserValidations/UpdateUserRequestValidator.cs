using Application.DTOs.UserDTOs;
using FluentValidation;

namespace Application.Validations.UserValidations;

public class UpdateUserRequestValidator: AbstractValidator<UpdateUserRequestDto>
{
    public UpdateUserRequestValidator()
    {
        RuleFor(x=> x.UserId)
            .NotNull().WithMessage("User Id cannot be null")
            .GreaterThan(0).WithMessage("User Id must be greater than 0");
        
        RuleFor(x => x.Name)
            .NotNull().WithMessage("Name is required.")
            .When( x => x.Name is not null);

        RuleFor(x => x.Dob)
            .LessThan(DateOnly.FromDateTime(DateTime.Now)).WithMessage("Date of birth must be in the past.")
            .GreaterThan(DateOnly.FromDateTime(DateTime.Now.AddYears(-120))).WithMessage("Date of birth must be within a realistic range.")
            .When(x => x.Dob is not null);

        RuleFor(x => x.Email)
            .EmailAddress().WithMessage("Email is not valid.")
            .When(x => !string.IsNullOrEmpty(x.Email));

        RuleFor(x => x.ContactNumberCountryCode)
            .Matches(@"^\+\d+$").WithMessage("Country code must be in the format '+<digits>'.")
            .When(x => !string.IsNullOrEmpty(x.ContactNumberCountryCode));

        RuleFor(x => x.ContactNumber)
            .Matches(@"^\d+$").WithMessage("Contact number must only contain digits.")
            .MinimumLength(7).WithMessage("Contact number must be at least 7 digits.")
            .MaximumLength(15).WithMessage("Contact number must not exceed 15 digits.")
            .When(x => !string.IsNullOrEmpty(x.ContactNumber));
        
        RuleFor(x => x.Password)
            .MinimumLength(8).WithMessage("Password must be at least 8 characters")
            .MaximumLength(100).WithMessage("Password must be between 8 and 100 characters")
            .Matches(@"[!@#$%^&*(),.?""':{}|<>]").WithMessage("Password must contain at least one special character")
            .Matches(@"[0123456789]").WithMessage("Password must contain at least one number")
            .When( x => !string.IsNullOrEmpty(x.Password));
    }
}