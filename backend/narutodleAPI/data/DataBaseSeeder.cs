using System.Text.Json;
using narutodleAPI.enums;
using narutodleAPI.dtos;
using narutodleAPI.utils;
using narutodleAPI.models;

namespace narutodleAPI.data;

public class DataBaseSeeder
{
    public JsonSerializerOptions JsonOptions {get; set;} = new() { PropertyNameCaseInsensitive = true };

    public async Task Seed(HttpClient client, AppDbContext db)
    {
        int currentPage = 1;
        bool hasMorePages = true;

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
            var apiResult = await JsonSerializer.DeserializeAsync<DattebayoResponse>(stream, this.JsonOptions); 

            if (apiResult?.Characters == null || apiResult.Characters.Count == 0)
            {
                hasMorePages = false;
                continue;
            }

            foreach (var ninja in apiResult.Characters)
            {
                var gender = Gender.Male;
                var debutArc = StoryArc.Unknown;
                var affiliations = new HashSet<Affiliation>();
                var kekkeiGenkais = new HashSet<KekkeiGenkai>();
                var classifications = new HashSet<Classification>();
                var natureTypes = new HashSet<NatureType>();
                var jutsuTypes = new HashSet<JutsuType>();
                var image = string.Empty;

                if (ninja.Images.ValueKind != JsonValueKind.Null) 
                {
                    if (ninja.Images.ValueKind == JsonValueKind.Array && ninja.Images.GetArrayLength() > 0)
                    {
                        var imgs = new List<string>();
                        foreach(var item in ninja.Images.EnumerateArray())
                        {
                            imgs.Add(item.ToString());
                        }
                        image = imgs.Count > 1 ? imgs[1] : imgs[0];
                    }
                    else if (ninja.Images.ValueKind == JsonValueKind.String)
                    {
                        image = NinjaJsonUtils.ParseNinjaProperty(ninja, "images").ToString();
                    }
                }

                if(string.IsNullOrEmpty(image)) continue;

                if (ninja.Personal.ValueKind == JsonValueKind.Object){ 
                    Enum.TryParse<Gender>(NinjaJsonUtils.ParseNinjaProperty(ninja, "sex").ToString(), out gender);
                    debutArc = NinjaDataParser.ParseArc(ninja.Debut?.Anime?? string.Empty);
                    affiliations = NinjaDataParser.ParseAffiliations(NinjaJsonUtils.ParseNinjaProperty(ninja, "affiliation").ToList());
                    kekkeiGenkais = NinjaDataParser.ParseKekkeiGenkai(NinjaJsonUtils.ParseNinjaProperty(ninja, "kekkeiGenkai").ToList());
                    classifications = NinjaDataParser.ParseClassifications(NinjaJsonUtils.ParseNinjaProperty(ninja, "affiliation").ToList());
                }

                natureTypes = NinjaDataParser.ParseNatures(ninja.NatureType ?? new());
                jutsuTypes = NinjaDataParser.ParseJutsus(ninja.Jutsu ?? new());
                
                int emptyCategories = 0;
                if (affiliations == null || !affiliations.Any()) emptyCategories++;
                if (jutsuTypes == null || !jutsuTypes.Any()) emptyCategories++;
                if (kekkeiGenkais == null || !kekkeiGenkais.Any()) emptyCategories++;
                if (natureTypes == null || !natureTypes.Any()) emptyCategories++;
                if (classifications == null || !classifications.Any()) emptyCategories++;

                if(emptyCategories > 3) continue;

                var newNinja = new Ninja{
                    Name = ninja.Name, 
                    Gender = gender,
                    DebutArc = debutArc,
                    Affiliations = affiliations?.ToList() ?? new(),
                    KekkeiGenkais = kekkeiGenkais?.ToList() ?? new(),
                    Classifications = classifications?.ToList() ?? new(),
                    NatureTypes = natureTypes?.ToList() ?? new(),
                    JutsuTypes = jutsuTypes?.ToList() ?? new(),
                    ImageUrl = image 
                };

                db.Ninjas.Add(newNinja);
            }
            currentPage++;
        }
        await db.SaveChangesAsync();
        
    }
}