using System.ComponentModel;

namespace narutodleAPI.enums;

public enum Classification
{
    Jinchuriki,
    Sage,
    [Description("Missing-nin")] MissingNin,
    Mercenary
}

public static class ClassificationTranslator
{
    public static readonly Dictionary<string, Classification> Map = new()
    {
        { "Jinchūriki", Classification.Jinchuriki },
        { "Sage", Classification.Sage },
        { "Missing-nin", Classification.MissingNin },
        { "Hunter-nin", Classification.Mercenary }
    };
}