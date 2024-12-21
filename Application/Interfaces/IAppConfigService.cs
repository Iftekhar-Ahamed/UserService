namespace Application.Interfaces;

public interface IAppConfigService
{
    Task<string>GetJwtSecretKeyAsync();
}