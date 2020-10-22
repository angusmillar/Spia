using System;
using System.IO;
using Spia.AdhaFhirGeneration.Factory;

namespace Spia.AdhaFhirGenerationRunnner
{
  class Program
  {
    static void Main(string[] args)
    {
      string RootPath = @"C:\temp\SPIAMessages";
      string[] FilePathArray = Directory.GetFiles($@"{RootPath}\HL7v2\", "*.hl7");

      DirectoryInfo FhirOutputDir = new DirectoryInfo($@"{RootPath}\FHIR");
      foreach (FileInfo file in FhirOutputDir.GetFiles())
      {
        file.Delete();
      }

      PathologyFactory PathologyFactory = new PathologyFactory();
      foreach (string FilePath in FilePathArray)
      {
        string Resource = PathologyFactory.CreateJson(FilePath);
        FileInfo fi = new FileInfo(FilePath);
        File.WriteAllText(Path.Combine(FhirOutputDir.FullName, fi.Name.Replace(fi.Extension, ".json")), Resource);
        Console.WriteLine($"{fi.Name.Replace(fi.Extension, ".json")}");
      }

      Console.WriteLine("Finished!");
    }
  }
}
