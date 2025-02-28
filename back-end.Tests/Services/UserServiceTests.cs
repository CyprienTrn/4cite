using Microsoft.EntityFrameworkCore;
using Xunit;
using back_end.Database;
using back_end.Services;
using back_end.Models;
using back_end.Enums;
using Microsoft.AspNetCore.Identity;
using FluentAssertions;
using Moq;

public class UserServiceTests : IDisposable
{
    private readonly ApplicationDbContext _context;
    private readonly UserService _userService;
    private readonly Mock<IPasswordHasher<User>> _passwordHasherMock;


    public UserServiceTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase") // Bd de test
            .Options;

        // Création d'un mock pour le hashage des mots de passe
        _passwordHasherMock = new Mock<IPasswordHasher<User>>();

        _context = new ApplicationDbContext(options);
        _userService = new UserService(_context, _passwordHasherMock.Object);

        _context.Database.EnsureCreated(); // S'assure que la bd et prête et à jour
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

    // Get all users tests
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

    // Create users tests
    [Fact]
    public void CreateUser_ShouldAddUserNotNull_WhenUserIsValid()
    {
        Guid guidUser = Guid.NewGuid();
        // Arrange
        var user = new User
        {
            Id = guidUser,
            Pseudo = "User1",
            Mail = "user1@hotmail.com",
            Password = "Password1",
            Role = RolesEnum.User
        };

        _passwordHasherMock.Setup(p => p.HashPassword(It.IsAny<User>(), It.IsAny<string>()))
                           .Returns("Password1");

        // Act
        _userService.CreateUser(user);

        // Assert
        var savedUser = _context.User.FirstOrDefault(u => u.Mail == "user1@hotmail.com");

        // Teste si le user n'est pas null
        savedUser.Should().NotBeNull();
    }

    [Fact]
    public void CreateUser_ShouldAddUserWithRightValues_WhenUserIsValid()
    {
        Guid guidUser = Guid.NewGuid();
        // Arrange
        var user = new User
        {
            Id = guidUser,
            Pseudo = "User1",
            Mail = "user1@hotmail.com",
            Password = "Password1",
            Role = RolesEnum.User
        };

        _passwordHasherMock.Setup(p => p.HashPassword(It.IsAny<User>(), It.IsAny<string>()))
                           .Returns("Password1");

        // Act
        _userService.CreateUser(user);

        // Assert
        var savedUser = _context.User.FirstOrDefault(u => u.Mail == "user1@hotmail.com");

        // Test des attributs du user
        savedUser.Id.Should().Be(guidUser);
        savedUser.Pseudo.Should().Be("User1");
        savedUser.Mail.Should().Be("user1@hotmail.com");
        savedUser.Password.Should().Be("Password1");
        savedUser.Role.Should().Be(RolesEnum.User);
    }

    [Fact]
    public void CreateUser_ShouldAddUserWithId_WhenUserIsValidAndWithoutId()
    {
        // Arrange
        var user = new User
        {
            Pseudo = "User1",
            Mail = "user1@hotmail.com",
            Password = "Password1",
            Role = RolesEnum.User
        };

        _passwordHasherMock.Setup(p => p.HashPassword(It.IsAny<User>(), It.IsAny<string>()))
                           .Returns("Password1");

        // Act
        _userService.CreateUser(user);

        // Assert
        var savedUser = _context.User.FirstOrDefault(u => u.Mail == "user1@hotmail.com");

        // Test si l'Id n'est pas null
        savedUser.Id.Should().NotBeEmpty();
        // Test si l'Id est bien de type Guid
        ((object)savedUser.Id).Should().BeOfType<Guid>();
    }

    [Fact]
    public void CreateUser_ShouldAddUserWithRoleUser_WhenUserIsValidAndWithoutRole()
    {
        // Arrange
        var user = new User
        {
            Pseudo = "User1",
            Mail = "user1@hotmail.com",
            Password = "Password1",
        };

        _passwordHasherMock.Setup(p => p.HashPassword(It.IsAny<User>(), It.IsAny<string>()))
                           .Returns("Password1");

        // Act
        _userService.CreateUser(user);

        // Assert
        var savedUser = _context.User.FirstOrDefault(u => u.Mail == "user1@hotmail.com");

        // Test que le rôle par défaut soit bien User
        savedUser.Role.Should().Be(RolesEnum.User);
    }

    [Fact]
    public void CreateUser_ShouldAddUserWithRoleUser_WhenUserIsValidAndWithABadRole()
    {
        // Arrange
        var user = new User
        {
            Pseudo = "User1",
            Mail = "user1@hotmail.com",
            Password = "Password1",
            Role = (RolesEnum)99999
        };

        _passwordHasherMock.Setup(p => p.HashPassword(It.IsAny<User>(), It.IsAny<string>()))
                           .Returns("Password1");

        // Act
        _userService.CreateUser(user);

        // Assert
        var savedUser = _context.User.FirstOrDefault(u => u.Mail == "user1@hotmail.com");

        // Test que le rôle par défaut soit bien User
        savedUser.Role.Should().Be(RolesEnum.User);
    }

    [Fact]
    public void CreateUser_ShouldAddUserWithRoleEmployee_WhenUserIsValidAndWithTheEmployeeRole()
    {
        // Arrange
        var user = new User
        {
            Pseudo = "User1",
            Mail = "user1@hotmail.com",
            Password = "Password1",
            Role = RolesEnum.Employee
        };

        _passwordHasherMock.Setup(p => p.HashPassword(It.IsAny<User>(), It.IsAny<string>()))
                           .Returns("Password1");

        // Act
        _userService.CreateUser(user);

        // Assert
        var savedUser = _context.User.FirstOrDefault(u => u.Mail == "user1@hotmail.com");

        // Test que le rôle par défaut soit bien User
        savedUser.Role.Should().Be(RolesEnum.Employee);
    }

    [Fact]
    public void CreateUser_ShouldAddUserWithRoleAdmin_WhenUserIsValidAndWithTheAdminRole()
    {
        // Arrange
        var user = new User
        {
            Pseudo = "User1",
            Mail = "user1@hotmail.com",
            Password = "Password1",
            Role = RolesEnum.Employee
        };

        _passwordHasherMock.Setup(p => p.HashPassword(It.IsAny<User>(), It.IsAny<string>()))
                           .Returns("Password1");

        // Act
        _userService.CreateUser(user);

        // Assert
        var savedUser = _context.User.FirstOrDefault(u => u.Mail == "user1@hotmail.com");

        // Test que le rôle par défaut soit bien User
        savedUser.Role.Should().Be(RolesEnum.Admin);
    }

    // Get user by id
    [Fact]
    public void GetUserById_ShouldReturnUser_WhenUserExists()
    {
        // Arrange
        var user = new User
        {
            Id = Guid.NewGuid(),
            Pseudo = "User1",
            Mail = "user1@hotmail.com",
            Password = "Password1"
        };
        _context.User.Add(user);
        _context.SaveChanges();

        // Act
        var result = _userService.GetUserById(user.Id);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(user.Id);
    }

    [Fact]
    public void GetUserById_ShouldThrowException_WhenUserDoesNotExist()
    {
        // Act 
        Guid guidUser = Guid.NewGuid();
        Action act = () => _userService.GetUserById(guidUser);

        // Assert
        act.Should().Throw<Exception>().WithMessage($"Utilisateur avec l'identifiant '{guidUser}' est introuvable");
    }
}
