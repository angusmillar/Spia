using System;
using Hl7.Fhir.Model;
using System.Linq;
using System.Collections.Generic;
using Hl7.Fhir.Utility;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Support;
using PeterPiper.Hl7.V2.Model;
using PeterPiper.Hl7.V2.Support.Tools;
using System.Text;
using Spia.PathologyReportModel.Model;

namespace Spia.AdhaFhirGeneration.Factory
{
  public class FhirPathologyFactory
  {
    private Spia.PathologyReportModel.Model.PathologyReport Report;
    private string PdfDirectory;
    private string SnomedFhirSystemUri = "http://snomed.info/sct";
    private string LoincFhirSystemUri = "http://loinc.org";
    private List<Observation> TotalObservationList;
    public FhirPathologyFactory()
    {

    }
    public string CreateJson(Spia.PathologyReportModel.Model.PathologyReportContainer PathologyReportContainer, string PdfDirectory)
    {
      TotalObservationList = new List<Observation>();
      Report = PathologyReportContainer.PathologyReport;
      this.PdfDirectory = PdfDirectory;


      List<Resource> BundleResourcerList = new List<Resource>();
      Hl7.Fhir.Model.Patient Patient = GetPatientMandatoryIdentifier(Report.Patient);
      Organization PathologyOrganisation = GetSendingOrganisation(Report.PerformingLaboratory);


      Practitioner OrderingPractitioner = GetOrderingPractitioner(Report.Request.RequestingProvider);
      PractitionerRole OrderingPractitionerRole = GetOrderingPractitionerRole(null, OrderingPractitioner);

      ServiceRequest PathologyServiceRequest = GetPathologyServiceRequest(OrderingPractitionerRole);
      Practitioner PathologistPractitioner = GetPathologistPractitioner(Report.ReportList[0].ReportingPathologist);
      PractitionerRole PathologistPractitionerRole = GetPractitionerRole(PathologyOrganisation, PathologistPractitioner, null, null);
      Specimen Specimen = GetSpecimen(Patient, Report.GetOldestCollectionDateTime());

      Hl7.Fhir.Model.DiagnosticReport DiagnosticReport = GetDiagnosticReport(Patient, PathologistPractitionerRole, Specimen, PathologyServiceRequest, Report.ReportList[0], Report.PerformingLaboratory);

      List<Observation> ObservationList = GetObservationList(Patient, Report.ReportList[0].Panel.ResultList, Specimen, PathologistPractitionerRole);
      Observation ParentObs = GetPanelObservation(DiagnosticReport.Code.Coding, Patient, Specimen, PathologistPractitionerRole, ObservationList);
      TotalObservationList.Insert(0, ParentObs);
      DiagnosticReport.Result = new List<ResourceReference>()
      {
        new ResourceReference($"{ParentObs.ResourceType.GetLiteral()}/{ParentObs.Id}", $"{ParentObs.Code.Coding[0].Display}")
      };

      Composition Composition = GetPathologyComposition(Patient, PathologistPractitionerRole, DiagnosticReport);

      BundleResourcerList.Add(Composition);
      BundleResourcerList.Add(Patient);
      BundleResourcerList.Add(DiagnosticReport);
      BundleResourcerList.Add(PathologyServiceRequest);
      BundleResourcerList.Add(OrderingPractitioner);
      BundleResourcerList.Add(OrderingPractitionerRole);
      BundleResourcerList.Add(PathologyOrganisation);
      BundleResourcerList.Add(PathologistPractitioner);
      BundleResourcerList.Add(PathologistPractitionerRole);
      BundleResourcerList.Add(Specimen);
      BundleResourcerList.AddRange(TotalObservationList);


      Bundle Bun = new Bundle();
      Bun.Id = System.Guid.NewGuid().ToFhirId();
      Bun.Type = Bundle.BundleType.Document;
      Bun.Timestamp = DateTimeOffset.Now;
      Bun.Entry = new List<Bundle.EntryComponent>();
      foreach (var Res in BundleResourcerList)
      {
        Bun.Entry.Add(new Bundle.EntryComponent()
        {
          FullUrl = $"urn:uuid:{Res.Id}",
          Resource = Res
        });
      }

      FhirJsonSerializer FhirJsonSerializer = new FhirJsonSerializer(new SerializerSettings() { Pretty = true });
      return FhirJsonSerializer.SerializeToString(Bun, Hl7.Fhir.Rest.SummaryType.False);
    }
    private Composition GetPathologyComposition(Hl7.Fhir.Model.Patient subject, PractitionerRole AuthorPractitionerRole, Hl7.Fhir.Model.DiagnosticReport PathologyReport)
    {


      var Comp = new Composition();
      Comp.Id = System.Guid.NewGuid().ToFhirId();
      Comp.Meta = new Meta();
      Comp.Meta.Profile = new List<string>() { "http://ns.electronichealth.net.au/ci/fhir/4.0/StructureDefinition/composition-pathreport-1" };
      Comp.Language = "en-AU";
      //Comp.Identifier = ?
      Comp.Status = CompositionStatus.Final;
      Comp.Type = new CodeableConcept("https://healthterminologies.gov.au/fhir/CodeSystem/nctis-data-components-1", "100.32001");
      Comp.Subject = new ResourceReference($"{subject.ResourceType.GetLiteral()}/{subject.Id}", subject.Name[0].Text);
      Comp.DateElement = new FhirDateTime(new DateTimeOffset(2020, 06, 12, 10, 30, 00, TimeSpan.FromHours(10)));
      Comp.Author = new List<ResourceReference>()
      {
        new ResourceReference($"{AuthorPractitionerRole.ResourceType.GetLiteral()}/{AuthorPractitionerRole.Id}")
      };
      Comp.Title = "Pathology Report";
      Comp.Attester = new List<Composition.AttesterComponent>()
      {
        new Composition.AttesterComponent()
        {
          Mode = Composition.CompositionAttestationMode.Legal,
          TimeElement = new FhirDateTime(new DateTimeOffset(2020, 06, 12, 10, 30, 00, TimeSpan.FromHours(10))),
          Party = new ResourceReference($"{AuthorPractitionerRole.ResourceType.GetLiteral()}/{AuthorPractitionerRole.Id}")
        }
      };
      Comp.Custodian = AuthorPractitionerRole.Organization;
      Comp.Section = new List<Composition.SectionComponent>();
      Comp.Section.Add(new Composition.SectionComponent()
      {
        Title = "Pathology Report",
        Code = new CodeableConcept(LoincFhirSystemUri, "11526-1"),
        Mode = ListMode.Snapshot,
        Text = new Narrative()
        {
          Status = Narrative.NarrativeStatus.Additional,
          Div = "<div xmlns=\"http://www.w3.org/1999/xhtml\">\n  <pre>Pathology Report narrative in full here </pre>\n</div>"
        },
        Entry = new List<ResourceReference>()
        {
          new ResourceReference($"{PathologyReport.ResourceType.GetLiteral()}/{PathologyReport.Id}", PathologyReport.Code.Coding[0].Display)
        }
      });
      return Comp;
    }
    private Hl7.Fhir.Model.Patient GetPatientMandatoryIdentifier(Spia.PathologyReportModel.Model.Patient Patient)
    {
      var Pat = new Hl7.Fhir.Model.Patient();
      Pat.Id = System.Guid.NewGuid().ToFhirId();
      var IdentifierList = new List<Hl7.Fhir.Model.Identifier>();
      var MedicareNumberList = Patient.IdentifierList.Where(x => x.Type == PathologyReportModel.Model.IdentifierType.MedicareNumber);
      foreach (var MedicareNumber in MedicareNumberList)
      {
        Hl7.Fhir.Model.Identifier MC = new Hl7.Fhir.Model.Identifier()
        {
          Type = new CodeableConcept("http://hl7.org/fhir/v2/0203", "MC", "Medicare Number"),
          System = "http://ns.electronichealth.net.au/id/medicare-number",
          Value = MedicareNumber.Value,
        };
        IdentifierList.Add(MC);
      }

      //var DVANumberFieldList = Patient.IdentifierList.Where(x => x.Type == PathologyReportModel.Model.IdentifierType.DVA);
      //foreach (var DVAField in DVANumberFieldList)
      //{
      //  string DVAColorText = "DVA Number";
      //  switch (DVAField.Component(5).AsString.ToUpper())
      //  {
      //    case "DVG":
      //      DVAColorText += " (Gold)";
      //      break;
      //    case "DVW":
      //      DVAColorText += " (White)";
      //      break;
      //    case "DVL":
      //      DVAColorText += " (Lilac)";
      //      break;
      //    case "DVO":
      //      DVAColorText += " (Orange)";
      //      break;
      //  }

      //  Identifier DVA = new Identifier()
      //  {
      //    Type = new CodeableConcept("http://hl7.org/fhir/v2/0203", "DVA", DVAColorText),
      //    System = "http://ns.electronichealth.net.au/id/dva",
      //    Value = DVAField.Component(1).AsString,
      //  };
      //  IdentifierList.Add(DVA);
      //}

      var MedicalRecordNumbnerList = Patient.IdentifierList.Where(x => x.Type == PathologyReportModel.Model.IdentifierType.MRN);
      foreach (var MedicalRecordNumber in MedicalRecordNumbnerList)
      {
        Hl7.Fhir.Model.Identifier MRN = new Hl7.Fhir.Model.Identifier()
        {
          Type = new CodeableConcept("http://hl7.org/fhir/v2/0203", "MR", "Medical Record Number"),
          System = $"http://{Report.PerformingLaboratory.FacilityCode}/id/MedicalRecordNumber/{MedicalRecordNumber.AssigningAuthority}",
          Value = MedicalRecordNumber.Value,
        };
        IdentifierList.Add(MRN);
      }

      var IhiNumbnerList = Patient.IdentifierList.Where(x => x.Type == PathologyReportModel.Model.IdentifierType.IHI);
      foreach (var IhiNumber in IhiNumbnerList)
      {
        Hl7.Fhir.Model.Identifier Ihi = new Hl7.Fhir.Model.Identifier()
        {
          Type = new CodeableConcept("http://hl7.org/fhir/v2/0203", "NI", "IHI"),
          System = $"http://ns.electronichealth.net.au/id/hi/ihi/1.0",
          Value = IhiNumber.Value.Replace(" ", ""),
        };
        IdentifierList.Add(Ihi);
      }

      if (IdentifierList.Count > 0)
      {
        Pat.Identifier = IdentifierList;
      }

      Pat.Active = true;
      Pat.Name = new List<HumanName>() { GetHumanName(Patient.Name) };

      switch (Patient.Gender)
      {
        case PathologyReportModel.Model.GenderType.Ambiguous:
          Pat.Gender = AdministrativeGender.Unknown;
          break;
        case PathologyReportModel.Model.GenderType.Female:
          Pat.Gender = AdministrativeGender.Female;
          break;
        case PathologyReportModel.Model.GenderType.Male:
          Pat.Gender = AdministrativeGender.Male;
          break;
        case PathologyReportModel.Model.GenderType.NotApplicable:
          Pat.Gender = AdministrativeGender.Other;
          break;
        case PathologyReportModel.Model.GenderType.Other:
          Pat.Gender = AdministrativeGender.Other;
          break;
        case PathologyReportModel.Model.GenderType.Unknown:
          Pat.Gender = AdministrativeGender.Unknown;
          break;
        default:
          throw new ApplicationException($"Unable to map {nameof(Patient.Gender)} to a FHIR Gender. Value was {Patient.Gender.ToString()}");
      }

      Pat.BirthDateElement = new Date(Patient.DateOfBirth.Year, Patient.DateOfBirth.Month, Patient.DateOfBirth.Day);

      foreach (var Addr in Patient.AddressList)
      {
        if (Pat.Address == null)
          Pat.Address = new List<Hl7.Fhir.Model.Address>();
        var Address = new Hl7.Fhir.Model.Address();
        Pat.Address.Add(Address);

        List<string> AddrLineList = null;
        if (!string.IsNullOrWhiteSpace(Addr.LineOne))
        {
          if (AddrLineList == null)
            AddrLineList = new List<string>();
          AddrLineList.Add(Addr.LineOne);
        }
        if (!string.IsNullOrWhiteSpace(Addr.LineTwo))
        {
          if (AddrLineList == null)
            AddrLineList = new List<string>();
          AddrLineList.Add(Addr.LineTwo);
        }
        if (AddrLineList != null)
          Address.Line = AddrLineList.ToArray();

        if (!string.IsNullOrWhiteSpace(Addr.City))
          Address.City = Addr.City;

        var StateTypeSupport = new Spia.PathologyReportModel.Support.StateTypeSupport();
        if (StateTypeSupport.TryLookupByEnum(Addr.State, out string StateCode))
        {
          Address.State = StateCode;
        }
        else
        {
          throw new ApplicationException($"Unable to convert the {Addr.State} of {Addr.State.ToString()} to the required code for FHIR.");
        }

        if (!string.IsNullOrWhiteSpace(Addr.PostCode))
          Address.PostalCode = Addr.PostCode;

        if (!string.IsNullOrWhiteSpace(Addr.Country))
          Address.Country = Addr.Country;

        switch (Addr.TypeCode)
        {
          case PathologyReportModel.Model.AddressType.FirmBusiness:
            Address.Use = Hl7.Fhir.Model.Address.AddressUse.Work;
            break;
          case PathologyReportModel.Model.AddressType.BadAddress:
            Address.Use = Hl7.Fhir.Model.Address.AddressUse.Old;
            break;
          case PathologyReportModel.Model.AddressType.BirthDeliveryLocation:
            Address.Use = Hl7.Fhir.Model.Address.AddressUse.Home;
            break;
          case PathologyReportModel.Model.AddressType.ResidenceAtbirth:
            Address.Use = Hl7.Fhir.Model.Address.AddressUse.Home;
            break;
          case PathologyReportModel.Model.AddressType.CurrentOrTemporary:
            Address.Use = Hl7.Fhir.Model.Address.AddressUse.Temp;
            break;
          case PathologyReportModel.Model.AddressType.CountryOfOrigin:
            Address.Use = Hl7.Fhir.Model.Address.AddressUse.Home;
            break;
          case PathologyReportModel.Model.AddressType.Home:
            Address.Use = Hl7.Fhir.Model.Address.AddressUse.Home;
            break;
          case PathologyReportModel.Model.AddressType.LegalAddress:
            Address.Use = Hl7.Fhir.Model.Address.AddressUse.Billing;
            break;
          case PathologyReportModel.Model.AddressType.Mailing:
            Address.Use = Hl7.Fhir.Model.Address.AddressUse.Home;
            break;
          case PathologyReportModel.Model.AddressType.Birth:
            Address.Use = Hl7.Fhir.Model.Address.AddressUse.Home;
            break;
          case PathologyReportModel.Model.AddressType.Office:
            Address.Use = Hl7.Fhir.Model.Address.AddressUse.Work;
            break;
          case PathologyReportModel.Model.AddressType.Permanent:
            Address.Use = Hl7.Fhir.Model.Address.AddressUse.Home;
            break;
          case PathologyReportModel.Model.AddressType.RegistryHome:
            Address.Use = Hl7.Fhir.Model.Address.AddressUse.Home;
            break;
          default:
            throw new ApplicationException($"Unable to convert the address {nameof(Addr.TypeCode)} of {Addr.TypeCode.ToString()} to a FHIR code.");
        }
      }

      Pat.Text = new Narrative();
      Pat.Text.Status = Narrative.NarrativeStatus.Generated;
      StringBuilder sb = new StringBuilder("<div xmlns=\"http://www.w3.org/1999/xhtml\">\n");
      sb.Append($"  <p>Patient Name: {Pat.Name[0].Text}, DOB: {Pat.BirthDateElement.ToString()}, Gender: {Pat.Gender.GetLiteral()}</p>\n");
      sb.Append("</div>");
      Pat.Text.Div = sb.ToString();

      return Pat;
    }
    private void SetNameRender(HumanName name)
    {
      string Fullname = string.Empty;
      if (name.Family != "")
      {
        Fullname = name.Family.ToUpper();
      }

      if (name.Given.Count() > 0)
      {
        Fullname += $", ";
        foreach (var Given in name.Given)
        {
          Fullname += $"{Given} ";
        }
        Fullname.TrimEnd(' ');
      }

      if (name.Prefix.Count() > 0)
      {
        foreach (var Prefix in name.Prefix)
        {
          Fullname += $"{Prefix}, ";
        }
        Fullname.TrimEnd(',');
      }
      name.Text = Fullname;
    }
    private Organization GetSendingOrganisation(Spia.PathologyReportModel.Model.Laboratory Laboratory)
    {
      var org = new Organization();
      org.Id = System.Guid.NewGuid().ToFhirId();

      var IdentiferList = new List<Hl7.Fhir.Model.Identifier>();
      var Id = new Hl7.Fhir.Model.Identifier()
      {
        Type = new CodeableConcept(null, null, "NATA Accreditation Number"),
        System = "http://hl7.org.au/id/nata-accreditation",
        Value = Laboratory.NataSiteNumber
      };
      org.Identifier = IdentiferList;

      org.ActiveElement = new FhirBoolean(true);
      org.Name = Report.PerformingLaboratory.FacilityName;

      org.Text = new Narrative();
      org.Text.Status = Narrative.NarrativeStatus.Generated;
      StringBuilder sb = new StringBuilder("<div xmlns=\"http://www.w3.org/1999/xhtml\">\n");
      sb.Append($"  <p>Organization Name: {org.Name}</p>\n");
      sb.Append("</div>");
      org.Text.Div = sb.ToString();

      return org;
    }
    private ServiceRequest GetPathologyServiceRequest(PractitionerRole OrderingPractitionerRole)
    {
      var sr = new ServiceRequest();
      sr.Id = System.Guid.NewGuid().ToFhirId();

      sr.Identifier = new List<Hl7.Fhir.Model.Identifier>()
      {
        new Hl7.Fhir.Model.Identifier()
        {
          System = $"http://{Report.PerformingLaboratory.FacilityCode}/id/orderingId/{Report.Request.RequestingFacility.Name}",
          Value = Report.Request.OrderNumber
        }
      };
      sr.Status = RequestStatus.Active;
      sr.Intent = RequestIntent.Order;
      sr.Category = new List<CodeableConcept>()
      {
        new CodeableConcept(SnomedFhirSystemUri,"108252007")
      };

      sr.Code = new CodeableConcept();
      sr.Code.Coding = new List<Coding>();
      if (Report.ReportList.Count > 1)
      {
        throw new ApplicationException("The FHIR generator does not currently support many reports in one Pathology report.");
      }
      sr.Code.Coding.Add(new Coding($"http://{Report.PerformingLaboratory.FacilityCode}/id/orderingCode/", Report.ReportList[0].ReportType.Local.Term, Report.ReportList[0].ReportType.Local.Description));
      if (Report.ReportList[0].ReportType.Snomed is object)
      {
        sr.Code.Coding.Add(new Coding(SnomedFhirSystemUri, Report.ReportList[0].ReportType.Snomed.Term, Report.ReportList[0].ReportType.Snomed.Description));
      }

      sr.AuthoredOnElement = new FhirDateTime(Report.Request.RequestedDate);

      sr.Requester = new ResourceReference($"{OrderingPractitionerRole.ResourceType.GetLiteral()}/{OrderingPractitionerRole.Id}");

      sr.Text = new Narrative();
      sr.Text.Status = Narrative.NarrativeStatus.Generated;
      StringBuilder sb = new StringBuilder("<div xmlns=\"http://www.w3.org/1999/xhtml\">\n");
      sb.Append($"  <p>ServiceRequest for: {sr.Code.Coding[0].Display}</p>\n");
      sb.Append($"  <p>Order Id : {sr.Identifier[0].Value}</p>\n");
      sb.Append($"  <p>Requested by : {OrderingPractitionerRole.Practitioner.Display}</p>\n");
      sb.Append($"  <p>Requested on : {sr.AuthoredOnElement.Value}</p>\n");
      sb.Append("</div>");
      sr.Text.Div = sb.ToString();

      return sr;
    }
    private Practitioner GetOrderingPractitioner(Spia.PathologyReportModel.Model.Provider Provider)
    {
      var prac = new Practitioner();
      prac.Id = System.Guid.NewGuid().ToFhirId();

      var IdentifierList = new List<Hl7.Fhir.Model.Identifier>();

      var HpiiList = Provider.IdentifierList.Where(x => x.Type == PathologyReportModel.Model.IdentifierType.HPII);
      foreach (var Hpii in HpiiList)
      {
        Hl7.Fhir.Model.Identifier HpiiIdentifier = new Hl7.Fhir.Model.Identifier()
        {
          Type = new CodeableConcept("http://hl7.org/fhir/v2/0203", "NPI", "HPI-I"),
          System = "http://ns.electronichealth.net.au/id/hi/hpii/1.0",
          Value = Hpii.Value
        };
        IdentifierList.Add(HpiiIdentifier);
      }
      //Local Code
      var LocalCodeList = Provider.IdentifierList.Where(x => x.Type == PathologyReportModel.Model.IdentifierType.LocalToLab);
      foreach (var LocalCode in LocalCodeList)
      {
        Hl7.Fhir.Model.Identifier HpiiIdentifier = new Hl7.Fhir.Model.Identifier()
        {
          Type = new CodeableConcept("http://hl7.org/fhir/v2/0203", "NPI", "HPI-I"),
          System = $"http://{Report.PerformingLaboratory.FacilityCode}/id/ProviderCode/{LocalCode.AssigningAuthority}",
          Value = LocalCode.Value
        };
        IdentifierList.Add(HpiiIdentifier);
      }

      if (IdentifierList.Count > 0)
      {
        prac.Identifier = IdentifierList;
      }

      prac.Active = true;
      prac.Name = new List<HumanName>();
      var Name = new HumanName();
      prac.Name.Add(Name);
      if (string.IsNullOrWhiteSpace(Provider.Name.Title))
      {
        Name.Prefix = new string[] { Provider.Name.Title };
      }
      if (string.IsNullOrWhiteSpace(Provider.Name.Given))
      {
        Name.Given = new string[] { Provider.Name.Given };
      }
      Name.Family = Provider.Name.Family;
      SetNameRender(prac.Name[0]);

      prac.Text = new Narrative();
      prac.Text.Status = Narrative.NarrativeStatus.Generated;
      StringBuilder sb = new StringBuilder("<div xmlns=\"http://www.w3.org/1999/xhtml\">\n");
      sb.Append($"  <p>Practitioner Name: {prac.Name[0].Text}</p>\n");
      sb.Append($"  <p>HPI-I : {prac.Identifier[0].Value}</p>\n");
      sb.Append("</div>");
      prac.Text.Div = sb.ToString();

      return prac;
    }
    private Practitioner GetPathologistPractitioner(Spia.PathologyReportModel.Model.Provider Provider)
    {
      var prac = new Practitioner();
      prac.Id = System.Guid.NewGuid().ToFhirId();

      var IdentifierList = new List<Hl7.Fhir.Model.Identifier>();

      var HpiiIdentifier = Provider.IdentifierList.SingleOrDefault(x => x.Type == PathologyReportModel.Model.IdentifierType.HPII);
      if (HpiiIdentifier is object)
      {
        Hl7.Fhir.Model.Identifier Hpii = new Hl7.Fhir.Model.Identifier()
        {
          Type = new CodeableConcept("http://hl7.org/fhir/v2/0203", "NPI", "HPI-I"),
          System = "http://ns.electronichealth.net.au/id/hi/hpii/1.0",
          Value = HpiiIdentifier.Value,
        };
        IdentifierList.Add(Hpii);
      }

      var LocalLabId = Provider.IdentifierList.SingleOrDefault(x => x.Type == PathologyReportModel.Model.IdentifierType.LocalToLab);
      if (LocalLabId is object)
      {
        Hl7.Fhir.Model.Identifier LocalId = new Hl7.Fhir.Model.Identifier()
        {
          //Type = new CodeableConcept(null, null, OrderingProviderComp.SubComponent(9).AsString),
          System = $"http://{Report.PerformingLaboratory.FacilityCode}/nataSiteNumber{Report.PerformingLaboratory.NataSiteNumber}/id/employeeId",
          Value = LocalLabId.Value,
        };
        IdentifierList.Add(LocalId);
      }

      if (IdentifierList.Count > 0)
      {
        prac.Identifier = IdentifierList;
      }

      prac.Active = true;
      prac.Name = new List<HumanName>();
      prac.Name.Add(GetHumanName(Provider.Name));      
      SetNameRender(prac.Name[0]);

      prac.Text = new Narrative();
      prac.Text.Status = Narrative.NarrativeStatus.Generated;
      StringBuilder sb = new StringBuilder("<div xmlns=\"http://www.w3.org/1999/xhtml\">\n");
      sb.Append($"  <p>Pathologists Name: {prac.Name[0].Text}</p>\n");
      sb.Append("</div>");
      prac.Text.Div = sb.ToString();
      return prac;
    }
    private HumanName GetHumanName(Name name)
    {
      if (name is null)
        throw new NullReferenceException($"{nameof(name)} can not be null.");
      var HumanName = new HumanName();
      if (name.Family is object)
      {
        HumanName.Family = name.Family;
      }
      if (name.Title is object)
      {
        HumanName.Prefix = new List<string>() { name.Title };
      }
      if (name.Given is object)
      {
        HumanName.Given = new List<string>() { name.Given };
        if (name.Middle is object)
        {
          HumanName.Given.ToList().Add(name.Middle);
        }
      }
      return HumanName;
    }
    private PractitionerRole GetOrderingPractitionerRole(Organization organization, Practitioner practitioner)
    {
      Hl7.Fhir.Model.Identifier MedicareProviderNumberIdentifier = null;
      var MedicareProviderNumber = Report.Request.RequestingProvider.IdentifierList.SingleOrDefault(x => x.Type == PathologyReportModel.Model.IdentifierType.MedicareProviderNumber);
      if (MedicareProviderNumber != null)
      {
        MedicareProviderNumberIdentifier = new Hl7.Fhir.Model.Identifier()
        {
          Type = new CodeableConcept("http://terminology.hl7.org.au/CodeSystem/v2-0203", "UPIN"),
          System = "http://ns.electronichealth.net.au/id/medicare-provider-number",
          Value = MedicareProviderNumber.Value
        };
      }
      return GetPractitionerRole(organization, practitioner, MedicareProviderNumberIdentifier, null);
    }
    private PractitionerRole GetPractitionerRole(Organization organization, Practitioner orderingPractitioner, Hl7.Fhir.Model.Identifier Identifier, string mobileNumber)
    {
      var pracRole = new PractitionerRole();
      pracRole.Id = System.Guid.NewGuid().ToFhirId();
      if (Identifier != null)
      {
        pracRole.Identifier = new List<Hl7.Fhir.Model.Identifier>()
        {
          Identifier
        };
      }
      pracRole.Active = true;
      pracRole.Practitioner = new ResourceReference($"{orderingPractitioner.ResourceType.GetLiteral()}/{orderingPractitioner.Id}", orderingPractitioner.Name[0].Text);
      if (organization != null)
      {
        pracRole.Organization = new ResourceReference($"{organization.ResourceType.GetLiteral()}/{organization.Id}", organization.Name);
      }

      if (mobileNumber != null && mobileNumber != "")
      {
        pracRole.Telecom = new List<ContactPoint>()
        {
        new ContactPoint(ContactPoint.ContactPointSystem.Phone, ContactPoint.ContactPointUse.Mobile, mobileNumber)
        };
      }

      pracRole.Text = new Narrative();
      pracRole.Text.Status = Narrative.NarrativeStatus.Generated;
      StringBuilder sb = new StringBuilder("<div xmlns=\"http://www.w3.org/1999/xhtml\">\n");
      if (orderingPractitioner != null)
        sb.Append($"  <p>Practitioner : {orderingPractitioner.Name[0].Text}</p>\n");
      if (organization != null)
        sb.Append($"  <p>At: {organization.Name}</p>\n");
      if (Identifier != null)
      {
        sb.Append($"  <p>Medicare Provider Number: {Identifier.Value}</p>\n");
      }
      sb.Append("</div>");
      pracRole.Text.Div = sb.ToString();

      return pracRole;

    }
    private Hl7.Fhir.Model.DiagnosticReport GetDiagnosticReport(Hl7.Fhir.Model.Patient subject, PractitionerRole PerformerPractitionerRole, Specimen Specimen, ServiceRequest PathologyServiceRequest, Report Panel, Laboratory PerformingLab)
    {

      var Diag = new Hl7.Fhir.Model.DiagnosticReport();
      Diag.Id = System.Guid.NewGuid().ToFhirId();
      Diag.Language = "en-AU";

      Diag.Identifier = new List<Hl7.Fhir.Model.Identifier>()
      {
        new Hl7.Fhir.Model.Identifier()
        {
          System = GetPerformingLabSystemRoot(PerformingLab.FacilityCode, PerformingLab.NataSiteNumber) + "/id/LaboratoryNumber",
          Value = Panel.ReportId
        }
      };
      Diag.BasedOn = new List<ResourceReference>()
      {
        new ResourceReference($"{PathologyServiceRequest.ResourceType.GetLiteral()}/{PathologyServiceRequest.Id}", PathologyServiceRequest.Code.Coding[0].Display)
      };

      switch (Panel.ReportStatus)
      {
        case ResultStatusType.Final:
          Diag.Status = Hl7.Fhir.Model.DiagnosticReport.DiagnosticReportStatus.Final;
          break;
        case ResultStatusType.Preliminary:
          Diag.Status = Hl7.Fhir.Model.DiagnosticReport.DiagnosticReportStatus.Preliminary;
          break;
        case ResultStatusType.NoResultsAvailableOrderCanceled:
          Diag.Status = Hl7.Fhir.Model.DiagnosticReport.DiagnosticReportStatus.Cancelled;
          break;
        case ResultStatusType.Correction:
          Diag.Status = Hl7.Fhir.Model.DiagnosticReport.DiagnosticReportStatus.Corrected;
          break;
        default:
          throw new ApplicationException($"Unable to convert a {nameof(Panel.ReportStatus)} of {Panel.ReportStatus.ToString()} to a FHIR status.");
      }

      Diag.Category = new List<CodeableConcept>()
      {
        //Issue: Shouldn't this be the DianosticServiceSectionID Code?
        new CodeableConcept("http://terminology.hl7.org/CodeSystem/v2-0074", "LAB")
      };

      Diag.Code = PathologyServiceRequest.Code;

      Diag.Subject = new ResourceReference($"{subject.ResourceType.GetLiteral()}/{subject.Id}", subject.Name[0].Text);
      Diag.Effective = Specimen.Collection.Collected;

      Diag.Issued = Panel.ReportReleaseDateTime;


      //Performer
      //Definition: The diagnostic service that is responsible for issuing the report.
      //Comments: This is not necessarily the source of the atomic data items or the entity that interpreted 
      //          the results. It is the entity that takes responsibility for the clinical report.
      Diag.Performer = new List<ResourceReference>()
      {
        new ResourceReference($"{PerformerPractitionerRole.ResourceType.GetLiteral()}/{PerformerPractitionerRole.Id}")
      };

      //ResultsInterpreter
      //Definition: The practitioner or organization that is responsible for the report's conclusions and interpretations.   
      //Comments: Might not be the same entity that takes responsibility for the clinical report. 
      //Angus: So in our case the responsible for issuing the report and responsible for the report's conclusions and interpretations are the same thing.
      Diag.ResultsInterpreter = Diag.Performer;

      Diag.Result = new List<ResourceReference>();


      Diag.Text = new Narrative();
      Diag.Text.Status = Narrative.NarrativeStatus.Generated;
      StringBuilder sb = new StringBuilder("<div xmlns=\"http://www.w3.org/1999/xhtml\">\n");
      sb.Append($"  <pre>\n");
      sb.Append($"  Report Name:  {PathologyServiceRequest.Code.Coding[0].Display}\n");
      sb.Append($"  Patient: {subject.Name[0].Text}\n");
      sb.Append($"  Laboratory Number:  {Diag.Identifier[0].Value}\n");
      sb.Append($"  Report Status:  {Diag.Status.GetLiteral()}\n");
      sb.Append($"  Collection Date: {(Diag.Effective as FhirDateTime).Value}\n");
      sb.Append($"  Reported Date: {Diag.IssuedElement}\n");
      sb.Append($"  Reporting Pathologists: {PerformerPractitionerRole.Practitioner.Display}\n");
      sb.Append($"  <pre>\n");
      sb.Append("</div>");
      Diag.Text.Div = sb.ToString();


      System.IO.FileInfo PdfFileinfo = new System.IO.FileInfo(System.IO.Path.Combine(this.PdfDirectory, Report.PdfFileName));
      if (PdfFileinfo.Exists)
      {
        var encoder = new System.Text.UTF8Encoding();
        Diag.PresentedForm = new List<Attachment>()
        {
          new Attachment()
          {
             ContentType = "application/pdf",
             Language = "en",
             Title = PathologyServiceRequest.Code.Coding[0].Display,
             Data = System.IO.File.ReadAllBytes(PdfFileinfo.FullName)
          }
        };
      }
      else
      {
        throw new System.IO.FileLoadException($"Unable to load the PDF attachment file from the path: {PdfFileinfo.FullName}");
      }


      return Diag;
    }
    private Specimen GetSpecimen(Hl7.Fhir.Model.Patient subject, DateTimeOffset OldestCollectionDate)
    {
      var Spec = new Specimen();
      Spec.Id = System.Guid.NewGuid().ToFhirId();
      Spec.Subject = new ResourceReference($"{subject.ResourceType.GetLiteral()}/{subject.Id}", subject.Name[0].Text);
      Spec.Collection = new Specimen.CollectionComponent()
      {
        Collected = new FhirDateTime(OldestCollectionDate)
      };
      Spec.Text = new Narrative();
      Spec.Text.Status = Narrative.NarrativeStatus.Generated;
      StringBuilder sb = new StringBuilder("<div xmlns=\"http://www.w3.org/1999/xhtml\">\n");
      sb.Append($"  <p>Specimen Collection Date: {Spec.Collection.Collected.ToString()}</p>\n");
      sb.Append("</div>");
      Spec.Text.Div = sb.ToString();

      return Spec;
    }
    private Observation GetPanelObservation(List<Coding> CodingList, Hl7.Fhir.Model.Patient patient, Specimen specimen, PractitionerRole PerformerPractitionerRole, List<Observation> MemberObservationList = null)
    {
      var Obs = new Observation();
      Obs.Id = System.Guid.NewGuid().ToFhirId();
      Obs.Status = GetObservationStatus(Report.ReportList[0].ReportStatus);
      
      //Issue: Need the Report ID on the simple Observation as below
      Obs.Identifier = new List<Hl7.Fhir.Model.Identifier>()
      {
        new Hl7.Fhir.Model.Identifier()
        {
          System = GetPerformingLabSystemRoot(Report.PerformingLaboratory.FacilityCode, Report.PerformingLaboratory.NataSiteNumber) + $"/id/ReportId",
          Value = Report.ReportList[0].ReportId
        }
      };
      Obs.Category = new List<CodeableConcept>()
      {
        new CodeableConcept("http://terminology.hl7.org/CodeSystem/observation-category", "laboratory", "Laboratory")
      };
      Obs.Code = new CodeableConcept();
      Obs.Code.Coding = CodingList;

      Obs.Subject = new ResourceReference($"{patient.ResourceType.GetLiteral()}/{patient.Id}", patient.Name[0].Text);
      Obs.Effective = specimen.Collection.Collected;
      Obs.Performer = new List<ResourceReference>()
      {
        new ResourceReference($"{PerformerPractitionerRole.ResourceType.GetLiteral()}/{PerformerPractitionerRole.Id}")
      };
      Obs.Specimen = new ResourceReference($"{specimen.ResourceType.GetLiteral()}/{specimen.Id}");


      //Add all member observations
      if (MemberObservationList != null && MemberObservationList.Count > 0)
      {
        Obs.HasMember = new List<ResourceReference>();
        foreach (var Observation in MemberObservationList)
        {
          Obs.HasMember.Add(new ResourceReference($"{Observation.ResourceType.GetLiteral()}/{Observation.Id}", Observation.Code.Coding[0].Display));
        }
      }

      //OBX|14|TX|TXT^TXT^AUSPDI^FBE^Full Blood Count^ADHAPath||~
      Obs.Text = new Narrative();
      Obs.Text.Status = Narrative.NarrativeStatus.Generated;
      StringBuilder sb = new StringBuilder("<div xmlns=\"http://www.w3.org/1999/xhtml\">\n");
      sb.Append($"  <pre>\n");
      sb.Append($"  No HL7 V2 display segment was found.");
      sb.Append($"  <pre>\n");
      sb.Append("</div>");
      Obs.Text.Div = sb.ToString();

      return Obs;
    }
    private Observation GetAtomicObservation(Result Result, Hl7.Fhir.Model.Patient patient, Specimen specimen, PractitionerRole PerformerPractitionerRole, IList<Result> ChildResultList = null)
    {
      var Obs = new Observation();
      TotalObservationList.Add(Obs);
      Obs.Id = System.Guid.NewGuid().ToFhirId();
      Obs.Status = GetObservationStatus(Result.Status);
      Obs.Category = new List<CodeableConcept>()
      {
        new CodeableConcept("http://terminology.hl7.org/CodeSystem/observation-category", "laboratory", "Laboratory")
      };
      Obs.Code = new CodeableConcept();
      Obs.Code.Coding = new List<Coding>();
      Obs.Code.Coding.Add(new Coding(GetPerformingLabSystemRoot(Report.PerformingLaboratory.FacilityCode, Report.PerformingLaboratory.NataSiteNumber) + $"/id/resultCodes", Result.Type.Local.Term, Result.Type.Local.Description));
      if (Result.Type.Lonic is object)
      {
        Obs.Code.Coding.Add(new Coding(LoincFhirSystemUri, Result.Type.Lonic.Term, Result.Type.Lonic.Description));
      }
      
      Obs.Subject = new ResourceReference($"{patient.ResourceType.GetLiteral()}/{patient.Id}", patient.Name[0].Text);
      Obs.Effective = specimen.Collection.Collected;
      Obs.Performer = new List<ResourceReference>()
      {
        new ResourceReference($"{PerformerPractitionerRole.ResourceType.GetLiteral()}/{PerformerPractitionerRole.Id}")
      };
      Obs.Specimen = new ResourceReference($"{specimen.ResourceType.GetLiteral()}/{specimen.Id}");

      var NormalInterpretation = new CodeableConcept("http://terminology.hl7.org/CodeSystem/v3-ObservationInterpretation", "N", "Normal");
      var HighInterpretation = new CodeableConcept("http://terminology.hl7.org/CodeSystem/v3-ObservationInterpretation", "H", "High");
      var LowInterpretation = new CodeableConcept("http://terminology.hl7.org/CodeSystem/v3-ObservationInterpretation", "L", "Low");
      var ResistantInterpretation = new CodeableConcept("http://terminology.hl7.org/CodeSystem/v3-ObservationInterpretation", "R", "Resistant");
      var SusceptibleInterpretation = new CodeableConcept("http://terminology.hl7.org/CodeSystem/v3-ObservationInterpretation", "S", "Susceptible");
      switch (Result.AbnormalFlag)
      {
        case "N":
          Obs.Interpretation = new List<CodeableConcept>() { NormalInterpretation };
          break;
        case "H":
          Obs.Interpretation = new List<CodeableConcept>() { HighInterpretation };
          break;
        case "L":
          Obs.Interpretation = new List<CodeableConcept>() { LowInterpretation };
          break;
        case "R":
          Obs.Interpretation = new List<CodeableConcept>() { ResistantInterpretation };
          break;
        case "S":
          Obs.Interpretation = new List<CodeableConcept>() { SusceptibleInterpretation };
          break;
        default:
          break;
      }

      if (Result.ReferenceRange is object)
      {
        Obs.ReferenceRange = new List<Observation.ReferenceRangeComponent>()
        {
          new Observation.ReferenceRangeComponent()
          {
            Text = Result.ReferenceRange
          }
        };
      }
      //Add all member observations
      if (ChildResultList != null && ChildResultList.Count > 0)
      {
        //Get the Child observations and add to this parent as Members.
        List<Observation> ChildObservationList = GetObservationList(patient, Result.ChildResultList, specimen, PerformerPractitionerRole);
        Obs.HasMember = new List<ResourceReference>();
        foreach (var ChildObservation in ChildObservationList)
        {
          Obs.HasMember.Add(new ResourceReference($"{ChildObservation.ResourceType.GetLiteral()}/{ChildObservation.Id}", ChildObservation.Code.Coding[0].Display));
          TotalObservationList.Add(ChildObservation);
        }
      }

      //Value from OBX-5
      if (Result.Value is object)
      {
        var ResultField = Creator.Field(Result.Value);
        if (Result.DataType.ToUpper() == "NM" || Result.DataType.ToUpper() == "SN")
        {
          string ValueString = string.Empty;
          if (Result.DataType.ToUpper() == "SN")
          {
            ValueString = ResultField.Component(2).AsString;
          }
          else if (Result.DataType.ToUpper() == "NM")
          {
            ValueString = ResultField.AsString;
          }

          Quantity Qty = new Quantity();
          if (Decimal.TryParse(ValueString, out decimal Value))
          {
            Qty.Value = Value;
          }
          else
          {
            throw new ApplicationException($"Unable to convert result value of datatype SN or NM value to a decimal, the value was: {Result.Value}.");
          }

          if (Result.DataType.ToUpper() == "SN")
          {
            switch (ResultField.Component(1).AsString)
            {
              case ">=":
                Qty.Comparator = Quantity.QuantityComparator.GreaterOrEqual;
                break;
              case ">":
                Qty.Comparator = Quantity.QuantityComparator.GreaterThan;
                break;
              case "<=":
                Qty.Comparator = Quantity.QuantityComparator.LessOrEqual;
                break;
              case "<":
                Qty.Comparator = Quantity.QuantityComparator.LessThan;
                break;
              default:
                break;
            }
          }

          if (Result.Units is object)
          {
            Qty.Unit = Result.Units;
          }
          Obs.Value = Qty;
        } 
        else if (Result.DataType.ToUpper() == "ST")
        {
          Obs.Value = new FhirString(ResultField.AsString);
        }
        else if (Result.DataType.ToUpper() == "NR")
        {
          var Range = new Hl7.Fhir.Model.Range();

          var LowSimpleQuantity = new SimpleQuantity();
          LowSimpleQuantity.Value = Convert.ToDecimal(ResultField.Component(1).AsString);
          Range.Low = LowSimpleQuantity;

          var HighSimpleQuantity = new SimpleQuantity();
          HighSimpleQuantity.Value = Convert.ToDecimal(ResultField.Component(2).AsString);
          Range.High = HighSimpleQuantity;

          if (Result.Units is Object)
          {
            Range.High.Unit = Result.Units;
            Range.Low.Unit = Result.Units;
          }

          Obs.Value = Range;
        } 
        else if (Result.DataType.ToUpper() == "FT")
        {         
          Obs.Value = new FhirString(ResultField.AsString);
        }
        else
        {
          //For FT I will strip the formattting and create a simple String for the Obs.value and then use the Formated text to generater a narrative that preservs the formating's bold and line breaks.
          throw new ApplicationException($"Detected result value datatype that is not supported by the FHIR generator. The datatype code was: {Result.DataType}. " +
            $"The supported datatype codes are (NM, SN, ST, NR)");
        }

      }

      Obs.Text = new Narrative();
      Obs.Text.Status = Narrative.NarrativeStatus.Generated;
      StringBuilder sb = new StringBuilder("<div xmlns=\"http://www.w3.org/1999/xhtml\">\n");
      sb.Append($"  <pre>\n");
      

      string Name = Obs.Code.Coding[0].Display;
      string Val = string.Empty;
      string Units = string.Empty;
      string AdnormalityCode = string.Empty;
      string ReferenceRange = string.Empty;

      if (Obs.Value is Quantity Quant)
      {
        if (Quant.Comparator != null)
        {
          Val = $"{Quant.Comparator.GetLiteral()}{Quant.Value.ToString()}{Quant.Value.ToString()}";
        }
        else
        {
          Val = $"{Quant.Value.ToString()}";
        }

        if (Quant.Unit != null && Quant.Unit != string.Empty)
        {
          Units = $"{Quant.Unit}";
        }

        if (Obs.Interpretation != null && Obs.Interpretation.Count > 0 && Obs.Interpretation[0].Coding != null && Obs.Interpretation[0].Coding.Count > 0)
        {
          AdnormalityCode = $"{Obs.Interpretation[0].Coding[0].Code}";
        }

        if (Obs.ReferenceRange != null && Obs.ReferenceRange.Count > 0 && Obs.ReferenceRange[0].Text != "")
        {
          ReferenceRange = $"({Obs.ReferenceRange[0].Text})";
        }
        //string Header = $"Name:{new String(' ', Name.Length + "Name".Length)} Value:{new String(' ', Val.Length + "Value".Length)} Unit:{new String(' ', Units.Length + "Unit".Length)} Normality:{new String(' ', AdnormalityCode.Length + "Normality".Length)} RefRange:{new String(' ', ReferenceRange.Length + "RefRange".Length)}";
        string ResultLine = $"{Name}: {Val} {Units} {AdnormalityCode} {ReferenceRange}";
        //sb.Append($"  {Header}\n");
        sb.Append($"  {ResultLine}\n");
      }
      else if (Obs.Value is Hl7.Fhir.Model.Range FhirRange)
      {
        if (FhirRange.Low.Unit.ToUpper() != FhirRange.High.Unit.ToUpper())
        {
          throw new ApplicationException($"We have a range result where to the Low value units do not equal the High value units. Low unit was {FhirRange.Low.Unit} and High unit was {FhirRange.High.Unit}");
        }
        sb.Append($"  {Name}: {FhirRange.Low.Value.ToString()} - {FhirRange.High.Value.ToString()} {FhirRange.High.Unit} \n");
      }
      else if (Obs.Value is FhirString FhirString)
      {
        if (Result.DataType.ToUpper() == "FT")
        {
          var ResultField = Creator.Field(Result.Value);
          for (int i = 0; i < ResultField.ContentCount; i++)
          {
            IContent Content = ResultField.Content(i);
            if (Content.ContentType == PeterPiper.Hl7.V2.Support.Content.ContentType.Escape)
            {
              if (Content.EscapeMetaData.EscapeType == PeterPiper.Hl7.V2.Support.Standard.EscapeType.NewLine)
              {
                sb.Append("<br />");
              }
              else if (Content.EscapeMetaData.EscapeType == PeterPiper.Hl7.V2.Support.Standard.EscapeType.HighlightOn)
              {
                sb.Append("<b>");
              }
              else if (Content.EscapeMetaData.EscapeType == PeterPiper.Hl7.V2.Support.Standard.EscapeType.HighlightOff)
              {
                sb.Append("</b>");
              }
              else
              {
                sb.Append(Content.AsString);
              }
            }
            else
            {
              sb.Append(Content.AsString);
            }
          }
        }
        else
        {
          sb.Append($"  {Name}: {FhirString.Value}\n");
        }        
      }

      sb.Append($"  </pre>\n");
      sb.Append("</div>");
      Obs.Text.Div = sb.ToString();

      return Obs;
    }
    private ObservationStatus GetObservationStatus(ResultStatusType status)
    {
      switch (status)
      {
        case ResultStatusType.Final:
          return ObservationStatus.Final;          
        case ResultStatusType.Preliminary:
          return ObservationStatus.Preliminary;
        case ResultStatusType.NoResultsAvailableOrderCanceled:
          return ObservationStatus.Cancelled;
        case ResultStatusType.Correction:
          return ObservationStatus.Corrected;
        default:
          throw new ApplicationException($"Unable to convert {nameof(status)} of {status.ToString()} to a FHIR status.");
      }
      throw new NotImplementedException();
    }
    private List<Observation> GetObservationList(Hl7.Fhir.Model.Patient patient, IList<Result> ResultList, Specimen specimen, PractitionerRole PerformerPractitionerRole)
    {
      List<Observation> LocalObservationList = new List<Observation>();
      foreach (var Result in ResultList)
      {
        //The method GetAtomicObservation adds all Observations it creates to the TotalObservationList global list, including the one it returns and all its children
        Observation NewParentObservation = GetAtomicObservation(Result, patient, specimen, PerformerPractitionerRole, Result.ChildResultList);
        LocalObservationList.Add(NewParentObservation);       
      }
      return LocalObservationList;
    }
    private string GetPerformingLabSystemRoot(string FacilityCode, string NataSiteNumber)
    {
      return $"http://{FacilityCode}/atNataSiteNumber{NataSiteNumber}";
    }    
  }
}
