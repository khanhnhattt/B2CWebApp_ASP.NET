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

        public User checkUserExist(LoginDTO loginDto)
        {
            User user = _userRepository.findUserByUsernameAndPassword(loginDto.Username, loginDto.Password);
            if (user == null) { return null; }
            return user;
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
