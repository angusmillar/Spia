using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PeterPiper.Hl7.V2.Model;
using Nehta.VendorLibrary.CDA.Common;
using Nehta.VendorLibrary.CDA.SCSModel.Common;
using Nehta.VendorLibrary.CDA.Common.Enums;
using System.Collections.ObjectModel;
using Nehta.VendorLibrary.CDA;
using Nehta.VendorLibrary.CDA.SCSModel;
using System.Xml;
using Spia.AdhaCdaGeneration.Model;

namespace Spia.AdhaCdaGeneration.Factory
{
  public class CdaCreator
  {
    private System.IO.DirectoryInfo TempWorkingDirectoryPath = null;
    private CdaCreatorInput Input;

    public CdaCreator()
    {
      TempWorkingDirectoryPath = new System.IO.DirectoryInfo(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "CdaGeneratorWorkingDirectory"));
      if (!TempWorkingDirectoryPath.Exists)
      {
        TempWorkingDirectoryPath.Create();
      }
    }

    public void Process2(Spia.PathologyReportModel.Model.PathologyReport Report, byte[] LogoBytes = null)
    {      
      TempWorkingDirectoryPath.GetFiles().ToList().ForEach(x => System.IO.File.Delete(x.FullName));

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
      DateTimeOffset DocumentCreationDateTime = Report.GetOldestReportReleaseDateTime().AddMinutes(2);
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

      PathologyLab PerformingPathologyLab = null;
      
      // custodian/assignedCustodian/representedCustodianOrganization/<Entity Identifier>
      cdaContext.Custodian.Participant.Organisation.Identifiers = new List<Identifier>
      {
        BaseCDAModel.CreateHealthIdentifier(HealthIdentifierType.HPIO, Report.PerformingLaboratory.Hpio.Replace(" ", string.Empty))
      };

      // custodian/assignedCustodian/representedCustodianOrganization/name
      cdaContext.Custodian.Participant.Organisation.Name = Report.PerformingLaboratory.FacilityName;

      ElectronicCommunicationDetail PerformingPathologyLabComms = BaseCDAModel.CreateElectronicCommunicationDetail(
            PerformingPathologyLab.Phone,
            ElectronicCommunicationMedium.Telephone,
            ElectronicCommunicationUsage.WorkPlace);

      if (!this.Input.IsMandatoryCDAElementsOnly)
      {
        // custodian/assignedCustodian/representedCustodianOrganization/<Address>        
        cdaContext.Custodian.Participant.Address = GetCdaAddress(PerformingPathologyLab.Address);

        // custodian/assignedCustodian/representedCustodianOrganization/<Electronic Communication Detail>        
        cdaContext.Custodian.Participant.ElectronicCommunicationDetail = PerformingPathologyLabComms;
      }

      // Legal Authenticator
      cdaContext.LegalAuthenticator = BaseCDAModel.CreateLegalAuthenticator();
      // LegalAuthenticator/assignedEntity
      cdaContext.LegalAuthenticator.Participant = BaseCDAModel.CreateParticipantForLegalAuthenticator();

      // LegalAuthenticator/assignedEntity/assignedPerson
      cdaContext.LegalAuthenticator.Participant.Person = BaseCDAModel.CreatePerson();

      DateTimeOffset OldestReportReleaseDate = GetOldestDateTime(this.Input.Message.SegmentList("OBR").Select(y => y.Field(22)));
      // LegalAuthenticator/time/@value            
      cdaContext.LegalAuthenticator.Participant.DateTimeAuthenticated = new ISO8601DateTime(OldestReportReleaseDate.DateTime, ISO8601DateTime.Precision.Second, OldestReportReleaseDate.Offset);

      string FirstReportingPathologistsId = Input.Message.Segment("OBR").Field(32).Component(1).SubComponent(1).AsString;
      if (!Input.Message.SegmentList("OBR").All(x => x.Field(32).Component(1).SubComponent(1).AsString.Equals(FirstReportingPathologistsId, StringComparison.CurrentCultureIgnoreCase)))
      {
        throw new ApplicationException("Only HL7 v2 messages with the same Reporting Pathologist Id in all OBR-32.1.1 segments are supported. Multiple reporting pathologists are not supported by this conversion tool.");
      }

      //LegalAuthenticator/assignedEntity/assignedPerson/<Person Name>
      var ReportingPathologistName = BaseCDAModel.CreatePersonName();
      ReportingPathologistName.GivenNames = new List<string> { Input.Message.Segment("OBR").Field(32).Component(1).SubComponent(3).AsString };
      ReportingPathologistName.FamilyName = Input.Message.Segment("OBR").Field(32).Component(1).SubComponent(2).AsString;
      ReportingPathologistName.Titles = new List<string> { Input.Message.Segment("OBR").Field(32).Component(1).SubComponent(6).AsString };
      ReportingPathologistName.NameUsages = new List<NameUsage> { NameUsage.Legal };

      cdaContext.LegalAuthenticator.Participant.Person.PersonNames = new List<Nehta.VendorLibrary.CDA.IPersonName> { ReportingPathologistName };

      Pathologist ReportingPathologistCDAMetadata = PerformingPathologyLab.ReportingPathologistsList.SingleOrDefault(x => x.LocalCode.Equals(FirstReportingPathologistsId, StringComparison.CurrentCultureIgnoreCase));
      if (ReportingPathologistCDAMetadata is null)
      {
        throw new ApplicationException($"Not ReportingPathologist CDA meta data found for the HL7 v2 OBR-32.1.1 ID of : {FirstReportingPathologistsId}");
      }

      // LegalAuthenticator/assignedEntity/<Entity Identifier>
      cdaContext.LegalAuthenticator.Participant.Person.Identifiers = new List<Identifier>
      {
        BaseCDAModel.CreateHealthIdentifier(HealthIdentifierType.HPII, ReportingPathologistCDAMetadata.Hpii)
      };

      if (!this.Input.IsMandatoryCDAElementsOnly)
      {
        // LegalAuthenticator/assignedEntity/code
        cdaContext.LegalAuthenticator.Role = BaseCDAModel.CreateRole(Occupation.Pathologist, Nehta.VendorLibrary.CDA.Generator.Enums.CodingSystem.ANZSCORevision1);

        cdaContext.LegalAuthenticator.Participant.Addresses = new List<IAddress>() { GetCdaAddress(PerformingPathologyLab.Address) };

        // LegalAuthenticator/assignedEntity/<Electronic Communication Detail>      
        cdaContext.LegalAuthenticator.Participant.ElectronicCommunicationDetails = new List<ElectronicCommunicationDetail> { PerformingPathologyLabComms };

        // LegalAuthenticator/assignedEntity/representedOrganization
        cdaContext.LegalAuthenticator.Participant.Organisation = BaseCDAModel.CreateOrganisationName();

        // LegalAuthenticator/assignedEntity/representedOrganization/name
        cdaContext.LegalAuthenticator.Participant.Organisation.Name = PerformingPathologyLab.Name;

        // LegalAuthenticator/assignedEntity/representedOrganization/<Entity Identifier>
        cdaContext.LegalAuthenticator.Participant.Organisation.Identifiers = new List<Identifier>();


        var CodableConceptNataSiteNumber = BaseCDAModel.CreateCodableText();
        CodableConceptNataSiteNumber.Code = "XX";
        CodableConceptNataSiteNumber.CodeSystemName = "Identifier Type (HL7)";
        CodableConceptNataSiteNumber.CodeSystemCode = "2.16.840.1.113883.12.203";
        CodableConceptNataSiteNumber.DisplayName = "Organization identifier";

        var NataSiteNumber = BaseCDAModel.CreateIdentifier("1.2.36.1.2001.1005.74", PerformingPathologyLab.NataSiteNumber);
        NataSiteNumber.Code = CodableConceptNataSiteNumber;
        NataSiteNumber.AssigningGeographicArea = "National Identifier";
        NataSiteNumber.AssigningAuthorityName = "NATA Site Number";
        cdaContext.LegalAuthenticator.Participant.Organisation.Identifiers.Add(NataSiteNumber);

        //cdaContext.LegalAuthenticator.Participant.Organisation.Identifiers.Add(BaseCDAModel.CreateIdentifier("NATA", Nehta.VendorLibrary.CDA.Generator.Enums.HealthcareIdentifierGeographicArea.NationalIdentifier, PerformingPathologyLab.NataSiteNumber, "1.2.36.1.2001.1005.12", BaseCDAModel.CreateCodableText("AUSNATA", Nehta.VendorLibrary.CDA.Generator.Enums.CodingSystem.HL7IdentifierType, "National Association of Testing Authorities (NATA) Site Number")));
        cdaContext.LegalAuthenticator.Participant.Organisation.Identifiers.Add(BaseCDAModel.CreateHealthIdentifier(HealthIdentifierType.HPIO, PerformingPathologyLab.Hpio));
      }
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
      pathologyResultReport.SCSContext.Author.Participant.Person.Organisation.Name = PerformingPathologyLab.Name;
      pathologyResultReport.SCSContext.Author.Participant.Person.Organisation.Identifiers = new List<Identifier>()
      {
        BaseCDAModel.CreateHealthIdentifier(HealthIdentifierType.HPIO, PerformingPathologyLab.Hpio)
      };
      pathologyResultReport.SCSContext.Author.Participant.ElectronicCommunicationDetails = new List<ElectronicCommunicationDetail>() { PerformingPathologyLabComms };

      if (!this.Input.IsMandatoryCDAElementsOnly)
      {

        // Document Author > Participant > Addresses
        pathologyResultReport.SCSContext.Author.Participant.Addresses = new List<IAddress>
        {
          GetCdaAddress(PerformingPathologyLab.Address)
        };

        // Document Author > Participant > Address (Note: optional in ACI)
        pathologyResultReport.SCSContext.Author.Participant.Person.Organisation.Addresses = new List<IAddress>
        {
          GetCdaAddress(PerformingPathologyLab.Address)
        };

        pathologyResultReport.SCSContext.Author.Participant.Person.Organisation.Name = PerformingPathologyLab.Name;
        pathologyResultReport.SCSContext.Author.Participant.Person.Organisation.NameUsage = OrganisationNameUsage.BusinessName;

        //pathologyResultReport.SCSContext.Author.Participant.Person.Organisation.Department = "Some department service provider";
        //pathologyResultReport.SCSContext.Author.Participant.Person.Organisation.EmploymentType = BaseCDAModel.CreateCodableText(null, null, null, "Casual", null);
        pathologyResultReport.SCSContext.Author.Participant.Person.Organisation.Occupation = BaseCDAModel.CreateRole(Occupation.Pathologist, Nehta.VendorLibrary.CDA.Generator.Enums.CodingSystem.ANZSCORevision1);
        //pathologyResultReport.SCSContext.Author.Participant.Person.Organisation.PositionInOrganisation = BaseCDAModel.CreateCodableText(null, null, null, "Manager", null);

        pathologyResultReport.SCSContext.Author.Participant.Person.Organisation.ElectronicCommunicationDetails = new List<ElectronicCommunicationDetail>
        {
          PerformingPathologyLabComms
        };
      }

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
        GetCdaAddress(PerformingPathologyLab.Address)
      };

