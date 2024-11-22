using Domain.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserInfoRepository(ChatDbContext chatDbContext) : IUserInfoRepository
{
    public async Task<bool> AddUserAsync(TblUserInformation user)
    {
        var data = await chatDbContext.TblUserInformations.Select(x => x).ToListAsync();
        var response = await chatDbContext.TblUserInformations.AddAsync(user);
        return true;
    }
}