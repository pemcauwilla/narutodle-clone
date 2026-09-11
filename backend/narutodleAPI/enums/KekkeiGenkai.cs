using System.ComponentModel;

namespace narutodleAPI.enums;

public enum KekkeiGenkaiType
{
    [Description("Dōjutsu")]
    Dojutsu,

    [Description("Advanced Nature")]
    AdvancedNature,

    [Description("Body Mutation")]
    BodyMutation
}

public static class KekkeiGenkaiTranslator
{
    public static readonly Dictionary<string, KekkeiGenkaiType> Map = new()
    {
        { "Sharingan", KekkeiGenkaiType.Dojutsu },
        { "Mangekyō Sharingan", KekkeiGenkaiType.Dojutsu },
        { "Mangekyō Sharingan  (Anime only)", KekkeiGenkaiType.Dojutsu },
        { "Eternal Mangekyō Sharingan", KekkeiGenkaiType.Dojutsu },
        { "Byakugan", KekkeiGenkaiType.Dojutsu },
        { "Rinnegan", KekkeiGenkaiType.Dojutsu },
        { "Tenseigan", KekkeiGenkaiType.Dojutsu },
        { "Jōgan", KekkeiGenkaiType.Dojutsu },
        { "Ketsuryūgan", KekkeiGenkaiType.Dojutsu },
        { "Senrigan", KekkeiGenkaiType.Dojutsu },
        { "Isshiki's Dōjutsu", KekkeiGenkaiType.Dojutsu },
        { "Isshiki's Dōjutsu  (Manga only)", KekkeiGenkaiType.Dojutsu },

        { "Wood Release", KekkeiGenkaiType.AdvancedNature },
        { "Wood Release  (Anime only)", KekkeiGenkaiType.AdvancedNature },
        { "Wood Release  (Novel only)", KekkeiGenkaiType.AdvancedNature },
        { "Ice Release", KekkeiGenkaiType.AdvancedNature },
        { "Lava Release", KekkeiGenkaiType.AdvancedNature },
        { "Lava Release  (Anime only)", KekkeiGenkaiType.AdvancedNature },
        { "Lava Release  (Novel only)", KekkeiGenkaiType.AdvancedNature },
        { "Storm Release", KekkeiGenkaiType.AdvancedNature },
        { "Boil Release", KekkeiGenkaiType.AdvancedNature },
        { "Explosion Release", KekkeiGenkaiType.AdvancedNature },
        { "Scorch Release", KekkeiGenkaiType.AdvancedNature },
        { "Magnet Release", KekkeiGenkaiType.AdvancedNature },
        { "Magnet Release  (Novel only)", KekkeiGenkaiType.AdvancedNature },
        { "Dust Release", KekkeiGenkaiType.AdvancedNature },
        { "Mud Release", KekkeiGenkaiType.AdvancedNature },
        { "Typhoon Release", KekkeiGenkaiType.AdvancedNature },

        { "Shikotsumyaku", KekkeiGenkaiType.BodyMutation },
        { "Jūgo's Clan's Kekkei Genkai", KekkeiGenkaiType.BodyMutation },
        { "Sakon and Ukon's Kekkei Genkai", KekkeiGenkaiType.BodyMutation },
        { "Iburi Clan's Kekkei Genkai", KekkeiGenkaiType.BodyMutation },
        { "Kurama Clan's Kekkei Genkai", KekkeiGenkaiType.BodyMutation }
    };
}