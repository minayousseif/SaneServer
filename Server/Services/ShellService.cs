using System;
using System.Diagnostics;

namespace SaneServer.Server.Services
{
    public class ShellService : IShellService
    {
        public class ShellStandardStream 
        {
            public string stdout;
            public string stderr;
            public string exception;
        }

        public ShellStandardStream Exec(string cmd) 
        {
            var escapedArgs = cmd.Replace("\"", "\\\"");
            ShellStandardStream shellStd = new ShellStandardStream();
            try {
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName  = "/bin/bash";
                startInfo.Arguments =  $"-c \"{escapedArgs}\"";
                startInfo.UseShellExecute = false;
                startInfo.CreateNoWindow  = true;
                startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                startInfo.RedirectStandardOutput = true;
                startInfo.RedirectStandardError  = true;
                using(Process process = Process.Start(startInfo)) 
                {
                    process.WaitForExit();
                    shellStd.stdout = process.StandardOutput.ReadToEnd();
                    shellStd.stderr = process.StandardError.ReadToEnd();
                }
            }
            catch(Exception ex)
            {
                shellStd.exception = ex.Message;
                return shellStd;
            }
            return shellStd;
        }
    }
}