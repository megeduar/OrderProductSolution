using Application.Common.Interface;
using Domain.Identity.Response;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Persistence.Identity;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity
{
    public class UserManagerServices : IUserManager
    {
        private readonly UserManager<User> _userManager;
        // private readonly JwtConfig _jwtConfig;

        public UserManagerServices(UserManager<User> userManager)
        {
            _userManager = userManager;
            // _jwtConfig = jwtConfig;
        }

        public async Task<AuthResult> Authenticate(string userName, string password)
        {
            var user = await _userManager.FindByNameAsync(userName);

            if (user == null)
            {
                return AuthResult.Failure(
                    new List<string>() 
                    { 
                        "Invalid authentication request" 
                    });
            }

            var isCorrect = await _userManager.CheckPasswordAsync(user, password);

            if (isCorrect)
            {
                var jwtToken = GenerateJwtToken(user);

                return AuthResult.ResponseToken(jwtToken);
            }
            else
            {
                return AuthResult.Failure(
                   new List<string>()
                   {
                        "Invalid authentication request"
                   });
            }
        }

        private string GenerateJwtToken(IdentityUser user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.UTF8.GetBytes("ijurkbdlhmklqacwqzdxmkkhvqowlyqa");

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim("Id", user.Id),
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);

            var jwtToken = jwtTokenHandler.WriteToken(token);

            return jwtToken;
        }
    }
}
