using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace SaneServer.Server.Services
{
    public class ShellService : IShellService
    {
        public class ShellStandardStream 
        {
            public int exitCode {get; set;}
            public bool Success { get { return this.exitCode == 0; } }
            public string stdout {get; set;}
            public string stderr {get; set;}
            public string exception {get; set;}
        }

        public ShellStandardStream Exec(string cmd, List<string> args = null) 
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) {
                throw new Exception("Executing a process from bash is not supported on this OS!");
            }
            ShellStandardStream shellStd = new ShellStandardStream();
            try {
                ProcessStartInfo startInfo = new ProcessStartInfo {
                    FileName  = "/bin/bash",
                    Arguments = $"-c \"{cmd} {CmdLineArgs(args)}\"",
                    UseShellExecute = false,
                    CreateNoWindow  = true,
                    WindowStyle = ProcessWindowStyle.Hidden,
                    RedirectStandardOutput = true,
                    RedirectStandardError  = true
                };
                using(Process process = Process.Start(startInfo)) 
                {
                    process.WaitForExit();
                    shellStd.stdout = process.StandardOutput.ReadToEnd();
                    shellStd.stderr = process.StandardError.ReadToEnd();
                    shellStd.exitCode = process.ExitCode;
                }
            }
            catch(Exception ex)
            {
                shellStd.exception = ex.Message;
                return shellStd;
            }
            return shellStd;   
        }

        private string CmdLineArgs (List<string> args) {
            if (args != null)
            {
                StringBuilder argsString = new StringBuilder();
                args.ForEach(arg => {
                    if (arg == null)
                    {
                        throw new Exception("Shell arguments can not contain null");
                    }
                    argsString.Append(Convert.ToString(arg, CultureInfo.InvariantCulture));
                    argsString.Append(" ");
                });
                // escape chars and trim the string
                return argsString.ToString().Replace("\"", "\\\"").Trim();
            }
            return string.Empty;
        }
  }
}