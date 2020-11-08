using Spia.AdhaCdaPackageGeneration.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spia.AdhaCdaPackageGeneration.Factory
{
  public class PackagerInput
  {
    public string CdaPackageOutputFilePath { get; set; }
    public string CdaDocumentInputFilePath { get; set; }
    public string PdfReportAttachment { get; set; }
    public byte[] CdaDocumentLogoBytes { get; set; }
    public ApproverPerson Approver { get; set; }
    public string NashCertificateSerial { get; set; }


  }
}
