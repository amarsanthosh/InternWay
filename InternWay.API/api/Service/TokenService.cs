using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using api.Interface;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace api.Service
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;
        private readonly SymmetricSecurityKey _key;
        private readonly UserManager<AppUser> _userManager;
        public TokenService(IConfiguration config, UserManager<AppUser> userManager)
        {
            _config = config;
            _userManager = userManager;
            _key = new SymmetricSecurityKey(
                System.Text.Encoding.UTF8.GetBytes(_config["JWT:SigningKey"]!)
            );

        }

        //JWT Token{Access token} -> short time
        public async Task<string> CreateToken(AppUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.GivenName, user.UserName!),
                new Claim(JwtRegisteredClaimNames.Email, user.Email!)
            };
            var userRoles = await _userManager.GetRolesAsync(user);
            // Add role claims
            foreach (var role in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);//“Use this secret key and this algorithm to lock (sign) the token.”
                                                                                             //Think of this as filling out a passport form: Who are you, when does it expire, who’s issuing it, etc.
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds,
                Issuer = _config["JWT:Issuer"],
                Audience = _config["JWT:Audience"]
            };

            //Generate the token
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            //writeToken -> converts the token as a String
            return tokenHandler.WriteToken(token);

        }
        //Refresh Token ->  long time
        public RefreshToken GenerateRefreshToken(string ipAddress)
        {
            return new RefreshToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Created = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddDays(7),
                CreatedByIp = ipAddress
            };
        }
    }
}