using System;
using System.Collections.Generic;
using System.Linq;
using SaneServer.Server.DTO;
using SaneServer.Server.Models;
using SaneServer.Server.Data;
using SaneServer.Server.Validators;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace SaneServer.Server.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _db;
        private readonly ICustomPasswordValidator _passwordValidator;

        public UserService(ApplicationDbContext db, ICustomPasswordValidator passwordValidator) 
        {
            _db = db;
            _passwordValidator = passwordValidator;
        }

        public bool Authenticate(string username, string password) {
            var user = _db.Users.Where(u => u.UserName == username).FirstOrDefault();
            if (user == null) { 
                return false; 
            }
            var passwordHasher = new PasswordHasher<string>();
            bool verified = passwordHasher.VerifyHashedPassword(username, user.PasswordHash, password) == PasswordVerificationResult.Success;
            if(verified) {
                return true;
            }

            return false;
        }
    }
}