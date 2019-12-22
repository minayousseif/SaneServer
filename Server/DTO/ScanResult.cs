using System.Collections.Generic;

namespace SaneServer.Server.DTO
{
    public class ScanResult
    {
        public string scanId {get; set;}
        public string fileStaticPath {get; set;}
        public List<string> fileNames {get; set;}
    }
}