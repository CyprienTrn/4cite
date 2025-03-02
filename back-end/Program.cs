using back_end.Database;
using Microsoft.EntityFrameworkCore;
using back_end.Services;
using back_end.Models;
using back_end.Interfaces;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Ajout des services
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>(); // Ajout de cette ligne

var config = builder.Configuration;

// Ajout de la configuration de l'API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Ajout des contrôleurs
builder.Services.AddControllers();

// Configuration PostgreSQL avec NetTopologySuite
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(config["ConnectionString:DefaultConnection"]));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.EnsureCreated(); // Crée la base de données et les tables si elles n'existent pas
}

// Configuration de Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();
app.Run();
