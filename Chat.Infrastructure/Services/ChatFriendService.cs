using Application.Core.DTOs.APIRequestResponseDTOs;
using Application.Core.DTOs.PaginationDTOs;
using Application.Core.Extensions.DtoExtensions;
using Application.Core.Helpers.BasicDataHelpers;
using Chat.Core.DTOs.UserChatFriendDTOs;
using Chat.Core.Interfaces;
using Domain.Enums;
using Domain.Interfaces.ChatRepositories;
using User.Core.DTOs.UserDTOs;

namespace Chat.Infrastructure.Services;

public class ChatFriendService(
    IChatFriendRepository chatFriendRepository
    ) : IChatFriendService
{
    public async Task<ApiResponseDto<string>> SentChatFriendRequest(AddNewChatFriendRequestDto addFriendRequestDto)
    {
        var response = new ApiResponseDto<string>();
        
        var friendShipHistory = await chatFriendRepository.GetFriendshipAsync(addFriendRequestDto.SelfUserId,
            addFriendRequestDto.RequestedUserId);
        
        if (friendShipHistory != null)
        {
            if ((FriendshipStatus)friendShipHistory.ApproveStatus == FriendshipStatus.New)
            {
                friendShipHistory.ApproveStatus = (int)FriendshipStatus.Pending;
                var updateResult = await chatFriendRepository.UpdateChatFriendRequestAsync(friendShipHistory);
                
                if (updateResult)
                {
                    response.Success("Request send",true);
                }
            }
            else if ((FriendshipStatus)friendShipHistory.ApproveStatus == FriendshipStatus.Accepted)
            {
                response.Failed("User is already your friend",true);
            }
            else if((FriendshipStatus)friendShipHistory.ApproveStatus == FriendshipStatus.Pending)
            {
                response.Failed((friendShipHistory.ActionBy == addFriendRequestDto.SelfUserId ? "Request already sent":"Please refresh. User already sent a request."), true);
            }
            else if ((FriendshipStatus)friendShipHistory.ApproveStatus == FriendshipStatus.Blocked)
            {
                response.Failed(
                    friendShipHistory.ActionBy == addFriendRequestDto.SelfUserId
                        ? "Please unblock user first"
                        : "User blocked you.", true);
            }
            else
            {
                throw new InvalidOperationException("Something went wrong");
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
    
    public async Task<ApiResponseDto<string>> CancelChatFriendRequest(CancelFriendRequestDto cancelFriendRequestDto)
    {
        var response = new ApiResponseDto<string>();
        
        var friendShipHistory = await chatFriendRepository.GetFriendshipAsync(cancelFriendRequestDto.SelfUserId,
            cancelFriendRequestDto.RequestedUserId);
        
        if (friendShipHistory != null)
        {
            if ((FriendshipStatus)friendShipHistory.ApproveStatus == FriendshipStatus.Pending)
            {
                friendShipHistory.ApproveStatus = (int)FriendshipStatus.New;
                
                var updateResult = await chatFriendRepository.UpdateChatFriendRequestAsync(friendShipHistory);
                
                if (updateResult)
                {
                    response.Success("Friend request canceled",true);
                }
                else
                {
                    response.Failed("Failed to cancel request",true);
                }
                
            }
            else if ((FriendshipStatus)friendShipHistory.ApproveStatus == FriendshipStatus.New)
            {
                response.Failed("Friend request already canceled",true);
            }
            else if ((FriendshipStatus)friendShipHistory.ApproveStatus == FriendshipStatus.Accepted)
            {
                response.Failed("User is already your friend",true);
            }
            else if ((FriendshipStatus)friendShipHistory.ApproveStatus == FriendshipStatus.Blocked)
            {
                if (friendShipHistory.ActionBy == cancelFriendRequestDto.SelfUserId)
                {
                    response.Failed("Please unblock user first",true);
                }
                else
                {
                    response.Failed("User blocked you",true);
                }
            }
            else
            {
                throw new InvalidOperationException("Something went wrong");
            }
        }
        else
        {
            throw new InvalidOperationException("Something went wrong");
        }
        
        return response;
    }
    
    public async Task<ApiResponseDto<List<SearchChatUserResultResponseDto>>> SearchChatUser(
        string searchTerm,
        long userId,
        int pageNumber,
        int pageSize
    )
    {
        var response = new ApiResponseDto<List<SearchChatUserResultResponseDto>>();
        
        var matchedResult = await chatFriendRepository.SearchChatUserAsync(searchTerm,userId,pageNumber,pageSize);

        var processedResponse = matchedResult.Select(result => new SearchChatUserResultResponseDto
        {
         Id   = result.Id,
         Name = DataAggregatorHelper.CombineNames([result.FirstName, result.MiddleName, result.LastName]),
         Avatar = "avatar.jpeg",
         FriendshipStatus = (result.ApproveStatus == (int)FriendshipStatus.Pending ? (int)(result.ActionBy == userId ? FriendshipStatus.Pending : FriendshipStatus.Requested) :result.ApproveStatus),
        }).ToList();
        
        response.Data = processedResponse;
        response.Success();
        
        return response;
    }

    public async Task<ApiResponseDto<List<FriendRequestDetailsDto>>> GetFriendRequests(PaginationDto<long> request)
    {
        var response = new ApiResponseDto<List<FriendRequestDetailsDto>>();
        
        var chatFriendRequests = await chatFriendRepository.GetFriendRequestsAsync(request);

        if (chatFriendRequests.Any())
        {
            var data = chatFriendRequests.Select(tcf => new FriendRequestDetailsDto()
            {
                ChatId = tcf.ChatId,
                ImageUrl = tcf.ImageUrl,
                Name = new NameElementDto
                {
                    Title = "",
                    FirstName = tcf.FirstName,
                    LastName = tcf.LastName,
                    MiddleName = tcf.MiddleName,
                },
                FriendshipStatus = (FriendshipStatus)tcf.FriendshipStatus
            }).ToList();

            response.Data = data;
            response.Success();
        }
        else
        {
            response.Failed("Currently you no friend requests",true);
        }

        return response;
    }
}