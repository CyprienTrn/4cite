using back_end.Database;
using back_end.Models;
using Microsoft.AspNetCore.Identity;

namespace back_end.Services
{
    public class UserService
    {
        private readonly ApplicationDbContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;

        public UserService(ApplicationDbContext context, IPasswordHasher<User> passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        public List<User> GetAllUsers()
        {
            try
            {
                return _context.User.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de la récupération des utilisateurs", ex);
            }
        }

        public User GetUserById(Guid id)
        {
            var user = _context.User.Find(id);

            if (user == null)
            {
                throw new Exception($"Utilisateur avec l'identifiant '{id}' est introuvable");
            }

            return user;
        }

        public User CreateUser(User user)
        {
            try
            {
                var hashedPassword = _passwordHasher.HashPassword(user, user.Password);
                user.Password = hashedPassword;

                _context.User.Add(user);
                _context.SaveChanges();

                return user;
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de la création de l'utilisateur", ex);
            }
        }

        public void DeleteUser(Guid id)
        {
            var user = _context.User.Find(id);

            if (user == null)
            {
                throw new Exception($"Utilisateur avec l'identifiant '{id}' est introuvable");
            }

            try
            {
                _context.User.Remove(user);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de la suppression de l'utilisateur", ex);
            }
        }

        public User UpdateUser(Guid id, User? user)
        {
            if (user == null)
            {
                throw new Exception("L'utilisateur à mettre à jour est inexistant");
            }

            var userToUpdate = _context.User.Find(id);

            if (userToUpdate == null)
            {
                throw new Exception($"Utilisateur avec l'identifiant '{id}' est introuvable");
            }

            try
            {
                userToUpdate.Pseudo = user.Pseudo;
                userToUpdate.Mail = user.Mail;
                userToUpdate.Role = user.Role;
                userToUpdate.Password = user.Password;

                _context.SaveChanges();

                return userToUpdate;
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de la mise à jour de l'utilisateur", ex);
            }
        }
    }
}