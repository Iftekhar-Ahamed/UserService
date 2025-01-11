using UserService.ApiEndPoints.Chat;
using UserService.ApiEndPoints.User;

namespace UserService.Extensions;

public static class ApiRoutesExtensions
{
    public static void RegisterApiRoutes(this IEndpointRouteBuilder app)
    {
        app.MapGroup("api/User")
            .MapUserPublicApis()
            .WithTags("User Public API");
        
        app.MapGroup("api/User")
            .MapUserApis()
            .RequireAuthorization()
            .WithTags("User API");

        app.MapGroup("api/Auth")
            .MapAuthApis()
            .WithTags("Auth API");
        
        app.MapGroup("api/Chat")
            .MapChatFriendMangeApis()
            .WithTags("Chat Friend Manage API");

    }
}