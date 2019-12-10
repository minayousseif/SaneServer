using SaneServer.Server.Models;

namespace SaneServer.Server.DTO
{
    public class UserResponse
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
    }
}