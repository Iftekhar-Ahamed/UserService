using Application.Core.DTOs.APIRequestResponseDTOs;
using User.Core.DTOs.UserDTOs;

namespace User.Core.Interfaces;

public interface IAuthService
{
    Task<ApiResponseDto<LogInResponseDto>> LoginAsync(LogInRequestDto request);
    Task<string> CreateAccessToken(string userId, List<string> userRoles);
    Task<string> CreateRefreshToken(string userId, List<string> userRoles);
}