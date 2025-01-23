using Newtonsoft.Json;

namespace Application.Core.Helpers.BasicDataHelpers;

public static class JsonConvertHelper
{
    public static string ConvertJsonString<T>(T value)
    {
        string result = JsonConvert.SerializeObject(value);
        return result;
    }
    
    public static T? JsonStringToObject<T>(string value)
    {
        var result = JsonConvert.DeserializeObject<T>(value);
        return result;
    }
}