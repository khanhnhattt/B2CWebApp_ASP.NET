using B2CWebApp.Models;
using B2CWebApp.Repositories.Impl;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using B2CWebApp.Repositories;
using Microsoft.AspNetCore.Identity;
using B2CWebApp.Models.ViewModel;

namespace B2CWebApp.Services.Impl
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;
        private readonly B2cContext _context = new B2cContext();

        public AuthService(IConfiguration configuration, IUserRepository userRepository)
        {
            _configuration = configuration;
            _userRepository = userRepository;
        }

        public string generateToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _configuration["JwtSettings:ValidIssuer"],
                _configuration["JwtSettings:ValidAudience"],
                null, 
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string addUser(User user)
        {
            User checkUsername = _context.Users.FirstOrDefault(u => u.Username == user.Username);
            if (checkUsername != null)
            {
                return "Username duplicated";
            }

            User checkMail = _context.Users.FirstOrDefault(u => u.Email == user.Email);
            if (checkMail != null)
            {
                return "Email duplicated";
            }

            try
            {
                _userRepository.add(user);
                _context.UserRoles.Add(new UserRole()
                {
                    RoleId = 2,
                    UserId = user.Id
                });
                _context.SaveChanges();
                return null;
            }
            catch (Exception e)
            {
                return "Register Error: "+e;
            }


        }

        public User checkUserExist(LoginDTO loginDto)
        {
            User user = _userRepository.findUserByUsernameAndPassword(loginDto.Username, loginDto.Password);
            if (user == null) { return null; }
            return user;
        }
    }
}
