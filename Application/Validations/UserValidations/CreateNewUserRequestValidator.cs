using Application.DTOs.UserDTOs;
using FluentValidation;

namespace Application.Validations.UserValidations;

public class CreateNewUserRequestValidator : AbstractValidator<CreateNewUserRequestDto>
{
    public CreateNewUserRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotNull().WithMessage("Name is required.");

        RuleFor(x => x.Dob)
            .LessThan(DateTime.Now).WithMessage("Date of birth must be in the past.")
            .GreaterThan(DateTime.Now.AddYears(-120)).WithMessage("Date of birth must be within a realistic range.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Email is not valid.");

        RuleFor(x => x.ContactNumberCountryCode)
            .NotEmpty().WithMessage("Country code is required.")
            .Matches(@"^\+\d+$").WithMessage("Country code must be in the format '+<digits>'.");

        RuleFor(x => x.ContactNumber)
            .NotEmpty().WithMessage("Contact number is required.")
            .Matches(@"^\d+$").WithMessage("Contact number must only contain digits.")
            .MinimumLength(7).WithMessage("Contact number must be at least 7 digits.")
            .MaximumLength(15).WithMessage("Contact number must not exceed 15 digits.");
    }
}