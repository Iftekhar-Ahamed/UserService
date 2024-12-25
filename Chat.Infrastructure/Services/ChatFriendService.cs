using Application.DTOs.APIRequestResponseDTOs;
using Chat.Core.DTOs.UserChatFriendDTOs;
using Chat.Core.Interfaces;

namespace Chat.Infrastructure.Services;

public class ChatFriendService : IChatFriendService
{
    public Task<ApiResponseDto<string>> AddNewChatFriendAsync(AddNewChatFriendRequestDto addFriendRequestDto)
    {
        throw new NotImplementedException();
    }
}