      pathologyResultReport.SCSContext.ReportingPathologist.Participant.ElectronicCommunicationDetails = new List<ElectronicCommunicationDetail> { PerformingPathologyLabComms };
      pathologyResultReport.SCSContext.ReportingPathologist.Participant.Person = pathologyResultReport.SCSContext.Author.Participant.Person;


      // Order Details
      pathologyResultReport.SCSContext.OrderDetails = DiagnosticImagingReport.CreateOrderDetails();

      // Requester Order Identifier
      //pathologyResultReport.SCSContext.OrderDetails.RequesterOrderIdentifier = BaseCDAModel.CreateIdentifier("1.2.36.1.2001.1005.52.8003620833333789", "10523479");

      // Requester
      pathologyResultReport.SCSContext.OrderDetails.Requester = BaseCDAModel.CreateRequester();

      DateTimeOffset OldestRequestedDateTime = GetOldestDateTime(this.Input.Message.SegmentList("OBR").Select(y => y.Field(27).Component(4)));
      pathologyResultReport.SCSContext.OrderDetails.Requester.ParticipationEndTime = new ISO8601DateTime(OldestRequestedDateTime.DateTime, ISO8601DateTime.Precision.Day, OldestRequestedDateTime.Offset);
      // Document Requester> Role
      pathologyResultReport.SCSContext.OrderDetails.Requester.Role = BaseCDAModel.CreateRole(Occupation.GeneralMedicalPractitioner, Nehta.VendorLibrary.CDA.Generator.Enums.CodingSystem.ANZSCORevision1);

      pathologyResultReport.SCSContext.OrderDetails.Requester.Participant = BaseCDAModel.CreateParticipantForRequester();
      pathologyResultReport.SCSContext.OrderDetails.Requester.Participant.Person = BaseCDAModel.CreatePersonWithOrganisation();
      // Participation Period


      var RequestingDoctorName = BaseCDAModel.CreatePersonName();
      RequestingDoctorName.FamilyName = this.Input.Message.Segment("OBR").Element(16).Repeat(1).Component(2).AsString;
      RequestingDoctorName.GivenNames = new List<string>() { this.Input.Message.Segment("OBR").Element(16).Repeat(1).Component(3).AsString };
      pathologyResultReport.SCSContext.OrderDetails.Requester.Participant.Person.PersonNames = new List<IPersonName>() { RequestingDoctorName };


      pathologyResultReport.SCSContext.OrderDetails.Requester.Participant.Person.Identifiers = new List<Identifier>();

      ///Get Requester HPI-I
      var RequesterHpii = this.Input.Message.Segment("OBR").ElementList.SingleOrDefault(x => x.Component(9).AsString.Equals("AUSHIC", StringComparison.CurrentCultureIgnoreCase) && x.Component(13).AsString.Equals("NPI", StringComparison.CurrentCultureIgnoreCase));
      if (RequesterHpii is object)
      {
        pathologyResultReport.SCSContext.OrderDetails.Requester.Participant.Person.Identifiers.Add(BaseCDAModel.CreateHealthIdentifier(HealthIdentifierType.HPII, RequesterHpii.Component(1).AsString));
      }

      ///Get Requester Medicare Provider Number
      var RequesterMedicareProvidernumber = this.Input.Message.Segment("OBR").ElementList.SingleOrDefault(x => x.Component(9).AsString.Equals("AUSHICPR", StringComparison.CurrentCultureIgnoreCase));
      if (RequesterMedicareProvidernumber is object)
      {
        var MedicareProvidernumberIdentifier = BaseCDAModel.CreateIdentifier("Medicare", Nehta.VendorLibrary.CDA.Generator.Enums.HealthcareIdentifierGeographicArea.NationalIdentifier, RequesterMedicareProvidernumber.Component(1).AsString, "1.2.36.174030967.0.2", BaseCDAModel.CreateCodableText("AUSHICPR", Nehta.VendorLibrary.CDA.Generator.Enums.CodingSystem.HL7IdentifierType, "Medicare Provider Number"));
        pathologyResultReport.SCSContext.OrderDetails.Requester.Participant.Person.Identifiers.Add(MedicareProvidernumberIdentifier);
      }

      // Subject Of Care
      pathologyResultReport.SCSContext.SubjectOfCare = BaseCDAModel.CreateSubjectOfCare();
      pathologyResultReport.SCSContext.SubjectOfCare.Participant = BaseCDAModel.CreateParticipantForSubjectOfCare();
      pathologyResultReport.SCSContext.SubjectOfCare.Participant.Person = BaseCDAModel.CreatePersonForSubjectOfCare();

      pathologyResultReport.SCSContext.SubjectOfCare.Participant.Person.PersonNames = new List<IPersonName>();
      foreach (var XPN in this.Input.Message.Segment("PID").Element(5).RepeatList)
      {
        pathologyResultReport.SCSContext.SubjectOfCare.Participant.Person.PersonNames.Add(GetCdaPatientName(XPN));
      }

      pathologyResultReport.SCSContext.SubjectOfCare.Participant.Person.Gender = GetCdaGender(this.Input.Message.Segment("PID").Field(8).AsString);
      if (this.Input.Message.Segment("PID").Field(7).Convert.DateTime.CanParseToDateTimeOffset)
      {
        DateTimeOffset DateOfBirth = this.Input.Message.Segment("PID").Field(7).Convert.DateTime.GetDateTimeOffset();
        pathologyResultReport.SCSContext.SubjectOfCare.Participant.Person.DateOfBirth = new ISO8601DateTime(DateOfBirth.DateTime, ISO8601DateTime.Precision.Day);
      }
      else
      {
        throw new ApplicationException($"Unable to source Date of Birth from PID-8. Value found was: {this.Input.Message.Segment("PID").Field(8).AsString}");
      }

      pathologyResultReport.SCSContext.SubjectOfCare.Participant.Person.IndigenousStatus = IndigenousStatus.NeitherAboriginalNorTorresStraitIslanderOrigin;

