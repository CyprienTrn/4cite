// using back_end.Database;
// using Microsoft.EntityFrameworkCore;
// using back_end.Services;

// var builder = WebApplication.CreateBuilder(args);

// builder.Services.AddScoped<UserService>();

// var config = builder.Configuration;

// // Add services to the container.
// // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();

// // Configuration PostgreSQL avec NetTopologySuite
// builder.Services.AddDbContext<ApplicationDbContext>(options =>
//     options.UseNpgsql(config["ConnectionString:DefaultConnection"]));

// var app = builder.Build();

// using (var scope = app.Services.CreateScope())
// {
//     var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
//     dbContext.Database.EnsureCreated(); // Crée la base de données et les tables si elles n'existent pas
// }

// // Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }

// app.UseHttpsRedirection();

// app.Run();


using back_end.Database;
using Microsoft.EntityFrameworkCore;
using back_end.Services;
using back_end.Models;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>(); // Ajout de cette ligne

var config = builder.Configuration;

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Ajoutez cette ligne pour les contrôleurs
builder.Services.AddControllers();  // Ajout nécessaire

// Configuration PostgreSQL avec NetTopologySuite
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(config["ConnectionString:DefaultConnection"]));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.EnsureCreated(); // Crée la base de données et les tables si elles n'existent pas
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Cette ligne permet d'utiliser les contrôleurs
app.MapControllers();

app.Run();
