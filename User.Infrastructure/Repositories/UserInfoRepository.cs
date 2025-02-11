using Application.Core.Helpers.BasicDataHelpers;
using Domain.Interfaces.UserRepositories;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace User.Infrastructure.Repositories;

public class UserInfoRepository(ChatDbContext chatDbContext) : IUserInfoRepository
{
    public async Task<bool> AddUserAsync(TblUserInformation user)
    {
        await chatDbContext.TblUserInformations.AddAsync(user);
        var res = await chatDbContext.SaveChangesAsync();
        
        return res == 1;
    }
    
    public async Task<bool> IsDuplicateUserAsync(string? email)
    {
        var matchedResult = await chatDbContext.TblUserInformations.FirstOrDefaultAsync
            (user => string.IsNullOrEmpty(email) || user.Email == email);
        
        return matchedResult != null;
    }

    public async Task<TblUserInformation?> GetUserByEmailAsync(string email)
    {
        var matchedResult = await chatDbContext.TblUserInformations.FirstOrDefaultAsync
            (user => user.Email == email);

        return matchedResult;
    }
    
    public async Task<TblUserInformation?> GetUserByIdAsync(int userId)
    {
        var matchedResult = await chatDbContext.TblUserInformations.FirstOrDefaultAsync
            (user => user.UserId == userId);

        return matchedResult;
    }

    public async Task<bool> UpdateUserAsync(TblUserInformation userInfo)
    {
        var result = chatDbContext.TblUserInformations.Update(userInfo);

        return await chatDbContext.SaveChangesAsync() == 1;
    }

    public async Task<List<TblUserInformation>> SearchUserAsync(string searchTerm,
        long userId,
        int pageNo = 0,
        int pageSize = 10)
    {
        var result = await chatDbContext.TblUserInformations
                .Where(user => user.Email.Contains(searchTerm) && user.UserId != userId)
                .ToListAsync();

        return result;
    }
}