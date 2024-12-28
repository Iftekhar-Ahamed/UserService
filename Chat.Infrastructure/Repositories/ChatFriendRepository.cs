using Domain.DTOs;
using Domain.Enums;
using Domain.Interfaces.ChatRepositories;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

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
            user.Email.Contains(searchTerm) && user.UserId != userId
        )
        .Skip(pageSize * (pageNumber - 1))
        .Take(pageSize);

        var matchedUserJoinWithFriendShipStatus = from user in matchedUsers
            join userChatFriend in chatDbContext.TblUserChatFriends
                on user.UserId equals userChatFriend.UserId into userChatFriendsGroup
            from userChatFriend in userChatFriendsGroup.DefaultIfEmpty()
            select new
            {
                user.UserId,
                user.FirstName,
                user.MiddleName,
                user.LastName,
                friendshipStatusId = userChatFriend == null 
                    ? 0
                    : userChatFriend.FriendShipStatusId
            };

        
        var finalResult = await (from user in matchedUserJoinWithFriendShipStatus
            join status in chatDbContext.TblUserChatFriendShipStatuses
                on user.friendshipStatusId equals status.Id into groupResult
            from result in groupResult.DefaultIfEmpty() 
            select new ChatUserSearchResultDto
            {
                Id = user.UserId,
                FirstName = user.FirstName,
                MiddleName = user.MiddleName,
                LastName = user.LastName,
                ApproveStatus = result == null ? (int)FriendshipStatus.New : result.ApproveStatus,
            }).ToListAsync();
                
        
        return finalResult;
    }
}