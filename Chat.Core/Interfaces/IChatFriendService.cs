using Application.DTOs.APIRequestResponseDTOs;
using Chat.Core.DTOs.UserChatFriendDTOs;

namespace Chat.Core.Interfaces;

public interface IChatFriendService
{
    Task<ApiResponseDto<string>>SentChatFriendRequest(AddNewChatFriendRequestDto addFriendRequestDto);
    Task<ApiResponseDto<List<SearchChatUserResultResponseDto>>> SearchChatUser(string searchTerm, long userId);
}