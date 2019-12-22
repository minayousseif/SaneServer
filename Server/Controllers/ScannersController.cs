using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SaneServer.Server.Models;
using SaneServer.Server.DTO;
using SaneServer.Server.Services;
using System;
using static SaneServer.Server.DTO.ScannerOptions;
using System.IO;
using SaneServer.Server.Helpers;

namespace SaneServer.Server.Controllers {
  
  [ApiController]
  [Route("[controller]")]
  public class ScannersController : ControllerBase {
    private readonly ISaneService _saneService;
    public ScannersController(ISaneService saneService)
    {
        _saneService = saneService;
    }

    [HttpGet("[action]")]
    public IActionResult GetScanners() {
      List<Scanner> scanners = _saneService.AvailableScanners();
      return Ok(scanners);
    }

    [HttpGet("[action]")]
    public IActionResult ScanImage() {
      ScanResult results = _saneService.ScanImage(new ScanArgs() {
        device = "brother4:net1;dev0",
        source = ScanSource.FlatBed,
        mode   = ScanMode.Color
      });
      return Ok(results);
    }

    [HttpGet("[action]/{fileType}/{fileName}")]
    public IActionResult GetScannedFile(string fileType, string fileName) {
      string file = Path.Combine(StaticFiles.scannedFilesDir, fileType, fileName);
      string extension = Path.GetExtension(fileName).Replace(".", "");
      string contentType = (fileType == "images") ? $"image/{extension}": "application/pdf";
      return PhysicalFile(file, contentType);
    }

  }
}