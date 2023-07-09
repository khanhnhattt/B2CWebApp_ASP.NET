using B2CWebApp.Models;

namespace B2CWebApp.Repositories.Impl
{
    public class UserRepository : IUserRepository
    {
        private readonly B2cContext _context = new B2cContext();

        public UserRepository()
        {
        }

        public User findUserByUsernameAndPassword(string username, string password)
        {
            User user = _context.Users.First(u => u.Username == username);
            if (user != null && password.Equals(user.Password))
            {
                return user;
            }
            else
            {
                return null;
            }
        }

        public void add(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public User findUserById(long v)
        {
            User user = _context.Users.FirstOrDefault(u => u.Id == v);
            if (user != null)
            {
                return user;
            }
            return null;
        }
    }
}
