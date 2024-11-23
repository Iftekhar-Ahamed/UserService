using Application.DTOs.UserDTOs;
using Application.Interfaces;
using Application.Validations.UserValidations;
using Domain.Interfaces;
using Domain.Models;
using FluentValidation;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using UserService.ActionFilters;

namespace UserService.ServiceConfiguration;

public static class ConfigureServices
{
    public static IServiceCollection ConfigureAllServices(this IServiceCollection services,IConfiguration configuration)
    {
        services.AddControllers(options => { options.Filters.Add<ValidateModelAttribute>(); });
        services.AddValidatorsFromAssemblyContaining<CreateNewUserRequestValidator>();
        services.AddValidatorsFromAssemblyContaining<LogInRequestDto>();
        
        services.AddDbContext<ChatDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IUserInfoService, UserInfoService>();
        services.AddScoped<IUserInfoRepository, UserInfoRepository>();

        return services;
    }
}