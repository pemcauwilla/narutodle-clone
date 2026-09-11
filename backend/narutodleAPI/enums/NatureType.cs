using System.Collections.Generic;

namespace narutodleAPI.enums;

public enum NatureType
{
    Fire,
    Wind,
    Lightning,
    Earth,
    Water
}

public static class NatureTranslator
{
    public static readonly Dictionary<string, List<NatureType>> Map = new()
    {
        { "Fire Release", new List<NatureType> { NatureType.Fire } },
        { "Wind Release", new List<NatureType> { NatureType.Wind } },
        { "Lightning Release", new List<NatureType> { NatureType.Lightning } },
        { "Earth Release", new List<NatureType> { NatureType.Earth } },
        { "Water Release", new List<NatureType> { NatureType.Water } },
        
        { "Wood Release", new List<NatureType> { NatureType.Earth, NatureType.Water } },
        { "Ice Release", new List<NatureType> { NatureType.Water, NatureType.Wind } },
        { "Lava Release", new List<NatureType> { NatureType.Fire, NatureType.Earth } },
        { "Storm Release", new List<NatureType> { NatureType.Lightning, NatureType.Water } },
        { "Boil Release", new List<NatureType> { NatureType.Water, NatureType.Fire } },
        { "Scorch Release", new List<NatureType> { NatureType.Fire, NatureType.Wind } },
        { "Explosion Release", new List<NatureType> { NatureType.Earth, NatureType.Lightning } },
        { "Magnet Release", new List<NatureType> { NatureType.Wind, NatureType.Earth } },
        
        { "Dust Release", new List<NatureType> { NatureType.Fire, NatureType.Earth, NatureType.Wind } }
    };
}