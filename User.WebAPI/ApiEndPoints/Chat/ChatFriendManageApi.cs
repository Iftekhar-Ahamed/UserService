using Application.Extensions.DtoExtensions;
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
}