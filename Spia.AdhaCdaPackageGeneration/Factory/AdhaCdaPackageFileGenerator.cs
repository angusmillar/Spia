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

    public void Process(ApproverPerson ApproverPerson, string RootPDFDirectoryPath, string CdaDocumentInputDirectoryPath, string CdaPackageOutputDirectory, byte[] CdaDocuemntLogoImageBytes = null)
    {
      Package CdaPackage = new Package();
      string[] SourceCdaDocumentsList = Directory.GetFiles(CdaDocumentInputDirectoryPath, "*.xml");     

      foreach (string CdaDocuemntFilePath in SourceCdaDocumentsList)
      {
        //Create CDA Document
        FileInfo CdaDocumentFileInfo = new FileInfo(CdaDocuemntFilePath);       
        FileInfo CdaPackageoutputFilePath = new FileInfo($@"{CdaPackageOutputDirectory}\{CdaDocumentFileInfo.Name.Replace(CdaDocumentFileInfo.Extension, ".zip")}");
        string CdaDocumentInputFilePath = $@"{CdaDocumentInputDirectoryPath}\{CdaDocumentFileInfo.Name}";
        string PdfFilePath = $@"{RootPDFDirectoryPath}\{CdaDocumentFileInfo.Name.Replace(CdaDocumentFileInfo.Extension, ".pdf")}";

        //CDA Package
        PackagerInput PackagerInput = new PackagerInput()
        {
          NashCertificateSerial = this.NashCertificateSerial,
          Approver = ApproverPerson,
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
