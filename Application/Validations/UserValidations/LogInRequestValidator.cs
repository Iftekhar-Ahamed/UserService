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
            .NotEmpty().WithMessage("Password is required")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters")
            .MaximumLength(100).WithMessage("Password must be between 8 and 100 characters")
            .Matches(@"[!@#$%^&*(),.?""':{}|<>]").WithMessage("Password must contain at least one special character")
            .Matches(@"[0123456789]").WithMessage("Password must contain at least one number");
    }
}