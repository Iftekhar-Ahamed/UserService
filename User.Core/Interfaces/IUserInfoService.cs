using Application.DTOs.APIRequestResponseDTOs;
using User.Core.DTOs.UserDTOs;

namespace User.Core.Interfaces;

public interface IUserInfoService
{
    Task<ApiResponseDto<bool>> CreateNewUserAsync(CreateNewUserRequestDto userInfo);
    Task<ApiResponseDto<bool>> UpdateUserAsync(UpdateUserRequestDto updateUserInfo);
    Task<ApiResponseDto<GetUserInformationByIdResponseDto>> GetUserInformationByIdAsync(int userId);
}