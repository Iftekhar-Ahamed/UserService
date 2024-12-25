using User.Core.DTOs.UserDTOs;
using User.Core.Interfaces;
using UserService.EndPointFilters;

namespace UserService.ApiEndPoints.User;

public static class AuthApi
{
    public static RouteGroupBuilder MapAuthApis(this RouteGroupBuilder groups)
    {
        groups.MapPost("/LogIn",Login)
            .AddEndpointFilter<ValidateModelFilter<LogInRequestDto>>()
            .Accepts<LogInRequestDto>("application/json");
        
        return groups;
    }

    private static async Task<IResult> Login(IAuthService authService, LogInRequestDto logInRequestRequest)
    {
        var res = await authService.LoginAsync(request: logInRequestRequest);

        if (!res.Success)
        {
            return TypedResults.BadRequest(res);
        }
        
        return TypedResults.Ok(res);
    }
}