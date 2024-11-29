using Application.DTOs.APIRequestResponseDTOs;
using Application.DTOs.UserDTOs;

namespace Application.Interfaces;

public interface IAuthService
{
    Task<ApiResponseDto<LogInResponseDto>> LoginAsync(LogInRequestDto request);
}