using Back_end.Models;
using Microsoft.AspNetCore.Identity;

namespace Back_end.Tests.Models
{
    public class UserTests
    {
        private readonly IPasswordHasher<User>? _passwordHasher;

        public UserTests()
        {
            // Va permettre de hasher le mot de passe et de les tester
            _passwordHasher = new PasswordHasher<User>();
        }

        [Fact]
        public void TestCreateUserDefault()
        {

            User user = new()
            {
                Mail = "john.doe@gmail.com",
                Pseudo = "JohnDoe",
                Password = "password",
                Role = "User"
            };

            Assert.Equal("john.doe@gmail.com", user.Mail);
            Assert.Equal("JohnDoe", user.Pseudo);
            Assert.Equal("User", user.Role);

            var passwordHash = _passwordHasher.HashPassword(user, "password");
            Assert.True(_passwordHasher.VerifyHashedPassword(user, passwordHash, "password") == PasswordVerificationResult.Success);
        }
    }
}