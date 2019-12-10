using SaneServer.Server.DTO;
using SaneServer.Server.Models;

namespace SaneServer.Server.Services
{
    public interface IUserService
    {
        UserResponse Authenticate(string username, string password);
        string GenerateJwtToken(User user);
    }
}