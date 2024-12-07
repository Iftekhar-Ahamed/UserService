using Application.DTOs.APIRequestResponseDTOs;
using Application.DTOs.UserDTOs;
using Application.Extensions.DtoExtensions;
using Application.Helpers.EncryptionDecryptionHelper;
using Application.Interfaces;
using Domain.Interfaces;
using Domain.Models;

namespace Infrastructure.Services;

public class AuthService(IUserInfoRepository userInfoRepository) : IAuthService
{
    public async Task<ApiResponseDto<LogInResponseDto>> LoginAsync(LogInRequestDto request)
    {
        var apiResponse = new ApiResponseDto<LogInResponseDto>();
        
        TblUserInformation? userInfo = await userInfoRepository.GetUserByEmailAsync(email: request.UserName);

        if (userInfo == null)
        {
            apiResponse.Message = "Username is invalid";
            apiResponse.Success = false;
        }
        else
        {
            if (string.IsNullOrEmpty(userInfo.Password))
            {
                apiResponse.Failed("Please set your password before trying to login",true);
                return apiResponse;
            }
            
            if (!OneWayEncryptionHelper.IsValidPassword(request.Password, userInfo.Password))
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
                apiResponse.Success("Welcome User");
            }
        }
        
        return apiResponse;
    }
}