using Spia.AdhaCdaPackageGeneration.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spia.AdhaCdaPackageGeneration.Factory
{
  public class AdhaCdaPackageFileGenerator
  {
    public delegate void LogEventMessage(string Message);

    public LogEventMessage LogEventMessageDelegate;
    public void Process(string RootHl7v2DirectoryPath, string RootPDFDirectoryPath, string OutputPath, byte[] CdaDocuemntLogoImageBytes = null)
    {
      this.Log("----------------------------------------------------------------------");
      this.Log("SPIA CDA Package");

      Package CdaPackage = new Package();
      string[] FilePathArray = Directory.GetFiles($@"{RootHl7v2DirectoryPath}", "*.hl7");

      //Approver for CDA Package
      ApproverPerson Approver = new ApproverPerson()
      {
        FamilyName = "Millar",
        GivenName = "Angus",
        Title = "Dr",
        Hpii = "8003 6188 2910 5369"
      };

      foreach (string FilePath in FilePathArray.OrderBy(x => x.Substring(0, 2)))
      {

        //Create CDA Document
        FileInfo fi = new FileInfo(FilePath);
        //CdaGeneratorInput.Message = Creator.Message(File.ReadAllText(FilePath));
        string FileNameForCdaAndPdf = fi.Name.Substring(fi.Name.IndexOf("SPIA Exemplar Report"), fi.Name.Length - fi.Name.IndexOf("SPIA Exemplar Report"));
        string CdaDocumentFilePath = $@"{OutputPath}\{FileNameForCdaAndPdf.Replace(fi.Extension, ".xml")}";
        string PdfFilePath = $@"{RootPDFDirectoryPath}\{FileNameForCdaAndPdf.Replace(fi.Extension, ".pdf")}";

        //CDA Package
        PackagerInput PackagerInput = new PackagerInput()
        {
          Approver = Approver,
          CdaDocumentFilePath = CdaDocumentFilePath,
          CdaDocumentLogoBytes = CdaDocuemntLogoImageBytes,          
          PdfReportAttachment = PdfFilePath,
        };
        CdaPackage.Process(PackagerInput);
        this.Log($"Generate: {fi.Name}");        
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
