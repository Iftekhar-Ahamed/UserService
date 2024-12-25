using Domain.Models;

namespace Domain.Interfaces.ChatRepositories;

public interface IChatFriendRepository
{
    Task<TblUserChatFriend?>GetFriendshipAsync(long selfUserId, long friendUserId);
    Task<bool>AddChatFriendRequestAsync(long selfUserId, long friendUserId);
}