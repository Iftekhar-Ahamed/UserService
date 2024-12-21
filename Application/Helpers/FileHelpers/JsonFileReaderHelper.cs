using Application.Helpers.BasicDataHelpers;

namespace Application.Helpers.FileHelpers;

public static class JsonFileReaderHelper
{
    public static async Task<T?> ReadJsonFile<T>(string filePath)
    {
        var result = default(T);

        if (File.Exists(filePath))
        {
            var jsonString = await File.ReadAllTextAsync(filePath);

            if (!string.IsNullOrEmpty(jsonString))
            {
                result = JsonConvertHelper.JsonStringToObject<T>(jsonString);
            }
            
        }
        
        return result;
    }
}