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
        /**
        * Test de la création d'un utilisateur avec un rôle User
        */
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
            Assert.True(user.IsMailValid());
            Assert.Equal("JohnDoe", user.Pseudo);
            Assert.Equal("User", user.Role);

            var passwordHash = _passwordHasher.HashPassword(user, "password");
            Assert.True(_passwordHasher.VerifyHashedPassword(user, passwordHash, "password") == PasswordVerificationResult.Success);
        }

        [Fact]
        /**
        * Test de la création d'un utilisateur avec un rôle Employee
        */
        public void TestCreateUserEmployee()
        {
            User user = new()
            {
                Mail = "john.doe@gmail.com",
                Pseudo = "JohnDoe",
                Password = "password",
                Role = "Employee"
            };

            Assert.Equal("john.doe@gmail.com", user.Mail);
            Assert.True(user.IsMailValid());
            Assert.Equal("JohnDoe", user.Pseudo);
            Assert.Equal("Employee", user.Role);

            var passwordHash = _passwordHasher.HashPassword(user, "password");
            Assert.True(_passwordHasher.VerifyHashedPassword(user, passwordHash, "password") == PasswordVerificationResult.Success);
        }

        [Fact]
        /**
        * Test de la création d'un utilisateur avec un rôle Admin
        */
        public void TestCreateUserAdmin()
        {
            User user = new()
            {
                Mail = "john.doe@gmail.com",
                Pseudo = "JohnDoe",
                Password = "password",
                Role = "Admin"
            };

            Assert.Equal("john.doe@gmail.com", user.Mail);
            Assert.True(user.IsMailValid());
            Assert.Equal("JohnDoe", user.Pseudo);
            Assert.Equal("Admin", user.Role);

            var passwordHash = _passwordHasher.HashPassword(user, "password");
            Assert.True(_passwordHasher.VerifyHashedPassword(user, passwordHash, "password") == PasswordVerificationResult.Success);
        }

        [Fact]
        /**
        * Test de la création d'un utilisateur avec un rôle inconnu
        * Le rôle doit être User, Employee ou Admin
        * Sinon le rôle sera User
        */
        public void TestCreateUserRoleUnknown()
        {
            User user = new()
            {
                Mail = "john.doe@gmail.com",
                Pseudo = "JohnDoe",
                Password = "password",
                Role = "zefjoij"
            };

            Assert.Equal("john.doe@gmail.com", user.Mail);
            Assert.True(user.IsMailValid());
            Assert.Equal("JohnDoe", user.Pseudo);
            Assert.Equal("User", user.Role);

            var passwordHash = _passwordHasher.HashPassword(user, "password");
            Assert.True(_passwordHasher.VerifyHashedPassword(user, passwordHash, "password") == PasswordVerificationResult.Success);
        }

        [Fact]
        public void TestMailNotValid()
        {
            User user = new()
            {
                Mail = "john.doe",
                Pseudo = "JohnDoe",
                Password = "password",
                Role = "User"
            };

            // Mail sans point
            Assert.False(user.IsMailValid());

            // Mail sans @
            user.Mail = "joh.n@123";
            Assert.False(user.IsMailValid());

            // Mail vide
            user.Mail = "";
            Assert.False(user.IsMailValid());
        }
    }
}