      //Phone numbers
      var PatientCommsList = new List<ElectronicCommunicationDetail>();
      ElectronicCommunicationDetail HomePhone = GetCdaComms(this.Input.Message.Segment("PID").Field(13));
      if (HomePhone is object)
      {
        PatientCommsList.Add(HomePhone);
      }
      ElectronicCommunicationDetail BusinessPhone = GetCdaComms(this.Input.Message.Segment("PID").Field(14));
      if (BusinessPhone is object)
      {
        PatientCommsList.Add(BusinessPhone);
      }
      if (PatientCommsList.Count > 0)
      {
        pathologyResultReport.SCSContext.SubjectOfCare.Participant.ElectronicCommunicationDetails = PatientCommsList;
      }


      //Address
      pathologyResultReport.SCSContext.SubjectOfCare.Participant.Addresses = new List<IAddress>();
      foreach (IField XDA in this.Input.Message.Segment("PID").Element(11).RepeatList)
      {
        pathologyResultReport.SCSContext.SubjectOfCare.Participant.Addresses.Add(GetCdaAddress(XDA));
      }

      pathologyResultReport.SCSContext.SubjectOfCare.Participant.Person.Identifiers = new List<Identifier>();

      var IhiNumber = this.Input.Message.Segment("PID").Element(3).RepeatList.SingleOrDefault(x => x.Component(4).AsString.Equals("AUSHIC", StringComparison.CurrentCultureIgnoreCase) && x.Component(5).AsString.Equals("NI", StringComparison.CurrentCultureIgnoreCase));
      if (IhiNumber is object)
      {
        pathologyResultReport.SCSContext.SubjectOfCare.Participant.Person.Identifiers.Add(BaseCDAModel.CreateHealthIdentifier(HealthIdentifierType.IHI, IhiNumber.Component(1).AsString));
      }

      var MedicareNumber = this.Input.Message.Segment("PID").Element(3).RepeatList.SingleOrDefault(x => x.Component(4).AsString.Equals("AUSHIC", StringComparison.CurrentCultureIgnoreCase) && x.Component(5).AsString.Equals("MC", StringComparison.CurrentCultureIgnoreCase));
      if (MedicareNumber is object)
      {
        //Medicare Number goes in Entitlements as it is not a true identifier legally
        var MedicareEntitlement = BaseCDAModel.CreateEntitlement();
        MedicareEntitlement.Id = BaseCDAModel.CreateMedicareNumber(MedicareNumberType.MedicareCardNumber, MedicareNumber.Component(1).AsString);
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
      foreach (var OBR in this.Input.Message.SegmentList("OBR"))
      {
        var TestResult = PathologyResultReport.CreatePathologyTestResult();
        pathologyResultReport.SCSContent.PathologyTestResult.Add(TestResult);

        // Please note optional field - Note: This field is only displayed in the Narrative
        TestResult.ReportingPathologistForTestResult = $"Dr {ReportingPathologistName.FamilyName.ToUpper()}, {String.Join(" ", ReportingPathologistName.GivenNames)}";

        // Test Result Name Code        
        if (OBR.Field(4).Component(3).AsString.Equals("SCT", StringComparison.CurrentCultureIgnoreCase))
        {
          if (!OBR.Field(4).Component(4).IsEmpty)
          {
            ReportLinkName = OBR.Field(4).Component(5).AsString;
            TestResult.TestResultName = BaseCDAModel.CreateCodableText(OBR.Field(4).Component(1).AsString, Nehta.VendorLibrary.CDA.Generator.Enums.CodingSystem.SNOMED, OBR.Field(4).Component(2).AsString, ReportLinkName);
          }
          else
          {
            ReportLinkName = OBR.Field(4).Component(2).AsString;
            TestResult.TestResultName = BaseCDAModel.CreateCodableText(OBR.Field(4).Component(1).AsString, Nehta.VendorLibrary.CDA.Generator.Enums.CodingSystem.SNOMED, OBR.Field(4).Component(2).AsString);
          }
        }
        else if (OBR.Field(4).Component(6).AsString.Equals("SCT", StringComparison.CurrentCultureIgnoreCase))
        {
          if (!OBR.Field(4).Component(1).IsEmpty)
          {
            ReportLinkName = OBR.Field(4).Component(2).AsString;
            TestResult.TestResultName = BaseCDAModel.CreateCodableText(OBR.Field(4).Component(4).AsString, Nehta.VendorLibrary.CDA.Generator.Enums.CodingSystem.SNOMED, OBR.Field(4).Component(5).AsString, ReportLinkName);
          }
          else
          {
            ReportLinkName = OBR.Field(4).Component(5).AsString;
            TestResult.TestResultName = BaseCDAModel.CreateCodableText(OBR.Field(4).Component(4).AsString, Nehta.VendorLibrary.CDA.Generator.Enums.CodingSystem.SNOMED, OBR.Field(4).Component(5).AsString);
          }
        }
        else if (!OBR.Field(4).Component(1).IsEmpty)
        {
          ReportLinkName = OBR.Field(4).Component(2).AsString;
          TestResult.TestResultName = BaseCDAModel.CreateCodableText(ReportLinkName);
        }
        else if (!OBR.Field(4).Component(4).IsEmpty)
        {
          ReportLinkName = OBR.Field(4).Component(5).AsString;
          TestResult.TestResultName = BaseCDAModel.CreateCodableText(ReportLinkName);
        }
        else
        {
          throw new ApplicationException($"Unable to parse OBR-4 ReportCode. Found {OBR.Field(4).AsStringRaw}");
        }

        // Department Code
        TestResult.PathologyDiscipline = GetCdaDiagnosticService(OBR.Field(24).AsString);

        // ResultStatus
        TestResult.OverallTestResultStatus = GetCdaResultStatus(OBR.Field(25).AsString);

        // Test Specimen Detail
        TestResult.TestSpecimenDetail = PathologyResultReport.CreateTestSpecimenDetail();
        DateTimeOffset OldestCollectionDateTime = GetOldestDateTime(this.Input.Message.SegmentList("OBR").Select(y => y.Field(7)));
        TestResult.TestSpecimenDetail.CollectionDateTime = new ISO8601DateTime(OldestCollectionDateTime.DateTime, ISO8601DateTime.Precision.Minute, OldestCollectionDateTime.Offset);

        TestResult.ObservationDateTime = TestResult.TestSpecimenDetail.CollectionDateTime;
      }

      // Related Document
      pathologyResultReport.SCSContent.RelatedDocument = PathologyResultReport.CreateRelatedDocument();

      if (!System.IO.File.Exists(this.Input.FilePathToPdfReport))
      {
        throw new FieldAccessException($"Unable to locate PDF attachment file at path: {this.Input.FilePathToPdfReport}");
      }

      if (!this.Input.FilePathToPdfReport.EndsWith(".pdf", StringComparison.CurrentCultureIgnoreCase))
      {
        throw new FieldAccessException($"The report attachment file must be in a .pdf format.");
      }

      // Pathology PDF
      var AttachmentPdf = BaseCDAModel.CreateExternalData();
      AttachmentPdf.ExternalDataMediaType = Nehta.VendorLibrary.CDA.Generator.Enums.MediaType.PDF;

      //We have to move the PDF attachment file to a new place and then rename it to 'attachment.pdf' and then after 
      //generating the CDA document we will delete at copy of the PDF.  
      System.IO.FileInfo TempAttachmentFilePath = new System.IO.FileInfo(System.IO.Path.Combine(TempWorkingDirectoryPath.FullName, "attachment.pdf"));
      System.IO.File.Copy(this.Input.FilePathToPdfReport, TempAttachmentFilePath.FullName);
      AttachmentPdf.Path = TempAttachmentFilePath.FullName;
      pathologyResultReport.SCSContent.RelatedDocument.ExaminationResultRepresentation = AttachmentPdf;


      // Document Provenance
      pathologyResultReport.SCSContent.RelatedDocument.DocumentDetails = BaseCDAModel.CreateDocumentDetails();

      // Report Identifier
      pathologyResultReport.SCSContent.RelatedDocument.DocumentDetails.ReportIdentifier = BaseCDAModel.CreateIdentifier($"1.2.36.1.2001.1005.54.{PerformingPathologyLab.Hpio}", this.Input.Message.Segment("OBR").Field(3).Component(1).AsString);

      // Report Date 
      pathologyResultReport.SCSContent.RelatedDocument.DocumentDetails.ReportDate = new ISO8601DateTime(OldestReportReleaseDate.DateTime, ISO8601DateTime.Precision.Second, OldestReportReleaseDate.Offset);

      // Result Status
      pathologyResultReport.SCSContent.RelatedDocument.DocumentDetails.ReportStatus = GetRolledUpReportStatus(this.Input.Message.SegmentList("OBR").Select(y => y.Field(25)));

      // Report Name       
      if (Input.Message.SegmentList("OBR").Count == 1)
      {
        pathologyResultReport.SCSContent.RelatedDocument.DocumentDetails.ReportDescription = ReportLinkName;
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

        if (System.IO.File.Exists(this.Input.CdaDocumentOutPutFilePath))
        {
          System.IO.File.Delete(this.Input.CdaDocumentOutPutFilePath);
        }

        using (var writer = XmlWriter.Create(this.Input.CdaDocumentOutPutFilePath, new XmlWriterSettings { Indent = true }))
        {
          xmlDoc.Save(writer);
        }
      }
      catch (Nehta.VendorLibrary.Common.ValidationException ex)
      {
        //Catch any validation exceptions
        var validationMessages = ex.GetMessagesString();

        //Handle any validation errors as appropriate.
        throw;
      }
      finally
      {
        TempAttachmentFilePath.Delete();
      }


    }

