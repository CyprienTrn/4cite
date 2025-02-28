using Microsoft.EntityFrameworkCore;
using Xunit;
using back_end.Database;
using back_end.Services;
using back_end.Models;
using Microsoft.AspNetCore.Identity;
using FluentAssertions;

public class UserServiceTests : IDisposable
{
    private readonly ApplicationDbContext _context;
    private readonly UserService _userService;

    public UserServiceTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase") // 🔹 Base en mémoire
            .Options;

        _context = new ApplicationDbContext(options);
        _userService = new UserService(_context, new PasswordHasher<User>());

        _context.Database.EnsureCreated(); // 🔹 S'assure que la base est prête
    }

    /**
    * Nettoie la base après chaque test
    * car IDisposable est implémenté
    */
    public void Dispose()
    {
        _context.Database.EnsureDeleted(); // 🔹 Nettoie après chaque test
        _context.Dispose();
    }

    [Fact]
    public void GetAllUsers_ShouldReturnAllUsers_WhenUsersExist()
    {
        // Arrange
        var user1 = new User { Pseudo = "User1", Mail = "user1@hotmail.com", Password = "Password1" };
        var user2 = new User { Pseudo = "User2", Mail = "user2@hotmail.com", Password = "Password2" };

        _context.User.Add(user1);
        _context.User.Add(user2);
        _context.SaveChanges();

        // Act
        var users = _userService.GetAllUsers();

        // Assert
        users.Should().HaveCount(2);
    }

    [Fact]
    public void GetAllUsers_ShouldReturnEmptyList_WhenNoUsersExist()
    {
        // Act
        var users = _userService.GetAllUsers();

        // Assert
        users.Should().BeEmpty();
    }

    [Fact]
    public void GetAllUsers_ShouldNotReturnEmptyList_WhenUsersExist()
    {
        // Arrange
        var user1 = new User { Pseudo = "User1", Mail = "user1@hotmail.com", Password = "Password1" };

        _context.User.Add(user1);
        _context.SaveChanges();

        // Act
        var users = _userService.GetAllUsers();

        // Assert
        users.Should().NotBeEmpty();
    }
}
