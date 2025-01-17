using System.ComponentModel.DataAnnotations;
using Application.Extensions.CommonExtensions;
using Chat.Core.DTOs.UserChatFriendDTOs;
using Chat.Core.Interfaces;
using UserService.EndPointFilters;

namespace UserService.ApiEndPoints.Chat;

public static class ChatFriendManageApi
{
    public static RouteGroupBuilder MapChatFriendMangeApis(this RouteGroupBuilder groups)
    {
        groups.MapPost("/SentChatFriendRequest",SentChatFriendRequest)
            .AddEndpointFilter<ValidateModelFilter<AddNewChatFriendRequestDto>>()
            .Accepts<AddNewChatFriendRequestDto>("application/json");
        groups.MapPost("/CancelChatFriendRequest",CancelChatFriendRequest)
            .AddEndpointFilter<ValidateModelFilter<CancelFriendRequestDto>>()
            .Accepts<CancelFriendRequestDto>("application/json");
        
        groups.MapGet("SearchChatUser/SearchTerm={searchTerm}&PageNo={pageNo}&PageSize={pageSize}", SearchChatUser);
        return groups;
    }

    private  static async Task<IResult> SentChatFriendRequest(IChatFriendService chatFriendService, AddNewChatFriendRequestDto request)
    {
        var result = await chatFriendService.SentChatFriendRequest(request);

        if (result.Success)
        {
            return TypedResults.Ok(result);
        }
        
        return TypedResults.BadRequest(result);
    }
    
    private  static async Task<IResult> CancelChatFriendRequest(IChatFriendService chatFriendService, CancelFriendRequestDto request)
    {
        var result = await chatFriendService.CancelChatFriendRequest(request);

        if (result.Success)
        {
            return TypedResults.Ok(result);
        }
        
        return TypedResults.BadRequest(result);
    }
    
    private static async Task<IResult> SearchChatUser(string searchTerm,[Required]int pageNo,[Required]int pageSize,IChatFriendService chatFriendService,HttpContext httpContext)
    {
        var response = await chatFriendService.SearchChatUser(searchTerm,httpContext.GetUserId(),pageNo,pageSize);
            
        if (response.Success)
        {
            return TypedResults.Ok(response);
        }
        
        return TypedResults.BadRequest(response);
    }
}