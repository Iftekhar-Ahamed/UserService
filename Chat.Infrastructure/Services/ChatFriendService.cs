using System.Collections.Concurrent;
using Application.DTOs.APIRequestResponseDTOs;
using Application.Extensions.DtoExtensions;
using Application.Helpers.BasicDataHelpers;
using Chat.Core.DTOs.UserChatFriendDTOs;
using Chat.Core.Interfaces;
using Domain.Enums;
using Domain.Interfaces.ChatRepositories;
using Domain.Interfaces.UserRepositories;

namespace Chat.Infrastructure.Services;

public class ChatFriendService(
    IChatFriendRepository chatFriendRepository,
    IUserInfoRepository userInfoRepository
    ) : IChatFriendService
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

    public async Task<ApiResponseDto<List<SearchChatUserResultResponseDto>>> SearchChatUser(string searchTerm,long userId)
    {
        var response = new ApiResponseDto<List<SearchChatUserResultResponseDto>>();
        
        var matchedResult = await userInfoRepository.SearchUserAsync(searchTerm,userId);

        var processedResponse = new ConcurrentBag<SearchChatUserResultResponseDto>();
        
        await Parallel.ForEachAsync(matchedResult, async (user, _) =>
        {
            var friendShipHistory = await chatFriendRepository.GetFriendshipAsync(userId, user.UserId);
            
            int friendshipStatus = friendShipHistory?.ApproveStatus ?? 0;
            
            processedResponse.Add(new SearchChatUserResultResponseDto
            {
                Id = user.UserId,
                Name = DataAggregatorHelper.CombineNames([user.FirstName,user.MiddleName ?? string.Empty,user.LastName]),
                Avatar = "avatar.jpg",
                FriendshipStatus = (int)friendshipStatus
            });
        });
        
        response.Data = processedResponse.ToList();
        response.Success();
        
        return response;
    }
}