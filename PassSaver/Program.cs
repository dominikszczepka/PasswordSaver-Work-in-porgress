using Microsoft.AspNetCore.Identity;
using PassSaver.Entities;
using PassSaver.Seeders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<UsersSeeder>();
builder.Services.AddScoped<PasswordSeeder>();
builder.Services.AddDbContext<PassSaverDbContext>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var userSeeder = scope.ServiceProvider.GetRequiredService<UsersSeeder>();
    userSeeder.Seed();
    var passwordSeeder = scope.ServiceProvider.GetRequiredService<PasswordSeeder>();
    passwordSeeder.Seed();
}
// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
