using Application.Core.DTOs.PaginationDTOs;
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
        var query = 
            from user in chatDbContext.TblUserInformations
            where user.Email.Contains(searchTerm) &&
                  user.IsActive == true &&
                  user.UserId != userId
            orderby user.UserId
            select new
            {
                user.UserId,
                user.FirstName,
                user.MiddleName,
                user.LastName
            } into filteredUsers
            from friendStatus in
                (from t1 in chatDbContext.TblUserChatFriends
                    join t2 in chatDbContext.TblUserChatFriends
                        on t1.FriendShipStatusId equals t2.FriendShipStatusId
                    where t1.UserId == userId && t1.UserId != t2.UserId
                    select new
                    {
                        t2.UserId,
                        t2.FriendShipStatusId
                    }).Where(ufs => ufs.UserId == filteredUsers.UserId)
                .DefaultIfEmpty()
            from friendshipStatus in chatDbContext.TblUserChatFriendShipStatuses
                .Where(fs => fs.Id == friendStatus.FriendShipStatusId)
                .DefaultIfEmpty()
            select new ChatUserSearchResultDto
            {
                Id = filteredUsers.UserId,
                FirstName = filteredUsers.FirstName,
                MiddleName = filteredUsers.MiddleName,
                LastName = filteredUsers.LastName,
                ApproveStatus = friendshipStatus != null ? friendshipStatus.ApproveStatus : 1
            };

        var result = await query.ToListAsync();

        return result;

    }

    public async Task<List<FriendRequestDetailsDto>> GetFriendRequestsAsync(PaginationDto<long> requestData)
    {
        var friendRequestDetails = await (from userChatList in chatDbContext.TblUserChatFriends
            join friendshipStatus in chatDbContext.TblUserChatFriendShipStatuses
                on userChatList.FriendShipStatusId equals friendshipStatus.Id
            where userChatList.UserId == requestData.Data
                && friendshipStatus.ApproveStatus == (int)FriendshipStatus.Pending
            select new FriendRequestDetailsDto
            {
                ChatId = userChatList.FriendShipStatusId,
                FriendshipStatus = (FriendshipStatus)friendshipStatus.ApproveStatus,
            }).ToListAsync();
        
        return friendRequestDetails;
    }
}