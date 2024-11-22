using Domain.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

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
}