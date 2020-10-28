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

      JsonTest();
      string RootSpiaDirectoryPath = @"C:\temp\SPIAMessages";
      SpiaFileGenerator SpiaFileGenerator = new SpiaFileGenerator();
      SpiaFileGenerator.Process(RootSpiaDirectoryPath);
    }

    private static void JsonTest()
    {
      string OutputDirectoryPath = @"C:\temp\SPIAMessages\PathologyReportData";
      DirectoryInfo OutputDirectoryInfo = new DirectoryInfo(@"C:\temp\SPIAMessages\PathologyReportData");
      OutputDirectoryInfo.CreateDirectoryIfNoExist();

      var SpiaPathologyReportFactory = new Spia.PathologyReportModel.Factory.SpiaPathologyReportFactory();
      var PathologyReportContainerList = new List<PathologyReportContainer>()
      {
        SpiaPathologyReportFactory.GetBloodGasArterial(),
        SpiaPathologyReportFactory.GetChlamydia(),
        SpiaPathologyReportFactory.GetEUC(),
        SpiaPathologyReportFactory.GetFBC(),
        SpiaPathologyReportFactory.GetHepBsAb(),
        SpiaPathologyReportFactory.GetHFE(),
        SpiaPathologyReportFactory.GetImmunoglobulinE(),
        SpiaPathologyReportFactory.GetINR(),
        SpiaPathologyReportFactory.GetKaryotyping(),
        SpiaPathologyReportFactory.GetLipids(),
        SpiaPathologyReportFactory.GetMSU(),
        SpiaPathologyReportFactory.GetProteinElectrophoresis(),
        SpiaPathologyReportFactory.GetSARSCoV2NAT(),
        SpiaPathologyReportFactory.GetSARSCoV2Serology()
      };

      foreach(var PathologyReportContainer in PathologyReportContainerList)
      {
        FileInfo JsonFilePath = new FileInfo($@"{OutputDirectoryPath}\{PathologyReportContainer.PathologyReport.PdfFileName.Replace(".pdf", ".json")}");
        var Writer = new Spia.PathologyReportModel.JsonFileGenerator();
        Writer.Write(PathologyReportContainer, JsonFilePath.FullName);
        //Test we can read it back it without error
        var Reader = new Spia.PathologyReportModel.JsonFileReader();
        var PathologyReportsRead = Reader.ReadPathologyReports(JsonFilePath.FullName);
      }
      
      
      


      

    }
  }
}

