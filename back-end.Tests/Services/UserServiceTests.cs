using back_end.Services;
using Microsoft.EntityFrameworkCore;
using back_end.Database;

namespace back_end.Tests.Services
{
    public class UserServiceTests
    {
        private readonly UserService _userService;
        private readonly ApplicationDbContext _context;

        public UserServiceTests()
        {
            // Configuration en mémoire d'EFCore pour simuler une DB
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new ApplicationDbContext(options);
            _userService = new UserService();
        }

        [Fact]
        public void Test1()
        {

        }
    }
}