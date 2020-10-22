using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using Spia.AdhaCdaGeneration.Model;
using Spia.AdhaCdaGeneration.Factory;
using Spia.AdhaCdaPackageGeneration.Factory;
using Spia.AdhaCdaPackageGeneration.Model;
using PeterPiper.Hl7.V2.Model;

namespace Spia.AdhaCdaGenerationRunner
{
  class Program
  {
    static void Main(string[] args)
    {
      Console.WriteLine("Starting CDA Creation");
      CdaCreatorInput CdaGeneratorInput = new CdaCreatorInput();
      CdaGeneratorInput.IsMandatoryCDAElementsOnly = false;
      CdaGeneratorInput.CdaDocumentLogoFilePath = @"C:\temp\SPIAMessages\CdaLogo\RCPA_PITUS_Logo.png";
      CdaGeneratorInput.CdaMetadata = new PathologyCdaMetadata()
      {
        PathologyLabList = new List<PathologyLab>()
        {
          new PathologyLab()
          {
            Name = "PITUS Pathology",
            NataSiteNumber = "9999",
            Hpio = "8003 6299 0001 9338",
            Phone = "+61 2 8356 5858",
            Address = new Address()
            {
              LineOne = "Durham Hall",
              LineTwo = "207 Albion Street",
              PostCode = "2010",
              Suburb = "Surry Hills",
              State = Address.StateType.NSW
            },
            ReportingPathologistsList = new List<Pathologist>()
            {
              new Pathologist()
              {
                Name = "Dr Bella Pathologist",
                LocalCode = "BPATH",
                Hpii = "8003 6111 0391 4531"
              },
              new Pathologist()
              {
                Name = "Dr Stanley Virologist",
                LocalCode = "SVIRO",
                Hpii = "8003 6100 3427 0591"
              },
              new Pathologist()
              {
                Name = "Dr Kondo Uhu Pathologist",
                LocalCode = "KUP",
                Hpii = "8003 6168 4816 8954"
              },
              new Pathologist()
              {
                Name = "Dr Manuel del Pathologist",
                LocalCode = "DPM",
                Hpii = "8003 6168 4816 8954"
              },
              new Pathologist()
              {
                Name = "Dr Evanna Pathologist",
                LocalCode = "EP",
                Hpii = "8003 6169 0858 7986"
              },
              new Pathologist()
              {
                Name = "Dr Bertram Pathologist",
                LocalCode = "BI",
                Hpii = "8003 6136 8407 5880"
              },
              new Pathologist()
              {
                Name = "Dr Mario Pathologist",
                LocalCode = "MARP",
                Hpii = "8003 6150 4939 6513"
              },
              new Pathologist()
              {
                Name = "Dr Marissa Pathologist",
                LocalCode = "MP",
                Hpii = "8003 6143 4432 9915"
              }
            }
          }
        }
      };

      //Approver for CDA Package
      ApproverPerson Approver = new ApproverPerson()
      {
        FamilyName = "Millar",
        GivenName = "Angus",
        Title = "Dr",
        Hpii = "8003 6188 2910 5369"
      };


      CdaCreator CdaCreator = new CdaCreator();
      Package CdaPackage = new Package();

      string RootPath = @"C:\temp\SPIAMessages";
      string[] FilePathArray = Directory.GetFiles($@"{RootPath}\HL7v2\", "*.hl7");

      //Delete all the old CDAPackages
      string[] OldCdaFileList = Directory.GetFiles($@"{RootPath}\CDA\", "*.zip");
      OldCdaFileList.ToList().ForEach(x => File.Delete(x));

      foreach (string FilePath in FilePathArray.OrderBy(x => x.Substring(0, 2)))
      {

        //Create CDA Document
        FileInfo fi = new FileInfo(FilePath);
        CdaGeneratorInput.Message = Creator.Message(File.ReadAllText(FilePath));
        string FileNameForCdaAndPdf = fi.Name.Substring(fi.Name.IndexOf("SPIA Exemplar Report"), fi.Name.Length - fi.Name.IndexOf("SPIA Exemplar Report"));
        CdaGeneratorInput.CdaDocumentOutPutFilePath = $@"{RootPath}\CDA\{FileNameForCdaAndPdf.Replace(fi.Extension, ".xml")}";
        CdaGeneratorInput.FilePathToPdfReport = $@"{RootPath}\PDF\{FileNameForCdaAndPdf.Replace(fi.Extension, ".pdf")}";
        CdaCreator.Process(CdaGeneratorInput);

        //CDA Package
        PackagerInput PackagerInput = new PackagerInput()
        {
          Approver = Approver,
          CdaDocumentFilePath = CdaGeneratorInput.CdaDocumentOutPutFilePath,
          CdaDocumentLogoFilePath = CdaGeneratorInput.CdaDocumentLogoFilePath,
          PdfReportAttachment = CdaGeneratorInput.FilePathToPdfReport,
        };
        CdaPackage.Process(PackagerInput);
        Console.WriteLine($"{fi.Name}");
      }

      Console.WriteLine($"Finished");
      Console.ReadKey();



    }
  }
}
