using Application.DTOs.APIRequestResponseDTOs;
using Application.DTOs.UserDTOs;

namespace Application.Interfaces;

public interface IUserInfoService
{
    Task<ApiResponseDto<bool>> CreateNewUserAsync(CreateNewUserRequestDto userInfo);
    Task<ApiResponseDto<bool>> UpdateUserAsync(UpdateUserRequestDto updateUserInfo);
    Task<ApiResponseDto<GetUserInformationByIdResponseDto>> GetUserInformationByIdAsync(int userId);
}