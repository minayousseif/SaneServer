namespace SaneServer.Server.DTO
{
  public class ScannerOptions {
     public class ScanFormat 
        { 
            public const string Jpeg = "jpeg"; 
            public const string Tiff = "tiff";
            public const string Png  = "png"; 
        }
        public class ScanMode 
        {
            public const string BlackAndWhite = "Black & White";
            public const string Gray = "Gray";
            public const string TrueGray = "True Gray";
            public const string Color = "24bit Color";
            public const string ColorFast = "24bit Color[Fast]";
        }

        public class ScanSource 
        {
            public const string FlatBed = "FlatBed";
            public const string AutomaticFeederLeft = "Automatic Document Feeder(left aligned)";
            public const string AutomaticFeederCenter = "Automatic Document Feeder(centrally aligned)";
        }

        public class ScanArgs 
        {
            public string device {get; set;}

            public string format {get; set;} = ScanFormat.Jpeg;

            public int resolution {get; set;} = 300;

            public string mode {get; set;}

            public string source {get; set;} = ScanSource.FlatBed;

        }

  }
}