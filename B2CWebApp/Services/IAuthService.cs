using B2CWebApp.DTOs;
using B2CWebApp.Models;

namespace B2CWebApp.Services
{
    public interface IAuthService
    {
        User checkUserExist(LoginDTO loginDto);
        string generateToken(User user);
        string addUser(User user);
    }
}
