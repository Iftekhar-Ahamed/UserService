using Application.Infrastructure.Services;
using Application.Interfaces;
using Domain.Interfaces;
using Domain.Models;
using FluentValidation;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using User.Core.DTOs.UserDTOs;
using User.Core.Interfaces;
using User.Core.Validations.UserValidations;

namespace UserService.ServiceConfiguration;

public static class ConfigureServices
{
    public static IServiceCollection ConfigureAllServices(this IServiceCollection services,IConfiguration configuration)
    {
        services.AddSingleton<IAppConfigService, AppConfigService>();
        
        services.AddValidatorsFromAssemblyContaining<CreateNewUserRequestValidator>();
        services.AddValidatorsFromAssemblyContaining<LogInRequestDto>();
        
        services.AddDbContext<ChatDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IUserInfoService, UserInfoService>();
        services.AddScoped<IUserInfoRepository, UserInfoRepository>();
        
        services.AddScoped<IAuthService, AuthService>();

        return services;
    }
}