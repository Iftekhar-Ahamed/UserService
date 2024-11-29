using Application.DTOs.UserDTOs;
using FluentValidation;

namespace Application.Validations.UserValidations;

public class LogInRequestValidator : AbstractValidator<LogInRequestDto>
{
    public LogInRequestValidator()
    {
        RuleFor( x => x.UserName)
            .NotEmpty().WithMessage("Username is required")
            .EmailAddress().WithMessage("Email is not valid");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required");
    }
}