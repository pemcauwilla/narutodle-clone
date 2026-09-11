using System.ComponentModel;
using System.Text.RegularExpressions;

namespace narutodleAPI.enums;

public enum StoryArc
{
    [Description("Land of Waves")] LandOfWaves,
    [Description("Chūnin Exams")] ChuninExams,
    [Description("Search for Tsunade")] SearchForTsunade,
    [Description("Sasuke Recovery")] SasukeRecovery,
    [Description("Part 1 Filler")] Part1Filler,

    [Description("Kazekage Rescue")] KazekageRescue,
    [Description("Tenchi Bridge")] TenchiBridge,
    [Description("Akatsuki Suppression")] AkatsukiSuppression,
    [Description("Itachi Pursuit")] ItachiPursuit,
    [Description("Pain's Assault")] PainAssault,
    [Description("Five Kage Summit")] FiveKageSummit,
    [Description("Fourth Shinobi War")] FourthShinobiWar,
    [Description("Blank Period")] BlankPeriod,
    [Description("Shippūden Filler")] ShippudenFiller,
    
    [Description("Boruto Era")] BorutoEra,
    
    Unknown
}

public static class ArcParser
{
    public static StoryArc ParseDebut(string rawDebut)
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