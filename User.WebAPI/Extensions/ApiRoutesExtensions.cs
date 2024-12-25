using UserService.ApiEndPoints;

namespace UserService.Extensions;

public static class ApiRoutesExtensions
{
    public static void RegisterApiRoutes(this IEndpointRouteBuilder app)
    {
        app.MapGroup("api/User.Infrastructure")
            .MapUserApis()
            .RequireAuthorization()
            .WithTags("User.Infrastructure API");

        app.MapGroup("api/Auth")
            .MapAuthApis()
            .WithTags("Auth API");
    }
}