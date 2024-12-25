namespace Application.Helpers.BasicDataHelpers;

public static class DataAggregatorHelper
{
    public static string CombineNames(params string[] names)
    {
        string finalName = string.Empty;
        
        foreach (var name in names)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                continue;
            }

            if (string.IsNullOrEmpty(finalName))
            {
                finalName = name;
            }
            else
            {
                finalName += " " + name;
            }
        }
        return finalName;
    }
}