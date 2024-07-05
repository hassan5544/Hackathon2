using Hackathon2.Models;

namespace Hackathon2.Repositories
{
    public interface IUserRepository
    {
        User GetUser(string username, string password);
        void AddUser(User user);
    }
}
