using System.ComponentModel;

namespace narutodleAPI.enums;

public enum Affiliation
{
    Konohagakure,
    Sunagakure,
    Kirigakure,
    Kumogakure,
    Iwagakure,
    Otogakure,
    Amegakure,
    Akatsuki,
    Kara,
    [Description("Mount Myōboku")] MountMyoboku,
    [Description("Ryūchi Cave")] RyuchiCave,
}

public static class AffiliationTranslator
{
    public static readonly Dictionary<string, Affiliation> Map = new()
    {
        { "Konohagakure", Affiliation.Konohagakure },
        { "Sunagakure", Affiliation.Sunagakure },
        { "Kirigakure", Affiliation.Kirigakure },
        { "Kumogakure", Affiliation.Kumogakure },
        { "Iwagakure", Affiliation.Iwagakure },
        { "Akatsuki", Affiliation.Akatsuki },
        { "Otogakure", Affiliation.Otogakure },
        { "Amegakure", Affiliation.Amegakure },
        { "Kara", Affiliation.Kara },
        { "Mount Myōboku", Affiliation.MountMyoboku },
        { "Ryūchi Cave", Affiliation.RyuchiCave },
        
        { "Konohagakure  (Manga only)", Affiliation.Konohagakure },
        { "Konohagakure  (Novel only)", Affiliation.Konohagakure },
        { "Kirigakure  (Anime only)", Affiliation.Kirigakure },
        { "Kumogakure  (Anime only)", Affiliation.Kumogakure },
        { "Akatsuki  (Anime only)", Affiliation.Akatsuki },
        { "Otogakure  (Anime only)", Affiliation.Otogakure },
        { "Mount Myōboku  (Anime only)", Affiliation.MountMyoboku },
        { "Ryūchi Cave  (Anime only)", Affiliation.RyuchiCave },

        { "Root", Affiliation.Konohagakure },
        { "Root  (Anime only)", Affiliation.Konohagakure },
        { "Konoha Orphanage", Affiliation.Konohagakure }
    };
}