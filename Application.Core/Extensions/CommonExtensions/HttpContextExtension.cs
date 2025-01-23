using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Application.Core.Extensions.CommonExtensions;

public static class HttpContextExtension
{
    public static int GetUserId(this HttpContext httpContext)
    {
        string? userIdClaim = httpContext.User.FindFirst(ClaimTypes.Name)?.Value;
    
        if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
        {
            throw new UnauthorizedAccessException("Invalid Token");
        }
        
        return userId;
    }
}