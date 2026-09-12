using System.Text.Json;

namespace narutodleAPI.dtos;

public class NinjaApiDto
{
    public string Name {get; set;} = string.Empty;
    public JsonElement Images {get; set;}

    public DebutArcApiDto? Debut {get; set;}

    public List<string>? Jutsu {get; set;}
    
    public List<string>? NatureType {get; set;}

    public JsonElement Personal {get; set;}
}

public class DebutArcApiDto
{
    public string? Anime {get; set;}
}

public class DattebayoResponse
{
    public List<NinjaApiDto>? Characters { get; set; }
}

