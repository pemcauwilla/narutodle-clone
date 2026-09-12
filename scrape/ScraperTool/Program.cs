using System.Text.Json;
using System.IO;

using HttpClient client = new();

HashSet<string> allGenders = new();
HashSet<string> allAffiliations = new();
HashSet<string> allNatureTypes = new();
HashSet<string> allKekkeiGenkais = new();
HashSet<string> allTrainedAttributes = new();
HashSet<string> allDebutArcs = new();
HashSet<string> allJutsuTypes = new();

int currentPage = 1;
bool hasMorePages = true;
var jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

Console.WriteLine("Starting scrape...");

while (hasMorePages)
{
    string url = $"https://dattebayo-api.onrender.com/characters?page={currentPage}";
    HttpResponseMessage response = await client.GetAsync(url); 

    if (!response.IsSuccessStatusCode)
    {
        hasMorePages = false;
        continue;
    }

    using var stream = await response.Content.ReadAsStreamAsync();
    var apiResult = await JsonSerializer.DeserializeAsync<DattebayoResponse>(stream, jsonOptions); 

    if (apiResult?.Characters == null || apiResult.Characters.Count == 0)
    {
        hasMorePages = false;
        continue;
    }

    Console.WriteLine($"Scraping page {currentPage}...");

    foreach (var ninja in apiResult.Characters)
    {
        if (ninja.Personal.ValueKind == JsonValueKind.Object){ 
            JsonUtils.ParseJson(ninja, "sex", allGenders);
            JsonUtils.ParseJson(ninja, "affiliation", allAffiliations);
            JsonUtils.ParseJson(ninja, "kekkeiGenkai", allKekkeiGenkais);
            JsonUtils.ParseJson(ninja, "classification", allTrainedAttributes);

        }
        if (ninja.NatureType != null) foreach (var nature in ninja.NatureType) allNatureTypes.Add(nature);
        if (ninja.Debut != null) allDebutArcs.Add(ninja.Debut.Anime ?? "ArtificialInvalidArc");
        if (ninja.Jutsu != null) foreach(var jutsu in ninja.Jutsu) allJutsuTypes.Add(jutsu ?? "ArtificialInvalidJutsu");
    }

    currentPage++;
}

Console.WriteLine("\nScrape Complete!");

Console.WriteLine("\nWriting results to ScrapedEnums.txt...");
List<string> fileLines = new List<string>();

fileLines.Add("=== GENDERS ===");
fileLines.AddRange(allGenders);

fileLines.Add("\n=== AFFILIATIONS ===");
fileLines.AddRange(allAffiliations);

fileLines.Add("\n=== NATURE TYPES ===");
fileLines.AddRange(allNatureTypes);

fileLines.Add("\n=== KEKKEI GENKAI ===");
fileLines.AddRange(allKekkeiGenkais);

fileLines.Add("\n=== DEBUT ARCS ===");
fileLines.AddRange(allDebutArcs);

fileLines.Add("\n=== ATTRIBUTES ===");
fileLines.AddRange(allTrainedAttributes);

fileLines.Add("\n=== JUTSUS ===");
fileLines.AddRange(allJutsuTypes);

await File.WriteAllLinesAsync("ScrapedEnums.txt", fileLines);

Console.WriteLine("Done! Check your folder for ScrapedEnums.txt");

public static class JsonUtils
{
    public static void ParseJson(ApiNinja ninja, string propName, HashSet<string> set)
    {
        if(ninja.Personal.TryGetProperty(propName, out JsonElement elem)){
            if (elem.ValueKind == JsonValueKind.Array)
            {
                foreach (var item in elem.EnumerateArray())
                {
                    set.Add(item.GetString() ?? "ArtificialInvalidString");
                }
            }     
     
            if (elem.ValueKind == JsonValueKind.String)
            {
                set.Add(elem.GetString() ?? "ArtificialInvalidString");
            }
        }
    }
}

public class DattebayoResponse
{
    public List<ApiNinja> Characters { get; set; }
}

public class ApiNinja
{
    public JsonElement Personal { get; set; }
    public ApiDebutArc? Debut {get; set;}
    public List<string>? Jutsu { get; set; }
    public List<string>? NatureType { get; set; }
}

public class ApiDebutArc
{
    public string? Anime {get; set;}
}
