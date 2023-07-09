using B2CWebApp.Models;

namespace B2CWebApp.Repositories
{
    public interface IUserRepository
    {
        User findUserByUsernameAndPassword(string username, string password);
        void add(User user);
        User findUserById(long v);
    }
}
