using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using SaneServer.Server.DTO;
using SaneServer.Server.Models;
using SaneServer.Server.Data;
using SaneServer.Server.Validators;
using Microsoft.AspNetCore.Identity;
using SaneServer.Server.Helpers;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Options;

namespace SaneServer.Server.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _db;
        private readonly ICustomPasswordValidator _passwordValidator;

        private readonly AppSettings _appSettings;

        public UserService(
            ApplicationDbContext db, 
            ICustomPasswordValidator passwordValidator,
            IOptions<AppSettings> appSettings
        ) 
        {
            _db = db;
            _passwordValidator = passwordValidator;
            _appSettings = appSettings.Value;
        }

        public AuthResponse Authenticate(string username, string password) {

            var user = _db.Users.Where(u => u.UserName == username).FirstOrDefault();
            
            if (user == null) {
                return null;
            }

            var passwordHasher = new PasswordHasher<string>();
            var results = passwordHasher.VerifyHashedPassword(username, user.PasswordHash, password);

            if(results != PasswordVerificationResult.Success) {
                return null;
            }
            var authResp = user.MapUserResponse();
            var tokenExpiration = DateTime.UtcNow.AddDays(1);
            authResp.Token = GenerateJwtToken(user, tokenExpiration);
            authResp.TokenUUID = Guid.NewGuid().ToString();
            authResp.TokenExpiration = tokenExpiration;
            
            return authResp;
        }

        public string GenerateJwtToken(User user, DateTime tokenExpiration) {
            var secretKey = Encoding.ASCII.GetBytes("");
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] 
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Role, user.RoleString)
                }),
                Expires = tokenExpiration,
                SigningCredentials = new SigningCredentials
                    (new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_appSettings.Secret)), 
                    SecurityAlgorithms.HmacSha512Signature
                )
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}