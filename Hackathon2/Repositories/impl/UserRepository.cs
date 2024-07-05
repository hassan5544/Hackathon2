using Hackathon2.Data;
using Hackathon2.Models;
using Microsoft.IdentityModel.Tokens;
using BCrypt;


namespace Hackathon2.Repositories.impl
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }
        public void AddUser(User user)
        {
            ValidateUserInput(user);
            CheckIfUserExists(user);
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

            _context.Users.Add(user);
            _context.SaveChanges();
        }

        private void ValidateUserInput(User baseEo)
        {
            if(baseEo == null || baseEo.Username.IsNullOrEmpty() || baseEo.Password.IsNullOrEmpty())
            {
                throw new ArgumentException("Invalid UserName or Password Input");
            }
        }

        private void CheckIfUserExists(User baseEo)
        {
            var result = _context.Users.SingleOrDefault(u => u.Username == baseEo.Username);
            if(result != null) 
            {
                throw new ArgumentException("User Name already taken");
            }
        }

        public User GetUser(string username, string password)
        {
            var result =  _context.Users.SingleOrDefault(u => u.Username == username);
            if(result != null && BCrypt.Net.BCrypt.Verify(password, result.Password))
            {
                return result;
            }
            if(result == null)
            {
                throw new Exception("User is not Registered");
            }
            return result;
        }
    }
}
