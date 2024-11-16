using Application.Validations.UserValidations;
using FluentValidation;
using FluentValidation.AspNetCore;
using UserService.ActionFilters;

namespace UserService.ServiceConfiguration;

public static class ConfigureServices
{
    public static IServiceCollection ConfigureAllServices( this IServiceCollection services)
    {
        services.AddControllers(options =>
        {
            options.Filters.Add<ValidateModelAttribute>();
        });
        services.AddValidatorsFromAssemblyContaining<CreateNewUserRequestValidator>();
        
        return services;
    }
}