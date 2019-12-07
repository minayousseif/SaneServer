namespace SaneServer.Server.Services
{
    public interface IUserService
    {
        bool Authenticate(string username, string password);
    }
}