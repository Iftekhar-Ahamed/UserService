using Domain.Models;

namespace Domain.Interfaces;

public interface IUserInfoRepository
{
    Task<bool> AddUserAsync(TblUserInformation user);
    Task<bool> IsDuplicateUserAsync(string? email);
    Task<TblUserInformation?> GetUserByEmailAsync(string email); 
}