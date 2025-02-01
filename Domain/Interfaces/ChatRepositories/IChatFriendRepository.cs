using Application.Core.DTOs.PaginationDTOs;
using Domain.Models;
using Domain.ViewModels;

namespace Domain.Interfaces.ChatRepositories;

public interface IChatFriendRepository
{
    Task<TblUserChatFriendShipStatus?>GetFriendshipAsync(long selfUserId, long friendUserId);
    Task<bool>AddChatFriendRequestAsync(long selfUserId, long friendUserId);
    Task<bool> UpdateChatFriendRequestAsync(TblUserChatFriendShipStatus updatedFriendship);
    Task<List<ChatUserSearchResultViewModel>>SearchChatUserAsync(
        string searchTerm,
        long userId,
        int pageNumber,
        int pageSize
    );
    Task<List<FriendRequestViewModel>>  GetFriendRequestsAsync(PaginationDto<long> requestData);
}