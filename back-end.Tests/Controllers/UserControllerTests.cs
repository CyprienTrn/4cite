using back_end.Controllers;
using back_end.Interfaces;
using back_end.Enums;
using back_end.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace back_end.Tests.Controllers
{
    public class UserControllerTests
    {
        private readonly UserController _controller;
        private readonly Mock<IUserService> _mockService = new Mock<IUserService>();

        public UserControllerTests()
        {
            _controller = new UserController(_mockService.Object);
        }

        // ==========================================
        //          Get all users routes
        // ==========================================

        /**
        * Teste si la méthode GetAllUsers retourne une liste de json d'utilisateurs
        */
        [Fact]
        public void GetAllUsers_ReturnsListOfUsers()
        {
            // Arrange
            var users = new List<User>
            {
                new() { Mail = "user1@hotmail.com", Pseudo = "user1", Password = "password1" },
                new() { Mail = "user2@hotmail.com", Pseudo = "user2", Password = "password2" }
            };

            _mockService.Setup(service => service.GetAllUsers()).Returns(users);

            // Act
            var result = _controller.GetAllUsers();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<User>>(okResult.Value);
            Assert.Equal(users.Count, returnValue.Count);

            _mockService.Verify(service => service.GetAllUsers(), Times.Once);
        }

        /**
        * Teste si la méthode GetAllUsers retourne un message d'erreur si aucun utilisateur n'est trouvé
        */
        [Fact]
        public void GetAllUsers_ReturnsNotFound()
        {
            // Arrange
            _mockService.Setup(service => service.GetAllUsers()).Returns(new List<User>());

            // Act
            var result = _controller.GetAllUsers();

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("Aucun utilisateur trouvé.", notFoundResult.Value);

            _mockService.Verify(service => service.GetAllUsers(), Times.Once);
        }

        /**
        * Teste si la méthode GetAllUsers retourne un message d'erreur si une exception est levée
        */
        [Fact]
        public void GetAllUsers_ReturnsInternalServerError()
        {
            // Arrange
            _mockService.Setup(service => service.GetAllUsers()).Throws(new Exception("Erreur interne"));

            // Act
            var result = _controller.GetAllUsers();

            // Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, statusCodeResult.StatusCode);
            Assert.Equal("Erreur interne : Erreur interne", statusCodeResult.Value);

            _mockService.Verify(service => service.GetAllUsers(), Times.Once);
        }

        // ==========================================
        //          Get user by ID routes
        // ==========================================

        /**
        * Teste si la méthode GetUserById retourne un utilisateur
        */
        [Fact]
        public void GetUserById_ReturnsUser()
        {
            // Arrange
            var id = Guid.NewGuid();
            var user = new User
            {
                Id = id,
                Mail = "user1@hotmail.com",
                Pseudo = "user1",
                Password = "password1"
            };

            _mockService.Setup(service => service.GetUserById(id)).Returns(user);

            // Act
            var result = _controller.GetUserById(id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<User>(okResult.Value);
            Assert.Equal(user, returnValue);

            _mockService.Verify(service => service.GetUserById(id), Times.Once);
        }

        /**
        * Teste si la méthode GetUserById retourne un message d'erreur si l'utilisateur n'est pas trouvé
        */
        [Fact]
        public void GetUserById_ReturnsNotFound()
        {
            // Arrange
            var id = Guid.NewGuid();

            _mockService.Setup(service => service.GetUserById(id)).Returns((User)null);

            // Act
            var result = _controller.GetUserById(id);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal($"Utilisateur avec l'ID {id} introuvable.", notFoundResult.Value);

            _mockService.Verify(service => service.GetUserById(id), Times.Once);
        }

        /**
        * Teste si la méthode GetUserById retourne un message d'erreur si une exception est levée
        */
        [Fact]
        public void GetUserById_ReturnsInternalServerError()
        {
            // Arrange
            var id = Guid.NewGuid();

            _mockService.Setup(service => service.GetUserById(id)).Throws(new Exception("Erreur interne"));

            // Act
            var result = _controller.GetUserById(id);

            // Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, statusCodeResult.StatusCode);
            Assert.Equal("Erreur interne : Erreur interne", statusCodeResult.Value);

            _mockService.Verify(service => service.GetUserById(id), Times.Once);
        }

        // ==========================================
        //          Create user routes
        // ==========================================

        /**
        * Teste si la méthode CreateUser retourne un utilisateur
        */
        [Fact]
        public void CreateUser_ReturnsUser()
        {
            // Arrange
            User user = new User
            {
                Id = Guid.NewGuid(),
                Pseudo = "User1",
                Mail = "user1@hotmail.com",
                Password = "Password1"
            };

            _mockService.Setup(service => service.CreateUser(user)).Returns(user);

            // Act
            var result = _controller.CreateUser(user);

            // Assert
            var okResult = Assert.IsType<ObjectResult>(result);
            var returnUser = Assert.IsType<User>(okResult.Value);
            Assert.Equal(user, returnUser);

            _mockService.Verify(service => service.CreateUser(user), Times.Once);
        }


        /**
        * Teste si la méthode CreateUser retourne un utilisateur avec les bons attributs spécifiés
        */
        [Fact]
        public void CreateUser_ReturnsUserWithRightSpecifiedAttributes()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            User user = new User
            {
                Id = id,
                Pseudo = "User1",
                Mail = "user1@hotmail.com",
                Password = "Password1"
            };

            _mockService.Setup(service => service.CreateUser(user)).Returns(user);

            // Act
            var result = _controller.CreateUser(user);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnUser = Assert.IsType<User>(okResult.Value);

            // Teste si les informations de l'utilisateur sont correctes
            Assert.Equal(id, returnUser.Id);
            Assert.Equal("User1", returnUser.Pseudo);
            Assert.Equal("user1@hotmail.com", returnUser.Mail);
            Assert.Equal(RolesEnum.User, returnUser.Role);
            // Teste si le mot de passe n'est pas en clair
            Assert.NotEqual("Password1", returnUser.Password);

            _mockService.Verify(service => service.CreateUser(user), Times.Once);
        }

        /**
        * Teste si la méthode CreateUser retourne un utilisateur avec le bon rôle spécifié
        */
        [Fact]
        public void CreateUser_ReturnsUserWithRightSpecifiedRole()
        {
            // Arrange
            User user = new User
            {
                Id = Guid.NewGuid(),
                Pseudo = "User1",
                Mail = "user1@hotmail.com",
                Password = "Password1",
                Role = RolesEnum.Admin
            };

            _mockService.Setup(service => service.CreateUser(user)).Returns(user);

            // Act
            var result = _controller.CreateUser(user);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnUser = Assert.IsType<User>(okResult.Value);

            // Teste si le rôle de l'utilisateur est correct
            Assert.Equal(RolesEnum.Admin, returnUser.Role);

            _mockService.Verify(service => service.CreateUser(user), Times.Once);
        }

        /**
        * Teste si la méthode CreateUser retourne un message d'erreur si les données de l'utilisateur sont manquantes
        */
        [Fact]
        public void CreateUser_ReturnsBadRequestWhenUserIsNull()
        {
            // Arrange
            User user = null;

            // Act
            var result = _controller.CreateUser(user);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Les données de l'utilisateur sont manquantes.", badRequestResult.Value);

            _mockService.Verify(service => service.CreateUser(user), Times.Never);
        }

        /**
        * Teste si la méthode CreateUser retourne un message d'erreur si une exception est levée
        */
        [Fact]
        public void CreateUser_ReturnsInternalServerError()
        {
            // Arrange
            User user = new User
            {
                Id = Guid.NewGuid(),
                Pseudo = "User1",
                Mail = "user1@hotmail.com",
                Password = "Password1"
            };

            _mockService.Setup(service => service.CreateUser(user)).Throws(new Exception("Erreur interne"));

            // Act
            var result = _controller.CreateUser(user);

            // Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, statusCodeResult.StatusCode);
            Assert.Equal("Erreur interne : Erreur interne", statusCodeResult.Value);

            _mockService.Verify(service => service.CreateUser(user), Times.Once);
        }
    }
}
