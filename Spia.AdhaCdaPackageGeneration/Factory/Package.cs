using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;
using Nehta.VendorLibrary.Common;
using Nehta.VendorLibrary.CDAPackage;

using System.Xml;
using System.IO;

namespace Spia.AdhaCdaPackageGeneration.Factory
{
  public class Package
  {

    public void Process(PackagerInput Input)
    {

      
      // ------------------------------------------------------------------------------
      // Set up signing certificate and identifiers
      // ------------------------------------------------------------------------------

      // Load certificate used to sign the CDA document
      X509Certificate2 signingCert = X509CertificateUtil.GetCertificate(
          "06fba6",
          X509FindType.FindBySerialNumber,
          StoreName.My,
          StoreLocation.CurrentUser,
          true);

      // ------------------------------------------------------------------------------
      // Create CDAPackage
      // ------------------------------------------------------------------------------

      // Create an approver
      var approver = new Approver()
      {
        PersonId = new Uri($"http://ns.electronichealth.net.au/id/hi/hpii/1.0/{Input.Approver.Hpii}"),
        PersonFamilyName = Input.Approver.FamilyName,
        PersonGivenNames = new List<string> { Input.Approver.GivenName },
        //PersonNameSuffixes = new List<string> { Input.Approver },
        PersonTitles = new List<string> { Input.Approver.Title }
      };

      // Create a CDAPackage instance
      var package = new Nehta.VendorLibrary.CDAPackage.CDAPackage(approver);

      if (!File.Exists(Input.CdaDocumentFilePath))
      {
        throw new ApplicationException($"Unable to locate the CDA file at file path: {Input.CdaDocumentFilePath}");
      }
      // Create the CDA root document for the CDA package      
      package.CreateRootDocument(File.ReadAllBytes(Input.CdaDocumentFilePath));

      //Add the PDF report attachment
      var PdfAtachmentFileInfo = new FileInfo(Input.PdfReportAttachment);
      if (!PdfAtachmentFileInfo.Exists)
        throw new ApplicationException($"Unable to locate attachment file at file path: {Input.PdfReportAttachment}");
      package.AddDocumentAttachment("attachment.pdf", File.ReadAllBytes(Input.PdfReportAttachment));

      //Add the logo image attachment
      package.AddDocumentAttachment("logo.png", Input.CdaDocumentLogoBytes);

      FileInfo CdaDocumentFileinfo = new FileInfo(Input.CdaDocumentFilePath);

      string CdaPackageFileName = CdaDocumentFileinfo.Name.Replace(CdaDocumentFileinfo.Extension, ".zip");
      CdaPackageFileName = Path.Combine(CdaDocumentFileinfo.DirectoryName, CdaPackageFileName);
      // Create the CDA package zip      
      CDAPackageUtility.CreateZip(package, CdaPackageFileName, signingCert);
      
      //Delete the raw CDA xml file
      CdaDocumentFileinfo.Delete();

    }



  }
}
