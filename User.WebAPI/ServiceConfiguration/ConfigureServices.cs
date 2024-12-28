using Application.Infrastructure.Services;
using Application.Interfaces;
using Chat.Core.Interfaces;
using Chat.Core.Validations.ChatFriendManageValidations;
using Chat.Infrastructure.Repositories;
using Chat.Infrastructure.Services;
using Domain.Interfaces.ChatRepositories;
using Domain.Interfaces.UserRepositories;
using Domain.Models;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using User.Core.DTOs.UserDTOs;
using User.Core.Interfaces;
using User.Core.Validations.UserValidations;
using User.Infrastructure.Repositories;
using User.Infrastructure.Services;

namespace UserService.ServiceConfiguration;

public static class ConfigureServices
{
    public static IServiceCollection ConfigureAllServices(this IServiceCollection services,IConfiguration configuration)
    {
        services.AddSingleton<IAppConfigService, AppConfigService>();
        
        services.AddValidatorsFromAssemblyContaining<CreateNewUserRequestValidator>();
        services.AddValidatorsFromAssemblyContaining<LogInRequestDto>();
        services.AddValidatorsFromAssemblyContaining<AddNewChatFriendRequestValidator>();
        
        services.AddDbContext<ChatDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IUserInfoService, UserInfoService>();
        services.AddScoped<IUserInfoRepository, UserInfoRepository>();
        
        services.AddScoped<IAuthService, AuthService>();

        services.AddScoped<IChatFriendService, ChatFriendService>();
        services.AddScoped<IChatFriendRepository, ChatFriendRepository>();

        return services;
    }
}