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
        GenerateCdaDocuments = true,
        NashCertificateSerial = "06fba6"
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
      catch(Exception Exec)
      {
        Console.WriteLine(Exec.Message);
        Console.WriteLine("");
        Console.WriteLine("Hit any key to end.");
        Console.ReadKey();
      }
      
    }
    
  }
}

