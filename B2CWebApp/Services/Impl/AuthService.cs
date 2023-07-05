using B2CWebApp.DTOs;
using B2CWebApp.Models;
using B2CWebApp.Repositories.Impl;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using B2CWebApp.Repositories;
using Microsoft.AspNetCore.Identity;

namespace B2CWebApp.Services.Impl
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;

        public AuthService(IConfiguration configuration, IUserRepository userRepository)
        {
            _configuration = configuration;
            _userRepository = userRepository;
        }

        public bool checkUserExist(LoginDTO loginDto)
        {
            User user = _userRepository.findUserByUsernameAndPassword(loginDto.Username, loginDto.Password);
            if (user == null) { return false; }
            return true;
        }

        public string generateToken(string username)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var secretKey = Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, username)
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public string addUser(User user)
        {
            try
            {
                _userRepository.add(user);
                return null;
            }
            catch (Exception e)
            {
                return "Register Error";
            }


        }
    }
}