    public void Process(CdaCreatorInput input)
    {
      this.Input = input;
      TempWorkingDirectoryPath.GetFiles().ToList().ForEach(x => System.IO.File.Delete(x.FullName));

      var pathologyResultReport = PathologyResultReport.CreatePathologyResultReport();

      // Include Logo
      if (Input.CdaDocuemntLogoImageBytes != null)
      {
        pathologyResultReport.IncludeLogo = true;
        pathologyResultReport.LogoByte = Input.CdaDocuemntLogoImageBytes;
      }
      else
      {
        pathologyResultReport.IncludeLogo = false;        
      }
      

      // Set Creation Time
      if (this.Input.Message.Segment("MSH").Field(7).Convert.DateTime.CanParseToDateTimeOffset)
      {
        DateTimeOffset V2MessageCreationDateTime = this.Input.Message.Segment("MSH").Field(7).Convert.DateTime.GetDateTimeOffset();        
        pathologyResultReport.DocumentCreationTime = new ISO8601DateTime(V2MessageCreationDateTime.DateTime, ISO8601DateTime.Precision.Second, V2MessageCreationDateTime.Offset);
      }

      #region Setup and populate the CDA context model

      // Setup and populate the CDA context model
      var cdaContext = PathologyResultReport.CreateCDAContext();
      // Document Id
      cdaContext.DocumentId = BaseCDAModel.CreateIdentifier(BaseCDAModel.CreateOid(), null);
      // Set Id  
      cdaContext.SetId = BaseCDAModel.CreateIdentifier(BaseCDAModel.CreateGuid(), null);
      // CDA Context Version
      cdaContext.Version = "1";
      // Custodian
      cdaContext.Custodian = BaseCDAModel.CreateCustodian();

      //custodian/assignedCustodian
      cdaContext.Custodian.Participant = BaseCDAModel.CreateParticipantCustodian(); ;

      // custodian/assignedCustodian/representedCustodianOrganization
      cdaContext.Custodian.Participant.Organisation = BaseCDAModel.CreateOrganisationName();

      PathologyLab PerformingPathologyLab = null;
      if (this.Input.Message.Segment("MSH").Field(4).Component(3).AsString.Equals("AUSNATA", StringComparison.CurrentCultureIgnoreCase))
      {
        PerformingPathologyLab = this.Input.CdaMetadata.PathologyLabList.SingleOrDefault(x => x.NataSiteNumber.Equals(this.Input.Message.Segment("MSH").Field(4).Component(2).AsString, StringComparison.CurrentCultureIgnoreCase));
        if (PerformingPathologyLab is null)
        {
          throw new ApplicationException($"Unable to locate CDA meta data for the performing lab's NATA Site number: {this.Input.Message.Segment("MSH").Field(4).Component(2).AsString}");
        }
      }
      else
      {
        throw new ApplicationException("Unable to locate the performing lab's NATA Site number in MSH-4 Sending Facility.");
      }

      // custodian/assignedCustodian/representedCustodianOrganization/<Entity Identifier>
      cdaContext.Custodian.Participant.Organisation.Identifiers = new List<Identifier>
      {
        BaseCDAModel.CreateHealthIdentifier(HealthIdentifierType.HPIO, PerformingPathologyLab.Hpio)
      };

      // custodian/assignedCustodian/representedCustodianOrganization/name
      cdaContext.Custodian.Participant.Organisation.Name = PerformingPathologyLab.Name;

      ElectronicCommunicationDetail PerformingPathologyLabComms = BaseCDAModel.CreateElectronicCommunicationDetail(
            PerformingPathologyLab.Phone,
            ElectronicCommunicationMedium.Telephone,
            ElectronicCommunicationUsage.WorkPlace);

      if (!this.Input.IsMandatoryCDAElementsOnly)
      {
        // custodian/assignedCustodian/representedCustodianOrganization/<Address>        
        cdaContext.Custodian.Participant.Address = GetCdaAddress(PerformingPathologyLab.Address);

        // custodian/assignedCustodian/representedCustodianOrganization/<Electronic Communication Detail>        
        cdaContext.Custodian.Participant.ElectronicCommunicationDetail = PerformingPathologyLabComms;
      }

      // Legal Authenticator
      cdaContext.LegalAuthenticator = BaseCDAModel.CreateLegalAuthenticator();
      // LegalAuthenticator/assignedEntity
      cdaContext.LegalAuthenticator.Participant = BaseCDAModel.CreateParticipantForLegalAuthenticator();

      // LegalAuthenticator/assignedEntity/assignedPerson
      cdaContext.LegalAuthenticator.Participant.Person = BaseCDAModel.CreatePerson();

      DateTimeOffset OldestReportReleaseDate = GetOldestDateTime(this.Input.Message.SegmentList("OBR").Select(y => y.Field(22)));
      // LegalAuthenticator/time/@value            
      cdaContext.LegalAuthenticator.Participant.DateTimeAuthenticated = new ISO8601DateTime(OldestReportReleaseDate.DateTime, ISO8601DateTime.Precision.Second, OldestReportReleaseDate.Offset);

      string FirstReportingPathologistsId = Input.Message.Segment("OBR").Field(32).Component(1).SubComponent(1).AsString;
      if (!Input.Message.SegmentList("OBR").All(x => x.Field(32).Component(1).SubComponent(1).AsString.Equals(FirstReportingPathologistsId, StringComparison.CurrentCultureIgnoreCase)))
      {
        throw new ApplicationException("Only HL7 v2 messages with the same Reporting Pathologist Id in all OBR-32.1.1 segments are supported. Multiple reporting pathologists are not supported by this conversion tool.");
      }

      //LegalAuthenticator/assignedEntity/assignedPerson/<Person Name>
      var ReportingPathologistName = BaseCDAModel.CreatePersonName();
      ReportingPathologistName.GivenNames = new List<string> { Input.Message.Segment("OBR").Field(32).Component(1).SubComponent(3).AsString };
      ReportingPathologistName.FamilyName = Input.Message.Segment("OBR").Field(32).Component(1).SubComponent(2).AsString;
      ReportingPathologistName.Titles = new List<string> { Input.Message.Segment("OBR").Field(32).Component(1).SubComponent(6).AsString };
      ReportingPathologistName.NameUsages = new List<NameUsage> { NameUsage.Legal };

      cdaContext.LegalAuthenticator.Participant.Person.PersonNames = new List<Nehta.VendorLibrary.CDA.IPersonName> { ReportingPathologistName };

      Pathologist ReportingPathologistCDAMetadata = PerformingPathologyLab.ReportingPathologistsList.SingleOrDefault(x => x.LocalCode.Equals(FirstReportingPathologistsId, StringComparison.CurrentCultureIgnoreCase));
      if (ReportingPathologistCDAMetadata is null)
      {
        throw new ApplicationException($"Not ReportingPathologist CDA meta data found for the HL7 v2 OBR-32.1.1 ID of : {FirstReportingPathologistsId}");
      }

      // LegalAuthenticator/assignedEntity/<Entity Identifier>
      cdaContext.LegalAuthenticator.Participant.Person.Identifiers = new List<Identifier>
      {
        BaseCDAModel.CreateHealthIdentifier(HealthIdentifierType.HPII, ReportingPathologistCDAMetadata.Hpii)
      };

      if (!this.Input.IsMandatoryCDAElementsOnly)
      {
        // LegalAuthenticator/assignedEntity/code
        cdaContext.LegalAuthenticator.Role = BaseCDAModel.CreateRole(Occupation.Pathologist, Nehta.VendorLibrary.CDA.Generator.Enums.CodingSystem.ANZSCORevision1);

        cdaContext.LegalAuthenticator.Participant.Addresses = new List<IAddress>() { GetCdaAddress(PerformingPathologyLab.Address) };

        // LegalAuthenticator/assignedEntity/<Electronic Communication Detail>      
        cdaContext.LegalAuthenticator.Participant.ElectronicCommunicationDetails = new List<ElectronicCommunicationDetail> { PerformingPathologyLabComms };

        // LegalAuthenticator/assignedEntity/representedOrganization
        cdaContext.LegalAuthenticator.Participant.Organisation = BaseCDAModel.CreateOrganisationName();

        // LegalAuthenticator/assignedEntity/representedOrganization/name
        cdaContext.LegalAuthenticator.Participant.Organisation.Name = PerformingPathologyLab.Name;

        // LegalAuthenticator/assignedEntity/representedOrganization/<Entity Identifier>
        cdaContext.LegalAuthenticator.Participant.Organisation.Identifiers = new List<Identifier>();


        var CodableConceptNataSiteNumber = BaseCDAModel.CreateCodableText();
        CodableConceptNataSiteNumber.Code = "XX";
        CodableConceptNataSiteNumber.CodeSystemName = "Identifier Type (HL7)";
        CodableConceptNataSiteNumber.CodeSystemCode = "2.16.840.1.113883.12.203";
        CodableConceptNataSiteNumber.DisplayName = "Organization identifier";
        
        var NataSiteNumber = BaseCDAModel.CreateIdentifier("1.2.36.1.2001.1005.74", PerformingPathologyLab.NataSiteNumber);
        NataSiteNumber.Code = CodableConceptNataSiteNumber;
        NataSiteNumber.AssigningGeographicArea = "National Identifier";
        NataSiteNumber.AssigningAuthorityName = "NATA Site Number";
        cdaContext.LegalAuthenticator.Participant.Organisation.Identifiers.Add(NataSiteNumber);

        //cdaContext.LegalAuthenticator.Participant.Organisation.Identifiers.Add(BaseCDAModel.CreateIdentifier("NATA", Nehta.VendorLibrary.CDA.Generator.Enums.HealthcareIdentifierGeographicArea.NationalIdentifier, PerformingPathologyLab.NataSiteNumber, "1.2.36.1.2001.1005.12", BaseCDAModel.CreateCodableText("AUSNATA", Nehta.VendorLibrary.CDA.Generator.Enums.CodingSystem.HL7IdentifierType, "National Association of Testing Authorities (NATA) Site Number")));
        cdaContext.LegalAuthenticator.Participant.Organisation.Identifiers.Add(BaseCDAModel.CreateHealthIdentifier(HealthIdentifierType.HPIO, PerformingPathologyLab.Hpio));
      }
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
      pathologyResultReport.SCSContext.Author.Participant.Person.Organisation.Name = PerformingPathologyLab.Name;
      pathologyResultReport.SCSContext.Author.Participant.Person.Organisation.Identifiers = new List<Identifier>()
      {
        BaseCDAModel.CreateHealthIdentifier(HealthIdentifierType.HPIO, PerformingPathologyLab.Hpio)
      };
      pathologyResultReport.SCSContext.Author.Participant.ElectronicCommunicationDetails = new List<ElectronicCommunicationDetail>() { PerformingPathologyLabComms };

      if (!this.Input.IsMandatoryCDAElementsOnly)
      {

        // Document Author > Participant > Addresses
        pathologyResultReport.SCSContext.Author.Participant.Addresses = new List<IAddress>
        {
          GetCdaAddress(PerformingPathologyLab.Address)
        };

        // Document Author > Participant > Address (Note: optional in ACI)
        pathologyResultReport.SCSContext.Author.Participant.Person.Organisation.Addresses = new List<IAddress>
        {
          GetCdaAddress(PerformingPathologyLab.Address)
        };

        pathologyResultReport.SCSContext.Author.Participant.Person.Organisation.Name = PerformingPathologyLab.Name;
        pathologyResultReport.SCSContext.Author.Participant.Person.Organisation.NameUsage = OrganisationNameUsage.BusinessName;

        //pathologyResultReport.SCSContext.Author.Participant.Person.Organisation.Department = "Some department service provider";
        //pathologyResultReport.SCSContext.Author.Participant.Person.Organisation.EmploymentType = BaseCDAModel.CreateCodableText(null, null, null, "Casual", null);
        pathologyResultReport.SCSContext.Author.Participant.Person.Organisation.Occupation = BaseCDAModel.CreateRole(Occupation.Pathologist, Nehta.VendorLibrary.CDA.Generator.Enums.CodingSystem.ANZSCORevision1);
        //pathologyResultReport.SCSContext.Author.Participant.Person.Organisation.PositionInOrganisation = BaseCDAModel.CreateCodableText(null, null, null, "Manager", null);

        pathologyResultReport.SCSContext.Author.Participant.Person.Organisation.ElectronicCommunicationDetails = new List<ElectronicCommunicationDetail>
        {
          PerformingPathologyLabComms
        };
      }

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
        GetCdaAddress(PerformingPathologyLab.Address)
      };

