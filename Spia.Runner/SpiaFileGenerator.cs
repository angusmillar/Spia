using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spia.AusHl7v2Generation.Factory.HL7.v2;
using Spia.AdhaFhirGeneration.Factory;
using Spia.Runner.SupportExtensions;
using Spia.AdhaCdaGeneration.Factory;
using Spia.AdhaCdaPackageGeneration.Factory;
using System.Drawing;

namespace Spia.Runner
{
  public class SpiaFileGenerator
  {
    public void Process(string RootSpiaDirectory)
    {
      DirectoryInfo RootOutputDirectoryInfo = new DirectoryInfo(RootSpiaDirectory);
      
      DirectoryInfo RootHl7v2OutputDirectory = new DirectoryInfo(Path.Combine(RootOutputDirectoryInfo.FullName, "HL7v2"));
      RootHl7v2OutputDirectory.CreateDirectoryIfNoExist();
      RootHl7v2OutputDirectory.DeleteAllContents();      

      Hl7v2FileGenerator Hl7v2 = new Hl7v2FileGenerator();
      Hl7v2.LogEventMessageDelegate = SpiaFileGenerator.WriteLine;
      Hl7v2.Process(RootHl7v2OutputDirectory.FullName);
      
      DirectoryInfo RootFhirOutputDirectory = new DirectoryInfo(Path.Combine(RootOutputDirectoryInfo.FullName, "FHIR"));
      RootFhirOutputDirectory.CreateDirectoryIfNoExist();
      RootFhirOutputDirectory.DeleteAllContents();

      AdhaFhirFileGenerator Fhir = new AdhaFhirFileGenerator();
      Fhir.LogEventMessageDelegate = SpiaFileGenerator.WriteLine;
      Fhir.Process(RootHl7v2OutputDirectory.FullName, RootFhirOutputDirectory.FullName);

      DirectoryInfo RootCdaOutputDirectory = new DirectoryInfo(Path.Combine(RootOutputDirectoryInfo.FullName, "CDA"));
      RootCdaOutputDirectory.CreateDirectoryIfNoExist();
      RootCdaOutputDirectory.DeleteAllContents();

      DirectoryInfo RootPdfDirectory = new DirectoryInfo(Path.Combine(RootOutputDirectoryInfo.FullName, "PDF"));
      FileInfo CdaDocuemntLogoFilePath = new FileInfo($@"{RootOutputDirectoryInfo.FullName}\CdaLogo\RCPA_PITUS_Logo.png");      

      AdhaCdaFileGenerator CdaDocument = new AdhaCdaFileGenerator();
      CdaDocument.LogEventMessageDelegate = SpiaFileGenerator.WriteLine;
      
      //Get the Logo and convert to Byte array
      ImageConverter converter = new ImageConverter();
      byte[] Logo = (byte[])converter.ConvertTo(Resource.RCPA_PITUS_Logo, typeof(byte[]));

      CdaDocument.Process(RootHl7v2OutputDirectory.FullName, RootPdfDirectory.FullName, RootCdaOutputDirectory.FullName, Logo);

      AdhaCdaPackageFileGenerator CdaPackager = new AdhaCdaPackageFileGenerator();
      RootCdaOutputDirectory.DeleteAllFiles(".zip");
      CdaPackager.LogEventMessageDelegate = SpiaFileGenerator.WriteLine;
      CdaPackager.Process(RootHl7v2OutputDirectory.FullName, RootPdfDirectory.FullName, RootCdaOutputDirectory.FullName, Logo);
      RootCdaOutputDirectory.DeleteAllFiles(".xml");

      WriteLine("");
      WriteLine("Finished");
      WriteLine("Hit any key to end.");
      Console.ReadKey();
    }

    public static void WriteLine(string FileName)
    {
      Console.WriteLine(FileName);
    }

  }
}
