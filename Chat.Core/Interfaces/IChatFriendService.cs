using Application.DTOs.APIRequestResponseDTOs;
using Chat.Core.DTOs.UserChatFriendDTOs;

namespace Chat.Core.Interfaces;

public interface IChatFriendService
{
    Task<ApiResponseDto<string>>SentChatFriendRequest(AddNewChatFriendRequestDto addFriendRequestDto);
}