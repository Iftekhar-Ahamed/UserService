using Newtonsoft.Json;

namespace Application.Helpers.BasicDataHelpers;

public static class JsonConvertHelper
{
    public static string ConvertJsonString<T>(T value)
    {
        string result = JsonConvert.SerializeObject(value);
        return result;
    }
}