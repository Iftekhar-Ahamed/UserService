using Application.DTOs.APIRequestResponseDTOs;
using Application.Extensions.DtoExtensions;
using Chat.Core.DTOs.UserChatFriendDTOs;
using Chat.Core.Interfaces;
using Domain.Enums;
using Domain.Interfaces.ChatRepositories;

namespace Chat.Infrastructure.Services;

public class ChatFriendService(IChatFriendRepository chatFriendRepository) : IChatFriendService
{
    public async Task<ApiResponseDto<string>> SentChatFriendRequest(AddNewChatFriendRequestDto addFriendRequestDto)
    {
        var response = new ApiResponseDto<string>();
        
        var friendShipHistory = await chatFriendRepository.GetFriendshipAsync(addFriendRequestDto.SelfUserId,
            addFriendRequestDto.RequestedUserId);
        
        if (friendShipHistory != null)
        {
            if ((FriendshipStatus)friendShipHistory.ApproveStatus == FriendshipStatus.Accepted)
            {
                response.Failed("User is already your friend",true);
            }
            else if (friendShipHistory.UserId == addFriendRequestDto.SelfUserId)
            {
                response.Failed(
                    (FriendshipStatus)friendShipHistory.ApproveStatus == FriendshipStatus.Blocked
                        ? "Please unblock user first"
                        : "Request already sent", true);
            }
            else
            {
                response.Failed(
                    (FriendshipStatus)friendShipHistory.ApproveStatus == FriendshipStatus.Blocked
                        ? "User blocked you"
                        : "Already user sent you a request. Please check friend request", true);
            }
            return response;
        }

        if (await chatFriendRepository.AddChatFriendRequestAsync(addFriendRequestDto.SelfUserId,
                addFriendRequestDto.RequestedUserId))
        {
            response.Success("Request send",true);
        }
        else
        {
            response.Failed("Failed to send request",true);
        }
        
        return response;
    }
}