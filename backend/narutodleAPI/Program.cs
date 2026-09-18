using Microsoft.EntityFrameworkCore;
using narutodleAPI.data;
using narutodleAPI.services;

using HttpClient client = new();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<AppDbContext>(options => 
    options.UseSqlite("Data Source=database/narutodle.db"));
builder.Services.AddScoped<GameService>();
builder.Services.AddSingleton<DailyNinjaService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    var seeder = new DataBaseSeeder();
    db.Database.Migrate(); 

    if(!db.Ninjas.Any()) 
    {
        await seeder.Seed(client, db);
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
