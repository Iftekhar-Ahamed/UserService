using Chat.Core.DTOs.UserChatFriendDTOs;
using Chat.Core.Interfaces;
using UserService.EndPointFilters;

namespace UserService.ApiEndPoints.Chat;

public static class ChatFriendManageApi
{
    public static RouteGroupBuilder MapChatFriendMangeApis(this RouteGroupBuilder groups)
    {
        groups.MapPost("/AddNewFriend",AddNewFriend)
            .AddEndpointFilter<ValidateModelFilter<AddNewChatFriendRequestDto>>()
            .Accepts<AddNewChatFriendRequestDto>("application/json");
        
        return groups;
    }

    private static Task<IResult> AddNewFriend(IChatFriendService chatFriendService, AddNewChatFriendRequestDto request)
    {
        throw new NotImplementedException();
    }
}