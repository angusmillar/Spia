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

namespace Spia.AdhaFhirGeneration.Factory
{
  public class PathologyFactory
  {
    private IMessage HL7 = null;
    private string SnomedFhirSystemUri = "http://snomed.info/sct";
    private string LoincFhirSystemUri = "http://loinc.org";
    private string SendingFacility = "UnknownSendingFacility";
    private List<Observation> TotalObservationList;
    public PathologyFactory()
    {

    }
    public string CreateJson(string FilePath)
    {
      TotalObservationList = new List<Observation>();
      //string V2Message = System.Text.Encoding.UTF8.GetString(FileResource._1_SPIA_Exemplar_Chlamydia);
            
      PeterPiper.Hl7.V2.Support.TextFile.Hl7StreamReader sr = new PeterPiper.Hl7.V2.Support.TextFile.Hl7StreamReader(FilePath);      
      HL7 = Creator.Message(sr.Read());
      this.SendingFacility = HL7.Segment("MSH").Field(4).Component(1).AsString.ToLower();
      List<Resource> BundleResourcerList = new List<Resource>();
      Patient Patient = GetPatientMandatoryIdentifier();
      Organization PathologyOrganisation = GetSendingOrganisation();


      Practitioner OrderingPractitioner = GetOrderingPractitioner();      
      PractitionerRole OrderingPractitionerRole = GetOrderingPractitionerRole(null, OrderingPractitioner);

      ServiceRequest PathologyServiceRequest = GetPathologyServiceRequest(OrderingPractitionerRole);
      Practitioner PathologistPractitioner = GetPathologistPractitioner();      
      PractitionerRole PathologistPractitionerRole = GetPractitionerRole(PathologyOrganisation, PathologistPractitioner, null, null);
      Specimen Specimen = GetSpecimen(Patient);

      Hl7.Fhir.Model.DiagnosticReport Report = GetDiagnosticReport(Patient, PathologistPractitionerRole, Specimen, PathologyServiceRequest);

      List<Observation> ObservationList = GetOBXSegments(Patient, Specimen, PathologistPractitionerRole);
      Observation ParentObs = GetSimpleObservation(Report.Code.Coding, Patient, Specimen, PathologistPractitionerRole, ObservationList);
      TotalObservationList.Insert(0, ParentObs);
      Report.Result = new List<ResourceReference>()
      {
        new ResourceReference($"{ParentObs.ResourceType.GetLiteral()}/{ParentObs.Id}", $"{ParentObs.Code.Coding[0].Display}")
      };
      
      Composition Composition = GetPathologyComposition(Patient, PathologistPractitionerRole, Report);

      BundleResourcerList.Add(Composition);
      BundleResourcerList.Add(Patient);
      BundleResourcerList.Add(Report);
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
    private Composition GetPathologyComposition(Patient subject, PractitionerRole AuthorPractitionerRole, Hl7.Fhir.Model.DiagnosticReport PathologyReport)
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
    private Patient GetPatientMandatoryIdentifier()
    {
      
      var Pat = new Patient();
      Pat.Id = System.Guid.NewGuid().ToFhirId();
      var IdentifierList = new List<Identifier>();
      IEnumerable<IField> MedicareNumberFieldList = HL7.Segment("PID").Element(3).RepeatList.Where(x => x.Component(4).AsString.ToUpper() == "AUSHIC" && x.Component(5).AsString.ToUpper() == "MC");
      foreach(var MedCareField in MedicareNumberFieldList)
      {
        Identifier MC = new Identifier()
        {
          Type = new CodeableConcept("http://hl7.org/fhir/v2/0203", "MC", "Medicare Number"),
          System = "http://ns.electronichealth.net.au/id/medicare-number",
          Value = MedCareField.Component(1).AsString,
        };
        IdentifierList.Add(MC);
      }

      IEnumerable<IField> DVANumberFieldList = HL7.Segment("PID").Element(3).RepeatList.Where(x => x.Component(4).AsString.ToUpper() == "AUSDVA");
      foreach (var DVAField in DVANumberFieldList)
      {
        string DVAColorText = "DVA Number";
        switch (DVAField.Component(5).AsString.ToUpper())
        {
          case "DVG":
            DVAColorText += " (Gold)";
            break;
          case "DVW":
            DVAColorText += " (White)";
            break;
          case "DVL":
            DVAColorText += " (Lilac)";
            break;
          case "DVO":
            DVAColorText += " (Orange)";
            break;
        }

        Identifier DVA = new Identifier()
        {
          Type = new CodeableConcept("http://hl7.org/fhir/v2/0203", "DVA", DVAColorText),
          System = "http://ns.electronichealth.net.au/id/dva",
          Value = DVAField.Component(1).AsString,
        };
        IdentifierList.Add(DVA);
      }

      IEnumerable<IField> MedicalRecordNumbnerFieldList = HL7.Segment("PID").Element(3).RepeatList.Where(x => x.Component(5).AsString.ToUpper() == "MR");
      foreach(var MRNField in MedicalRecordNumbnerFieldList)
      {
        Identifier MRN = new Identifier()
        {
          Type = new CodeableConcept("http://hl7.org/fhir/v2/0203", "MR", "Medical Record Number"),
          System = $"http://{this.SendingFacility}/id/MedicalRecordNumber/{MRNField.Component(5).AsString}",
          Value = MRNField.Component(1).AsString,
        };
        IdentifierList.Add(MRN);
      }

      IEnumerable<IField> IhiNumbnerFieldList = HL7.Segment("PID").Element(3).RepeatList.Where(x => x.Component(4).AsString.ToUpper() == "AUSHIC" && x.Component(5).AsString.ToUpper() == "NI");
      foreach (var IhiField in IhiNumbnerFieldList)
      {
        Identifier Ihi = new Identifier()
        {
          Type = new CodeableConcept("http://hl7.org/fhir/v2/0203", "NI", "IHI"),
          System = $"http://ns.electronichealth.net.au/id/hi/ihi/1.0",
          Value = IhiField.Component(1).AsString,
        };
        IdentifierList.Add(Ihi);
      }

      if (IdentifierList.Count > 0)
      {
        Pat.Identifier = IdentifierList;
      }

      Pat.Active = true;

      var NameList = new List<HumanName>();
      List<IField> NameFieldList = HL7.Segment("PID").Element(5).RepeatList.ToList();
      foreach(var NameField in NameFieldList)
      {
        var Name = new HumanName();
        if (NameField.Component(1).AsString != string.Empty)
        {
          Name.Family = NameField.Component(1).AsString;
        }
        List<string> GivenNames = new List<string>();
        if (NameField.Component(2).AsString != string.Empty)
        {
          GivenNames.Add(NameField.Component(2).AsString);
        }
        if (NameField.Component(3).AsString != string.Empty)
        {
          GivenNames.Add(NameField.Component(3).AsString);
        }
        Name.Given = GivenNames.ToArray();
        SetNameRender(Name);
        NameList.Add(Name);
      }
      Pat.Name = NameList;

      switch (HL7.Segment("PID").Field(8).AsString.ToUpper())
      {
        case "M":
          Pat.Gender = AdministrativeGender.Male;
          break;
        case "F":
          Pat.Gender = AdministrativeGender.Female;
          break;
        case "O":
          Pat.Gender = AdministrativeGender.Other;
          break;
        case "U":
          Pat.Gender = AdministrativeGender.Unknown;
        break;
        default:
          Pat.Gender = AdministrativeGender.Unknown;
          break;
      }

      if (HL7.Segment("PID").Field(7).Convert.DateTime.CanParseToDateTimeOffset)
      {
        var HL7Date = HL7.Segment("PID").Field(7).Convert.DateTime.GetDateTimeOffset();
        Pat.BirthDateElement = new Date(HL7Date.Year, HL7Date.Month, HL7Date.Day);
      }

      foreach (var V2Address in HL7.Segment("PID").Element(11).RepeatList)
      {
        if (Pat.Address == null)
          Pat.Address = new List<Address>();
        var Address = new Address();
        Pat.Address.Add(Address);

        List<string> AddrLineList = null;
        if (V2Address.Component(1).AsString != "")
        {
          if (AddrLineList == null)
            AddrLineList = new List<string>();
          AddrLineList.Add(V2Address.Component(1).AsString);
        }
        if (V2Address.Component(2).AsString != "")
        {
          if (AddrLineList == null)
            AddrLineList = new List<string>();
          AddrLineList.Add(V2Address.Component(2).AsString);
        }
        if (AddrLineList != null)
          Address.Line = AddrLineList.ToArray();

        if (V2Address.Component(3).AsString != "")
          Address.City = V2Address.Component(3).AsString;

        if (V2Address.Component(4).AsString != "")
          Address.State = V2Address.Component(4).AsString;

        if (V2Address.Component(5).AsString != "")
          Address.PostalCode = V2Address.Component(5).AsString;

        if (V2Address.Component(6).AsString == "AUS")
          Address.Country = "AUS";

        if (V2Address.Component(7).AsString != "")
        {
          switch (V2Address.Component(7).AsString.ToUpper())
          {
            case "H":
              Address.Use = Address.AddressUse.Home;
              break;
            case "B":
              Address.Use = Address.AddressUse.Work;
              break;            
          }
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
        foreach(var Prefix in name.Prefix)
        {
          Fullname += $"{Prefix}, "; 
        }
        Fullname.TrimEnd(',');
      }
      name.Text = Fullname;
    }
    private Organization GetSendingOrganisation()
    {
      var HL7SendingFacility = HL7.Segment("MSH").Field(4);

      var org = new Organization();
      org.Id = System.Guid.NewGuid().ToFhirId();
      var IdentiferList = new List<Identifier>();
      
      if (HL7SendingFacility.Component(3).AsString == "AUSNATA")
      {
        var Id = new Identifier()
        {
          Type = new CodeableConcept(null, null, "NATA Accreditation Number"),
          System = "http://hl7.org.au/id/nata-accreditation",
          Value = HL7SendingFacility.Component(2).AsString
        };
      }

      if (IdentiferList.Count > 0)
      {
        org.Identifier = IdentiferList;
      }
      
      org.ActiveElement = new FhirBoolean(true);
      org.Name = HL7SendingFacility.Component(1).AsString;

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
      var PlacerOrderNumberField = HL7.Segment("OBR").Field(2);
      sr.Identifier = new List<Identifier>()
      {
        new Identifier()
        {
          System = $"http://{this.SendingFacility}/id/orderingId/{PlacerOrderNumberField.Component(2).AsString}",
          Value = PlacerOrderNumberField.Component(1).AsString
        }
      };
      sr.Status = RequestStatus.Active;
      sr.Intent = RequestIntent.Order;
      sr.Category = new List<CodeableConcept>()
      {
        new CodeableConcept(SnomedFhirSystemUri,"108252007")
      };
      var UniversalOrderIdField = HL7.Segment("OBR").Field(4);
      sr.Code = new CodeableConcept();      
      sr.Code.Coding = new List<Coding>();
      if (UniversalOrderIdField.Component(1).AsString != string.Empty)
      {
        if (UniversalOrderIdField.Component(3).AsString == "SCT")
        {
          sr.Code.Coding.Add(new Coding(SnomedFhirSystemUri, UniversalOrderIdField.Component(1).AsString, UniversalOrderIdField.Component(2).AsString));
        }
        else
        {
          sr.Code.Coding.Add(new Coding($"http://{this.SendingFacility}/id/orderingCode/{UniversalOrderIdField.Component(3).AsString}", UniversalOrderIdField.Component(1).AsString, UniversalOrderIdField.Component(2).AsString));
        }
      }
      if (UniversalOrderIdField.Component(4).AsString != string.Empty)
      {
        if (UniversalOrderIdField.Component(6).AsString == "SCT")
        {
          sr.Code.Coding.Add(new Coding(SnomedFhirSystemUri, UniversalOrderIdField.Component(4).AsString, UniversalOrderIdField.Component(5).AsString));
        }
        else
        {
          sr.Code.Coding.Add(new Coding($"http://{this.SendingFacility}/id/orderingCode/{UniversalOrderIdField.Component(6).AsString}", UniversalOrderIdField.Component(4).AsString, UniversalOrderIdField.Component(5).AsString));
        }
      }
      if (HL7.Segment("OBR").Field(27).Component(4).Convert.DateTime.CanParseToDateTimeOffset)
      {        
        sr.AuthoredOnElement = new FhirDateTime(HL7.Segment("OBR").Field(27).Component(4).Convert.DateTime.GetDateTimeOffset());
      }      
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
    private Practitioner GetOrderingPractitioner()
    {
      var OrderingProviderList = HL7.Segment("OBR").Element(16).RepeatList;

      var prac = new Practitioner();
      prac.Id = System.Guid.NewGuid().ToFhirId();

      var IdentifierList = new List<Identifier>();

      IEnumerable<IField> HpiiFieldList = OrderingProviderList.Where(x => x.Component(9).AsString.ToUpper() == "AUSHIC" && x.Component(13).AsString.ToUpper() == "NPI");
      foreach (var HpiiField in HpiiFieldList)
      {
        Identifier Hpii = new Identifier()
        {
          Type = new CodeableConcept("http://hl7.org/fhir/v2/0203", "NPI", "HPI-I"),
          System = "http://ns.electronichealth.net.au/id/hi/hpii/1.0",
          Value = HpiiField.Component(1).AsString,
        };
        IdentifierList.Add(Hpii);
      }


      if (IdentifierList.Count > 0)
      {
        prac.Identifier = IdentifierList;
      }
      
      prac.Active = true;
      prac.Name = new List<HumanName>()
      {
        new HumanName()
        {
          Prefix = new string[] { OrderingProviderList.ToArray()[0].Component(6).AsString},
          Family = OrderingProviderList.ToArray()[0].Component(2).AsString,
          Given = new string[] { OrderingProviderList.ToArray()[0].Component(3).AsString }
        }
      };
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
    private Practitioner GetPathologistPractitioner()
    {
      var OrderingProviderComp = HL7.Segment("OBR").Field(32).Component(1);
      var prac = new Practitioner();
      prac.Id = System.Guid.NewGuid().ToFhirId();

      var IdentifierList = new List<Identifier>();

      //If it's a HPI-I then OBR-32.9 = AUSHIC (This is only a HIPS standard not the Australian Standard)
      if(OrderingProviderComp.SubComponent(9).AsString == "AUSHIC")
      {
        Identifier Hpii = new Identifier()
        {
          Type = new CodeableConcept("http://hl7.org/fhir/v2/0203", "NPI", "HPI-I"),
          System = "http://ns.electronichealth.net.au/id/hi/hpii/1.0",
          Value = OrderingProviderComp.SubComponent(1).AsString,
        };
        IdentifierList.Add(Hpii);
      }
      else
      {
        Identifier LocalId = new Identifier()
        {
          Type = new CodeableConcept(null, null, OrderingProviderComp.SubComponent(9).AsString),
          System = $"http://{this.SendingFacility}/id/employeeId/{OrderingProviderComp.SubComponent(9).AsString}",
          Value = OrderingProviderComp.SubComponent(1).AsString,
        };
        IdentifierList.Add(LocalId);
      }
      
      if (IdentifierList.Count > 0)
      {
        prac.Identifier = IdentifierList;
      }

      prac.Active = true;
      prac.Name = new List<HumanName>()
      {
        new HumanName()
        {
          Prefix = new string[] {OrderingProviderComp.SubComponent(6).AsString},
          Family = OrderingProviderComp.SubComponent(2).AsString,
          Given = new string[] { OrderingProviderComp.SubComponent(3).AsString }
        }
      };
      SetNameRender(prac.Name[0]);

      prac.Text = new Narrative();
      prac.Text.Status = Narrative.NarrativeStatus.Generated;
      StringBuilder sb = new StringBuilder("<div xmlns=\"http://www.w3.org/1999/xhtml\">\n");
      sb.Append($"  <p>Pathologists Name: {prac.Name[0].Text}</p>\n");      
      sb.Append("</div>");
      prac.Text.Div = sb.ToString();
      return prac;
    }
    private PractitionerRole GetOrderingPractitionerRole(Organization organization, Practitioner practitioner)
    {
      var OrderingProviderList = HL7.Segment("OBR").Element(16).RepeatList;
      IField MedicareProviderNumberField = OrderingProviderList.SingleOrDefault(x => x.Component(9).AsString.ToUpper() == "AUSHICPR");
      if (MedicareProviderNumberField != null)
      {
        Identifier MedicareProviderNumber = new Identifier()
        {
          Type = new CodeableConcept("http://terminology.hl7.org.au/CodeSystem/v2-0203", "UPIN"),
          System = "http://ns.electronichealth.net.au/id/medicare-provider-number",
          Value = "394440LA"
        };
        return GetPractitionerRole(organization, practitioner, MedicareProviderNumber, null);
      }
      return GetPractitionerRole(organization, practitioner, null, null);      
    }
    private PractitionerRole GetPractitionerRole(Organization organization, Practitioner orderingPractitioner, Identifier Identifier, string mobileNumber)
    {     
      var pracRole = new PractitionerRole();
      pracRole.Id = System.Guid.NewGuid().ToFhirId();
      if (Identifier != null)
      {
        pracRole.Identifier = new List<Identifier>()
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
    private Hl7.Fhir.Model.DiagnosticReport GetDiagnosticReport(Patient subject, PractitionerRole PerformerPractitionerRole, Specimen Specimen, ServiceRequest PathologyServiceRequest)
    {
      ISegment OBR = HL7.Segment("OBR");      

      var Diag = new Hl7.Fhir.Model.DiagnosticReport();
      Diag.Id = System.Guid.NewGuid().ToFhirId();
      Diag.Language = "en-AU";

      Diag.Identifier = new List<Identifier>()
      {
        new Identifier()
        {
          System = $"http://{this.SendingFacility}/id/LaboratoryNumber",
          Value = OBR.Field(3).Component(1).AsString.Split('-')[0]
        }
      };
      Diag.BasedOn = new List<ResourceReference>()
      {
        new ResourceReference($"{PathologyServiceRequest.ResourceType.GetLiteral()}/{PathologyServiceRequest.Id}", PathologyServiceRequest.Code.Coding[0].Display)
      };

      switch (OBR.Field(25).AsString.ToUpper())
      {
        case "F":
          Diag.Status = Hl7.Fhir.Model.DiagnosticReport.DiagnosticReportStatus.Final;
          break;
        case "X":
          Diag.Status = Hl7.Fhir.Model.DiagnosticReport.DiagnosticReportStatus.Cancelled;
          break;
        case "C":
          Diag.Status = Hl7.Fhir.Model.DiagnosticReport.DiagnosticReportStatus.Corrected;
          break;
        case "P":
          Diag.Status = Hl7.Fhir.Model.DiagnosticReport.DiagnosticReportStatus.Preliminary;
          break;
        default:
          throw new ApplicationException($"OBX-25 status code unknown. Found: {OBR.Field(25).AsString}");          
      }
      
      Diag.Category = new List<CodeableConcept>()
      {
        //Issue: Shouldn't this be the DianosticServiceSectionID Code?
        new CodeableConcept("http://terminology.hl7.org/CodeSystem/v2-0074", "LAB")
      };

      Diag.Code = PathologyServiceRequest.Code;

      Diag.Subject = new ResourceReference($"{subject.ResourceType.GetLiteral()}/{subject.Id}", subject.Name[0].Text);
      Diag.Effective = Specimen.Collection.Collected;
      if (OBR.Field(22).Convert.DateTime.CanParseToDateTimeOffset)
      {
        Diag.Issued = OBR.Field(22).Convert.DateTime.GetDateTimeOffset();
      }

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

      IEnumerable<ISegment> DisplaySegmentList = HL7.SegmentList("OBX").Where(x => x.Field(3).Component(1).AsString == "PDF" && x.Field(3).Component(3).AsString == "AUSPDI");
      

      if (DisplaySegmentList != null)
      {
        var encoder = new System.Text.UTF8Encoding();
        Diag.PresentedForm = new List<Attachment>()
        {
          new Attachment()
          {
             ContentType = "application/pdf",
             Language = "en",
             Title = PathologyServiceRequest.Code.Coding[0].Display,
             Data = encoder.GetBytes(DisplaySegmentList.ToList()[0].Field(5).AsString)
          }
        };
      }
      

      return Diag;
    }
    private Specimen GetSpecimen(Patient subject)
    {
      var Spec = new Specimen();
      Spec.Id = System.Guid.NewGuid().ToFhirId();
      Spec.Subject = new ResourceReference($"{subject.ResourceType.GetLiteral()}/{subject.Id}", subject.Name[0].Text);
      if(HL7.Segment("OBR").Field(7).Convert.DateTime.CanParseToDateTimeOffset)
      {
        Spec.Collection = new Specimen.CollectionComponent()
        {
          Collected = new FhirDateTime(HL7.Segment("OBR").Field(7).Convert.DateTime.GetDateTimeOffset())
        };
      }

      Spec.Text = new Narrative();
      Spec.Text.Status = Narrative.NarrativeStatus.Generated;
      StringBuilder sb = new StringBuilder("<div xmlns=\"http://www.w3.org/1999/xhtml\">\n");     
      sb.Append($"  <p>Specimen Collection Date: {Spec.Collection.Collected.ToString()}</p>\n");
      sb.Append("</div>");
      Spec.Text.Div = sb.ToString();

      return Spec;
    }

    private Observation GetSimpleObservation(List<Coding> CodingList, Patient patient, Specimen specimen, PractitionerRole PerformerPractitionerRole, List<Observation> MemberObservationList = null)
    {      
      var Obs = new Observation();
      Obs.Id = System.Guid.NewGuid().ToFhirId();
      switch (HL7.Segment("OBR").Field(25).AsString.ToUpper())
      {
        case "F":
          Obs.Status = ObservationStatus.Final;
          break;
        case "X":
          Obs.Status = ObservationStatus.Cancelled;
          break;
        case "C":
          Obs.Status = ObservationStatus.Corrected;
          break;
        case "P":
          Obs.Status = ObservationStatus.Preliminary;
          break;
        default:
          throw new ApplicationException($"OBX-25 status code unknown. Found: {HL7.Segment("OBR").Field(25).AsString}");
      }
      
      //Issue: Need the Report ID on the simple Observation as below
      Obs.Identifier = new List<Identifier>()
      {
        new Identifier()
        {
          System = $"http://{this.SendingFacility}/id/ReportId",
          Value = HL7.Segment("OBR").Field(3).Component(1).AsString
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
      ISegment DisplaySeg = HL7.SegmentList().ToList()
        .SingleOrDefault(x =>
          x.Field(3).Component(1).AsString.ToUpper() == "TXT" &&
          x.Field(3).Component(3).AsString.ToUpper() == "AUSPDI");
      if (DisplaySeg != null)
      {
        foreach (var Line in DisplaySeg.Element(5).RepeatList)
        {
          sb.Append($"  {Line.AsString}\n");
        }
      }
      else
      {
        sb.Append($"  No HL7 V2 display segment was found.");
      }
      sb.Append($"  <pre>\n");
      sb.Append("</div>");
      Obs.Text.Div = sb.ToString();

      return Obs;
    }
    private Observation GetAtomicObservation(ISegment OBX, Patient patient, Specimen specimen, PractitionerRole PerformerPractitionerRole, List<Observation> MemberObservationList = null)
    {      
      var Obs = new Observation();
      Obs.Id = System.Guid.NewGuid().ToFhirId();
      Obs.Status = ObservationStatus.Final;
      Obs.Category = new List<CodeableConcept>()
      {
        new CodeableConcept("http://terminology.hl7.org/CodeSystem/observation-category", "laboratory", "Laboratory")
      };
      Obs.Code = new CodeableConcept();
      Obs.Code.Coding = new List<Coding>();
      if (OBX.Field(3).Component(1).AsString != string.Empty)
      {
        if (OBX.Field(3).Component(3).AsString.ToUpper() == "LN")
        {
          Obs.Code.Coding.Add(new Coding(LoincFhirSystemUri, OBX.Field(3).Component(1).AsString, OBX.Field(3).Component(2).AsString));
        }
        else
        {
          Obs.Code.Coding.Add(new Coding($"http://{this.SendingFacility}/id/resultCodes", OBX.Field(3).Component(1).AsString, OBX.Field(3).Component(2).AsString));
        }
      }
      if (OBX.Field(3).Component(4).AsString != string.Empty)
      {
        if (OBX.Field(3).Component(6).AsString.ToUpper() == "LN")
        {
          Obs.Code.Coding.Add(new Coding(LoincFhirSystemUri, OBX.Field(3).Component(4).AsString, OBX.Field(3).Component(5).AsString));
        }
        else
        {
          Obs.Code.Coding.Add(new Coding($"http://{this.SendingFacility}/id/resultCodes", OBX.Field(3).Component(4).AsString, OBX.Field(3).Component(5).AsString));
        }
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
      switch (OBX.Field(8).AsString.ToUpper())
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

      if (OBX.Field(7).AsString != "")
      {
        Obs.ReferenceRange = new List<Observation.ReferenceRangeComponent>()
        {
          new Observation.ReferenceRangeComponent()
          {
            Text = OBX.Field(7).AsString
          }
        };
      }      

      //Add all member observations
      if(MemberObservationList != null && MemberObservationList.Count > 0)
      {
        Obs.HasMember = new List<ResourceReference>();
        foreach(var Observation in MemberObservationList)
        {
          Obs.HasMember.Add(new ResourceReference($"{Observation.ResourceType.GetLiteral()}/{Observation.Id}", Observation.Code.Coding[0].Display));
        }
      }

      //Value from OBX-5
      if (OBX.Field(2).AsString.ToUpper() == "SN" || OBX.Field(2).AsString.ToUpper() == "NM")
      {
        string ValueString = string.Empty;
        if (OBX.Field(2).AsString.ToUpper() == "SN")
        {
          ValueString = OBX.Field(5).Component(2).AsString;
        }
        else if (OBX.Field(2).AsString.ToUpper() == "NM")
        {
          ValueString = OBX.Field(5).AsString;
        }
        Quantity Qty = new Quantity();        
        if (Decimal.TryParse(ValueString, out decimal Value))
        {
          Qty.Value = Value;
        }
        else
        {
          throw new ApplicationException($"Unable to convert HL7 V2 OBX datatype SN or NM value to a decimal, the value was: {OBX.Field(5).AsString} ");
        }

        if (OBX.Field(2).AsString.ToUpper() == "SN")
        {
          switch (OBX.Field(5).Component(1).AsString)
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
        
        if (!OBX.Field(6).IsEmpty)
        {
          Qty.Unit = OBX.Field(6).Component(1).AsString;
        }
        Obs.Value = Qty;
      }

      if (OBX.Field(2).AsString.ToUpper() == "ST")
      {
        Obs.Value = new FhirString(OBX.Field(5).AsString);        
      }

      if (OBX.Field(2).AsString.ToUpper() == "NR")
      {
        var Range = new Hl7.Fhir.Model.Range();
        
        var LowSimpleQuantity = new SimpleQuantity();
        LowSimpleQuantity.Value = Convert.ToDecimal(OBX.Field(5).Component(1).AsString);
        Range.Low = LowSimpleQuantity;

        var HighSimpleQuantity = new SimpleQuantity();
        HighSimpleQuantity.Value = Convert.ToDecimal(OBX.Field(5).Component(2).AsString);
        Range.High = HighSimpleQuantity;

        if (!OBX.Field(6).IsEmpty)
        {
          Range.High.Unit = OBX.Field(6).Component(1).AsString;
          Range.Low.Unit = OBX.Field(6).Component(1).AsString;
        }

        Obs.Value = Range;
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

      }
      else if (Obs.Value is FhirString FhirString)
      {
        sb.Append($"  {Name}: {FhirString.Value}\n");        
      }

      sb.Append($"  </pre>\n");
      sb.Append("</div>");
      Obs.Text.Div = sb.ToString();

      return Obs;
    }
    private List<Observation> GetOBXSegments(Patient patient, Specimen specimen, PractitionerRole PerformerPractitionerRole)
    {      
      List<Observation> ObservationList = new List<Observation>();      
      List<ISegment> OBXList = HL7.SegmentList("OBX").ToList();
      Dictionary<int, ISegment> OBXIdDone = new Dictionary<int, ISegment>();

      //Check All OBX-1 are integers
      if (OBXList.Any(x => !x.Field(1).Convert.Integer.IsNumeric))
      {
        throw new ApplicationException($"OBX-1 must be an integer, found an OBX which was not a integer.");
      }

      //Remove the Display Segments from being created as FHIR Observations
      var DisplaySegs = OBXList.Where(x => x.Field(3).Component(3).AsString == "AUSPDI");
      foreach(var Display in DisplaySegs)
      {
        OBXIdDone.Add(Display.Field(1).Convert.Integer.Int32, Display);
      }
      
      foreach (var OBX in OBXList)
      {        
        if (!OBXIdDone.ContainsKey(OBX.Field(1).Convert.Integer.Int32))
        {                
          if (OBX.Field(4).AsString == "")
          {
            var NoSubIdOb = GetAtomicObservation(OBX, patient, specimen, PerformerPractitionerRole);
            ObservationList.Add(NoSubIdOb);
            TotalObservationList.Add(NoSubIdOb);
            OBXIdDone.Add(OBX.Field(1).Convert.Integer.Int32, OBX);            
          }
          else
          {
            string SubId = OBX.Field(4).AsString;
            if (!SubId.Contains('.') && OBX.Field(4).Convert.Integer.IsNumeric)
            {
              var ParentObservation = GetAtomicObservation(OBX, patient, specimen, PerformerPractitionerRole);
              OBXIdDone.Add(OBX.Field(1).Convert.Integer.Int32, OBX);
              TotalObservationList.Add(ParentObservation);

              var NextList = OBXList.Where(x => x.Field(4).AsString == $"{OBX.Field(4).AsString}.1");
              List<Observation> ChildrenObservationList = new List<Observation>();
              foreach(var SameSubIdObs in NextList)
              {
                if (!OBXIdDone.ContainsKey(SameSubIdObs.Field(1).Convert.Integer.Int32))
                {
                  var ChildObs = GetAtomicObservation(SameSubIdObs, patient, specimen, PerformerPractitionerRole);
                  ChildrenObservationList.Add(ChildObs);
                  TotalObservationList.Add(ChildObs);
                  OBXIdDone.Add(SameSubIdObs.Field(1).Convert.Integer.Int32, OBX);
                }
              }
              if (ChildrenObservationList.Count > 0)
              {
                ParentObservation.HasMember = new List<ResourceReference>();
                foreach(var item in ChildrenObservationList)
                {
                  ParentObservation.HasMember.Add(new ResourceReference($"{item.ResourceType.GetLiteral()}/{item.Id}", item.Code.Coding[0].Display));
                }
              }
              ObservationList.Add(ParentObservation);              
            }
            else if (!SubId.Contains('.'))
            {
              foreach (var SubOBX in OBXList.Where(x => x.Field(4).AsString == SubId))
              {
                var NonDotSubId = GetAtomicObservation(SubOBX, patient, specimen, PerformerPractitionerRole);
                ObservationList.Add(NonDotSubId);
                TotalObservationList.Add(NonDotSubId);
                OBXIdDone.Add(OBX.Field(1).Convert.Integer.Int32, OBX);
              }
            }
          }              
        }        
      }
      return ObservationList;
    }



    
  }
}
