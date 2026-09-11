using narutodleAPI.enums;

namespace narutodleAPI.models;

internal class Ninja
{
    public int Id {get; set;}
    public string Name {get; set;} = string.Empty;

    public string? ImageUrl {get; set;} 

    public string? Gender {get; set;} 
    public StoryArc? DebutArc {get; set;}
    
    public List<Affiliation>? Affiliations{get; set;}
    public List<JutsuType>? JutsuTypes {get; set;}
    public List<NatureType>? NatureTypes {get; set;}
    public List<Classification>? Classifications {get; set;}
}