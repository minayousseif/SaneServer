using System;

namespace SaneServer.Server.DTO
{
    public class AuthResponse
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
        public string TokenUUID { get; set; }
        public DateTime TokenExpiration {get; set;}
    }
}