using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spia.AdhaCdaGeneration.Factory
{
  public class AdhaCdaFileGenerator
  {
    public delegate void LogEventMessage(string Message);
    public LogEventMessage LogEventMessageDelegate;

    public void Process(Spia.PathologyReportModel.Model.PathologyReportContainer PathologyReportContainer, string RootPDFDirectoryPath, string CdaOutputPath, byte[] CdaDocuemntLogoImageBytes = null)
    {    
      CdaCreator CdaCreator = new CdaCreator();
      CdaCreator.Process(PathologyReportContainer.PathologyReport, CdaOutputPath, RootPDFDirectoryPath, CdaDocuemntLogoImageBytes);
      Log($"{PathologyReportContainer.PathologyReport.PdfFileName.Replace(".pdf", ".xml")}");
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
