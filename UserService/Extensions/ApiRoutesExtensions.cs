using UserService.ApiEndPoints;

namespace UserService.Extensions;

public static class ApiRoutesExtensions
{
    public static void RegisterApiRoutes(this IEndpointRouteBuilder app)
    {
        app.MapGroup("api/User")
            .MapUserApis()
            .WithTags("User API");

        app.MapGroup("api/Auth")
            .MapAuthApis()
            .WithTags("Auth API");
    }
}