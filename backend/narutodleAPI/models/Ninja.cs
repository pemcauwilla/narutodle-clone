using narutodleAPI.enums;

namespace narutodleAPI.models;

public class Ninja
{
    public int Id {get; set;}
    public string Name {get; set;} = string.Empty;

    public string? ImageUrl {get; set;} 

    public Gender? Gender {get; set;} 
    public StoryArc? DebutArc {get; set;}
    
    public List<Affiliation> Affiliations{get; set;} = new();
    public List<JutsuType> JutsuTypes {get; set;} = new();
    public List<NatureType> NatureTypes {get; set;} = new();
    public List<KekkeiGenkai> KekkeiGenkais {get; set;} = new();
    public List<Classification> Classifications {get; set;} = new();
}