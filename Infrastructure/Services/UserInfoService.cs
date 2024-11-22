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
        var response = new ApiResponseDto<bool>{ Message = "Something went wrong" };
        
        TblUserInformation user = new TblUserInformation
        {
            UserId = 1,
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
            
        }

        return response;
    }
}