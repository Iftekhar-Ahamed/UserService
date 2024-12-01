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
        var response = new ApiResponseDto<bool>{ Message = "Something went wrong",ShowMessage = true };

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

    public Task<ApiResponseDto<bool>> UpdateUserAsync(UpdateUserRequestDto updateUserInfo)
    {
        throw new NotImplementedException();
    }
}