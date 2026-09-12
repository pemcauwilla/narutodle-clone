using Microsoft.EntityFrameworkCore;
using narutodleAPI.models;

namespace narutodleAPI.data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Ninja> Ninjas { get; set; }
}