using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Collections.Generic;
using SaneServer.Server.Data;
using SaneServer.Server.Models;
using SaneServer.Server.DTO;
using static SaneServer.Server.DTO.ScannerOptions;


namespace SaneServer.Server.Services
{
    public class SaneService : ISaneService
    {
        private const String scanimage = "scanimage";
        private readonly ApplicationDbContext _db;
        private readonly IShellService _shell;

        public SaneService(ApplicationDbContext db, IShellService shell)
        {
            _db    = db; 
            _shell = shell;
        }

        public List<Scanner> AvailableScanners() 
        {
            List<Scanner> scanners = new List<Scanner>();
            List<string> cmdLineArgs = new List<string>() {
                "-f '{\"Id\":%i,\"Name\":\"%d\",\"FriendlyName\": \"%m\",\"Vendor\":\"%v\",\"Model\": \"%t\"}%n'"
            };

            var shellStd = _shell.Exec(scanimage, cmdLineArgs);

            if (!String.IsNullOrEmpty(shellStd.stdout) && 
                String.IsNullOrEmpty(shellStd.stderr) && 
                String.IsNullOrEmpty(shellStd.exception)) 
            {
                List<string> jsonList = shellStd.stdout.Split(new Char []{'\n'}).ToList();
                jsonList.ForEach(sline => { 
                    if(!String.IsNullOrEmpty(sline)) {
                        scanners.Add(JsonSerializer.Deserialize<Scanner>(sline));
                    }
                });
            }
            return scanners; 
        }

        public ScanResult ScanImage(ScanArgs args) 
        {
            ScanResult scanResult = new ScanResult();
            List<string> cmdLineArgs = new List<string>();

            if(!String.IsNullOrEmpty(args.device))
            {
                string staticPath     = "ScannedFiles/images";
                string outputLocation = Path.Combine(Directory.GetCurrentDirectory(), "ScannedFiles", "images");
                scanResult.scanId     = Guid.NewGuid().ToString();
                scanResult.fileStaticPath = $"/{staticPath}";
                string fileNameFilter     = string.Format("{0}-*.{1}", scanResult.scanId, args.format);
                string filePathPattern    = string.Format("{0}/{1}-%d.{2}", outputLocation, scanResult.scanId, args.format);

                cmdLineArgs.AddRange(new string[] {
                    string.Format("-d '{0}'", args.device),
                    string.Format("--source '{0}'", args.source),
                    string.Format("--format={0}", args.format),
                    string.Format("--resolution {0}", args.resolution),
                    //"-x 215 -y 297"
                });

                if (!String.IsNullOrEmpty(args.mode))
                {
                    cmdLineArgs.Add(string.Format("--mode '{0}'", args.mode));
                }

                if (args.source == ScanSource.FlatBed)
                {
                    cmdLineArgs.Add(string.Format("> {0}", filePathPattern.Replace("%d", "1")));
                }
                else 
                {
                    cmdLineArgs.Add(string.Format("--batch={0}", filePathPattern));
                }

                var shellStd  = _shell.Exec(scanimage, cmdLineArgs);
                Console.WriteLine($"OUTPUT {shellStd.stdout}");
                Console.WriteLine($"ERROR  {shellStd.stderr}");

                scanResult.fileNames = Directory.EnumerateFiles(outputLocation, fileNameFilter, SearchOption.AllDirectories)
                                                .Select(file => {
                                                    return Path.GetFileName(file);
                                                }).ToList();

                if((!String.IsNullOrEmpty(shellStd.stdout) && 
                    String.IsNullOrEmpty(shellStd.stderr) && 
                    String.IsNullOrEmpty(shellStd.exception)) || scanResult.fileNames.Count > 0)
                { 
                    Console.WriteLine(">>>>>>>> SUCCEED <<<<<<<<<<<<");
                    Console.WriteLine(shellStd.stdout);
                } 
                else
                {
                    Console.WriteLine(">>>>>>>> ERROR <<<<<<<<<<<<");
                    Console.WriteLine(shellStd.stderr);
                }
            }
            else {
                throw new Exception("Scanner device name is required to scan");
            }

            return scanResult;
        }

    }
}