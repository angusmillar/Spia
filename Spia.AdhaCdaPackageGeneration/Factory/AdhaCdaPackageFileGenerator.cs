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
    private string NashCertificateSerial;

    public AdhaCdaPackageFileGenerator(string nashCertificateSerial)
    {
      this.NashCertificateSerial = nashCertificateSerial;
    }

    public void Process(string RootHl7v2DirectoryPath, string RootPDFDirectoryPath, string CdaDocumentInputDirectoryPath, string CdaPackageOutputDirectory, byte[] CdaDocuemntLogoImageBytes = null)
    {
      Package CdaPackage = new Package();
      string[] SourceCdaDocumentsList = Directory.GetFiles(CdaDocumentInputDirectoryPath, "*.xml");
      //string[] FilePathArray = Directory.GetFiles($@"{RootHl7v2DirectoryPath}", "*.hl7");

      //Approver for CDA Package
      ApproverPerson Approver = new ApproverPerson()
      {
        FamilyName = "Millar",
        GivenName = "Angus",
        Title = "Dr",
        Hpii = "8003 6188 2910 5369"
      };

      foreach (string CdaDocuemntFilePath in SourceCdaDocumentsList)
      {

        //Create CDA Document
        FileInfo CdaDocumentFileInfo = new FileInfo(CdaDocuemntFilePath);
        //CdaGeneratorInput.Message = Creator.Message(File.ReadAllText(FilePath));
        //string FileNameForCdaAndPdf = CdaDocumentFileInfo.Name.Replace(".xml", "");
        FileInfo CdaPackageoutputFilePath = new FileInfo($@"{CdaPackageOutputDirectory}\{CdaDocumentFileInfo.Name.Replace(CdaDocumentFileInfo.Extension, ".zip")}");
        string CdaDocumentInputFilePath = $@"{CdaDocumentInputDirectoryPath}\{CdaDocumentFileInfo.Name}";
        string PdfFilePath = $@"{RootPDFDirectoryPath}\{CdaDocumentFileInfo.Name.Replace(CdaDocumentFileInfo.Extension, ".pdf")}";

        //CDA Package
        PackagerInput PackagerInput = new PackagerInput()
        {
          NashCertificateSerial = this.NashCertificateSerial,
          Approver = Approver,
          CdaDocumentInputFilePath = CdaDocumentInputFilePath,
          CdaPackageOutputFilePath = CdaPackageoutputFilePath.FullName,
          CdaDocumentLogoBytes = CdaDocuemntLogoImageBytes,          
          PdfReportAttachment = PdfFilePath,
        };
        CdaPackage.Process(PackagerInput);
        this.Log($"{CdaPackageoutputFilePath.Name}");        
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
