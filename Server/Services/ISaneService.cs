using System.Collections.Generic;
using SaneServer.Server.DTO;
using SaneServer.Server.Models;
using static SaneServer.Server.DTO.ScannerOptions;

namespace SaneServer.Server.Services
{
    public interface ISaneService
    {
         
        List<Scanner> AvailableScanners();
        ScanResult ScanImage(ScanArgs args);
    }
}