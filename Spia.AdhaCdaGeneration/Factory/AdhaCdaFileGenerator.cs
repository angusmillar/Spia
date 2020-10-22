using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PeterPiper.Hl7.V2.Model;
using Spia.AdhaCdaGeneration.Model;

namespace Spia.AdhaCdaGeneration.Factory
{
  public class AdhaCdaFileGenerator
  {
    public delegate void LogEventMessage(string Message);

    public LogEventMessage LogEventMessageDelegate;
    public void Process(string RootHl7v2DirectoryPath, string RootPDFDirectoryPath, string OutputPath, byte[] CdaDocuemntLogoImageBytes = null)
    {
      this.Log("----------------------------------------------------------------------");
      this.Log("SPIA CDA Document");
      CdaCreatorInput CdaGeneratorInput = new CdaCreatorInput();
      CdaGeneratorInput.IsMandatoryCDAElementsOnly = false;
      CdaGeneratorInput.CdaDocuemntLogoImageBytes = CdaDocuemntLogoImageBytes; ;
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

      CdaCreator CdaCreator = new CdaCreator();
      
      string[] FilePathArray = Directory.GetFiles(RootHl7v2DirectoryPath, "*.hl7");
      
      foreach (string FilePath in FilePathArray.OrderBy(x => x.Substring(0, 2)))
      {

        //Create CDA Document
        FileInfo fi = new FileInfo(FilePath);
        CdaGeneratorInput.Message = Creator.Message(File.ReadAllText(FilePath));
        string FileNameForCdaAndPdf = fi.Name.Substring(fi.Name.IndexOf("SPIA Exemplar Report"), fi.Name.Length - fi.Name.IndexOf("SPIA Exemplar Report"));
        CdaGeneratorInput.FilePathToPdfReport = $@"{RootPDFDirectoryPath}\{FileNameForCdaAndPdf.Replace(fi.Extension, ".pdf")}";

        CdaGeneratorInput.CdaDocumentOutPutFilePath = $@"{OutputPath}\{FileNameForCdaAndPdf.Replace(fi.Extension, ".xml")}";        
        CdaCreator.Process(CdaGeneratorInput);
        this.Log($"Generate: {FileNameForCdaAndPdf.Replace(fi.Extension, ".xml")}");
      }

           
    }

    private void Log(string messagae)
    {
      if (this.LogEventMessageDelegate != null)
      {
        LogEventMessageDelegate.Invoke(messagae);
      }
    }
  }
}
