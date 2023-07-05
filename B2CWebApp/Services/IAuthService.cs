using B2CWebApp.DTOs;
using B2CWebApp.Models;

namespace B2CWebApp.Services
{
    public interface IAuthService
    {
        bool checkUserExist(LoginDTO loginDto);
        string generateToken(string username);
        string addUser(User user);
    }
}
