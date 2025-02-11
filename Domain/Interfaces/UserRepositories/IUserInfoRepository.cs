using Domain.Models;

namespace Domain.Interfaces.UserRepositories;

public interface IUserInfoRepository
{
    Task<bool> AddUserAsync(TblUserInformation user);
    Task<bool> IsDuplicateUserAsync(string? email);
    Task<TblUserInformation?> GetUserByEmailAsync(string email);
    Task<TblUserInformation?> GetUserByIdAsync(int userId);
    Task<bool> UpdateUserAsync(TblUserInformation userInfo);
    Task<List<TblUserInformation>>SearchUserAsync(string searchTerm,long userId,int pageNo = 0,int pageSize = 10);
}