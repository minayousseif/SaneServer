using System.IO;
using System.Collections.Generic;

namespace SaneServer.Server.Helpers
{
  public class StaticFiles 
  {
    public static string scannedFilesDir = Path.Combine(Directory.GetCurrentDirectory(), "ScannedFiles");
    public static void CreateStaticFilesDirs() {
      List<string> dirNames = new List<string> () {
        "images",
        "pdf"
      };
      dirNames.ForEach(dir => {
        string fullDirPath =  Path.Combine(scannedFilesDir, dir);
        if (!Directory.Exists(fullDirPath))
        {
          Directory.CreateDirectory(fullDirPath);
        }
      });
    }
  }
}