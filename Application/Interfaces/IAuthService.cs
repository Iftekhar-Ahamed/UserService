using Application.DTOs.APIRequestResponseDTOs;
using Application.DTOs.UserDTOs;

namespace Application.Interfaces;

public interface IAuthService
{
    Task<ApiResponseDto<LogInResponseDto>> LoginAsync(LogInRequestDto request);
    Task<string> CreateAccessToken(string userId, List<string> userRoles);
    Task<string> CreateRefreshToken(string userId, List<string> userRoles);
}