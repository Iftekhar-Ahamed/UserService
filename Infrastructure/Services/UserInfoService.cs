using Application.DTOs.APIRequestResponseDTOs;
using Application.DTOs.UserDTOs;
using Application.Interfaces;
using Domain.Interfaces;
using Domain.Models;

namespace Infrastructure.Services;

public class UserInfoService (IUserInfoRepository userInfoRepository): IUserInfoService
{
    public async Task<ApiResponseDto<bool>> CreateNewUserAsync(CreateNewUserRequestDto userInfo)
    {
        var response = new ApiResponseDto<bool>{ Message = "Something went wrong",ShowMessage = true };

        if (await userInfoRepository.IsDuplicateUserAsync(userInfo.Email))
        {
            response.Message = "User already exists!";
            response.Success = false;
        }
        else
        {
            TblUserInformation user = new TblUserInformation
            {
                Title = userInfo.Name.Title,
                FirstName = userInfo.Name.FirstName,
                MiddleName = userInfo.Name.MiddleName,
                LastName = userInfo.Name.LastName,
                Dob = userInfo.Dob,
                Email = userInfo.Email,
                ContactNumberCountryCode = userInfo.ContactNumberCountryCode,
                ContactNumber = userInfo.ContactNumber,
                IsActive = true,
                CreationDateTime = DateTime.Now
            };

            if (await userInfoRepository.AddUserAsync(user))
            {
                response.Message = "Successfully User Created!";
                response.Success = true;
            }
            else
            {
                throw new ApplicationException("Failed to save user!");
            }
        }
        
        return response;
    }
}