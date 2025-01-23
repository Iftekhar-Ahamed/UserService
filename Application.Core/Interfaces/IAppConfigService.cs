namespace Application.Core.Interfaces;

public interface IAppConfigService
{
    Task<string>GetJwtSecretKeyAsync();
}