      pathologyResultReport.SCSContext.ReportingPathologist.Participant.ElectronicCommunicationDetails = new List<ElectronicCommunicationDetail> { PerformingPathologyLabComms };
      pathologyResultReport.SCSContext.ReportingPathologist.Participant.Person = pathologyResultReport.SCSContext.Author.Participant.Person;


      // Order Details
      pathologyResultReport.SCSContext.OrderDetails = DiagnosticImagingReport.CreateOrderDetails();

      // Requester Order Identifier
      //pathologyResultReport.SCSContext.OrderDetails.RequesterOrderIdentifier = BaseCDAModel.CreateIdentifier("1.2.36.1.2001.1005.52.8003620833333789", "10523479");

      // Requester
      pathologyResultReport.SCSContext.OrderDetails.Requester = BaseCDAModel.CreateRequester();

      DateTimeOffset OldestRequestedDateTime = GetOldestDateTime(this.Input.Message.SegmentList("OBR").Select(y => y.Field(27).Component(4)));
      pathologyResultReport.SCSContext.OrderDetails.Requester.ParticipationEndTime = new ISO8601DateTime(OldestRequestedDateTime.DateTime, ISO8601DateTime.Precision.Day, OldestRequestedDateTime.Offset);
      // Document Requester> Role
      pathologyResultReport.SCSContext.OrderDetails.Requester.Role = BaseCDAModel.CreateRole(Occupation.GeneralMedicalPractitioner, Nehta.VendorLibrary.CDA.Generator.Enums.CodingSystem.ANZSCORevision1);

      pathologyResultReport.SCSContext.OrderDetails.Requester.Participant = BaseCDAModel.CreateParticipantForRequester();
      pathologyResultReport.SCSContext.OrderDetails.Requester.Participant.Person = BaseCDAModel.CreatePersonWithOrganisation();
      // Participation Period


      var RequestingDoctorName = BaseCDAModel.CreatePersonName();
      RequestingDoctorName.FamilyName = this.Input.Message.Segment("OBR").Element(16).Repeat(1).Component(2).AsString;
      RequestingDoctorName.GivenNames = new List<string>() { this.Input.Message.Segment("OBR").Element(16).Repeat(1).Component(3).AsString };
      pathologyResultReport.SCSContext.OrderDetails.Requester.Participant.Person.PersonNames = new List<IPersonName>() { RequestingDoctorName };


      pathologyResultReport.SCSContext.OrderDetails.Requester.Participant.Person.Identifiers = new List<Identifier>();

      ///Get Requester HPI-I
      var RequesterHpii = this.Input.Message.Segment("OBR").ElementList.SingleOrDefault(x => x.Component(9).AsString.Equals("AUSHIC", StringComparison.CurrentCultureIgnoreCase) && x.Component(13).AsString.Equals("NPI", StringComparison.CurrentCultureIgnoreCase));
      if (RequesterHpii is object)
      {
        pathologyResultReport.SCSContext.OrderDetails.Requester.Participant.Person.Identifiers.Add(BaseCDAModel.CreateHealthIdentifier(HealthIdentifierType.HPII, RequesterHpii.Component(1).AsString));
      }

