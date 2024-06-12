namespace CulinaryGuide.Server.HelperClasses;

public static class StringExtensions
{
    public static string CapitalizeFirstLetter(this string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return value;
        }

        return char.ToUpper(value[0]) + value.Substring(1);
    }
    
    public static string CapitalizeWordsAndJoin(this string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return value;
        }

        var words = value.Split(',')
            .Select(word => word.Trim())
            .Select(word => char.ToUpper(word[0]) + word.Substring(1).ToLower());

        return string.Join(", ", words);
    }
}