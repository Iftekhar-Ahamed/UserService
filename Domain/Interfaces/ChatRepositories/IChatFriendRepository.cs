using Domain.DTOs;
using Domain.Models;

namespace Domain.Interfaces.ChatRepositories;

public interface IChatFriendRepository
{
    Task<TblUserChatFriendShipStatus?>GetFriendshipAsync(long selfUserId, long friendUserId);
    Task<bool>AddChatFriendRequestAsync(long selfUserId, long friendUserId);
    Task<bool> UpdateChatFriendRequestAsync(TblUserChatFriendShipStatus updatedFriendship);
    Task<List<ChatUserSearchResultDto>>SearchChatUserAsync(
        string searchTerm,
        long userId,
        int pageNumber,
        int pageSize
    );
}