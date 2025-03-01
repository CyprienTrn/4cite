using back_end.Controllers;
using back_end.Services;
using back_end.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace back_end.Tests.Controllers
{
    public class UserControllerTests
    {
        private readonly UserController _controller;
        private readonly Mock<UserService> _mockService = new Mock<UserService>();

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
    }
}
