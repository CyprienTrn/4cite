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
            return [];
        }
    }
}