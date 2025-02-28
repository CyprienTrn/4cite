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

    }
}