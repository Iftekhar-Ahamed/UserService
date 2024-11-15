using Application.Validations.UserValidations;
using FluentValidation;
using FluentValidation.AspNetCore;

namespace UserService.Middlewares;

public static class ConfigureServicesMiddleware
{
    public static IServiceCollection ConfigureServices( this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssemblyContaining<CreateNewUserRequestValidator>();
        
        return services;
    }
}