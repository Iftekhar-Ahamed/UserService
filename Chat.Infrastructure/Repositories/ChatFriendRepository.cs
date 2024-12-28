using Domain.DTOs;
using Domain.Interfaces.ChatRepositories;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace Chat.Infrastructure.Repositories;

public class ChatFriendRepository(ChatDbContext chatDbContext) : IChatFriendRepository
{
    public async Task<TblUserChatFriendShipStatus?> GetFriendshipAsync(long selfUserId, long friendUserId)
    {   
        var res = await chatDbContext.TblUserChatFriendShipStatuses
            .Where(user =>
                (user.UserId == selfUserId && user.FriendId == friendUserId || 
                user.UserId == friendUserId && user.FriendId == selfUserId))
            .FirstOrDefaultAsync();
        
        return res;
    }
    
    public Task<bool> AddChatFriendRequestAsync(long selfUserId, long friendUserId)
    {
        throw new NotImplementedException();
    }

    public async Task<List<ChatUserSearchResultDto>> SearchChatUserAsync(
        string searchTerm,
        long userId,
        int pageNumber,
        int pageSize
    )
    {
        var matchedUsers = chatDbContext.TblUserInformations.Where
        (
            user => 
            user.Email.Contains(searchTerm) && user.UserId == userId
        )
        .Skip(pageSize * (pageNumber - 1))
        .Take(pageSize);
        
        return [];
    }
}