using Application.Core.DTOs.CommonDTOs;
using Application.Core.Helpers.FileHelpers;
using Application.Core.Interfaces;

namespace Application.Infrastructure.Services;

public class AppConfigService() : IAppConfigService
{
    private AppSettingConfigurationDto? _appSettingConfiguration;
    private readonly string _filePath = Path.GetFullPath("appsettings.json");

    public async Task<string> GetJwtSecretKeyAsync()
    {
        _appSettingConfiguration ??= (await JsonFileReaderHelper.ReadJsonFile<AppSettingConfigurationDto>(_filePath) ??
                                      throw new NullReferenceException("Failed to create app config instance"));
        
        return _appSettingConfiguration.SecretKey;
    }
}