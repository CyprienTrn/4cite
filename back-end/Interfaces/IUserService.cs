using back_end.Models;

namespace back_end.Interfaces
{
    public interface IUserService
    {
        List<User> GetAllUsers();
        User GetUserById(Guid id);
        User CreateUser(User user);
    }
}