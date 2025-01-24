using System.Security.Claims;

namespace Application.Core.Extensions.CommonExtensions;

public static class HttpContextExtension
{
    public static int GetUserId( this ClaimsPrincipal user)
    {
        string? userIdClaim = user.FindFirst(ClaimTypes.Name)?.Value;
    
        if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
        {
            throw new UnauthorizedAccessException("Invalid Token");
        }
        
        return userId;
    }
}