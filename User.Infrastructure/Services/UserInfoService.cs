using Application.DTOs.APIRequestResponseDTOs;
using Application.Extensions.DtoExtensions;
using Application.Helpers.EncryptionDecryptionHelper;
using Domain.Interfaces.UserRepositories;
using Domain.Models;
using User.Core.DTOs.UserDTOs;
using User.Core.Interfaces;

namespace User.Infrastructure.Services;

public class UserInfoService (IUserInfoRepository userInfoRepository): IUserInfoService
{
    public async Task<ApiResponseDto<bool>> CreateNewUserAsync(CreateNewUserRequestDto userInfo)
    {
        var response = new ApiResponseDto<bool>();

        if (await userInfoRepository.IsDuplicateUserAsync(email: userInfo.Email))
        {
            response.Failed("User.Infrastructure already exists with provided email",true);
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
                Password = OneWayEncryptionHelper.EncryptPassword(userInfo.Password),
                ContactNumberCountryCode = userInfo.ContactNumberCountryCode,
                ContactNumber = userInfo.ContactNumber,
                IsActive = true,
                CreationDateTime = DateTime.Now
            };

            if (await userInfoRepository.AddUserAsync(user: newUser))
            {
                response.Success("Successfully User.Infrastructure Created!",true);
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
            response.Failed("User.Infrastructure does not exist",true);
            
            return response;
        }

        exitingInformationUser.Title = updateUserInfo.Name?.Title ?? exitingInformationUser.Title;
        exitingInformationUser.FirstName = updateUserInfo.Name?.FirstName ?? exitingInformationUser.FirstName;
        exitingInformationUser.MiddleName = updateUserInfo.Name?.MiddleName ?? exitingInformationUser.MiddleName;
        exitingInformationUser.LastName = updateUserInfo.Name?.LastName ?? exitingInformationUser.LastName;
        exitingInformationUser.Dob = updateUserInfo.Dob ?? exitingInformationUser.Dob;
        exitingInformationUser.Email = updateUserInfo.Email ?? exitingInformationUser.Email;
        exitingInformationUser.ContactNumberCountryCode = updateUserInfo.ContactNumberCountryCode ??
                                                          exitingInformationUser.ContactNumberCountryCode;
        exitingInformationUser.ContactNumber = updateUserInfo.ContactNumber ?? exitingInformationUser.ContactNumber;
        exitingInformationUser.Password = updateUserInfo.Password == null
            ? exitingInformationUser.Password
            : OneWayEncryptionHelper.EncryptPassword(updateUserInfo.Password);
        exitingInformationUser.IsActive = updateUserInfo.IsActive ?? exitingInformationUser.IsActive;
        exitingInformationUser.LastModifiedDateTime = DateTime.Now;

        if (await userInfoRepository.UpdateUserAsync(exitingInformationUser))
        {
            response.Success("Successfully Updated User.Infrastructure!", true);
        }
        else
        {
            response.Failed("Please Try Again!",true);
        }
        
        return response;
    }

    public async Task<ApiResponseDto<GetUserInformationByIdResponseDto>> GetUserInformationByIdAsync(int userId)
    {
        var response = new ApiResponseDto<GetUserInformationByIdResponseDto>();
        
        var userInformation = await userInfoRepository.GetUserByIdAsync(userId);

        if (userInformation == null)
        {
            response.Data = null;
            response.Failed("User.Infrastructure does not exist",true);
        }
        else
        {
            response.Success("Successfully Retrieved User.Infrastructure!",true);
            response.Data = new GetUserInformationByIdResponseDto
            {
                UserId = userInformation.UserId,
                Name = new NameElementDto
                {
                    Title = userInformation.Title,
                    FirstName = userInformation.FirstName,
                    MiddleName = userInformation.MiddleName,
                    LastName = userInformation.LastName,
                },
                Dob = userInformation.Dob,
                Email = userInformation.Email,
                ContactNumberCountryCode = userInformation.ContactNumberCountryCode,
                ContactNumber = userInformation.ContactNumber,
                IsActive = userInformation.IsActive
            };
            
        }
        
        return response;
    }
}