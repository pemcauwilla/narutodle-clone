using System.Collections.Generic;

namespace narutodleAPI.enums;

public enum JutsuType
{
    Ninjutsu,
    Taijutsu,
    Genjutsu 
}

public static class JutsuTranslator
{
    public static readonly Dictionary<string, JutsuType> Map = new()
    {
        { "Ninjutsu", JutsuType.Ninjutsu },
        { "Taijutsu", JutsuType.Taijutsu },
        { "Genjutsu", JutsuType.Genjutsu },
        { "Senjutsu", JutsuType.Ninjutsu },
        
        { "Fūinjutsu", JutsuType.Ninjutsu },
        { "Sealing Jutsu", JutsuType.Ninjutsu },
        
        { "Kinjutsu", JutsuType.Ninjutsu },
        { "Forbidden Jutsu", JutsuType.Ninjutsu },
        
        { "Juinjutsu", JutsuType.Ninjutsu },
        { "Curse Mark Jutsu", JutsuType.Ninjutsu },
        
        { "Bukijutsu", JutsuType.Ninjutsu },
        { "Kenjutsu", JutsuType.Ninjutsu }, 
        { "Shurikenjutsu", JutsuType.Ninjutsu } 
    };
}