      ///Get Requester Medicare Provider Number
      var RequesterMedicareProvidernumber = this.Input.Message.Segment("OBR").ElementList.SingleOrDefault(x => x.Component(9).AsString.Equals("AUSHICPR", StringComparison.CurrentCultureIgnoreCase));
      if (RequesterMedicareProvidernumber is object)
      {
        var MedicareProvidernumberIdentifier = BaseCDAModel.CreateIdentifier("Medicare", Nehta.VendorLibrary.CDA.Generator.Enums.HealthcareIdentifierGeographicArea.NationalIdentifier, RequesterMedicareProvidernumber.Component(1).AsString, "1.2.36.174030967.0.2", BaseCDAModel.CreateCodableText("AUSHICPR", Nehta.VendorLibrary.CDA.Generator.Enums.CodingSystem.HL7IdentifierType, "Medicare Provider Number"));
        pathologyResultReport.SCSContext.OrderDetails.Requester.Participant.Person.Identifiers.Add(MedicareProvidernumberIdentifier);
      }

      // Subject Of Care
      pathologyResultReport.SCSContext.SubjectOfCare = BaseCDAModel.CreateSubjectOfCare();
      pathologyResultReport.SCSContext.SubjectOfCare.Participant = BaseCDAModel.CreateParticipantForSubjectOfCare();
      pathologyResultReport.SCSContext.SubjectOfCare.Participant.Person = BaseCDAModel.CreatePersonForSubjectOfCare();

      pathologyResultReport.SCSContext.SubjectOfCare.Participant.Person.PersonNames = new List<IPersonName>();
      foreach (var XPN in this.Input.Message.Segment("PID").Element(5).RepeatList)
      {
        pathologyResultReport.SCSContext.SubjectOfCare.Participant.Person.PersonNames.Add(GetCdaPatientName(XPN));
      }

      pathologyResultReport.SCSContext.SubjectOfCare.Participant.Person.Gender = GetCdaGender(this.Input.Message.Segment("PID").Field(8).AsString);
      if (this.Input.Message.Segment("PID").Field(7).Convert.DateTime.CanParseToDateTimeOffset)
      {
        DateTimeOffset DateOfBirth = this.Input.Message.Segment("PID").Field(7).Convert.DateTime.GetDateTimeOffset();
        pathologyResultReport.SCSContext.SubjectOfCare.Participant.Person.DateOfBirth = new ISO8601DateTime(DateOfBirth.DateTime, ISO8601DateTime.Precision.Day);
      }
      else
      {
        throw new ApplicationException($"Unable to source Date of Birth from PID-8. Value found was: {this.Input.Message.Segment("PID").Field(8).AsString}");
      }

      pathologyResultReport.SCSContext.SubjectOfCare.Participant.Person.IndigenousStatus = IndigenousStatus.NeitherAboriginalNorTorresStraitIslanderOrigin;

      //Phone numbers
      var PatientCommsList = new List<ElectronicCommunicationDetail>();
      ElectronicCommunicationDetail HomePhone = GetCdaComms(this.Input.Message.Segment("PID").Field(13));
      if (HomePhone is object)
      {
        PatientCommsList.Add(HomePhone);
      }
      ElectronicCommunicationDetail BusinessPhone = GetCdaComms(this.Input.Message.Segment("PID").Field(14));
      if (BusinessPhone is object)
      {
        PatientCommsList.Add(BusinessPhone);
      }
      if (PatientCommsList.Count > 0)
      {
        pathologyResultReport.SCSContext.SubjectOfCare.Participant.ElectronicCommunicationDetails = PatientCommsList;
      }


      //Address
      pathologyResultReport.SCSContext.SubjectOfCare.Participant.Addresses = new List<IAddress>();
      foreach (IField XDA in this.Input.Message.Segment("PID").Element(11).RepeatList)
      {
        pathologyResultReport.SCSContext.SubjectOfCare.Participant.Addresses.Add(GetCdaAddress(XDA));
      }

      pathologyResultReport.SCSContext.SubjectOfCare.Participant.Person.Identifiers = new List<Identifier>();

      var IhiNumber = this.Input.Message.Segment("PID").Element(3).RepeatList.SingleOrDefault(x => x.Component(4).AsString.Equals("AUSHIC", StringComparison.CurrentCultureIgnoreCase) && x.Component(5).AsString.Equals("NI", StringComparison.CurrentCultureIgnoreCase));
      if (IhiNumber is object)
      {
        pathologyResultReport.SCSContext.SubjectOfCare.Participant.Person.Identifiers.Add(BaseCDAModel.CreateHealthIdentifier(HealthIdentifierType.IHI, IhiNumber.Component(1).AsString));
      }

      var MedicareNumber = this.Input.Message.Segment("PID").Element(3).RepeatList.SingleOrDefault(x => x.Component(4).AsString.Equals("AUSHIC", StringComparison.CurrentCultureIgnoreCase) && x.Component(5).AsString.Equals("MC", StringComparison.CurrentCultureIgnoreCase));
      if (MedicareNumber is object)
      {
        //Medicare Number goes in Entitlements as it is not a true identifier legally
        var MedicareEntitlement = BaseCDAModel.CreateEntitlement();
        MedicareEntitlement.Id = BaseCDAModel.CreateMedicareNumber(MedicareNumberType.MedicareCardNumber, MedicareNumber.Component(1).AsString);
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
      foreach (var OBR in this.Input.Message.SegmentList("OBR"))
      {
        var TestResult = PathologyResultReport.CreatePathologyTestResult();
        pathologyResultReport.SCSContent.PathologyTestResult.Add(TestResult);

        // Please note optional field - Note: This field is only displayed in the Narrative
        TestResult.ReportingPathologistForTestResult = $"Dr {ReportingPathologistName.FamilyName.ToUpper()}, {String.Join(" ", ReportingPathologistName.GivenNames)}";

        // Test Result Name Code        
        if (OBR.Field(4).Component(3).AsString.Equals("SCT", StringComparison.CurrentCultureIgnoreCase))
        {
          if (!OBR.Field(4).Component(4).IsEmpty)
          {
            ReportLinkName = OBR.Field(4).Component(5).AsString;
            TestResult.TestResultName = BaseCDAModel.CreateCodableText(OBR.Field(4).Component(1).AsString, Nehta.VendorLibrary.CDA.Generator.Enums.CodingSystem.SNOMED, OBR.Field(4).Component(2).AsString, ReportLinkName);            
          }
          else
          {
            ReportLinkName = OBR.Field(4).Component(2).AsString;
            TestResult.TestResultName = BaseCDAModel.CreateCodableText(OBR.Field(4).Component(1).AsString, Nehta.VendorLibrary.CDA.Generator.Enums.CodingSystem.SNOMED, OBR.Field(4).Component(2).AsString);
          }
        }
        else if (OBR.Field(4).Component(6).AsString.Equals("SCT", StringComparison.CurrentCultureIgnoreCase))
        {
          if (!OBR.Field(4).Component(1).IsEmpty)
          {
            ReportLinkName = OBR.Field(4).Component(2).AsString;
            TestResult.TestResultName = BaseCDAModel.CreateCodableText(OBR.Field(4).Component(4).AsString, Nehta.VendorLibrary.CDA.Generator.Enums.CodingSystem.SNOMED, OBR.Field(4).Component(5).AsString, ReportLinkName);
          }
          else
          {
            ReportLinkName = OBR.Field(4).Component(5).AsString;
            TestResult.TestResultName = BaseCDAModel.CreateCodableText(OBR.Field(4).Component(4).AsString, Nehta.VendorLibrary.CDA.Generator.Enums.CodingSystem.SNOMED, OBR.Field(4).Component(5).AsString);
          }
        }
        else if (!OBR.Field(4).Component(1).IsEmpty)
        {
          ReportLinkName = OBR.Field(4).Component(2).AsString;
          TestResult.TestResultName = BaseCDAModel.CreateCodableText(ReportLinkName);
        }
        else if (!OBR.Field(4).Component(4).IsEmpty)
        {
          ReportLinkName = OBR.Field(4).Component(5).AsString;
          TestResult.TestResultName = BaseCDAModel.CreateCodableText(ReportLinkName);
        }
        else
        {
          throw new ApplicationException($"Unable to parse OBR-4 ReportCode. Found {OBR.Field(4).AsStringRaw}");
        }

        // Department Code
        TestResult.PathologyDiscipline = GetCdaDiagnosticService(OBR.Field(24).AsString);        

        // ResultStatus
        TestResult.OverallTestResultStatus = GetCdaResultStatus(OBR.Field(25).AsString);

        // Test Specimen Detail
        TestResult.TestSpecimenDetail = PathologyResultReport.CreateTestSpecimenDetail();
        DateTimeOffset OldestCollectionDateTime = GetOldestDateTime(this.Input.Message.SegmentList("OBR").Select(y => y.Field(7)));
        TestResult.TestSpecimenDetail.CollectionDateTime = new ISO8601DateTime(OldestCollectionDateTime.DateTime, ISO8601DateTime.Precision.Minute, OldestCollectionDateTime.Offset);

        TestResult.ObservationDateTime = TestResult.TestSpecimenDetail.CollectionDateTime;
      }

      // Related Document
      pathologyResultReport.SCSContent.RelatedDocument = PathologyResultReport.CreateRelatedDocument();

      if (!System.IO.File.Exists(this.Input.FilePathToPdfReport))
      {
        throw new FieldAccessException($"Unable to locate PDF attachment file at path: {this.Input.FilePathToPdfReport}");
      }

      if (!this.Input.FilePathToPdfReport.EndsWith(".pdf", StringComparison.CurrentCultureIgnoreCase))
      {
        throw new FieldAccessException($"The report attachment file must be in a .pdf format.");
      }

      // Pathology PDF
      var AttachmentPdf = BaseCDAModel.CreateExternalData();
      AttachmentPdf.ExternalDataMediaType = Nehta.VendorLibrary.CDA.Generator.Enums.MediaType.PDF;

      //We have to move the PDF attachment file to a new place and then rename it to 'attachment.pdf' and then after 
      //generating the CDA document we will delete at copy of the PDF.  
      System.IO.FileInfo TempAttachmentFilePath = new System.IO.FileInfo(System.IO.Path.Combine(TempWorkingDirectoryPath.FullName, "attachment.pdf"));      
      System.IO.File.Copy(this.Input.FilePathToPdfReport, TempAttachmentFilePath.FullName);
      AttachmentPdf.Path = TempAttachmentFilePath.FullName;      
      pathologyResultReport.SCSContent.RelatedDocument.ExaminationResultRepresentation = AttachmentPdf;


      // Document Provenance
      pathologyResultReport.SCSContent.RelatedDocument.DocumentDetails = BaseCDAModel.CreateDocumentDetails();

      // Report Identifier
      pathologyResultReport.SCSContent.RelatedDocument.DocumentDetails.ReportIdentifier = BaseCDAModel.CreateIdentifier($"1.2.36.1.2001.1005.54.{PerformingPathologyLab.Hpio}", this.Input.Message.Segment("OBR").Field(3).Component(1).AsString);

      // Report Date 
      pathologyResultReport.SCSContent.RelatedDocument.DocumentDetails.ReportDate = new ISO8601DateTime(OldestReportReleaseDate.DateTime, ISO8601DateTime.Precision.Second, OldestReportReleaseDate.Offset);

      // Result Status
      pathologyResultReport.SCSContent.RelatedDocument.DocumentDetails.ReportStatus = GetRolledUpReportStatus(this.Input.Message.SegmentList("OBR").Select(y => y.Field(25)));

      // Report Name       
      if (Input.Message.SegmentList("OBR").Count == 1)
      {
        pathologyResultReport.SCSContent.RelatedDocument.DocumentDetails.ReportDescription = ReportLinkName;
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

        if (System.IO.File.Exists(this.Input.CdaDocumentOutPutFilePath))
        {
          System.IO.File.Delete(this.Input.CdaDocumentOutPutFilePath);
        }

        using (var writer = XmlWriter.Create(this.Input.CdaDocumentOutPutFilePath, new XmlWriterSettings { Indent = true }))
        {
          xmlDoc.Save(writer);
        }
      }
      catch (Nehta.VendorLibrary.Common.ValidationException ex)
      {
        //Catch any validation exceptions
        var validationMessages = ex.GetMessagesString();

        //Handle any validation errors as appropriate.
        throw;
      }
      finally
      {
        TempAttachmentFilePath.Delete();
      }


    }

