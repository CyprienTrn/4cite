using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using back_end.Services;
using back_end.Database;
using FluentAssertions;
using back_end.Models;
using Moq;

namespace back_end.Tests.Services
{
    public class UserServiceTests
    {
        private readonly UserService _userService;
        private readonly ApplicationDbContext _context;
        private readonly Mock<IPasswordHasher<User>> _passwordHasherMock;


        public UserServiceTests()
        {
            // Configuration en mémoire d'EFCore pour simuler une DB
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            // Création d'un mock pour le hashage des mots de passe
            _passwordHasherMock = new Mock<IPasswordHasher<User>>();

            _context = new ApplicationDbContext(options);
            _userService = new UserService(_context, _passwordHasherMock.Object);
        }

        [Fact]
        public void GetAllUsers_ShouldReturnEmptyList_WhenNoUsersExist()
        {
            // Act
            var users = _userService.GetAllUsers();

            // Assert
            users.Should().BeEmpty();
        }
    }
}