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
        var matchedFriendship = await chatDbContext.TblUserChatFriends
            .Where(friendShip =>
                friendShip.UserId == friendUserId ||
                friendShip.UserId == selfUserId)
            .GroupBy(friendShip => friendShip.FriendShipStatusId)
            .Select(group => new { group.Key, Count = group.Count() })
            .FirstOrDefaultAsync(group => group.Count > 1);

        if (matchedFriendship != null)
        {
            return await chatDbContext.TblUserChatFriendShipStatuses
                .FirstOrDefaultAsync(status => status.Id == matchedFriendship.Key);
        }

        return null;
    }
    
    public async Task<bool> AddChatFriendRequestAsync(long selfUserId, long friendUserId)
    {
        await using var transaction = await chatDbContext.Database.BeginTransactionAsync();

        try
        {
            var friendshipStatus = new TblUserChatFriendShipStatus
            {
                ActionBy = (int)selfUserId,
                ApproveStatus = (int)FriendshipStatus.Pending,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            await chatDbContext.TblUserChatFriendShipStatuses.AddAsync(friendshipStatus);
            await chatDbContext.SaveChangesAsync();

            var friendEntries = new List<TblUserChatFriend>
            {
                new TblUserChatFriend
                {
                    UserId = (int)friendUserId,
                    FriendShipStatusId = friendshipStatus.Id,
                    IsActive = true,
                    CreationDateTime = DateTime.Now
                },
                new TblUserChatFriend
                {
                    UserId = (int)selfUserId,
                    FriendShipStatusId = friendshipStatus.Id,
                    IsActive = true,
                    CreationDateTime = DateTime.Now
                }
            };

            await chatDbContext.TblUserChatFriends.AddRangeAsync(friendEntries);
            await chatDbContext.SaveChangesAsync();

            await transaction.CommitAsync();
            return true;
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            return false;
        }
    }


    public async Task<bool> UpdateChatFriendRequestAsync(TblUserChatFriendShipStatus updatedFriendship)
    {
        chatDbContext.TblUserChatFriendShipStatuses.Update(updatedFriendship);
        return await chatDbContext.SaveChangesAsync() == 1;
    }

    public async Task<List<ChatUserSearchResultDto>> SearchChatUserAsync(
        string searchTerm,
        long userId,
        int pageNumber,
        int pageSize
    )
    {
        var filteredUserInfo = chatDbContext.TblUserInformations
            .Where(u => u.Email.Contains(searchTerm) && u.IsActive == true)
            .Skip(0) 
            .Take(10) 
            .Select(u => new
            {
                u.UserId,
                u.FirstName,
                u.MiddleName,
                u.LastName
            });

        var friendshipStatus = chatDbContext.TblUserChatFriends
            .Where(ucf => ucf.UserId == userId)
            .Select(ucf => new
            {
                ucf.UserId,
                ucf.FriendShipStatusId
            });

        var result = await (from f in filteredUserInfo
            join ucf in chatDbContext.TblUserChatFriends on f.UserId equals ucf.UserId into ucfGroup
            from ucf in ucfGroup.DefaultIfEmpty()
            join fs in friendshipStatus on ucf.FriendShipStatusId equals fs.FriendShipStatusId into fsGroup
            from fs in fsGroup.DefaultIfEmpty()
            join cfs in chatDbContext.TblUserChatFriendShipStatuses on fs.FriendShipStatusId equals cfs.Id into cfsGroup
            from cfs in cfsGroup.DefaultIfEmpty()
            where f.UserId != userId
            select new ChatUserSearchResultDto
            {
                Id = f.UserId,
                FirstName = f.FirstName,
                MiddleName = f.MiddleName,
                LastName = f.LastName,
                ApproveStatus = cfs == null ? 1 : cfs.ApproveStatus
            }).ToListAsync();

        return result;

    }
}