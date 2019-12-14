using System;
using SaneServer.Server.DTO;
using SaneServer.Server.Models;

namespace SaneServer.Server.Services
{
    public interface IUserService
    {
        AuthResponse Authenticate(string username, string password);
        string GenerateJwtToken(User user, DateTime tokenExpiration);
    }
}