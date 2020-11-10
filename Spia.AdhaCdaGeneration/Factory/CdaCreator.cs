using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nehta.VendorLibrary.CDA.Common;
using Nehta.VendorLibrary.CDA.SCSModel.Common;
using Nehta.VendorLibrary.CDA.Common.Enums;
using System.Collections.ObjectModel;
using Nehta.VendorLibrary.CDA;
using Nehta.VendorLibrary.CDA.SCSModel;
using System.Xml;

namespace Spia.AdhaCdaGeneration.Factory
{
  public class CdaCreator
  {
    private System.IO.DirectoryInfo TempWorkingDirectoryPath = null;
    
    public CdaCreator()
    {
      TempWorkingDirectoryPath = new System.IO.DirectoryInfo(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "CdaGeneratorWorkingDirectory"));
      if (!TempWorkingDirectoryPath.Exists)
      {
        TempWorkingDirectoryPath.Create();
      }
    }

    public void Process(Spia.PathologyReportModel.Model.PathologyReport Report, string CdaOutputDirectory, string PdfDirectory, byte[] LogoBytes = null)
    {      
      var pathologyResultReport = PathologyResultReport.CreatePathologyResultReport();

      // Include Logo
      if (LogoBytes != null)
      {
        pathologyResultReport.IncludeLogo = true;
        pathologyResultReport.LogoByte = LogoBytes;
      }
      else
      {
        pathologyResultReport.IncludeLogo = false;
      }


      // Set Creation Time      
      DateTimeOffset DocumentCreationDateTime = Report.GetOldestReportReleaseDateTime();
      pathologyResultReport.DocumentCreationTime = new ISO8601DateTime(DocumentCreationDateTime.DateTime, ISO8601DateTime.Precision.Second, DocumentCreationDateTime.Offset);


      #region Setup and populate the CDA context model

      // Setup and populate the CDA context model
      var cdaContext = PathologyResultReport.CreateCDAContext();
      // Document Id
      cdaContext.DocumentId = BaseCDAModel.CreateIdentifier(BaseCDAModel.CreateOid(), null);
      // Set Id  
      cdaContext.SetId = BaseCDAModel.CreateIdentifier(BaseCDAModel.CreateGuid(), null);

      // CDA Context Version , this needs to increment for each correction to the first report instance
      cdaContext.Version = "1";
      // Custodian
      cdaContext.Custodian = BaseCDAModel.CreateCustodian();

      //custodian/assignedCustodian
      cdaContext.Custodian.Participant = BaseCDAModel.CreateParticipantCustodian(); ;

      // custodian/assignedCustodian/representedCustodianOrganization
      cdaContext.Custodian.Participant.Organisation = BaseCDAModel.CreateOrganisationName();

      // custodian/assignedCustodian/representedCustodianOrganization/<Entity Identifier>
      cdaContext.Custodian.Participant.Organisation.Identifiers = new List<Identifier>
      {
        BaseCDAModel.CreateHealthIdentifier(HealthIdentifierType.HPIO, Report.PerformingLaboratory.Hpio.Replace(" ", string.Empty))
      };

      // custodian/assignedCustodian/representedCustodianOrganization/name
      cdaContext.Custodian.Participant.Organisation.Name = Report.PerformingLaboratory.FacilityName;

      ElectronicCommunicationDetail PerformingPathologyLabComms = BaseCDAModel.CreateElectronicCommunicationDetail(
            Report.PerformingLaboratory.BusinessPhoneNumber,
            ElectronicCommunicationMedium.Telephone,
            ElectronicCommunicationUsage.WorkPlace);

      // custodian/assignedCustodian/representedCustodianOrganization/<Address>        
      cdaContext.Custodian.Participant.Address = GetCdaAddress(Report.PerformingLaboratory.Address);

      // custodian/assignedCustodian/representedCustodianOrganization/<Electronic Communication Detail>        
      cdaContext.Custodian.Participant.ElectronicCommunicationDetail = PerformingPathologyLabComms;


      // Legal Authenticator
      cdaContext.LegalAuthenticator = BaseCDAModel.CreateLegalAuthenticator();
      // LegalAuthenticator/assignedEntity
      cdaContext.LegalAuthenticator.Participant = BaseCDAModel.CreateParticipantForLegalAuthenticator();

      // LegalAuthenticator/assignedEntity/assignedPerson
      cdaContext.LegalAuthenticator.Participant.Person = BaseCDAModel.CreatePerson();

      DateTimeOffset OldestReportReleaseDate = Report.GetOldestReportReleaseDateTime();
      // LegalAuthenticator/time/@value            
      cdaContext.LegalAuthenticator.Participant.DateTimeAuthenticated = new ISO8601DateTime(OldestReportReleaseDate.DateTime, ISO8601DateTime.Precision.Second, OldestReportReleaseDate.Offset);


      Spia.PathologyReportModel.Model.Provider FirstReportingPathologist = Report.ReportList.First().ReportingPathologist;      

      //LegalAuthenticator/assignedEntity/assignedPerson/<Person Name>
      var ReportingPathologistName = BaseCDAModel.CreatePersonName();
      if (FirstReportingPathologist.Name.Given is object)
      {
        ReportingPathologistName.GivenNames = new List<string> { FirstReportingPathologist.Name.Given };
      }

      ReportingPathologistName.FamilyName = FirstReportingPathologist.Name.Family;
      if (FirstReportingPathologist.Name.Title is object)
      {
        ReportingPathologistName.Titles = new List<string> { FirstReportingPathologist.Name.Title };
      }

      ReportingPathologistName.NameUsages = new List<NameUsage> { NameUsage.Legal };

      cdaContext.LegalAuthenticator.Participant.Person.PersonNames = new List<Nehta.VendorLibrary.CDA.IPersonName> { ReportingPathologistName };

      // LegalAuthenticator/assignedEntity/<Entity Identifier>
      cdaContext.LegalAuthenticator.Participant.Person.Identifiers = new List<Identifier>
      {
        BaseCDAModel.CreateHealthIdentifier(HealthIdentifierType.HPII, FirstReportingPathologist.IdentifierList.SingleOrDefault(x => x.Type == PathologyReportModel.Model.IdentifierType.HPII).Value.Replace(" ", ""))
      };

      // LegalAuthenticator/assignedEntity/code
      cdaContext.LegalAuthenticator.Role = BaseCDAModel.CreateRole(Occupation.Pathologist, Nehta.VendorLibrary.CDA.Generator.Enums.CodingSystem.ANZSCORevision1);

      cdaContext.LegalAuthenticator.Participant.Addresses = new List<IAddress>() { GetCdaAddress(Report.PerformingLaboratory.Address) };

      // LegalAuthenticator/assignedEntity/<Electronic Communication Detail>      
      cdaContext.LegalAuthenticator.Participant.ElectronicCommunicationDetails = new List<ElectronicCommunicationDetail> { PerformingPathologyLabComms };

      // LegalAuthenticator/assignedEntity/representedOrganization
      cdaContext.LegalAuthenticator.Participant.Organisation = BaseCDAModel.CreateOrganisationName();

      // LegalAuthenticator/assignedEntity/representedOrganization/name
      cdaContext.LegalAuthenticator.Participant.Organisation.Name = Report.PerformingLaboratory.FacilityName;

      // LegalAuthenticator/assignedEntity/representedOrganization/<Entity Identifier>
      cdaContext.LegalAuthenticator.Participant.Organisation.Identifiers = new List<Identifier>();

      var CodableConceptNataSiteNumber = BaseCDAModel.CreateCodableText();
      CodableConceptNataSiteNumber.Code = "XX";
      CodableConceptNataSiteNumber.CodeSystemName = "Identifier Type (HL7)";
      CodableConceptNataSiteNumber.CodeSystemCode = "2.16.840.1.113883.12.203";
      CodableConceptNataSiteNumber.DisplayName = "Organization identifier";

      var NataSiteNumber = BaseCDAModel.CreateIdentifier("1.2.36.1.2001.1005.74", Report.PerformingLaboratory.NataSiteNumber);
      NataSiteNumber.Code = CodableConceptNataSiteNumber;
      NataSiteNumber.AssigningGeographicArea = "National Identifier";
      NataSiteNumber.AssigningAuthorityName = "NATA Site Number";
      cdaContext.LegalAuthenticator.Participant.Organisation.Identifiers.Add(NataSiteNumber);

      //cdaContext.LegalAuthenticator.Participant.Organisation.Identifiers.Add(BaseCDAModel.CreateIdentifier("NATA", Nehta.VendorLibrary.CDA.Generator.Enums.HealthcareIdentifierGeographicArea.NationalIdentifier, PerformingPathologyLab.NataSiteNumber, "1.2.36.1.2001.1005.12", BaseCDAModel.CreateCodableText("AUSNATA", Nehta.VendorLibrary.CDA.Generator.Enums.CodingSystem.HL7IdentifierType, "National Association of Testing Authorities (NATA) Site Number")));
      cdaContext.LegalAuthenticator.Participant.Organisation.Identifiers.Add(BaseCDAModel.CreateHealthIdentifier(HealthIdentifierType.HPIO, Report.PerformingLaboratory.Hpio.Replace(" ", "")));

      pathologyResultReport.CDAContext = cdaContext;

      #endregion

      #region Setup and Populate the SCS Context model

      pathologyResultReport.SCSContext = PathologyResultReport.CreateSCSContext();

      // Author Health Care Provider
      pathologyResultReport.SCSContext.Author = BaseCDAModel.CreateAuthorHealthcareProvider();

      // Document Author > Participant
      pathologyResultReport.SCSContext.Author.Participant = BaseCDAModel.CreateParticipantForAuthorHealthcareProvider();

      pathologyResultReport.SCSContext.Author.AuthorParticipationPeriodOrDateTimeAuthored = BaseCDAModel.CreateParticipationPeriod(new ISO8601DateTime(OldestReportReleaseDate.DateTime, ISO8601DateTime.Precision.Second, OldestReportReleaseDate.Offset));

      // Document Author > Role = AddressPurpose.Residential
      pathologyResultReport.SCSContext.Author.Role = BaseCDAModel.CreateRole(Occupation.Pathologist, Nehta.VendorLibrary.CDA.Generator.Enums.CodingSystem.ANZSCORevision1);

      // Document Author > Participant > Person or Organisation or Device > Person > Person Name (Note: 1..* in ACI)
      pathologyResultReport.SCSContext.Author.Participant.Person = BaseCDAModel.CreatePersonHealthcareProvider();
      pathologyResultReport.SCSContext.Author.Participant.Person.PersonNames = new List<IPersonName> { ReportingPathologistName };

      //person.PersonNames = new List<IPersonName> { ReportingPathologistName };
      //if (!mandatoryOnly)
      //{
      //  // Not providing a family name will insert a nullflavor of 'NI'
      //  name.FamilyName = "Doctor family name";
      //}

      pathologyResultReport.SCSContext.Author.Participant.Person.Identifiers = cdaContext.LegalAuthenticator.Participant.Person.Identifiers;
      pathologyResultReport.SCSContext.Author.Participant.Person.Organisation = BaseCDAModel.CreateEmploymentOrganisation();
      pathologyResultReport.SCSContext.Author.Participant.Person.Organisation.Name = Report.PerformingLaboratory.FacilityName;
      pathologyResultReport.SCSContext.Author.Participant.Person.Organisation.Identifiers = new List<Identifier>()
      {
        BaseCDAModel.CreateHealthIdentifier(HealthIdentifierType.HPIO, Report.PerformingLaboratory.Hpio.Replace(" ", ""))
      };
      pathologyResultReport.SCSContext.Author.Participant.ElectronicCommunicationDetails = new List<ElectronicCommunicationDetail>() { PerformingPathologyLabComms };


      // Document Author > Participant > Addresses
      pathologyResultReport.SCSContext.Author.Participant.Addresses = new List<IAddress>
        {
          GetCdaAddress(Report.PerformingLaboratory.Address)
        };

      // Document Author > Participant > Address (Note: optional in ACI)
      pathologyResultReport.SCSContext.Author.Participant.Person.Organisation.Addresses = new List<IAddress>
        {
          GetCdaAddress(Report.PerformingLaboratory.Address)
        };

      pathologyResultReport.SCSContext.Author.Participant.Person.Organisation.Name = Report.PerformingLaboratory.FacilityName;
      pathologyResultReport.SCSContext.Author.Participant.Person.Organisation.NameUsage = OrganisationNameUsage.BusinessName;

      //pathologyResultReport.SCSContext.Author.Participant.Person.Organisation.Department = "Some department service provider";
      //pathologyResultReport.SCSContext.Author.Participant.Person.Organisation.EmploymentType = BaseCDAModel.CreateCodableText(null, null, null, "Casual", null);
      pathologyResultReport.SCSContext.Author.Participant.Person.Organisation.Occupation = BaseCDAModel.CreateRole(Occupation.Pathologist, Nehta.VendorLibrary.CDA.Generator.Enums.CodingSystem.ANZSCORevision1);
      //pathologyResultReport.SCSContext.Author.Participant.Person.Organisation.PositionInOrganisation = BaseCDAModel.CreateCodableText(null, null, null, "Manager", null);

      pathologyResultReport.SCSContext.Author.Participant.Person.Organisation.ElectronicCommunicationDetails = new List<ElectronicCommunicationDetail>
        {
          PerformingPathologyLabComms
        };


      // The Reporting Pathologist
      pathologyResultReport.SCSContext.ReportingPathologist = PathologyResultReport.CreateReportingPathologist();

      // Document reportingPathologist > Participant
      pathologyResultReport.SCSContext.ReportingPathologist.Participant = PathologyResultReport.CreateParticipantForReportingPathologist();

      pathologyResultReport.SCSContext.ReportingPathologist.Participant.Person = BaseCDAModel.CreatePersonWithOrganisation();
      pathologyResultReport.SCSContext.ReportingPathologist.Participant.Person.PersonNames = new List<IPersonName> { ReportingPathologistName };

      // Participation Period
      pathologyResultReport.SCSContext.ReportingPathologist.ParticipationEndTime = new ISO8601DateTime(OldestReportReleaseDate.DateTime, ISO8601DateTime.Precision.Second, OldestReportReleaseDate.Offset);

      // Document reportingPathologist > Role
      pathologyResultReport.SCSContext.ReportingPathologist.Role = PathologyResultReport.CreateRole(Occupation.Pathologist);

      // Document reportingPathologist > Participant > Address
      pathologyResultReport.SCSContext.ReportingPathologist.Participant.Addresses = new List<IAddress>()
      {
        GetCdaAddress(Report.PerformingLaboratory.Address)
      };

      pathologyResultReport.SCSContext.ReportingPathologist.Participant.ElectronicCommunicationDetails = new List<ElectronicCommunicationDetail> { PerformingPathologyLabComms };
      pathologyResultReport.SCSContext.ReportingPathologist.Participant.Person = pathologyResultReport.SCSContext.Author.Participant.Person;


      // Order Details
      pathologyResultReport.SCSContext.OrderDetails = DiagnosticImagingReport.CreateOrderDetails();

      // Requester Order Identifier
      //pathologyResultReport.SCSContext.OrderDetails.RequesterOrderIdentifier = BaseCDAModel.CreateIdentifier("1.2.36.1.2001.1005.52.8003620833333789", "10523479");

      // Requester
      pathologyResultReport.SCSContext.OrderDetails.Requester = BaseCDAModel.CreateRequester();

      pathologyResultReport.SCSContext.OrderDetails.Requester.ParticipationEndTime = new ISO8601DateTime(Report.Request.RequestedDate.DateTime, ISO8601DateTime.Precision.Day, Report.Request.RequestedDate.Offset);
      // Document Requester> Role
      pathologyResultReport.SCSContext.OrderDetails.Requester.Role = BaseCDAModel.CreateRole(Occupation.GeneralMedicalPractitioner, Nehta.VendorLibrary.CDA.Generator.Enums.CodingSystem.ANZSCORevision1);

      pathologyResultReport.SCSContext.OrderDetails.Requester.Participant = BaseCDAModel.CreateParticipantForRequester();
      pathologyResultReport.SCSContext.OrderDetails.Requester.Participant.Person = BaseCDAModel.CreatePersonWithOrganisation();
      // Participation Period


      var RequestingDoctorName = BaseCDAModel.CreatePersonName();
      RequestingDoctorName.FamilyName = Report.Request.RequestingProvider.Name.Family;
      if (Report.Request.RequestingProvider.Name.Given is object)
      {
        RequestingDoctorName.GivenNames = new List<string>() { Report.Request.RequestingProvider.Name.Given };
      }
      pathologyResultReport.SCSContext.OrderDetails.Requester.Participant.Person.PersonNames = new List<IPersonName>() { RequestingDoctorName };


      pathologyResultReport.SCSContext.OrderDetails.Requester.Participant.Person.Identifiers = new List<Identifier>();

      ///Get Requester HPI-I
      var RequesterHpii = Report.Request.RequestingProvider.IdentifierList.SingleOrDefault(x => x.Type == PathologyReportModel.Model.IdentifierType.HPII);
      if (RequesterHpii is object)
      {
        pathologyResultReport.SCSContext.OrderDetails.Requester.Participant.Person.Identifiers.Add(BaseCDAModel.CreateHealthIdentifier(HealthIdentifierType.HPII, RequesterHpii.Value.Replace(" ", "")));
      }

      ///Get Requester Medicare Provider Number
      var RequesterMedicareProvidernumber = Report.Request.RequestingProvider.IdentifierList.SingleOrDefault(x => x.Type == PathologyReportModel.Model.IdentifierType.MedicareProviderNumber);
      if (RequesterMedicareProvidernumber is object)
      {
        var MedicareProvidernumberIdentifier = BaseCDAModel.CreateIdentifier("Medicare", Nehta.VendorLibrary.CDA.Generator.Enums.HealthcareIdentifierGeographicArea.NationalIdentifier, RequesterMedicareProvidernumber.Value, "1.2.36.174030967.0.2", BaseCDAModel.CreateCodableText("AUSHICPR", Nehta.VendorLibrary.CDA.Generator.Enums.CodingSystem.HL7IdentifierType, "Medicare Provider Number"));
        pathologyResultReport.SCSContext.OrderDetails.Requester.Participant.Person.Identifiers.Add(MedicareProvidernumberIdentifier);
      }

      // Subject Of Care
      pathologyResultReport.SCSContext.SubjectOfCare = BaseCDAModel.CreateSubjectOfCare();
      pathologyResultReport.SCSContext.SubjectOfCare.Participant = BaseCDAModel.CreateParticipantForSubjectOfCare();
      pathologyResultReport.SCSContext.SubjectOfCare.Participant.Person = BaseCDAModel.CreatePersonForSubjectOfCare();

      pathologyResultReport.SCSContext.SubjectOfCare.Participant.Person.PersonNames = new List<IPersonName>();
      pathologyResultReport.SCSContext.SubjectOfCare.Participant.Person.PersonNames.Add(GetCdaPatientName(Report.Patient.Name));

      pathologyResultReport.SCSContext.SubjectOfCare.Participant.Person.Gender = GetCdaGender(Report.Patient.Gender);

      pathologyResultReport.SCSContext.SubjectOfCare.Participant.Person.DateOfBirth = new ISO8601DateTime(Report.Patient.DateOfBirth, ISO8601DateTime.Precision.Day);

      pathologyResultReport.SCSContext.SubjectOfCare.Participant.Person.IndigenousStatus = IndigenousStatus.NeitherAboriginalNorTorresStraitIslanderOrigin;

      //Phone numbers      
      if (Report.Patient.HomePhoneNumber is object)
      {
        if (pathologyResultReport.SCSContext.SubjectOfCare.Participant.ElectronicCommunicationDetails is null)
        {
          pathologyResultReport.SCSContext.SubjectOfCare.Participant.ElectronicCommunicationDetails = new List<ElectronicCommunicationDetail>();
        }

        //Work out if it is a Mobile number
        ElectronicCommunicationMedium ElectronicCommunicationMedium = ElectronicCommunicationMedium.Telephone;
        if (Report.Patient.HomePhoneNumber.Replace(" ", "").StartsWith("04", StringComparison.CurrentCultureIgnoreCase) || Report.Patient.HomePhoneNumber.Replace(" ", "").StartsWith("+614", StringComparison.CurrentCultureIgnoreCase))
        {
          ElectronicCommunicationMedium = ElectronicCommunicationMedium.Mobile;
        }
        pathologyResultReport.SCSContext.SubjectOfCare.Participant.ElectronicCommunicationDetails.Add(
          BaseCDAModel.CreateElectronicCommunicationDetail(
          Report.Patient.HomePhoneNumber,
          ElectronicCommunicationMedium,
          ElectronicCommunicationUsage.Home)
        );
      }

      //Address
      pathologyResultReport.SCSContext.SubjectOfCare.Participant.Addresses = new List<IAddress>();
      foreach (var Address in Report.Patient.AddressList)
      {
        pathologyResultReport.SCSContext.SubjectOfCare.Participant.Addresses.Add(GetCdaAddress(Address));
      }

      pathologyResultReport.SCSContext.SubjectOfCare.Participant.Person.Identifiers = new List<Identifier>();

      var IhiNumber = Report.Patient.IdentifierList.SingleOrDefault(x => x.Type == PathologyReportModel.Model.IdentifierType.IHI);
      if (IhiNumber is object)
      {
        pathologyResultReport.SCSContext.SubjectOfCare.Participant.Person.Identifiers.Add(BaseCDAModel.CreateHealthIdentifier(HealthIdentifierType.IHI, IhiNumber.Value.Replace(" ", "")));
      }

      var MedicareNumber = Report.Patient.IdentifierList.SingleOrDefault(x => x.Type == PathologyReportModel.Model.IdentifierType.MedicareNumber);
      if (MedicareNumber is object)
      {
        //Medicare Number goes in Entitlements as it is not a true identifier legally
        var MedicareEntitlement = BaseCDAModel.CreateEntitlement();
        MedicareEntitlement.Id = BaseCDAModel.CreateMedicareNumber(MedicareNumberType.MedicareCardNumber, MedicareNumber.Value.Replace(" ", ""));
        MedicareEntitlement.Type = EntitlementType.MedicareBenefits;
        //MedicareEntitlement.ValidityDuration = BaseCDAModel.CreateInterval(new ISO8601DateTime(DateTime.Now), new ISO8601DateTime(DateTime.Now));
        pathologyResultReport.SCSContext.SubjectOfCare.Participant.Entitlements = new List<Entitlement> { MedicareEntitlement };
      }

      //var MrnNumber = this.Input.Message.Segment("PID").Element(3).RepeatList.SingleOrDefault(x => x.Component(5).AsString.Equals("MR", StringComparison.CurrentCultureIgnoreCase));
      //if (MrnNumber is object)
      //{
      //  pathologyResultReport.SCSContext.SubjectOfCare.Participant.Person.Identifiers.Add(BaseCDAModel.CreateMedicalRecordNumber(MrnNumber.Component(1).AsString, "We need a root OID for the MRN  Facility", MrnNumber.Component(4).AsString));
      //}
      #endregion

      #region Setup and populate the SCS Content model

      // Setup and populate the SCS Content model
      pathologyResultReport.SCSContent = PathologyResultReport.CreateSCSContent();

      // Pathology Test Result
      string ReportLinkName = string.Empty;
      pathologyResultReport.SCSContent.PathologyTestResult = new List<Nehta.VendorLibrary.CDA.SCSModel.Pathology.PathologyTestResult>();
      foreach (var Panel in Report.ReportList)
      {
        var TestResult = PathologyResultReport.CreatePathologyTestResult();
        pathologyResultReport.SCSContent.PathologyTestResult.Add(TestResult);

        // Please note optional field - Note: This field is only displayed in the Narrative
        TestResult.ReportingPathologistForTestResult = Report.ReportList.First().ReportingPathologist.Name.Title ?? "";
        TestResult.ReportingPathologistForTestResult = $"{TestResult.ReportingPathologistForTestResult} {Report.ReportList.First().ReportingPathologist.Name.Family.ToUpper()}";
        TestResult.ReportingPathologistForTestResult = $"{TestResult.ReportingPathologistForTestResult}, {Report.ReportList.First().ReportingPathologist.Name.Given ?? ""}";


        if (!string.IsNullOrWhiteSpace(Panel.ReportType.Local.Oid))
        {
          TestResult.TestResultName = BaseCDAModel.CreateCodableText();
          TestResult.TestResultName.Code = Panel.ReportType.Local.Term;
          TestResult.TestResultName.DisplayName = Panel.ReportType.Local.Description;
          TestResult.TestResultName.CodeSystemCode = Panel.ReportType.Local.Oid;
          TestResult.TestResultName.OriginalText = Panel.ReportType.Local.Description;
        }
        else
        {
          TestResult.TestResultName = BaseCDAModel.CreateCodableText();
          TestResult.TestResultName.OriginalText = Panel.ReportType.Local.Description;
        }

        if (Panel.ReportType.Snomed is object)
        {
          TestResult.TestResultName.Translations = new List<ICodableTranslation>();
          TestResult.TestResultName.Translations.Add(BaseCDAModel.CreateCodableTranslation(Panel.ReportType.Snomed.Term, Nehta.VendorLibrary.CDA.Generator.Enums.CodingSystem.SNOMED, Panel.ReportType.Snomed.Description));
        }

        // Department Code
        TestResult.PathologyDiscipline = GetCdaDiagnosticService(Panel.Department);

        // ResultStatus
        TestResult.OverallTestResultStatus = GetCdaResultStatus(Panel.ReportStatus);

        // Test Specimen Detail
        TestResult.TestSpecimenDetail = PathologyResultReport.CreateTestSpecimenDetail();
        TestResult.TestSpecimenDetail.CollectionDateTime = new ISO8601DateTime(Panel.CollectionDateTime.DateTime, ISO8601DateTime.Precision.Minute, Panel.CollectionDateTime.Offset);
        TestResult.ObservationDateTime = TestResult.TestSpecimenDetail.CollectionDateTime;
      }

      // Related Document
      pathologyResultReport.SCSContent.RelatedDocument = PathologyResultReport.CreateRelatedDocument();
      System.IO.FileInfo PdfFileInfo = new System.IO.FileInfo(System.IO.Path.Combine(PdfDirectory, Report.PdfFileName));

      if (!PdfFileInfo.Exists)
      {
        throw new FieldAccessException($"Unable to locate PDF attachment file at path: {PdfFileInfo.FullName}");
      }

      if (!PdfFileInfo.Extension.EndsWith(".pdf", StringComparison.CurrentCultureIgnoreCase))
      {
        throw new FieldAccessException($"The report attachment file must be in a .pdf format.");
      }

      // Pathology PDF
      var AttachmentPdf = BaseCDAModel.CreateExternalData();
      AttachmentPdf.ExternalDataMediaType = Nehta.VendorLibrary.CDA.Generator.Enums.MediaType.PDF;

      //We have to move the PDF attachment file to a new place and then rename it to 'attachment.pdf' and then after 
      //generating the CDA document we will delete at copy of the PDF.  
      System.IO.FileInfo TempAttachmentFilePath = new System.IO.FileInfo(System.IO.Path.Combine(TempWorkingDirectoryPath.FullName, "attachment.pdf"));
      PdfFileInfo.CopyTo(TempAttachmentFilePath.FullName);
      AttachmentPdf.Path = TempAttachmentFilePath.FullName;
      pathologyResultReport.SCSContent.RelatedDocument.ExaminationResultRepresentation = AttachmentPdf;

      // Document Provenance
      pathologyResultReport.SCSContent.RelatedDocument.DocumentDetails = BaseCDAModel.CreateDocumentDetails();

      // Report Identifier
      pathologyResultReport.SCSContent.RelatedDocument.DocumentDetails.ReportIdentifier = BaseCDAModel.CreateIdentifier($"1.2.36.1.2001.1005.54.{Report.PerformingLaboratory.Hpio.Replace(" ", "")}", Report.ReportList.First().ReportId);

      // Report Date 
      pathologyResultReport.SCSContent.RelatedDocument.DocumentDetails.ReportDate = new ISO8601DateTime(Report.GetOldestReportReleaseDateTime().DateTime, ISO8601DateTime.Precision.Second, Report.GetOldestReportReleaseDateTime().Offset);

      // Result Status
      pathologyResultReport.SCSContent.RelatedDocument.DocumentDetails.ReportStatus = GetCdaResultStatus(Report.GetRolledUpReportStatus());

      // Report Name       
      if (Report.ReportList.Count == 1)
      {
        pathologyResultReport.SCSContent.RelatedDocument.DocumentDetails.ReportDescription = Report.ReportList[0].ReportType.Local.Description;
      }
      else
      {
        pathologyResultReport.SCSContent.RelatedDocument.DocumentDetails.ReportDescription = "Pathology Report";
      }


      #endregion


      XmlDocument xmlDoc;
      try
      {
        Nehta.VendorLibrary.CDA.Generator.CDAGenerator.NarrativeGenerator = new Nehta.VendorLibrary.CDA.Generator.CDANarrativeGenerator();

        //Pass the Event Summary model into the GeneratePathologyResultReport method 
        xmlDoc = Nehta.VendorLibrary.CDA.Generator.CDAGenerator.GeneratePathologyResultReport(pathologyResultReport);

        System.IO.DirectoryInfo CdaOutputDirectoryInfo = new System.IO.DirectoryInfo(CdaOutputDirectory);
        System.IO.FileInfo CdaFileInfo = new System.IO.FileInfo(System.IO.Path.Combine(CdaOutputDirectoryInfo.FullName, Report.PdfFileName.Replace(".pdf", ".xml")));
        if (System.IO.File.Exists(CdaFileInfo.FullName))
        {
          System.IO.File.Delete(CdaFileInfo.FullName);
        }

        using (var writer = XmlWriter.Create(CdaFileInfo.FullName, new XmlWriterSettings { Indent = true }))
        {
          xmlDoc.Save(writer);
        }
      }
      catch (Nehta.VendorLibrary.Common.ValidationException ex)
      {
        //Catch any validation exceptions
        var validationMessages = ex.GetMessagesString();
        throw new ApplicationException($"Error encountered in generating CDA document: {validationMessages}");
      }
      finally
      {
        TempAttachmentFilePath.Delete();
      }
    }
    private ICodableText GetCdaResultStatus(Spia.PathologyReportModel.Model.ResultStatusType ReportStatusType)
    {
      switch (ReportStatusType)
      {
        case PathologyReportModel.Model.ResultStatusType.Final:
          return BaseCDAModel.CreateResultStatus(Hl7V3ResultStatus.FinalResults, "Final");
        case PathologyReportModel.Model.ResultStatusType.Preliminary:
          return BaseCDAModel.CreateResultStatus(Hl7V3ResultStatus.Preliminary, "Preliminary");
        case PathologyReportModel.Model.ResultStatusType.NoResultsAvailableOrderCanceled:
          return BaseCDAModel.CreateResultStatus(Hl7V3ResultStatus.NoResultsAvailableOrderCanceled, "OrderCanceled");
        case PathologyReportModel.Model.ResultStatusType.Correction:
          return BaseCDAModel.CreateResultStatus(Hl7V3ResultStatus.CorrectionToResults, "Correction");
        default:
          throw new ApplicationException($"Unable to map result status to CDA result status. Status was: {ReportStatusType.ToString()}");
      }
    }    
    private DiagnosticService GetCdaDiagnosticService(Spia.PathologyReportModel.Model.DiagnosticService diagnosticService)
    {
      switch (diagnosticService)
      {
        case PathologyReportModel.Model.DiagnosticService.Audiology:
          return DiagnosticService.Audiology;
        case PathologyReportModel.Model.DiagnosticService.BedsideICUMonitoring:
          return DiagnosticService.BedsideICUMonitoring;
        case PathologyReportModel.Model.DiagnosticService.BloodBank:
          return DiagnosticService.BloodBank;
        case PathologyReportModel.Model.DiagnosticService.Cytogenetics:
          return DiagnosticService.Other;
        case PathologyReportModel.Model.DiagnosticService.BloodGases:
          return DiagnosticService.BloodGases;
        case PathologyReportModel.Model.DiagnosticService.CardiacCatheterization:
          return DiagnosticService.CardiacCatheterization;
        case PathologyReportModel.Model.DiagnosticService.CardiacUltrasound:
          return DiagnosticService.CardiacUltrasound;
        case PathologyReportModel.Model.DiagnosticService.CATScan:
          return DiagnosticService.CATScan;
        case PathologyReportModel.Model.DiagnosticService.Chemistry:
          return DiagnosticService.Chemistry;
        case PathologyReportModel.Model.DiagnosticService.Cineradiograph:
          return DiagnosticService.Cineradiograph;
        case PathologyReportModel.Model.DiagnosticService.Cytopathology:
          return DiagnosticService.Cytopathology;
        case PathologyReportModel.Model.DiagnosticService.Electrocardiac:
          return DiagnosticService.Electrocardiac;
        case PathologyReportModel.Model.DiagnosticService.Electroneuro:
          return DiagnosticService.Electroneuro;
        case PathologyReportModel.Model.DiagnosticService.Genetics:
          return DiagnosticService.Other;
        case PathologyReportModel.Model.DiagnosticService.Hematology:
          return DiagnosticService.Hematology;
        case PathologyReportModel.Model.DiagnosticService.Immunology:
          return DiagnosticService.Immunology;
        case PathologyReportModel.Model.DiagnosticService.Laboratory:
          return DiagnosticService.Laboratory;
        case PathologyReportModel.Model.DiagnosticService.Microbiology:
          return DiagnosticService.Microbiology;
        case PathologyReportModel.Model.DiagnosticService.Mycobacteriology:
          return DiagnosticService.Mycobacteriology;
        case PathologyReportModel.Model.DiagnosticService.Mycology:
          return DiagnosticService.Mycology;
        case PathologyReportModel.Model.DiagnosticService.NuclearMagneticResonance:
          return DiagnosticService.NuclearMagneticResonance;
        case PathologyReportModel.Model.DiagnosticService.NuclearMedicineScan:
          return DiagnosticService.NuclearMedicineScan;
        case PathologyReportModel.Model.DiagnosticService.NursingServiceMeasures:
          return DiagnosticService.NursingServiceMeasures;
        case PathologyReportModel.Model.DiagnosticService.OBUltrasound:
          return DiagnosticService.OBUltrasound;
        case PathologyReportModel.Model.DiagnosticService.OccupationalTherapy:
          return DiagnosticService.OccupationalTherapy;
        case PathologyReportModel.Model.DiagnosticService.Other:
          return DiagnosticService.Other;
        case PathologyReportModel.Model.DiagnosticService.OutsideLab:
          return DiagnosticService.OutsideLab;
        case PathologyReportModel.Model.DiagnosticService.Pharmacy:
          return DiagnosticService.Pharmacy;
        case PathologyReportModel.Model.DiagnosticService.PhysicalTherapy:
          return DiagnosticService.PhysicalTherapy;
        case PathologyReportModel.Model.DiagnosticService.Physician:
          return DiagnosticService.Physician;
        case PathologyReportModel.Model.DiagnosticService.PulmonaryFunction:
          return DiagnosticService.PulmonaryFunction;
        case PathologyReportModel.Model.DiagnosticService.RadiationTherapy:
          return DiagnosticService.RadiationTherapy;
        case PathologyReportModel.Model.DiagnosticService.Radiograph:
          return DiagnosticService.Radiograph;
        case PathologyReportModel.Model.DiagnosticService.Radiology:
          return DiagnosticService.Radiology;
        case PathologyReportModel.Model.DiagnosticService.RadiologyUltrasound:
          return DiagnosticService.RadiologyUltrasound;
        case PathologyReportModel.Model.DiagnosticService.RespiratoryCare:
          return DiagnosticService.RespiratoryCare;
        case PathologyReportModel.Model.DiagnosticService.HistologyAndAnatomicalPathology:
          return DiagnosticService.Laboratory;
        case PathologyReportModel.Model.DiagnosticService.Serology:
          return DiagnosticService.Serology;
        case PathologyReportModel.Model.DiagnosticService.SurgicalPathology:
          return DiagnosticService.SurgicalPathology;
        case PathologyReportModel.Model.DiagnosticService.Toxicology:
          return DiagnosticService.Toxicology;
        case PathologyReportModel.Model.DiagnosticService.VascularUltrasound:
          return DiagnosticService.VascularUltrasound;
        case PathologyReportModel.Model.DiagnosticService.Virology:
          return DiagnosticService.Virology;
        default:
          throw new ApplicationException($"Unable to translate the {nameof(PathologyReportModel.Model.DiagnosticService)} of {diagnosticService.ToString()} to a CDA equivalent.");
      }
    }    
    private IPersonName GetCdaPatientName(Spia.PathologyReportModel.Model.Name Name)
    {
      var CdaName = BaseCDAModel.CreatePersonName();
      CdaName.FamilyName = Name.Family;
      if (Name.Given is object)
      {
        CdaName.GivenNames = new List<string>() { Name.Given };
        if (Name.Middle is object)
        {
          CdaName.GivenNames.Add(Name.Middle);
        }
      }

      if (Name.Title is object)
      {
        CdaName.Titles = new List<string>() { Name.Title };
      }
      CdaName.NameUsages = new List<NameUsage>() { NameUsage.Legal };
      return CdaName;
    }
    private Gender GetCdaGender(Spia.PathologyReportModel.Model.GenderType GenderType)
    {
      switch (GenderType)
      {
        case PathologyReportModel.Model.GenderType.Ambiguous:
          return Gender.IntersexOrIndeterminate;
        case PathologyReportModel.Model.GenderType.Female:
          return Gender.Female;
        case PathologyReportModel.Model.GenderType.Male:
          return Gender.Male;
        case PathologyReportModel.Model.GenderType.NotApplicable:
          return Gender.NotStated;
        case PathologyReportModel.Model.GenderType.Other:
          return Gender.IntersexOrIndeterminate;
        case PathologyReportModel.Model.GenderType.Unknown:
          return Gender.NotStated;
        default:
          return Gender.Undefined;
      }
    }
    private IAddress GetCdaAddress(Spia.PathologyReportModel.Model.Address Address)
    {
      IAddress CdaAddress = BaseCDAModel.CreateAddress();
      switch (Address.TypeCode)
      {
        case PathologyReportModel.Model.AddressType.FirmBusiness:
          CdaAddress.AddressPurpose = AddressPurpose.Business;
          break;
        case PathologyReportModel.Model.AddressType.BadAddress:
          CdaAddress.AddressPurpose = AddressPurpose.NotStatedUnknownInadequatelyDescribed;
          break;
        case PathologyReportModel.Model.AddressType.BirthDeliveryLocation:
          CdaAddress.AddressPurpose = AddressPurpose.Residential;
          break;
        case PathologyReportModel.Model.AddressType.ResidenceAtbirth:
          CdaAddress.AddressPurpose = AddressPurpose.Residential;
          break;
        case PathologyReportModel.Model.AddressType.CurrentOrTemporary:
          CdaAddress.AddressPurpose = AddressPurpose.TemporaryAccommodation;
          break;
        case PathologyReportModel.Model.AddressType.CountryOfOrigin:
          CdaAddress.AddressPurpose = AddressPurpose.Residential;
          break;
        case PathologyReportModel.Model.AddressType.Home:
          CdaAddress.AddressPurpose = AddressPurpose.Residential;
          break;
        case PathologyReportModel.Model.AddressType.LegalAddress:
          CdaAddress.AddressPurpose = AddressPurpose.MailingOrPostal;
          break;
        case PathologyReportModel.Model.AddressType.Mailing:
          CdaAddress.AddressPurpose = AddressPurpose.MailingOrPostal;
          break;
        case PathologyReportModel.Model.AddressType.Birth:
          CdaAddress.AddressPurpose = AddressPurpose.Residential;
          break;
        case PathologyReportModel.Model.AddressType.Office:
          CdaAddress.AddressPurpose = AddressPurpose.Business;
          break;
        case PathologyReportModel.Model.AddressType.Permanent:
          CdaAddress.AddressPurpose = AddressPurpose.Residential;
          break;
        case PathologyReportModel.Model.AddressType.RegistryHome:
          CdaAddress.AddressPurpose = AddressPurpose.Residential;
          break;
        default:
          throw new ApplicationException($"Unable to convert Address {nameof(Address.TypeCode)} of {Address.TypeCode.ToString()} to a CDA address type");
      }
      CdaAddress.AustralianAddress = BaseCDAModel.CreateAustralianAddress();
      CdaAddress.AustralianAddress.UnstructuredAddressLines = new List<string> { Address.LineOne };
      if (!string.IsNullOrWhiteSpace(Address.LineTwo))
      {
        CdaAddress.AustralianAddress.UnstructuredAddressLines.Add(Address.LineTwo);
      }
      CdaAddress.AustralianAddress.UnstructuredAddressLines = new List<string> { Address.LineOne };
      CdaAddress.AustralianAddress.SuburbTownLocality = Address.Suburb;
      CdaAddress.AustralianAddress.PostCode = Address.PostCode;

      switch (Address.State)
      {
        case Spia.PathologyReportModel.Model.StateType.WA:
          CdaAddress.AustralianAddress.State = AustralianState.WA;
          break;
        case Spia.PathologyReportModel.Model.StateType.NT:
          CdaAddress.AustralianAddress.State = AustralianState.NT;
          break;
        case Spia.PathologyReportModel.Model.StateType.QLD:
          CdaAddress.AustralianAddress.State = AustralianState.QLD;
          break;
        case Spia.PathologyReportModel.Model.StateType.ACT:
          CdaAddress.AustralianAddress.State = AustralianState.ACT;
          break;
        case Spia.PathologyReportModel.Model.StateType.NSW:
          CdaAddress.AustralianAddress.State = AustralianState.NSW;
          break;
        case Spia.PathologyReportModel.Model.StateType.VIC:
          CdaAddress.AustralianAddress.State = AustralianState.VIC;
          break;
        case Spia.PathologyReportModel.Model.StateType.SA:
          CdaAddress.AustralianAddress.State = AustralianState.SA;
          break;
        case Spia.PathologyReportModel.Model.StateType.TAZ:
          CdaAddress.AustralianAddress.State = AustralianState.TAS;
          break;
        default:
          CdaAddress.AustralianAddress.State = AustralianState.Undefined;
          break;
      }
      return CdaAddress;
    }   

  }
}
