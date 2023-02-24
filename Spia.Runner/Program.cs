using System;


namespace Spia.Runner
{
  class Program
  {
    static void Main(string[] args)
    {
      var Options = new SpiaFileGeneratorOptions(
        outputDirectory: @"D:\Temp\spia\PI-Pilot-22-24\Output",
        pdfAttachmentInputDirectory: @"D:\Temp\spia\PI-Pilot-22-24\PDF",
        logicalModelInputDirectory: @"D:\Temp\spia\PI-Pilot-22-24\Input")
      {
        GenerateLogicalModels = false,
        GenerateHL7Version2Messages = true,
        GenerateFhirBundles = true,
        GenerateCdaDocuments = true,
        GenerateCdaPackages = true,        
        NashCertificateSerial = "2AB5",
        CdaPackagerApproverPerson = new AdhaCdaPackageGeneration.Model.ApproverPerson()
        {
          FamilyName = "Millar",
          GivenName = "Angus",
          Title = "Dr",
          Hpii = "8003 6188 2910 5369"
        }
      };

      SpiaFileGenerator SpiaFileGenerator = new SpiaFileGenerator(Options);
      try
      {
        SpiaFileGenerator.Process();
        Console.WriteLine("");
        Console.WriteLine("Finished");
        Console.WriteLine("Hit any key to end.");
        Console.ReadKey();
      }
      catch (Exception Exec)
      {
        Console.WriteLine(Exec.Message);
        Console.WriteLine("");
        Console.WriteLine("Hit any key to end.");
        Console.ReadKey();
      }

    }

  }
}

