using System;
using SaneServer.Server.DTO;
using SaneServer.Server.Models;

namespace SaneServer.Server.Helpers
{
    public static class UserExtensionMethods
    {
        public static AuthResponse MapUserResponse(this User user) => 
            new AuthResponse {
                UserName = user.UserName,
                Email = user.Email,
                Role = user.RoleString
            };
    }
}