using System.Text.Json;
using narutodleAPI.dtos;

namespace narutodleAPI.utils;

public static class NinjaJsonUtils
{
    public static HashSet<string> ParseNinjaProperty(NinjaApiDto ninja, string propName)
    {
        if(ninja.Personal.TryGetProperty(propName, out JsonElement elem)){
            if (elem.ValueKind == JsonValueKind.Array)
            {
                HashSet<string> set = new();
                foreach (var item in elem.EnumerateArray())
                {
                    set.Add(item.GetString() ?? string.Empty);
                }
                return set;
            }

            if (elem.ValueKind == JsonValueKind.String)
            {
                return new HashSet<string>(){elem.GetString() ?? string.Empty};
            }         
        }
        return new();
    }
}