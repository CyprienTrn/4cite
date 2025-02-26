using back_end.Models;
using back_end.Enums;
using Microsoft.AspNetCore.Identity;

namespace back_end.Tests.Models
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
                Role = RolesEnum.User
            };

            Assert.Equal("john.doe@gmail.com", user.Mail);
            Assert.True(User.IsMailValid(user.Mail));
            Assert.Equal("JohnDoe", user.Pseudo);
            Assert.Equal(RolesEnum.User, user.Role);

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
                Role = RolesEnum.Employee
            };

            Assert.Equal("john.doe@gmail.com", user.Mail);
            Assert.True(User.IsMailValid(user.Mail));
            Assert.Equal("JohnDoe", user.Pseudo);
            Assert.Equal(RolesEnum.Employee, user.Role);

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
                Role = RolesEnum.Admin
            };

            Assert.Equal("john.doe@gmail.com", user.Mail);
            Assert.True(User.IsMailValid(user.Mail));
            Assert.Equal("JohnDoe", user.Pseudo);
            Assert.Equal(RolesEnum.Admin, user.Role);

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
                Role = (RolesEnum)99999 // On simule un rôle inconnu
            };

            Assert.Equal("john.doe@gmail.com", user.Mail);
            Assert.True(User.IsMailValid(user.Mail));
            Assert.Equal("JohnDoe", user.Pseudo);
            Assert.Equal(RolesEnum.User, user.Role);

            var passwordHash = _passwordHasher.HashPassword(user, "password");
            Assert.True(_passwordHasher.VerifyHashedPassword(user, passwordHash, "password") == PasswordVerificationResult.Success);
        }

        [Fact]
        /**
        * Test de la création d'un utilisateur sans spécifier de rôle
        * Le rôle doit être User
        */
        public void TestCreateUserWithoutSpecifyingRole()
        {
            User user = new()
            {
                Mail = "john.doe@gmail.com",
                Pseudo = "JohnDoe",
                Password = "password"
            };

            Assert.Equal("john.doe@gmail.com", user.Mail);
            Assert.True(User.IsMailValid(user.Mail));
            Assert.Equal("JohnDoe", user.Pseudo);
            Assert.Equal(RolesEnum.User, user.Role);

            var passwordHash = _passwordHasher.HashPassword(user, "password");
            Assert.True(_passwordHasher.VerifyHashedPassword(user, passwordHash, "password") == PasswordVerificationResult.Success);
        }

        [Fact]
        /**
        * Test de la création d'un utilisateur avec un mail non valide
        */
        public void TestMailNotValid()
        {
            User user = new()
            {
                Mail = "john.doe",
                Pseudo = "JohnDoe",
                Password = "password",
                Role = RolesEnum.User
            };

            // Mail sans point
            Assert.False(User.IsMailValid(user.Mail)); ;

            // Mail sans @
            user.Mail = "joh.n@123";
            Assert.False(User.IsMailValid(user.Mail)); ;

            // Mail vide
            user.Mail = "";
            Assert.False(User.IsMailValid(user.Mail)); ;
        }
    }
}