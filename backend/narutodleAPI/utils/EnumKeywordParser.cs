namespace narutodleAPI.utils;

public static class EnumKeywordParser
{
    public static List<T> Parse<T>(List<string> rawApiStrings, Dictionary<T, List<string>> keywordMap) where T : struct, Enum
    {
        var matchedEnums = new HashSet<T>();

        if(rawApiStrings == null || rawApiStrings.Count == 0) return new List<T>();

        foreach(var rawString in rawApiStrings)
        {
            var lowerString = rawString.ToLower();

            foreach(var kvp in keywordMap)
            {
                if(kvp.Value.Any(keyword => lowerString.Contains(keyword)))
                {
                    matchedEnums.Add(kvp.Key);
                }
            }
        }
        return matchedEnums.ToList();
    }
}