using System.Collections.Generic;

namespace SaneServer.Server.Services
{
    public interface IShellService
    {
        ShellService.ShellStandardStream Exec(string cmd, List<string> args);
    } 
}