    private ICodableText GetRolledUpReportStatus(IEnumerable<IField> V2ReportStatusList)
    {
      if (V2ReportStatusList.Any(x => x.AsString == "C"))
      {
        return GetCdaResultStatus("C");
      }
      else if (V2ReportStatusList.Any(x => x.AsString == "P"))
      {
        return GetCdaResultStatus("P");
      }
      else if (V2ReportStatusList.Any(x => x.AsString == "F"))
      {
        return GetCdaResultStatus("F");
      }
      else
      {
        throw new ApplicationException("Unable to find any known report statuses in OBR-25.");
      }
    }

    private ICodableText GetCdaResultStatus(string ReportStatus)
    {
      switch (ReportStatus.ToUpper())
      {
        case "F":
          return BaseCDAModel.CreateResultStatus(Hl7V3ResultStatus.FinalResults, "Final");
        case "P":
          return BaseCDAModel.CreateResultStatus(Hl7V3ResultStatus.Preliminary, "Preliminary");
        case "C":
          return BaseCDAModel.CreateResultStatus(Hl7V3ResultStatus.CorrectionToResults, "Correction");        
        default:
          throw new ApplicationException($"Unable to map result status to CDA result status. Status was: {ReportStatus}");
      }
    }

    private DiagnosticService GetCdaDiagnosticService(string DiagnosticServiceSectionCode)
    {
      switch (DiagnosticServiceSectionCode.ToUpper())
      {
        case "AU":
          return DiagnosticService.Audiology;
        case "BG":
          return DiagnosticService.BloodGases;
        case "BLB":
          return DiagnosticService.BloodBank;
        case "CG":
          return DiagnosticService.Laboratory;
        case "CUS":
          return DiagnosticService.CardiacUltrasound;
        case "CTH":
          return DiagnosticService.CardiacCatheterization;
        case "CT":
          return DiagnosticService.CATScan;
        case "CH":
          return DiagnosticService.Chemistry;
        case "CP":
          return DiagnosticService.Cytopathology;
        case "EC":
          return DiagnosticService.Electrocardiac;
        case "EN":
          return DiagnosticService.Electroneuro;
        case "GE":
          return DiagnosticService.Laboratory;
        case "HM":
          return DiagnosticService.Hematology;
        case "ICU":
          return DiagnosticService.BedsideICUMonitoring;
        case "IMM":
          return DiagnosticService.Immunology;
        case "LAB":
          return DiagnosticService.Laboratory;
        case "MB":
          return DiagnosticService.Microbiology;
        case "MCB":
          return DiagnosticService.Mycobacteriology;
        case "MYC":
          return DiagnosticService.Mycology;
        case "NMR":
          return DiagnosticService.NuclearMagneticResonance;
        case "NMS":
          return DiagnosticService.NuclearMedicineScan;
        case "NRS":
          return DiagnosticService.NursingServiceMeasures;
        case "OUS":
          return DiagnosticService.OBUltrasound;
        case "OT":
          return DiagnosticService.OccupationalTherapy;
        case "OTH":
          return DiagnosticService.Other;
        case "OSL":
          return DiagnosticService.OutsideLab;
        case "PHR":
          return DiagnosticService.Pharmacy;
        case "PT":
          return DiagnosticService.PhysicalTherapy;
        case "PHY":
          return DiagnosticService.Physician;
        case "PF":
          return DiagnosticService.PulmonaryFunction;
        case "RAD":
          return DiagnosticService.Radiology;
        case "RUS":
          return DiagnosticService.RadiologyUltrasound;
        case "RC":
          return DiagnosticService.RespiratoryCare;
        case "RT":
          return DiagnosticService.RadiationTherapy;
        case "RX":
          return DiagnosticService.Radiograph;
        case "SR":
          return DiagnosticService.Serology;
        case "SP":
          return DiagnosticService.SurgicalPathology;
        case "TX":
          return DiagnosticService.Toxicology;
        case "VUS":
          return DiagnosticService.VascularUltrasound;
        case "VR":
          return DiagnosticService.Virology;
        case "XRC":
          return DiagnosticService.Cineradiograph;        
        default:
          throw new ApplicationException($"Unable to map Diagnostic Service Section Code To CDA code set. found code: {DiagnosticServiceSectionCode}");
      }

    }

