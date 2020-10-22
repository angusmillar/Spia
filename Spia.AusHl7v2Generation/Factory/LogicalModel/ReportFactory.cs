using Spia.AusHl7v2Generation.Model.Logical;
using System;
using System.Collections.Generic;
using System.Text;

namespace Spia.AusHl7v2Generation.Factory.LogicalModel
{
  public static class ReportFactory
  {
    public static Report GetChlamydiaReport()
    {
      var ReportId = new EntityIdentifier("1978881874", LabInfo.FacilityCode) { UniversalId = LabInfo.NATAAccNumber, UniversalIdType = "AUSNATA" };
      var CollectionDateTime = new DateTimeOffset(2019, 09, 25, 07, 20, 00, TimeSpan.FromHours(10));
      var SpecimenReceivedDateTime = new DateTimeOffset(2019, 09, 25, 10, 24, 00, TimeSpan.FromHours(10));
      var ReportReleaseDateTime = new DateTimeOffset(2019, 09, 25, 07, 20, 00, TimeSpan.FromHours(10));
      var ReportType = new CodedElement(null, null)
      {
        International = new Coded("398452009", "Chlamydia trachomatis nucleic acid assay", "SCT"),
        Local = new Coded("CHY", "Chlamydia trachomatis nucleic acid", $"NATA{LabInfo.NATAAccNumber}")
      };
      var ReportStatus = 'F';
      var DiagnosticServiceSectionId = "SR";
      var ReportingPathologist = new Provider(new Name("Pathologist", "Bella") { Title = "Dr" });
      ReportingPathologist.IdentifierList.Add(new Identifier("BPATH") { AssigingAuthority = $"NATA{LabInfo.NATAAccNumber}" });

      var SendingApplication = new HierarchicDesignator(LabInfo.SystemCode);
      var SendingFacility = new HierarchicDesignator(LabInfo.FacilityCode) { UniversalId = LabInfo.NATAAccNumber, UniversalIdType = "AUSNATA" };

      Report Report = new Report(
        ReportId,
        CollectionDateTime,
        SpecimenReceivedDateTime,
        ReportReleaseDateTime,
        ReportType,
        ReportStatus,
        DiagnosticServiceSectionId,
        ReportingPathologist,
        SendingApplication,
        SendingFacility);

      return Report;
    }
    public static Report GetColorectalCancerReport()
    {
      throw new NotImplementedException();
    }
    public static Report GetEUCReport()
    {
      var ReportId = new EntityIdentifier("1978881822", LabInfo.FacilityCode) { UniversalId = LabInfo.NATAAccNumber, UniversalIdType = "AUSNATA" };
      var CollectionDateTime = new DateTimeOffset(2019, 12, 02, 07, 50, 00, TimeSpan.FromHours(10));
      var SpecimenReceivedDateTime = new DateTimeOffset(2019, 12, 02, 11, 00, 00, TimeSpan.FromHours(10));
      var ReportReleaseDateTime = new DateTimeOffset(2019, 12, 02, 07, 50, 00, TimeSpan.FromHours(10));
      var ReportType = new CodedElement(null, null)
      {
        International = new Coded("444164000", "Urea, electrolytes and creatinine measurement", "SCT"),
        Local = new Coded("EUC", "Electrolytes Urea Creatinine", $"NATA{LabInfo.NATAAccNumber}")
      };
      var ReportStatus = 'F';
      var DiagnosticServiceSectionId = "CH";
      var ReportingPathologist = new Provider(new Name("Pathologist", "Mario") { Title = "Dr" });
      ReportingPathologist.IdentifierList.Add(new Identifier("MARP") { AssigingAuthority = $"NATA{LabInfo.NATAAccNumber}" });

      var SendingApplication = new HierarchicDesignator(LabInfo.SystemCode);
      var SendingFacility = new HierarchicDesignator(LabInfo.FacilityCode) { UniversalId = LabInfo.NATAAccNumber, UniversalIdType = "AUSNATA" };


      Report Report = new Report(
        ReportId,
        CollectionDateTime,
        SpecimenReceivedDateTime,
        ReportReleaseDateTime,
        ReportType,
        ReportStatus,
        DiagnosticServiceSectionId,
        ReportingPathologist,
        SendingApplication,
        SendingFacility);

      return Report;
    }
    public static Report GetFBC()
    {
      var ReportId = new EntityIdentifier("1978881888", LabInfo.FacilityCode) { UniversalId = LabInfo.NATAAccNumber, UniversalIdType = "AUSNATA" };
      var CollectionDateTime = new DateTimeOffset(2019, 08, 02, 10, 40, 00, TimeSpan.FromHours(10));
      var SpecimenReceivedDateTime = new DateTimeOffset(2019, 08, 02, 12, 00, 00, TimeSpan.FromHours(10));
      var ReportReleaseDateTime = new DateTimeOffset(2019, 08, 02, 12, 52, 00, TimeSpan.FromHours(10));
      var ReportType = new CodedElement(null, null)
      {
        International = new Coded("26604007", "Full blood count", "SCT"),
        Local = new Coded("FBC", "Full blood count", $"NATA{LabInfo.NATAAccNumber}")
      };
      var ReportStatus = 'F';
      var DiagnosticServiceSectionId = "HM";
      var ReportingPathologist = new Provider(new Name("Pathologist", "Marissa") { Title = "Dr" });
      ReportingPathologist.IdentifierList.Add(new Identifier("MP") { AssigingAuthority = $"NATA{LabInfo.NATAAccNumber}" });

      var SendingApplication = new HierarchicDesignator(LabInfo.SystemCode);
      var SendingFacility = new HierarchicDesignator(LabInfo.FacilityCode) { UniversalId = LabInfo.NATAAccNumber, UniversalIdType = "AUSNATA" };

      Report Report = new Report(
        ReportId,
        CollectionDateTime,
        SpecimenReceivedDateTime,
        ReportReleaseDateTime,
        ReportType,
        ReportStatus,
        DiagnosticServiceSectionId,
        ReportingPathologist,
        SendingApplication,
        SendingFacility);

      return Report;
    }
    public static Report GetHFEReport()
    {
      var ReportId = new EntityIdentifier("1978881777", LabInfo.FacilityCode) { UniversalId = LabInfo.NATAAccNumber, UniversalIdType = "AUSNATA" };
      var CollectionDateTime = new DateTimeOffset(2019, 11, 29, 07, 40, 00, TimeSpan.FromHours(10));
      var SpecimenReceivedDateTime = new DateTimeOffset(2019, 11, 29, 12, 03, 00, TimeSpan.FromHours(10));
      var ReportReleaseDateTime = new DateTimeOffset(2019, 11, 29, 16, 38, 00, TimeSpan.FromHours(10));
      var ReportType = new CodedElement(null, null)
      {
        International = new Coded("401085002", "Haemochromatosis gene screening test", "SCT"),
        Local = new Coded("HFE", "Haemochromatosis Genotyping ", $"NATA{LabInfo.NATAAccNumber}")
      };
      var ReportStatus = 'F';
      var DiagnosticServiceSectionId = "GE";
      var ReportingPathologist = new Provider(new Name("Pathologist", "Kondo") { Middle = "Uhu", Title = "Dr" });
      ReportingPathologist.IdentifierList.Add(new Identifier("KUP") { AssigingAuthority = $"NATA{LabInfo.NATAAccNumber}" });

      var SendingApplication = new HierarchicDesignator(LabInfo.SystemCode);
      var SendingFacility = new HierarchicDesignator(LabInfo.FacilityCode) { UniversalId = LabInfo.NATAAccNumber, UniversalIdType = "AUSNATA" };

      Report Report = new Report(
        ReportId,
        CollectionDateTime,
        SpecimenReceivedDateTime,
        ReportReleaseDateTime,
        ReportType,
        ReportStatus,
        DiagnosticServiceSectionId,
        ReportingPathologist,
        SendingApplication,
        SendingFacility);

      return Report;
    }
    public static Report GetHepBsAbReport()
    {
      var ReportId = new EntityIdentifier("1978881874", LabInfo.FacilityCode) { UniversalId = LabInfo.NATAAccNumber, UniversalIdType = "AUSNATA" };
      var CollectionDateTime = new DateTimeOffset(2019, 09, 25, 07, 20, 00, TimeSpan.FromHours(10));
      var SpecimenReceivedDateTime = new DateTimeOffset(2019, 09, 25, 10, 24, 00, TimeSpan.FromHours(10));
      var ReportReleaseDateTime = new DateTimeOffset(2019, 09, 25, 07, 20, 00, TimeSpan.FromHours(10));
      var ReportType = new CodedElement(null, null)
      {
        International = new Coded("315130004", "Hepatitis B surface antibody level", "SCT"),
        Local = new Coded("HepBsAb", "Hepatitis B surface Ab", $"NATA{LabInfo.NATAAccNumber}")
      };
      var ReportStatus = 'F';
      var DiagnosticServiceSectionId = "SR";
      var ReportingPathologist = new Provider(new Name("Virologist", "Stanley ") { Title = "Dr" });
      ReportingPathologist.IdentifierList.Add(new Identifier("SVIRO") { AssigingAuthority = $"NATA{LabInfo.NATAAccNumber}" });

      var SendingApplication = new HierarchicDesignator(LabInfo.SystemCode);
      var SendingFacility = new HierarchicDesignator(LabInfo.FacilityCode) { UniversalId = LabInfo.NATAAccNumber, UniversalIdType = "AUSNATA" };

      Report Report = new Report(
        ReportId,
        CollectionDateTime,
        SpecimenReceivedDateTime,
        ReportReleaseDateTime,
        ReportType,
        ReportStatus,
        DiagnosticServiceSectionId,
        ReportingPathologist,
        SendingApplication,
        SendingFacility);

      return Report;
    }
    public static Report GetmmunoglobulinE()
    {
      var ReportId = new EntityIdentifier("1978881822", LabInfo.FacilityCode) { UniversalId = LabInfo.NATAAccNumber, UniversalIdType = "AUSNATA" };
      var CollectionDateTime = new DateTimeOffset(2019, 12, 02, 07, 20, 00, TimeSpan.FromHours(10));
      var SpecimenReceivedDateTime = new DateTimeOffset(2019, 12, 02, 11, 04, 00, TimeSpan.FromHours(10));
      var ReportReleaseDateTime = new DateTimeOffset(2019, 12, 02, 07, 50, 00, TimeSpan.FromHours(10));
      var ReportType = new CodedElement(null, null)
      {
        International = new Coded("41960005", "IgE measurement", "SCT"),
        Local = new Coded("ImmunoIgG", "Immunoglobulin E", $"NATA{LabInfo.NATAAccNumber}")
      };
      var ReportStatus = 'F';
      var DiagnosticServiceSectionId = "IMM";
      var ReportingPathologist = new Provider(new Name("Immunologist", "Bertram") { Title = "Dr" });
      ReportingPathologist.IdentifierList.Add(new Identifier("BI") { AssigingAuthority = $"NATA{LabInfo.NATAAccNumber}" });

      var SendingApplication = new HierarchicDesignator(LabInfo.SystemCode);
      var SendingFacility = new HierarchicDesignator(LabInfo.FacilityCode) { UniversalId = LabInfo.NATAAccNumber, UniversalIdType = "AUSNATA" };

      Report Report = new Report(
        ReportId,
        CollectionDateTime,
        SpecimenReceivedDateTime,
        ReportReleaseDateTime,
        ReportType,
        ReportStatus,
        DiagnosticServiceSectionId,
        ReportingPathologist,
        SendingApplication,
        SendingFacility);

      return Report;
    }
    public static Report GetINR()
    {
      var ReportId = new EntityIdentifier("1878881888", LabInfo.FacilityCode) { UniversalId = LabInfo.NATAAccNumber, UniversalIdType = "AUSNATA" };
      var CollectionDateTime = new DateTimeOffset(2019, 08, 02, 11, 30, 00, TimeSpan.FromHours(10));
      var SpecimenReceivedDateTime = new DateTimeOffset(2019, 08, 02, 12, 00, 00, TimeSpan.FromHours(10));
      var ReportReleaseDateTime = new DateTimeOffset(2019, 08, 02, 14, 32, 00, TimeSpan.FromHours(10));
      var ReportType = new CodedElement(null, null)
      {
        International = new Coded("440685005", "Calculation of international normalised ratio", "SCT"),
        Local = new Coded("INR", "INR", $"NATA{LabInfo.NATAAccNumber}")
      };
      var ReportStatus = 'F';
      var DiagnosticServiceSectionId = "HM";
      var ReportingPathologist = new Provider(new Name("Pathologist", "Marissa") { Title = "Dr" });
      ReportingPathologist.IdentifierList.Add(new Identifier("MP") { AssigingAuthority = $"NATA{LabInfo.NATAAccNumber}" });

      var SendingApplication = new HierarchicDesignator(LabInfo.SystemCode);
      var SendingFacility = new HierarchicDesignator(LabInfo.FacilityCode) { UniversalId = LabInfo.NATAAccNumber, UniversalIdType = "AUSNATA" };

      Report Report = new Report(
        ReportId,
        CollectionDateTime,
        SpecimenReceivedDateTime,
        ReportReleaseDateTime,
        ReportType,
        ReportStatus,
        DiagnosticServiceSectionId,
        ReportingPathologist,
        SendingApplication,
        SendingFacility);

      return Report;
    }
    public static Report GetKaryotyping()
    {
      var ReportId = new EntityIdentifier("1978881774", LabInfo.FacilityCode) { UniversalId = LabInfo.NATAAccNumber, UniversalIdType = "AUSNATA" };
      var CollectionDateTime = new DateTimeOffset(2019, 11, 29, 10, 40, 00, TimeSpan.FromHours(10));
      var SpecimenReceivedDateTime = new DateTimeOffset(2019, 11, 29, 12, 05, 00, TimeSpan.FromHours(10));
      var ReportReleaseDateTime = new DateTimeOffset(2019, 11, 30, 12, 55, 00, TimeSpan.FromHours(10));
      var ReportType = new CodedElement(null, null)
      {
        International = new Coded("1308381000168103", "Whole blood cytogenetic analysis", "SCT"),
        Local = new Coded("KARYO", "Karyotype analysis", $"NATA{LabInfo.NATAAccNumber}")
      };
      var ReportStatus = 'F';
      var DiagnosticServiceSectionId = "HM";
      var ReportingPathologist = new Provider(new Name("Pathologist", "Kondo") { Middle = "Uhu", Title = "Dr" });
      ReportingPathologist.IdentifierList.Add(new Identifier("KUP") { AssigingAuthority = $"NATA{LabInfo.NATAAccNumber}" });

      var SendingApplication = new HierarchicDesignator(LabInfo.SystemCode);
      var SendingFacility = new HierarchicDesignator(LabInfo.FacilityCode) { UniversalId = LabInfo.NATAAccNumber, UniversalIdType = "AUSNATA" };

      Report Report = new Report(
        ReportId,
        CollectionDateTime,
        SpecimenReceivedDateTime,
        ReportReleaseDateTime,
        ReportType,
        ReportStatus,
        DiagnosticServiceSectionId,
        ReportingPathologist,
        SendingApplication,
        SendingFacility);

      return Report;
    }
    public static Report GetLipids()
    {
      var ReportId = new EntityIdentifier("1978881822", LabInfo.FacilityCode) { UniversalId = LabInfo.NATAAccNumber, UniversalIdType = "AUSNATA" };
      var CollectionDateTime = new DateTimeOffset(2019, 12, 04, 07, 36, 00, TimeSpan.FromHours(10));
      var SpecimenReceivedDateTime = new DateTimeOffset(2019, 12, 04, 10, 14, 00, TimeSpan.FromHours(10));
      var ReportReleaseDateTime = new DateTimeOffset(2019, 12, 04, 14, 32, 00, TimeSpan.FromHours(10));
      var ReportType = new CodedElement(null, null)
      {
        International = new Coded("252150008", "Fasting lipid profile", "SCT"),
        Local = new Coded("LIPIDS", "Lipids", $"NATA{LabInfo.NATAAccNumber}")
      };
      var ReportStatus = 'F';
      var DiagnosticServiceSectionId = "CH";
      var ReportingPathologist = new Provider(new Name("Pathologist", "Manuel") { Title = "Dr", Middle = "del" });
      ReportingPathologist.IdentifierList.Add(new Identifier("DPM") { AssigingAuthority = $"NATA{LabInfo.NATAAccNumber}" });

      var SendingApplication = new HierarchicDesignator(LabInfo.SystemCode);
      var SendingFacility = new HierarchicDesignator(LabInfo.FacilityCode) { UniversalId = LabInfo.NATAAccNumber, UniversalIdType = "AUSNATA" };

      Report Report = new Report(
        ReportId,
        CollectionDateTime,
        SpecimenReceivedDateTime,
        ReportReleaseDateTime,
        ReportType,
        ReportStatus,
        DiagnosticServiceSectionId,
        ReportingPathologist,
        SendingApplication,
        SendingFacility);

      return Report;
    }
    public static Report GetMSUReport()
    {
      var ReportId = new EntityIdentifier("2001277757", LabInfo.FacilityCode) { UniversalId = LabInfo.NATAAccNumber, UniversalIdType = "AUSNATA" };
      var CollectionDateTime = new DateTimeOffset(2020, 01, 22, 07, 40, 00, TimeSpan.FromHours(10));
      var SpecimenReceivedDateTime = new DateTimeOffset(2020, 01, 22, 10, 10, 00, TimeSpan.FromHours(10));
      var ReportReleaseDateTime = new DateTimeOffset(2020, 01, 24, 14, 40, 00, TimeSpan.FromHours(10));
      var ReportType = new CodedElement(null, null)
      {
        International = new Coded("401324008", "Urine microscopy, culture and sensitivities", "SCT"),
        Local = new Coded("UMCS", "MCS urine", $"NATA{LabInfo.NATAAccNumber}")
      };
      var ReportStatus = 'F';
      var DiagnosticServiceSectionId = "MB";
      var ReportingPathologist = new Provider(new Name("Pathologist", "Evanna") { Title = "Dr" });
      ReportingPathologist.IdentifierList.Add(new Identifier("EP") { AssigingAuthority = $"NATA{LabInfo.NATAAccNumber}" });

      var SendingApplication = new HierarchicDesignator(LabInfo.SystemCode);
      var SendingFacility = new HierarchicDesignator(LabInfo.FacilityCode) { UniversalId = LabInfo.NATAAccNumber, UniversalIdType = "AUSNATA" };

      Report Report = new Report(
        ReportId,
        CollectionDateTime,
        SpecimenReceivedDateTime,
        ReportReleaseDateTime,
        ReportType,
        ReportStatus,
        DiagnosticServiceSectionId,
        ReportingPathologist,
        SendingApplication,
        SendingFacility);

      return Report;
    }
    public static Report GetProteinElectrophoresis()
    {
      var ReportId = new EntityIdentifier("19118881777", LabInfo.FacilityCode) { UniversalId = LabInfo.NATAAccNumber, UniversalIdType = "AUSNATA" };
      var CollectionDateTime = new DateTimeOffset(2019, 12, 02, 07, 50, 00, TimeSpan.FromHours(10));
      var SpecimenReceivedDateTime = new DateTimeOffset(2019, 12, 02, 11, 04, 00, TimeSpan.FromHours(10));
      var ReportReleaseDateTime = new DateTimeOffset(2019, 12, 02, 15, 50, 00, TimeSpan.FromHours(10));
      var ReportType = new CodedElement(null, null)
      {
        International = new Coded("4903000", "Serum protein electrophoresis", "SCT"),
        Local = new Coded("PROTELECT", "Protein electrophoresis", $"NATA{LabInfo.NATAAccNumber}")
      };
      var ReportStatus = 'F';
      var DiagnosticServiceSectionId = "IMM";
      var ReportingPathologist = new Provider(new Name("Immunologist", "Bertram") { Title = "Dr" });
      ReportingPathologist.IdentifierList.Add(new Identifier("BI") { AssigingAuthority = $"NATA{LabInfo.NATAAccNumber}" });

      var SendingApplication = new HierarchicDesignator(LabInfo.SystemCode);
      var SendingFacility = new HierarchicDesignator(LabInfo.FacilityCode) { UniversalId = LabInfo.NATAAccNumber, UniversalIdType = "AUSNATA" };

      Report Report = new Report(
        ReportId,
        CollectionDateTime,
        SpecimenReceivedDateTime,
        ReportReleaseDateTime,
        ReportType,
        ReportStatus,
        DiagnosticServiceSectionId,
        ReportingPathologist,
        SendingApplication,
        SendingFacility);

      return Report;
    }
    public static Report GetBloodGasArterial()
    {
      var ReportId = new EntityIdentifier("1978881828", LabInfo.FacilityCode) { UniversalId = LabInfo.NATAAccNumber, UniversalIdType = "AUSNATA" };
      var CollectionDateTime = new DateTimeOffset(2019, 11, 09, 07, 40, 00, TimeSpan.FromHours(10));
      var SpecimenReceivedDateTime = new DateTimeOffset(2019, 12, 09, 11, 00, 00, TimeSpan.FromHours(10));
      var ReportReleaseDateTime = new DateTimeOffset(2019, 11, 09, 07, 50, 00, TimeSpan.FromHours(10));
      var ReportType = new CodedElement(null, null)
      {
        International = new Coded("91308007", "Blood gases, arterial measurement", "SCT"),
        Local = new Coded("BGASA", "Blood gas arterial", $"NATA{LabInfo.NATAAccNumber}")
      };
      var ReportStatus = 'F';
      var DiagnosticServiceSectionId = "CH";

      var ReportingPathologist = new Provider(new Name("Pathologist", "Mario") { Title = "Dr" });
      ReportingPathologist.IdentifierList.Add(new Identifier("MARP") { AssigingAuthority = $"NATA{LabInfo.NATAAccNumber}" });

      var SendingApplication = new HierarchicDesignator(LabInfo.SystemCode);
      var SendingFacility = new HierarchicDesignator(LabInfo.FacilityCode) { UniversalId = LabInfo.NATAAccNumber, UniversalIdType = "AUSNATA" };

      Report Report = new Report(
        ReportId,
        CollectionDateTime,
        SpecimenReceivedDateTime,
        ReportReleaseDateTime,
        ReportType,
        ReportStatus,
        DiagnosticServiceSectionId,
        ReportingPathologist,
        SendingApplication,
        SendingFacility);

      return Report;
    }    
    public static Report GetSARSCoV2NAT()
    {
      var ReportId = new EntityIdentifier("2078881879", LabInfo.FacilityCode) { UniversalId = LabInfo.NATAAccNumber, UniversalIdType = "AUSNATA" };
      
      var CollectionDateTime = new DateTimeOffset(2020, 08, 20, 11, 20, 00, TimeSpan.FromHours(10));      
      var SpecimenReceivedDateTime = new DateTimeOffset(2020, 08, 20, 13, 24, 00, TimeSpan.FromHours(10));      
      var ReportReleaseDateTime = new DateTimeOffset(2020, 08, 20, 20, 28, 00, TimeSpan.FromHours(10));
      var ReportType = new CodedElement(null, null)
      {
        International = new Coded("1445431000168101", "COVID-19 nucleic acid assay", "SCT"),
        Local = new Coded("COVID2", "SARS-CoV-2 nucleic acid", $"NATA{LabInfo.NATAAccNumber}")
      };
      var ReportStatus = 'F';
      //ToDo: 'Molecular Biology' is not in the code set, currently using 'MB:Microbiology' as per O&O Working group advice
      var DiagnosticServiceSectionId = "MB";
      
      var ReportingPathologist = new Provider(new Name("Pathologist", "Bella") { Title = "Dr" });
      ReportingPathologist.IdentifierList.Add(new Identifier("BPATH") { AssigingAuthority = $"NATA{LabInfo.NATAAccNumber}" });

      var SendingApplication = new HierarchicDesignator(LabInfo.SystemCode);
      var SendingFacility = new HierarchicDesignator(LabInfo.FacilityCode) { UniversalId = LabInfo.NATAAccNumber, UniversalIdType = "AUSNATA" };

      Report Report = new Report(
        ReportId,
        CollectionDateTime,
        SpecimenReceivedDateTime,
        ReportReleaseDateTime,
        ReportType,
        ReportStatus,
        DiagnosticServiceSectionId,
        ReportingPathologist,
        SendingApplication,
        SendingFacility);

      return Report;
    }
    public static Report GetSARSCoV2Serology()
    {
      var ReportId = new EntityIdentifier("1978881814", LabInfo.FacilityCode) { UniversalId = LabInfo.NATAAccNumber, UniversalIdType = "AUSNATA" };
      
      var CollectionDateTime = new DateTimeOffset(2020, 7, 25, 08, 20, 00, TimeSpan.FromHours(10));      
      var SpecimenReceivedDateTime = new DateTimeOffset(2020, 7, 25, 10, 02, 00, TimeSpan.FromHours(10));      
      var ReportReleaseDateTime = new DateTimeOffset(2020, 07, 25, 19, 20, 00, TimeSpan.FromHours(10));
      var ReportType = new CodedElement(null, null)
      {
        International = new Coded("1454651000168108", "COVID-19 serology", "SCT"),
        Local = new Coded("COVID2SER", "SARS-CoV-2 Serology", $"NATA{LabInfo.NATAAccNumber}")
      };
      var ReportStatus = 'F';
      var DiagnosticServiceSectionId = "SR";

      var ReportingPathologist = new Provider(new Name("Pathologist", "Bella") { Title = "Dr" });
      ReportingPathologist.IdentifierList.Add(new Identifier("BPATH") { AssigingAuthority = $"NATA{LabInfo.NATAAccNumber}" });

      var SendingApplication = new HierarchicDesignator(LabInfo.SystemCode);
      var SendingFacility = new HierarchicDesignator(LabInfo.FacilityCode) { UniversalId = LabInfo.NATAAccNumber, UniversalIdType = "AUSNATA" };

      Report Report = new Report(
        ReportId,
        CollectionDateTime,
        SpecimenReceivedDateTime,
        ReportReleaseDateTime,
        ReportType,
        ReportStatus,
        DiagnosticServiceSectionId,
        ReportingPathologist,
        SendingApplication,
        SendingFacility);

      return Report;
    }
  }
}
