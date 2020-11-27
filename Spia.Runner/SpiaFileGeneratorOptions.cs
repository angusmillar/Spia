using Spia.AdhaCdaPackageGeneration.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spia.Runner
{
  public class SpiaFileGeneratorOptions
  {
    public SpiaFileGeneratorOptions(string outputDirectory, string logicalModelInputDirectory, string pdfAttachmentInputDirectory)
    {
      OutputDirectory = outputDirectory;
      GenerateLogicalModels = false;
      GenerateHL7Version2Messages = true;
      GenerateFhirBundles = true;
      GenerateCdaDocuments = true;
      GenerateCdaPackages = true;
      NashCertificateSerial = string.Empty;
      PdfAttachmentInputDirectory = pdfAttachmentInputDirectory;
      LogicalModelInputDirectory = logicalModelInputDirectory;
    }

    public void Validate()
    {
      if (this.GenerateCdaPackages && string.IsNullOrWhiteSpace(this.NashCertificateSerial))
      {
        throw new ApplicationException($"When {nameof(this.GenerateCdaPackages)} is True a {nameof(this.NashCertificateSerial)} must be provided.");
      }

      if (this.GenerateCdaPackages && this.CdaPackagerApproverPerson is null)
      {
        throw new ApplicationException($"When {nameof(this.GenerateCdaPackages)} is True the {nameof(this.CdaPackagerApproverPerson)} must be provided.");
      }

      if (string.IsNullOrWhiteSpace(this.OutputDirectory))
      {
        throw new ApplicationException($"The {nameof(this.OutputDirectory)} path must not be an empty string.");
      }

      if (string.IsNullOrWhiteSpace(this.LogicalModelInputDirectory))
      {
        throw new ApplicationException($"The {nameof(this.LogicalModelInputDirectory)} path must not be an empty string.");
      }

      if (string.IsNullOrWhiteSpace(this.PdfAttachmentInputDirectory))
      {
        throw new ApplicationException($"The {nameof(this.PdfAttachmentInputDirectory)} path must not be an empty string.");
      }
      
    }
    /// <summary>
    /// Defaults to False
    /// </summary>
    public bool GenerateLogicalModels { get; set; }
    
    /// <summary>
    /// Generate the HL7 v2 Messages
    /// </summary>
    public bool GenerateHL7Version2Messages { get; set; }

    /// <summary>
    /// Generate the FHIR Bundles
    /// </summary>
    public bool GenerateFhirBundles { get; set; }

    /// <summary>
    /// Generate the CDA Documents
    /// </summary>
    public bool GenerateCdaDocuments { get; set; }

    /// <summary>
    /// Generate the CDA Packages (As required to upload to the My HealthRecord)
    /// PLEASE NOTE THIS OPTIONS WILL REQUIRE A NASH CERTIFICATE OR TEST NASH CERTIFICATE
    /// </summary>
    public bool GenerateCdaPackages { get; set; }

    /// <summary>
    /// The serial of the NASH certificate which must be valid and not expired and 
    /// loaded into the Windows certificate store for the CurrentUser
    /// e.g "06fba6"
    /// </summary>
    public string NashCertificateSerial { get; set; }

    /// <summary>
    /// The Person that approves the CDA Package being uploaded to the My Health Record
    /// </summary>
    public ApproverPerson CdaPackagerApproverPerson { get; set; }
    
    /// <summary>
    /// The output directory where all generated files will be written 
    /// </summary>
    public string OutputDirectory{ get; set; }

    /// <summary>
    /// The directory where the PDF Pathology Report Attachments are located
    /// </summary>
    public string PdfAttachmentInputDirectory { get; set; }

    /// <summary>
    /// The directory where the .json Pathology Report Logical Model files are located and will be read from.
    /// </summary>
    public string LogicalModelInputDirectory { get; set; }
  }
}
