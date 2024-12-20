using Application.DTOs.UserDTOs;
using Application.Interfaces;

namespace UserService.ApiEndPoints;

public static class UserApi
{
    const string ContentType = "application/json";
    public static RouteGroupBuilder MapUserApis(this RouteGroupBuilder groups)
    {
        groups.MapPost("/CreateUser", CreateNewUser).Accepts<CreateNewUserRequestDto>(ContentType);
        groups.MapPost("/UpdateUser", UpdateUser).Accepts<UpdateUserRequestDto>(ContentType);
        groups.MapGet("GetUserById/UserId={userId}", GetUserById);
        
        return groups;
    }

    private static async Task<IResult> CreateNewUser(IUserInfoService userInfoService,CreateNewUserRequestDto createNewUserRequest)
    {
        var response = await userInfoService.CreateNewUserAsync(userInfo: createNewUserRequest);
            
        if (response.Success)
        {
            return TypedResults.Ok(response);
        }
            
        return TypedResults.BadRequest(response);
    }
    
    private static async Task<IResult> UpdateUser(IUserInfoService userInfoService,UpdateUserRequestDto updateUserRequest)
    {
        var response = await userInfoService.UpdateUserAsync(updateUserInfo: updateUserRequest);
            
        if (response.Success)
        {
            return TypedResults.Ok(response);
        }
            
        return TypedResults.BadRequest(response);
    }
    
    private static async Task<IResult> GetUserById(IUserInfoService userInfoService,int userId)
    {
        var response = await userInfoService.GetUserInformationByIdAsync(userId: userId);
            
        if (response.Success)
        {
            return TypedResults.Ok(response);
        }
        
        return TypedResults.BadRequest(response);
    }
}