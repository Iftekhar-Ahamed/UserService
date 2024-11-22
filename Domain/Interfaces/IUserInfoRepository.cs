using Domain.Models;

namespace Domain.Interfaces;

public interface IUserInfoRepository
{
    Task<bool> AddUserAsync(TblUserInformation user);
}