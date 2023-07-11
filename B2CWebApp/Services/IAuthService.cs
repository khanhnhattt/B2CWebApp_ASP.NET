using B2CWebApp.Models;
using B2CWebApp.Models.ViewModel;

namespace B2CWebApp.Services
{
    public interface IAuthService
    {
        User checkUserExist(LoginDTO loginDto);
        string generateToken(User user);
        string addUser(User user);
    }
}
