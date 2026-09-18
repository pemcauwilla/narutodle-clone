using narutodleAPI.enums;

namespace narutodleAPI.dtos;

public enum MatchStatus
{
    Incorrect,
    Partial,
    Correct,
    Earlier,
    Later
}

public class PropertyResult<T>
{
    public T? Value {get; set;}
    public MatchStatus Status {get; set;}
}

public class GuessResultDto
{
    public string? Name { get; set; }
    public string? ImageUrl { get; set; }
    public bool IsVictory { get; set; }

    public PropertyResult<string>? Gender { get; set; } 
    public PropertyResult<List<string>>? Affiliations { get; set; }
    public PropertyResult<List<string>>? JutsuTypes { get; set; }
    public PropertyResult<List<string>>? KekkeiGenkais { get; set; }
    public PropertyResult<List<string>>? NatureTypes { get; set; }
    public PropertyResult<List<string>>? Classifications { get; set; }
    public PropertyResult<string>? DebutArc { get; set; }
}