using System.Collections.Generic;
using System.Text.RegularExpressions;
using narutodleAPI.enums;

namespace narutodleAPI.utils;

public static class NinjaDataParser
{
    private static readonly Dictionary<Affiliation, List<string>> AffiliationKeywords = new()
    {
        { Affiliation.Konohagakure, new List<string> { "konoha", "root" } },
        { Affiliation.Sunagakure, new List<string> { "suna" } },
        { Affiliation.Kirigakure, new List<string> { "kiri" } },
        { Affiliation.Kumogakure, new List<string> { "kumo" } },
        { Affiliation.Iwagakure, new List<string> { "iwa" } },
        { Affiliation.Otogakure, new List<string> { "oto" } },
        { Affiliation.Amegakure, new List<string> { "ame" } },
        { Affiliation.Akatsuki, new List<string> { "akatsuki" } },
        { Affiliation.Kara, new List<string> { "kara" } },
        { Affiliation.MountMyoboku, new List<string> { "myōboku", "myoboku" } },
        { Affiliation.RyuchiCave, new List<string> { "ryūchi", "ryuchi" } }
    };

    private static readonly Dictionary<Classification, List<string>> ClassificationKeywords = new()
    {
        { Classification.Jinchuriki, new List<string> { "jinchūriki", "jinchuriki" } },
        { Classification.Sage, new List<string> { "sage" } },
        { Classification.MissingNin, new List<string> { "missing" } },
        { Classification.Mercenary, new List<string> { "mercenary", "hunter" } }
    };

    private static readonly Dictionary<JutsuType, List<string>> JutsuKeywords = new()
    {
        { JutsuType.Ninjutsu, new List<string> { "release", "clone", "bullet", "summoning", "seal", "rasengan", "chidori", "formation", "wall", "mode", "art" } },
        { JutsuType.Genjutsu, new List<string> { "genjutsu", "illusion", "tsukuyomi", "izanagi", "izanami", "kotoamatsukami", "mirage" } },
        { JutsuType.Taijutsu, new List<string> { "fist", "whirlwind", "lotus", "combo", "kick", "drop", "entry", "lariat", "palm", "fang", "gates" } }
    };

    private static readonly Dictionary<KekkeiGenkai, List<string>> KekkeiGenkaiKeywords = new()
    {
        { KekkeiGenkai.Dojutsu, new List<string> { 
            "sharingan", "byakugan", "rinnegan", "tenseigan", "jōgan", "jogan", 
            "ketsuryūgan", "ketsuryugan", "senrigan", "dōjutsu", "dojutsu" 
        }},
        { KekkeiGenkai.AdvancedNature, new List<string> { 
            "wood", "ice", "lava", "storm", "boil", "explosion", 
            "scorch", "magnet", "dust", "mud", "typhoon" 
        }},
        { KekkeiGenkai.BodyMutation, new List<string> { 
            "shikotsumyaku", "jūgo", "jugo", "sakon", "iburi", "kurama" 
        }}
    };

    private static readonly Dictionary<NatureType, List<string>> NatureKeywords = new()
    {
        { NatureType.Fire, new List<string> { "fire", "lava", "boil", "scorch", "dust" } },
        { NatureType.Wind, new List<string> { "wind", "ice", "scorch", "magnet", "dust" } },
        { NatureType.Lightning, new List<string> { "lightning", "storm", "explosion" } },
        { NatureType.Earth, new List<string> { "earth", "wood", "lava", "explosion", "magnet", "dust" } },
        { NatureType.Water, new List<string> { "water", "wood", "ice", "storm", "boil" } }
    };

    public static HashSet<Affiliation> ParseAffiliations(List<string> rawStrings)
    {
        return EnumKeywordParser.Parse(rawStrings, AffiliationKeywords).ToHashSet();
    }

    public static HashSet<Classification> ParseClassifications(List<string> rawStrings)
    {
        return EnumKeywordParser.Parse(rawStrings, ClassificationKeywords).ToHashSet();
    }

    public static HashSet<JutsuType> ParseJutsus(List<string> rawStrings)
    {
        return EnumKeywordParser.Parse(rawStrings, JutsuKeywords).ToHashSet();
    }

    public static HashSet<KekkeiGenkai> ParseKekkeiGenkai(List<string> rawStrings)
    {
        return EnumKeywordParser.Parse(rawStrings, KekkeiGenkaiKeywords).ToHashSet();
    }

    public static HashSet<NatureType> ParseNatures(List<string> rawStrings)
    {
        return EnumKeywordParser.Parse(rawStrings, NatureKeywords).ToHashSet();
    }

    public static StoryArc ParseArc(string rawDebut)
    {
        if (string.IsNullOrWhiteSpace(rawDebut))
            return StoryArc.Unknown;

        if (rawDebut.StartsWith("Boruto"))
            return StoryArc.BorutoEra;

        var match = Regex.Match(rawDebut, @"Episode #(\d+)");
        if (!match.Success || !int.TryParse(match.Groups[1].Value, out int episodeNumber))
        {
            return StoryArc.Unknown; 
        }

        bool isShippuden = rawDebut.Contains("Shippūden");

        if (!isShippuden)
        {
            return episodeNumber switch
            {
                <= 19 => StoryArc.LandOfWaves,
                <= 80 => StoryArc.ChuninExams,
                <= 100 => StoryArc.SearchForTsunade,
                <= 135 => StoryArc.SasukeRecovery,
                _ => StoryArc.Unknown
            };
        }
        else
        {
            return episodeNumber switch
            {
                <= 32 => StoryArc.KazekageRescue,
                <= 53 => StoryArc.TenchiBridge,
                <= 89 => StoryArc.AkatsukiSuppression,
                <= 112 => StoryArc.ShippudenFiller, 
                <= 143 => StoryArc.ItachiPursuit, 
                <= 151 => StoryArc.ShippudenFiller, 
                <= 175 => StoryArc.PainAssault,
                <= 196 => StoryArc.ShippudenFiller, 
                <= 214 => StoryArc.FiveKageSummit,
                <= 479 => StoryArc.FourthShinobiWar, 
                <= 500 => StoryArc.BlankPeriod, 
                _ => StoryArc.Unknown
            };
        }
    }
    
}