    private ElectronicCommunicationDetail GetCdaComms(IField XTN)
    {
      ElectronicCommunicationUsage? Usage = null;
      ElectronicCommunicationMedium? Medium = null;

      if (!XTN.IsEmpty)
      {
        //Usage
        switch (XTN.Component(2).AsString.ToUpper())
        {
          case "ASN":
            Usage = ElectronicCommunicationUsage.AnsweringService;
            break;
          case "BPN":
            Usage = ElectronicCommunicationUsage.Pager;
            break;
          case "EMR":
            Usage = ElectronicCommunicationUsage.EmergencyContact;
            break;
          case "NET":
            Usage = ElectronicCommunicationUsage.Undefined;
            break;
          case "ORN":
            Usage = ElectronicCommunicationUsage.Home;
            break;
          case "PRN":
            Usage = ElectronicCommunicationUsage.PrimaryHome;
            break;
          case "VHN":
            Usage = ElectronicCommunicationUsage.VacationHome;
            break;
          case "WPN":
            Usage = ElectronicCommunicationUsage.WorkPlace;
            break;
          default:
            Usage = ElectronicCommunicationUsage.Undefined;
            break;
        }

        //Medium
        switch (XTN.Component(3).AsString.ToUpper())
        {
          case "BP":
            Medium = ElectronicCommunicationMedium.Page;
            break;
          case "CP":
            Medium = ElectronicCommunicationMedium.Mobile;
            break;
          case "FX":
            Medium = ElectronicCommunicationMedium.Fax;
            break;
          case "INTERNET":
            Medium = ElectronicCommunicationMedium.Email;
            break;
          case "MD":
            Medium = ElectronicCommunicationMedium.Modem;
            break;
          case "PH":
            Medium = ElectronicCommunicationMedium.Telephone;
            break;
          case "X.400":
            Medium = ElectronicCommunicationMedium.Telnet;
            break;
          default:
            Medium = ElectronicCommunicationMedium.Undefined;
            break;
        }

        return BaseCDAModel.CreateElectronicCommunicationDetail(
            this.Input.Message.Segment("PID").Field(13).Component(7).AsString,
            Medium,
            Usage);
      }
      else
      {
        return null;
      }

    }

    private IPersonName GetCdaPatientName(IField XPN)
    {
      var Name = BaseCDAModel.CreatePersonName();
      Name.FamilyName = XPN.Component(1).AsString;
      Name.GivenNames = new List<string>() { XPN.Component(2).AsString };
      if (!XPN.Component(3).IsEmpty)
      {
        Name.GivenNames.Add(XPN.Component(3).AsString);
      }
      if (!XPN.Component(5).IsEmpty)
      {
        Name.Titles = new List<string>() { XPN.Component(5).AsString };
      }
      Name.NameUsages = new List<NameUsage>();
      switch (XPN.Component(7).AsString.ToUpper())
      {
        case "A":
          Name.NameUsages.Add(NameUsage.OtherName);
          break;
        case "B":
          Name.NameUsages.Add(NameUsage.NewbornName);
          break;
        case "C":
          Name.NameUsages.Add(NameUsage.OtherName);
          break;
        case "D":
          Name.NameUsages.Add(NameUsage.Legal);
          break;
        case "I":
          Name.NameUsages.Add(NameUsage.Legal);
          break;
        case "L":
          Name.NameUsages.Add(NameUsage.Legal);
          break;
        case "M":
          Name.NameUsages.Add(NameUsage.MaidenName);
          break;
        case "N":
          Name.NameUsages.Add(NameUsage.OtherName);
          break;
        case "P":
          Name.NameUsages.Add(NameUsage.Legal);
          break;
        case "U":
          Name.NameUsages.Add(NameUsage.Undefined);
          break;
        default:
          Name.NameUsages.Add(NameUsage.Undefined);
          break;
      }
      return Name;
    }

    private Gender? GetCdaGender(string SexChar)
    {
      switch (SexChar.ToUpper())
      {
        case "M":
          return Gender.Male;
        case "F":
          return Gender.Female;
        case "O":
          return Gender.IntersexOrIndeterminate;
        case "A":
          return Gender.IntersexOrIndeterminate;
        case "N":
          return Gender.NotStated;
        case "U":
          return Gender.Undefined;
        default:
          return Gender.Undefined;
      }
    }

    private IAddress GetCdaAddress(Address Address)
    {
      IAddress CdaAddress = BaseCDAModel.CreateAddress();
      CdaAddress.AddressPurpose = AddressPurpose.Business;
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
        case Address.StateType.WA:
          CdaAddress.AustralianAddress.State = AustralianState.WA;
          break;
        case Address.StateType.NT:
          CdaAddress.AustralianAddress.State = AustralianState.NT;
          break;
        case Address.StateType.QLD:
          CdaAddress.AustralianAddress.State = AustralianState.QLD;
          break;
        case Address.StateType.ACT:
          CdaAddress.AustralianAddress.State = AustralianState.ACT;
          break;
        case Address.StateType.NSW:
          CdaAddress.AustralianAddress.State = AustralianState.NSW;
          break;
        case Address.StateType.VIC:
          CdaAddress.AustralianAddress.State = AustralianState.VIC;
          break;
        case Address.StateType.SA:
          CdaAddress.AustralianAddress.State = AustralianState.SA;
          break;
        case Address.StateType.TAZ:
          CdaAddress.AustralianAddress.State = AustralianState.TAS;
          break;
        default:
          CdaAddress.AustralianAddress.State = AustralianState.Undefined;
          break;
      }
      return CdaAddress;
    }
    private IAddress GetCdaAddress(IField XDA)
    {
      IAddress CdaAddress = BaseCDAModel.CreateAddress();
      switch (XDA.Component(7).AsString.ToUpper())
      {
        case "H":
          CdaAddress.AddressPurpose = AddressPurpose.Residential;
          break;
        case "L":
          CdaAddress.AddressPurpose = AddressPurpose.MailingOrPostal;
          break;
        case "B":
          CdaAddress.AddressPurpose = AddressPurpose.Business;
          break;
        case "O":
          CdaAddress.AddressPurpose = AddressPurpose.Business;
          break;
        default:
          CdaAddress.AddressPurpose = AddressPurpose.NotStatedUnknownInadequatelyDescribed;
          break;
      }
      CdaAddress.AddressPurpose = AddressPurpose.Residential;
      CdaAddress.AustralianAddress = BaseCDAModel.CreateAustralianAddress();
      CdaAddress.AustralianAddress.UnstructuredAddressLines = new List<string> { XDA.Component(1).AsString };
      CdaAddress.AustralianAddress.SuburbTownLocality = XDA.Component(3).AsString;
      CdaAddress.AustralianAddress.PostCode = XDA.Component(5).AsString;
      switch (XDA.Component(4).AsString.ToUpper())
      {
        case "WA":
          CdaAddress.AustralianAddress.State = AustralianState.WA;
          break;
        case "NT":
          CdaAddress.AustralianAddress.State = AustralianState.NT;
          break;
        case "QLD":
          CdaAddress.AustralianAddress.State = AustralianState.QLD;
          break;
        case "ACT":
          CdaAddress.AustralianAddress.State = AustralianState.ACT;
          break;
        case "NSW":
          CdaAddress.AustralianAddress.State = AustralianState.NSW;
          break;
        case "VIC":
          CdaAddress.AustralianAddress.State = AustralianState.VIC;
          break;
        case "SA":
          CdaAddress.AustralianAddress.State = AustralianState.SA;
          break;
        case "TAZ":
          CdaAddress.AustralianAddress.State = AustralianState.TAS;
          break;
        default:
          CdaAddress.AustralianAddress.State = AustralianState.Undefined;
          break;
      }
      return CdaAddress;
    }

    private DateTimeOffset GetOldestDateTime(IEnumerable<IField> DateList)
    {
      List<DateTimeOffset> DateTimeOffsetList = new List<DateTimeOffset>();
      foreach (var Date in DateList)
      {
        if (Date.Convert.DateTime.CanParseToDateTimeOffset)
        {
          DateTimeOffsetList.Add(Date.Convert.DateTime.GetDateTimeOffset());
        }
        else
        {
          throw new ApplicationException($"Unable to convert a the string {Date} to a DateTime.");
        }
      }
      return GetOldesDateTime(DateTimeOffsetList);
    }

    private DateTimeOffset GetOldestDateTime(IEnumerable<IComponent> DateList)
    {
      List<DateTimeOffset> DateTimeOffsetList = new List<DateTimeOffset>();
      foreach (var Date in DateList)
      {
        if (Date.Convert.DateTime.CanParseToDateTimeOffset)
        {
          DateTimeOffsetList.Add(Date.Convert.DateTime.GetDateTimeOffset());
        }
        else
        {
          throw new ApplicationException($"Unable to convert a the string {Date} to a DateTime.");
        }
      }
      return GetOldesDateTime(DateTimeOffsetList);
    }

    private DateTimeOffset GetOldesDateTime(IList<DateTimeOffset> DateList)
    {
      if (DateList is null)
      {
        throw new ArgumentNullException(nameof(DateList));
      }

      if (DateList.Count() == 0)
      {
        throw new ApplicationException("An empty date list was provided to the method.");
      }

      DateTimeOffset OldestReportReleaseDate = DateTimeOffset.MinValue;
      foreach (var ThisReportsReleaseDate in DateList)
      {
        if (OldestReportReleaseDate < ThisReportsReleaseDate)
        {
          OldestReportReleaseDate = ThisReportsReleaseDate;
        }
      }
      return OldestReportReleaseDate;
    }
  }
}
