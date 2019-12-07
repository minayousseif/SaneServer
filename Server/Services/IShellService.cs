namespace SaneServer.Server.Services
{
    public interface IShellService
    {
        ShellService.ShellStandardStream Exec(string cmd);
    } 
}