using Application.DTOs.UserDTOs;
using Application.Interfaces;
using UserService.EndPontFilters;

namespace UserService.ApiEndPoints;

public static class UserApi
{
    const string ContentType = "application/json";
    
    public static RouteGroupBuilder MapUserApis(this RouteGroupBuilder groups)
    {
        #region Get Requests

        groups.MapGet("GetUserById/UserId={userId}", GetUserById);

        #endregion
        
        #region Post Requests

        groups.MapPost("/CreateUser", CreateNewUser)
            .AddEndpointFilter<ValidateModelFilter<CreateNewUserRequestDto>>()
            .Accepts<CreateNewUserRequestDto>(ContentType);
        groups.MapPost("/UpdateUser", UpdateUser)
            .AddEndpointFilter<ValidateModelFilter<UpdateUserRequestDto>>()
            .Accepts<UpdateUserRequestDto>(ContentType);

        #endregion
        
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