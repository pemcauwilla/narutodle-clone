using narutodleAPI.models;
using narutodleAPI.data;
using Microsoft.EntityFrameworkCore;

namespace narutodleAPI.services;

public class DailyNinjaService
{
    private Ninja? _cachedNinja;
    private DateTime _lastUpdateDate;
    private readonly IServiceScopeFactory _scopeFactory;

    public DailyNinjaService(IServiceScopeFactory scopeFactory)
    {   
        _scopeFactory = scopeFactory;
    }

    public async Task<Ninja> GetDailyNinjaAsync()
    {
        var today = DateTime.UtcNow.Date;

        if(_lastUpdateDate == today && _cachedNinja != null) return _cachedNinja;

        int seed = (today.Year * 10000) + (today.Month * 100) + today.Day;
        var random = new Random(seed);

        using (var scope = _scopeFactory.CreateScope())
        {
            var db = scope.ServiceProvider.GetService<AppDbContext>();

            int totalNinjas = await db!.Ninjas.CountAsync();
            int randomPosition = random.Next(0, totalNinjas);

            _cachedNinja = await db.Ninjas.Skip(randomPosition).FirstOrDefaultAsync();
            _lastUpdateDate = today;
        }

        return _cachedNinja!;
    }
}