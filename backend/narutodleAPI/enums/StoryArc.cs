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

