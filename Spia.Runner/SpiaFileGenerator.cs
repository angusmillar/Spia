﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spia.AusHl7v2Generation.Factory;
using Spia.AdhaFhirGeneration.Factory;
using Spia.Runner.SupportExtensions;
using Spia.AdhaCdaGeneration.Factory;
using Spia.AdhaCdaPackageGeneration.Factory;
using System.Drawing;
using Spia.PathologyReportModel.Model;

namespace Spia.Runner
{
  public class SpiaFileGenerator
  {
    private readonly SpiaFileGeneratorOptions Options;
    public SpiaFileGenerator(SpiaFileGeneratorOptions Options)
    {
      this.Options = Options;
    }

    public void Process()
    {
      this.Options.Validate();
      DirectoryInfo RootOutputDirectoryInfo = new DirectoryInfo(this.Options.PrimarySpiaDirectory);
      DirectoryInfo RootHl7v2OutputDirectory = new DirectoryInfo(Path.Combine(RootOutputDirectoryInfo.FullName, "HL7v2 Messages"));
      DirectoryInfo RootPdfDirectory = new DirectoryInfo(Options.PdfPathologyReportAttachmentDirectory);
      DirectoryInfo RootReportDataDirectory = new DirectoryInfo(Options.PathologyReportDataDirectory);
      DirectoryInfo RootFhirOutputDirectory = new DirectoryInfo(Path.Combine(RootOutputDirectoryInfo.FullName, "FHIR Bundles"));
      DirectoryInfo RootCdaPackagesOutputDirectory = new DirectoryInfo(Path.Combine(RootOutputDirectoryInfo.FullName, "CDA Packages"));
      DirectoryInfo RootCdaDocumentOutputDirectory = new DirectoryInfo(Path.Combine(RootOutputDirectoryInfo.FullName, "CDA Documents"));
      DirectoryInfo TempWorkingCDADocumentDirectoryInfo = new System.IO.DirectoryInfo(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "CdaGeneratorWorkingDirectory\\CDADocuments"));

      List<PathologyReportContainer> PathologyReportContainerList = new List<PathologyReportContainer>();

      //If below is true we generate the Pathology Data json files from the hard coded source.
      if (this.Options.GeneratePathologyReportModels)
      {
        WriteLine("----------------------------------------------------------------------");
        WriteLine($"The option {nameof(this.Options.GeneratePathologyReportModels)} is set to True.");
        WriteLine("Generate Pathology Report Data files");
        RootReportDataDirectory.CreateDirectoryIfNoExist();
        RootReportDataDirectory = new DirectoryInfo(Options.PathologyReportDataDirectory);
        if (RootReportDataDirectory.GetFiles("*.json", SearchOption.TopDirectoryOnly).Count() > 0)
        {
          throw new ApplicationException($"The ReportData directory must empty when generating Pathology Report Data files.");
        }

        var SpiaPathologyReportFactory = new Spia.PathologyReportModel.Factory.SpiaPathologyReportFactory();
        PathologyReportContainerList = SpiaPathologyReportFactory.GetAll();
        foreach (var PathologyReportContainer in PathologyReportContainerList)
        {

          FileInfo JsonFilePath = new FileInfo($@"{RootReportDataDirectory.FullName}\{PathologyReportContainer.PathologyReport.PdfFileName.Replace(".pdf", ".json")}");
          WriteLine(JsonFilePath.Name);
          var Writer = new Spia.PathologyReportModel.JsonFileGenerator();
          Writer.Write(PathologyReportContainer, JsonFilePath.FullName);

          //Test we can read it back it without error
          var Reader = new Spia.PathologyReportModel.JsonFileReader();
          var PathologyReportsRead = Reader.ReadPathologyReports(JsonFilePath.FullName);
        }
      }


      //Read in all Pathology Data json files found in the directory      
      WriteLine("----------------------------------------------------------------------");
      WriteLine("Reading in Pathology Report Data files");
      WriteLine($"Input Directory: {RootReportDataDirectory.FullName}");
      if (!RootReportDataDirectory.Exists)
      {
        throw new ApplicationException($"The Pathology Report Data directory does not exist at :{RootReportDataDirectory.FullName}");
      }

      FileInfo[] ReportDataFileInfoArray = RootReportDataDirectory.GetFiles("*.json", SearchOption.TopDirectoryOnly);
      if (ReportDataFileInfoArray.Count() == 0)
      {
        throw new ApplicationException($"The Pathology Report Data directory contains no Pathology Report Data .json files.");
      }
      foreach (FileInfo ReportDataFileInfo in RootReportDataDirectory.GetFiles("*.json", SearchOption.TopDirectoryOnly))
      {
        var Reader = new Spia.PathologyReportModel.JsonFileReader();
        try
        {
          WriteLine(ReportDataFileInfo.Name);
          var PathologyReportsRead = Reader.ReadPathologyReports(ReportDataFileInfo.FullName);
          PathologyReportContainerList.Add(PathologyReportsRead);
        }
        catch (Exception Exec)
        {
          WriteLine($"The Pathology Report Data in the file named {ReportDataFileInfo.Name} had the following issue:");
          throw Exec;
        }
      }

