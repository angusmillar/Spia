using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spia.PathologyReportModel.Model;
using Spia.Runner.SupportExtensions;

namespace Spia.Runner
{
  class Program
  {
    static void Main(string[] args)
    {
      var Options = new SpiaFileGeneratorOptions(
        primarySpiaDirectory: @"C:\temp\SPIAMessages\GeneratedFiles",
        pdfPathologyReportAttachmentDirectory: @"C:\temp\SPIAMessages\PDF",
        pathologyReportDataDirectory: @"C:\temp\SPIAMessages\PathologyReportData")
      {
        GeneratePathologyReportModels = false,
        GenerateHL7Version2Messages = true,
        GenerateCdaDocuments = true,
        GenerateCdaPackages = true,
        GenerateFhirBundles = false,
        NashCertificateSerial = "06fba6",
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

