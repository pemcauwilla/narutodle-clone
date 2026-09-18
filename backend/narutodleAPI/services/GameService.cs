using Microsoft.EntityFrameworkCore;
using narutodleAPI.data;
using narutodleAPI.dtos;
using narutodleAPI.enums;
using narutodleAPI.utils;

namespace narutodleAPI.services;

public class GameService
{
    private readonly AppDbContext _db;
    private readonly DailyNinjaService _dailyNinjaService;

    public GameService(AppDbContext db, DailyNinjaService dailyNinjaService)
    {
        _db = db;
        _dailyNinjaService = dailyNinjaService;
    }

    public async Task<GuessResultDto?> ProcessGuess(string guessedName)
    {
        var guess = await _db.Ninjas.FirstOrDefaultAsync(n => n.Name.ToLower() == guessedName.ToLower());
        if (guess == null) return null; 

        var target = await _dailyNinjaService.GetDailyNinjaAsync();

        var result = new GuessResultDto
        {
            Name = guess.Name,
            ImageUrl = guess.ImageUrl,
            IsVictory = guess.Id == target.Id,

            Gender = new PropertyResult<string> 
            {
                Value = guess.Gender.GetDescription(),
                Status = CompareValues(guess.Gender, target.Gender)
            },

            DebutArc = new PropertyResult<string> 
            { 
                Value = (guess.DebutArc ?? StoryArc.Unknown).GetDescription(), 
                Status = CompareArcs(guess.DebutArc ?? StoryArc.Unknown, target.DebutArc ?? StoryArc.Unknown) 
            },

            Classifications = new PropertyResult<List<string>> 
            { 
                Value = guess.Classifications?.Select(c => c.GetDescription()).ToList() ?? new List<string>(), 
                Status = CompareLists(guess.Classifications, target.Classifications) 
            },

            Affiliations = new PropertyResult<List<string>> 
            { 
                Value = guess.Affiliations?.Select(a => a.GetDescription()).ToList() ?? new List<string>(), 
                Status = CompareLists(guess.Affiliations, target.Affiliations) 
            },

            JutsuTypes = new PropertyResult<List<string>> 
            { 
                Value = guess.JutsuTypes?.Select(j => j.GetDescription()).ToList() ?? new List<string>(), 
                Status = CompareLists(guess.JutsuTypes, target.JutsuTypes) 
            },

            KekkeiGenkais = new PropertyResult<List<string>> 
            { 
                Value = guess.KekkeiGenkais?.Select(k => k.GetDescription()).ToList() ?? new List<string>(), 
                Status = CompareLists(guess.KekkeiGenkais, target.KekkeiGenkais) 
            },

            NatureTypes = new PropertyResult<List<string>> 
            { 
                Value = guess.NatureTypes?.Select(n => n.GetDescription()).ToList() ?? new List<string>(), 
                Status = CompareLists(guess.NatureTypes, target.NatureTypes) 
            }
        };

        return result;
    }

    public async Task<List<NinjaDropdownDto>> GetNinjaDropdownListAsync()
    {
        return await _db.Ninjas
            .Select(n => new NinjaDropdownDto(n.Name, n.ImageUrl ?? string.Empty))
            .ToListAsync();
    }
    private MatchStatus CompareArcs(StoryArc guessArc, StoryArc targetArc)
    {
        if (guessArc == targetArc) return MatchStatus.Correct;
        return (int)guessArc > (int)targetArc ? MatchStatus.Earlier : MatchStatus.Later;
    }

    private MatchStatus CompareLists<T>(List<T> guessList, List<T> targetList)
    {
        guessList ??= new List<T>();
        targetList ??= new List<T>();

        if (guessList.Count == targetList.Count && !guessList.Except(targetList).Any())
            return MatchStatus.Correct;

        if (guessList.Intersect(targetList).Any())
            return MatchStatus.Partial;

        return MatchStatus.Incorrect;
    }

    private MatchStatus CompareValues<T>(T guessValue, T targetValue)
    {
        if (EqualityComparer<T>.Default.Equals(guessValue, targetValue))
        {
            return MatchStatus.Correct;
        }
        
        return MatchStatus.Incorrect;
    }
}