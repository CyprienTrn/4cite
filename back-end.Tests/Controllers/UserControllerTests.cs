using back_end.Controllers;
using back_end.Services;
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
    }
}
