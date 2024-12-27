using Domain.Interfaces.ChatRepositories;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Chat.Infrastructure.Repositories;

public class ChatFriendRepository(ChatDbContext chatDbContext) : IChatFriendRepository
{
    public async Task<TblUserChatFriend?> GetFriendshipAsync(long selfUserId, long friendUserId)
    {   
        var res = await chatDbContext.TblUserChatFriends
            .Where(user =>
                (user.UserId == selfUserId && user.FriendId == friendUserId || 
                user.UserId == friendUserId && user.FriendId == selfUserId))
            .FirstOrDefaultAsync();
        
        return res;
    }
    
    //public async Task<>

    public Task<bool> AddChatFriendRequestAsync(long selfUserId, long friendUserId)
    {
        throw new NotImplementedException();
    }
}