using Application.Core.DTOs.APIRequestResponseDTOs;
using Application.Core.DTOs.PaginationDTOs;
using Chat.Core.DTOs.UserChatFriendDTOs;
using Domain.Models;

namespace Chat.Core.Interfaces;

public interface IChatFriendService
{
    Task<ApiResponseDto<string>>SentChatFriendRequest(AddNewChatFriendRequestDto addFriendRequestDto);
    Task<ApiResponseDto<string>> CancelChatFriendRequest(CancelFriendRequestDto cancelFriendRequestDto);
    Task<ApiResponseDto<List<SearchChatUserResultResponseDto>>> SearchChatUser(
        string searchTerm,
        long userId,
        int pageNumber,
        int pageSize);
    //Task<ApiResponseDto<List<FriendRequestDetailsDto>>> GetFriendRequests(PaginationDto<long> request);
    Task<ApiResponseDto<List<FriendRequestDetailsDto>>>GetFriendRequests(PaginationDto<long> request);
}