      if (Options.GenerateHL7Version2Messages)
      {
        //Validation for HL7 v2
        foreach (PathologyReportContainer PathologyReportContainer in PathologyReportContainerList)
        {         
          var ErrorMessageList = new List<string>();
          if (!PathologyReportContainer.IsValid(PathologyReportContainer, PathologyReportModel.CustomAttribute.ScopeType.Hl7v2, ErrorMessageList))
          {
            StringBuilder sb = new StringBuilder("HL7 v2 validation errors where detected:\n");
            ErrorMessageList.ForEach(x => sb.Append(x + "\n"));
            throw new ApplicationException(sb.ToString()); ;
          }
        }

        RootHl7v2OutputDirectory.CreateDirectoryIfNoExist();
        RootHl7v2OutputDirectory.DeleteAllFileAndDirectories();
        //Generate HL7 v2 Messages for each Pathology Data json file      
        WriteLine("----------------------------------------------------------------------");
        WriteLine("Generate HL7 v2 Messages");
        WriteLine($"Output Directory: {RootHl7v2OutputDirectory.FullName}");
        foreach (PathologyReportContainer PathologyReportContainer in PathologyReportContainerList)
        {
          string HL7Message = MessageFactory.GetMessage(PathologyReportContainer.PathologyReport, RootPdfDirectory.FullName);
          FileInfo HL7MessageFileInfo = new FileInfo($@"{RootHl7v2OutputDirectory}\{PathologyReportContainer.PathologyReport.PdfFileName.Replace(".pdf", ".hl7")}");
          File.WriteAllText(HL7MessageFileInfo.FullName, HL7Message);
          WriteLine(HL7MessageFileInfo.Name);
        }
      }

      //Generate FHIR bundles
      if (Options.GenerateFhirBundles)
      {

        RootFhirOutputDirectory.CreateDirectoryIfNoExist();
        RootFhirOutputDirectory.DeleteAllFileAndDirectories();

        AdhaFhirFileGenerator Fhir = new AdhaFhirFileGenerator();
        Fhir.LogEventMessageDelegate = SpiaFileGenerator.WriteLine;
        Fhir.Process(RootHl7v2OutputDirectory.FullName, RootFhirOutputDirectory.FullName);
      }

      //Get the Logo and convert to Byte array
      ImageConverter converter = new ImageConverter();
      byte[] Logo = (byte[])converter.ConvertTo(Resource.RCPA_PITUS_Logo, typeof(byte[]));

      DirectoryInfo CurrentCDADocuementDirectoryInfo = null;
      //Generate CDA Documents
      if (Options.GenerateCdaDocuments || Options.GenerateCdaPackages)
      {
        if (!Options.GenerateCdaDocuments)
        {
          CurrentCDADocuementDirectoryInfo = TempWorkingCDADocumentDirectoryInfo;
        }
        else
        {
          CurrentCDADocuementDirectoryInfo = RootCdaDocumentOutputDirectory;
        }

        CurrentCDADocuementDirectoryInfo.CreateDirectoryIfNoExist();
        CurrentCDADocuementDirectoryInfo.DeleteAllFileAndDirectories();
        //Generate HL7 v2 Messages for each Pathology Data json file      
        if (Options.GenerateCdaDocuments)
        {
          WriteLine("----------------------------------------------------------------------");
          WriteLine($"Generate CDA Documents");
          WriteLine($"Output Directory: {CurrentCDADocuementDirectoryInfo.FullName}");
        } 
        else
        {
          WriteLine("----------------------------------------------------------------------");
          WriteLine($"Temporarily generate CDA Documents for CDA Packaging");         
        }
        //Validation for CDA
        foreach (PathologyReportContainer PathologyReportContainer in PathologyReportContainerList)
        {
          var ErrorMessageList = new List<string>();
          if (!PathologyReportContainer.IsValid(PathologyReportContainer, PathologyReportModel.CustomAttribute.ScopeType.Cda, ErrorMessageList))
          {
            StringBuilder sb = new StringBuilder("CDA validation errors where detected:\n");
            ErrorMessageList.ForEach(x => sb.Append(x + "\n"));
            throw new ApplicationException(sb.ToString()); ;
          }
        }
        foreach (PathologyReportContainer PathologyReportContainer in PathologyReportContainerList)
        {
          var FileName = PathologyReportContainer.PathologyReport.PdfFileName;
          AdhaCdaFileGenerator CdaDocument = new AdhaCdaFileGenerator();
          CdaDocument.LogEventMessageDelegate = SpiaFileGenerator.WriteLine;
          CdaDocument.Process(PathologyReportContainer, RootPdfDirectory.FullName, CurrentCDADocuementDirectoryInfo.FullName, Logo);
        }
      }

      //Generate CDA Package
      if (Options.GenerateCdaPackages)
      {
        WriteLine("----------------------------------------------------------------------");
        WriteLine($"Generate CDA Packages");
        WriteLine($"Output Directory: {RootCdaPackagesOutputDirectory.FullName}");
        AdhaCdaPackageFileGenerator CdaPackager = new AdhaCdaPackageFileGenerator(Options.NashCertificateSerial);
        RootCdaPackagesOutputDirectory.CreateDirectoryIfNoExist();
        RootCdaPackagesOutputDirectory.DeleteAllFiles(".zip");
        CdaPackager.LogEventMessageDelegate = SpiaFileGenerator.WriteLine;
        CdaPackager.Process(Options.CdaPackagerApproverPerson, RootPdfDirectory.FullName, CurrentCDADocuementDirectoryInfo.FullName, RootCdaPackagesOutputDirectory.FullName, Logo);
        if (!Options.GenerateCdaDocuments)
        {
          CurrentCDADocuementDirectoryInfo.DeleteAllFiles(".xml");
        }
      }

    }

    public static void WriteLine(string Message)
    {
      Console.WriteLine(Message);
    }

  }
}
