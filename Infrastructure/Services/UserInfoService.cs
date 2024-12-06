using Application.DTOs.APIRequestResponseDTOs;
using Application.DTOs.UserDTOs;
using Application.Extensions.DtoExtensions;
using Application.Interfaces;
using Domain.Interfaces;
using Domain.Models;

namespace Infrastructure.Services;

public class UserInfoService (IUserInfoRepository userInfoRepository): IUserInfoService
{
    public async Task<ApiResponseDto<bool>> CreateNewUserAsync(CreateNewUserRequestDto userInfo)
    {
        var response = new ApiResponseDto<bool>();

        if (await userInfoRepository.IsDuplicateUserAsync(email: userInfo.Email))
        {
            response.Failed("User already exists with provided email",true);
        }
        else
        {
            TblUserInformation newUser = new TblUserInformation
            {
                Title = userInfo.Name.Title,
                FirstName = userInfo.Name.FirstName,
                MiddleName = userInfo.Name.MiddleName,
                LastName = userInfo.Name.LastName,
                Dob = userInfo.Dob,
                Email = userInfo.Email,
                Password = userInfo.Password,
                ContactNumberCountryCode = userInfo.ContactNumberCountryCode,
                ContactNumber = userInfo.ContactNumber,
                IsActive = true,
                CreationDateTime = DateTime.Now
            };

            if (await userInfoRepository.AddUserAsync(user: newUser))
            {
                response.Success("Successfully User Created!",true);
            }
            else
            {
                response.Failed("Please Try Again!",true);
            }
        }
        
        return response;
    }

    public async Task<ApiResponseDto<bool>> UpdateUserAsync(UpdateUserRequestDto updateUserInfo)
    {
        var response = new ApiResponseDto<bool>();
        
        var exitingInformationUser = await userInfoRepository.GetUserByIdAsync(updateUserInfo.UserId);

        if (exitingInformationUser == null)
        {
            response.Data = false;
            response.Failed("User does not exist",true);
            
            return response;
        }

        var updateUserModel = new TblUserInformation
        {
            Title = updateUserInfo.Name?.Title ?? exitingInformationUser.Title,
            FirstName = updateUserInfo.Name?.FirstName ?? exitingInformationUser.FirstName,
            MiddleName = updateUserInfo.Name?.MiddleName ?? exitingInformationUser.MiddleName,
            LastName = updateUserInfo.Name?.LastName ?? exitingInformationUser.LastName,
            Dob = updateUserInfo.Dob ?? exitingInformationUser.Dob,
            Email = updateUserInfo.Email ?? exitingInformationUser.Email,
            ContactNumberCountryCode = updateUserInfo.ContactNumberCountryCode ?? exitingInformationUser.ContactNumberCountryCode,
            ContactNumber = updateUserInfo.ContactNumber ?? exitingInformationUser.ContactNumber,
            Password = updateUserInfo.Password ?? exitingInformationUser.Password,
            IsActive = updateUserInfo.IsActive ?? exitingInformationUser.IsActive,
            CreationDateTime = exitingInformationUser.CreationDateTime,
            LastModifiedDateTime = DateTime.Now
        };

        if (await userInfoRepository.UpdateUserAsync(updateUserModel))
        {
            response.Success("Successfully Updated User!", true);
        }
        else
        {
            response.Failed("Please Try Again!",true);
        }
        
        return response;
    }

    public async Task<ApiResponseDto<UserInformationDto>> GetUserInformationByIdAsync(int userId)
    {
        var response = new ApiResponseDto<UserInformationDto>();
        
        var userInformation = await userInfoRepository.GetUserByIdAsync(userId);

        if (userInformation == null)
        {
            response.Data = null;
            response.Failed("User does not exist",true);
        }
        else
        {
            response.Success("Successfully Retrieved User!",true);
            response.Data = new UserInformationDto
            {
                
            };
            
        }
        
        return response;
    }
}