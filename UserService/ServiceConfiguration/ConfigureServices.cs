using Application.Interfaces;
using Application.Validations.UserValidations;
using Domain.Interfaces;
using FluentValidation;
using Infrastructure.Repositories;
using Infrastructure.Services;
using UserService.ActionFilters;

namespace UserService.ServiceConfiguration;

public static class ConfigureServices
{
    public static IServiceCollection ConfigureAllServices(this IServiceCollection services)
    {
        services.AddControllers(options => { options.Filters.Add<ValidateModelAttribute>(); });
        services.AddValidatorsFromAssemblyContaining<CreateNewUserRequestValidator>();

        services.AddScoped<IUserInfoService, UserInfoService>();
        services.AddScoped<IUserInfoRepository, UserInfoRepository>();

        return services;
    }
}