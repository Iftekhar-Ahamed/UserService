namespace Domain.Interfaces;

public interface IUserInfoRepository
{
    Task<bool> AddUserAsync();
}