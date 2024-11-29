using Application.DTOs.APIRequestResponseDTOs;
using Application.DTOs.UserDTOs;
using Application.Interfaces;
using Domain.Interfaces;
using Domain.Models;

namespace Infrastructure.Services;

public class AuthService(IUserInfoRepository userInfoRepository) : IAuthService
{
    public async Task<ApiResponseDto<LogInResponseDto>> LoginAsync(LogInRequestDto request)
    {
        var apiResponse = new ApiResponseDto<LogInResponseDto>
        {
            Message = string.Empty,
            Success = false,
        };
        
        TblUserInformation? userInfo = await userInfoRepository.GetUserByEmailAsync(email: request.UserName);

        if (userInfo == null)
        {
            apiResponse.Message = "Username is invalid";
            apiResponse.Success = false;
        }
        else
        {
            if (userInfo.Password != request.Password)
            {
                apiResponse.Message = "Wrong Password";
                apiResponse.Success = false;
            }
            else
            {
                apiResponse.Data = new LogInResponseDto
                {
                    AccessToken = string.Empty,
                    RefreshToken = string.Empty,
                };
                apiResponse.Success = true;
            }
        }
        
        return apiResponse;
    }
}