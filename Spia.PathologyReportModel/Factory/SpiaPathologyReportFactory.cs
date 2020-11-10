using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spia.PathologyReportModel.Model;

namespace Spia.PathologyReportModel.Factory
{
  public class SpiaPathologyReportFactory
  {
    private SpiaPatientFactory PatientFactory;
    private SpiaProviderFactory ProviderFactory;
    private SpiaLaboratoryFactory LaboratoryFactory;
    public SpiaPathologyReportFactory()
    {
      PatientFactory = new SpiaPatientFactory();
      ProviderFactory = new SpiaProviderFactory();
      LaboratoryFactory = new SpiaLaboratoryFactory();
    }

    public List<PathologyReportContainer> GetAll()
    {
      return new List<PathologyReportContainer>()
      {
        GetBloodGasArterial(),
        GetChlamydia(),
        GetEUC(),
        GetFBC(),
        GetHepBsAb(),
        GetHFE(),
        GetImmunoglobulinE(),
        GetINR(),
        GetKaryotyping(),
        GetLipids(),
        GetMSU(),
        GetProteinElectrophoresis(),
        GetSARSCoV2NAT(),
        GetSARSCoV2Serology()
      };
    }
    public PathologyReportContainer GetChlamydia()
    {
      return new PathologyReportContainer()
      {
        PathologyReport = new PathologyReport()
        {
          PerformingLaboratory = LaboratoryFactory.GetPITUSLaboratory(),
          Patient = PatientFactory.GetPatient(PatientType.GeorginaROSSLAND),
          Request = new Request()
          {
            RequestedDate = new DateTimeOffset(2019, 09, 23, 00, 00, 00, TimeSpan.FromHours(10)),
            OrderNumber = "00000001",
            RequestingFacility = new Organisation()
            {
              Name = "Antenatal Clinic Sunrise Hospital",
              Identifier = new Identifier()
              {
                Value = "143569C9-8AFC-4BBD-A663-95079AE10B57",
                Type = IdentifierType.GUID
              }
            },
            RequestingApplication = "Best Practice 1.8.5.743",
            RequestingProvider = ProviderFactory.GetTrishFamilyDr(MedicareProviderNumber: "8203015Y"),
            ClinicalNotes = "First trimester antenatal screen, ~ 10 weeks pregnant (G1P0)",
            CallBackPhoneNumber = null,
            CopyToList = new List<Provider>()
            {
              ProviderFactory.GetBiancaMidwife()
            }
          },
          PdfFileName = "SPIA Exemplar Report Chlamydia trachomatis nucleic acid v1.6.pdf",
          ReportList = new List<Report>()
          {
            new Report()
            {              
              ReportId = "1978881874",              
              CollectionDateTime = new DateTimeOffset(2019, 09, 25, 07, 20, 00, TimeSpan.FromHours(10)),
              SpecimenReceivedDateTime = new DateTimeOffset(2019, 09, 25, 10, 24, 00, TimeSpan.FromHours(10)),
              ReportReleaseDateTime = new DateTimeOffset(2019, 09, 25, 07, 20, 00, TimeSpan.FromHours(10)),
              ReportType = new ReportType()
              {
                Local = new Code() { Term = "CHY", Description = "Chlamydia trachomatis nucleic acid" },
                Snomed = new Code() { Term = "398452009", Description = "Chlamydia trachomatis nucleic acid assay" }
              },
              ReportStatus =  ResultStatusType.Final,
              Department = DiagnosticService.Serology,
              ReportingPathologist = ProviderFactory.GetBellaPathologist(),              
              Panel = new Panel()
              {                
                ResultList = new List<Result>()
                {
                  new Result()
                  {
                    Type = new ResultType()
                    {
                      Local = new Code()
                      {
                        Term = "CHLY",
                        Description = "Chlamydia trachomatis DNA"
                      },
                      Lonic = new Code()
                      {
                        Term = "21613-5",
                        Description = "Chlamydia trachomatis DNA"
                      }
                    },
                    DataType = "ST",
                    Value = "Negative",
                    Units = null,
                    ReferenceRange = null,
                    AbnormalFlag = "N",
                    ObservationDateTime = new DateTimeOffset(2019, 09, 25, 07, 20, 00, TimeSpan.FromHours(10)).Subtract(TimeSpan.FromMinutes(5)),
                    Status = ResultStatusType.Final,
                    ChildResultList = null
                  }
                }
              }
            }
          }
        }
      };     
    }
    public PathologyReportContainer GetEUC()
    {
      return new PathologyReportContainer()
      {
        PathologyReport = new PathologyReport()
        {
          PerformingLaboratory = LaboratoryFactory.GetPITUSLaboratory(),
          Patient = PatientFactory.GetPatient(PatientType.GlennFERNIE),
          Request = new Request()
          {
            RequestedDate = new DateTimeOffset(2019, 09, 23, 00, 00, 00, TimeSpan.FromHours(10)),
            OrderNumber = "00000002",
            RequestingFacility = new Organisation()
            {
              Name = "Dermatology Clinic",
              Identifier = new Identifier()
              {
                Value = "D101F20B-1453-47A1-AD3F-A2845964A84E",
                Type = IdentifierType.GUID
              }
            },
            RequestingApplication = "Best Practice 1.8.5.743",
            RequestingProvider = ProviderFactory.GetTrishFamilyDr(MedicareProviderNumber: "9885728J"),
            ClinicalNotes = "Recent fungal infection R toes",
            CallBackPhoneNumber = "07302308594",
            CopyToList = new List<Provider>()
            {
              ProviderFactory.GetDermatologyClinic(),
              ProviderFactory.GetBrendanDermatologist()
            }
          },
          PdfFileName = "SPIA Exemplar Report Electrolytes Urea Creatinine v1.5.pdf",
          ReportList = new List<Report>()
          {
            new Report()
            {              
              ReportId = "1978881822",              
              CollectionDateTime = new DateTimeOffset(2019, 12, 02, 07, 50, 00, TimeSpan.FromHours(10)),
              SpecimenReceivedDateTime = new DateTimeOffset(2019, 12, 02, 11, 00, 00, TimeSpan.FromHours(10)),
              ReportReleaseDateTime = new DateTimeOffset(2019, 12, 02, 07, 50, 00, TimeSpan.FromHours(10)),
              ReportType = new ReportType()
              {
                //As of meeting on the 10/11/2020 @ 2:00pm, chnaged this to just Biochemistry - General
                Local = new Code() { Term = "BG", Description = "Biochemistry - General" },
                //Local = new Code() { Term = "EUC", Description = "Electrolytes Urea Creatinine" },
                Snomed = null,
                //Snomed = new Code() { Term = "444164000", Description = "Urea, electrolytes and creatinine measurement" }
              },
              ReportStatus =  ResultStatusType.Final,
              Department = DiagnosticService.Chemistry,
              ReportingPathologist = ProviderFactory.GetMarioPathologist(),              
              Panel = new Panel()
              {                
                ResultList = new List<Result>()
                {
                  new Result()
                  {
                    Type = new ResultType()
                    {
                      Local = new Code()
                      {
                        Term = "Na",
                        Description = "Sodium"
                      },
                      Lonic = new Code()
                      {
                        Term = "2951-2",
                        Description = "Sodium"
                      }
                    },
                    DataType = "NM",
                    Value = "136",
                    Units = "mmol/L",
                    ReferenceRange = "135-145",
                    AbnormalFlag = "N",
                    ObservationDateTime = new DateTimeOffset(2019, 12, 02, 07, 50, 00, TimeSpan.FromHours(10)).Subtract(TimeSpan.FromMinutes(5)),
                    Status = ResultStatusType.Final,
                    ChildResultList = null
                  },
                  new Result()
                  {
                    Type = new ResultType()
                    {
                      Local = new Code()
                      {
                        Term = "K",
                        Description = "Potassium"
                      },
                      Lonic = new Code()
                      {
                        Term = "2823-3",
                        Description = "Potassium"
                      }
                    },
                    DataType = "NM",
                    Value = "5.2",
                    Units = "mmol/L",
                    ReferenceRange = "3.5-5.2",
                    AbnormalFlag = "N",
                    ObservationDateTime = new DateTimeOffset(2019, 12, 02, 07, 50, 00, TimeSpan.FromHours(10)).Subtract(TimeSpan.FromMinutes(5)),
                    Status = ResultStatusType.Final,
                    ChildResultList = null
                  },
                  new Result()
                  {
                    Type = new ResultType()
                    {
                      Local = new Code()
                      {
                        Term = "Cl",
                        Description = "Chloride"
                      },
                      Lonic = new Code()
                      {
                        Term = "2075-0",
                        Description = "Chloride"
                      }
                    },
                    DataType = "NM",
                    Value = "96",
                    Units = "mmol/L",
                    ReferenceRange = "95-110",
                    AbnormalFlag = "N",
                    ObservationDateTime = new DateTimeOffset(2019, 12, 02, 07, 50, 00, TimeSpan.FromHours(10)).Subtract(TimeSpan.FromMinutes(5)),
                    Status = ResultStatusType.Final,
                    ChildResultList = null
                  },
                  new Result()
                  {
                    Type = new ResultType()
                    {
                      Local = new Code()
                      {
                        Term = "BICARB",
                        Description = "Bicarbonate"
                      },
                      Lonic = new Code()
                      {
                        Term = "1963-8",
                        Description = "Bicarbonate"
                      }
                    },
                    DataType = "NM",
                    Value = "27",
                    Units = "mmol/L",
                    ReferenceRange = "22-32",
                    AbnormalFlag = "N",
                    ObservationDateTime = new DateTimeOffset(2019, 12, 02, 07, 50, 00, TimeSpan.FromHours(10)).Subtract(TimeSpan.FromMinutes(5)),
                    Status = ResultStatusType.Final,
                    ChildResultList = null
                  },
                  new Result()
                  {
                    Type = new ResultType()
                    {
                      Local = new Code()
                      {
                        Term = "UREA",
                        Description = "Urea"
                      },
                      Lonic = new Code()
                      {
                        Term = "22664-7",
                        Description = "Urea"
                      }
                    },
                    DataType = "NM",
                    Value = "5.7",
                    Units = "mmol/L",
                    ReferenceRange = "3.0-8.5",
                    AbnormalFlag = "N",
                    ObservationDateTime = new DateTimeOffset(2019, 12, 02, 07, 50, 00, TimeSpan.FromHours(10)).Subtract(TimeSpan.FromMinutes(5)),
                    Status = ResultStatusType.Final,
                    ChildResultList = null
                  },
                  new Result()
                  {
                    Type = new ResultType()
                    {
                      Local = new Code()
                      {
                        Term = "CREAT",
                        Description = "Creatinine"
                      },
                      Lonic = new Code()
                      {
                        Term = "14682-9",
                        Description = "Creatinine"
                      }
                    },
                    DataType = "NM",
                    Value = "87",
                    Units = "umol/L",
                    ReferenceRange = "60-110",
                    AbnormalFlag = "N",
                    ObservationDateTime = new DateTimeOffset(2019, 12, 02, 07, 50, 00, TimeSpan.FromHours(10)).Subtract(TimeSpan.FromMinutes(5)),
                    Status = ResultStatusType.Final,
                    ChildResultList = null
                  },
                  new Result()
                  {
                    Type = new ResultType()
                    {
                      Local = new Code()
                      {
                        Term = "EGFR",
                        Description = "Glomerular filtration rate estimated"
                      },
                      Lonic = new Code()
                      {
                        Term = "62238-1",
                        Description = "eGFR"
                      }
                    },
                    DataType = "NM",
                    Value = "88",
                    Units = "mL/min/1.73m^2",
                    ReferenceRange = "60-120",
                    AbnormalFlag = "N",
                    ObservationDateTime = new DateTimeOffset(2019, 12, 02, 07, 50, 00, TimeSpan.FromHours(10)).Subtract(TimeSpan.FromMinutes(5)),
                    Status = ResultStatusType.Final,
                    ChildResultList = null
                  },
                  new Result()
                  {
                    Type = new ResultType()
                    {
                      Local = new Code()
                      {
                        Term = "INTERP",
                        Description = "Interpretation"
                      },
                      Lonic = null,
                    },
                    DataType = "ST",
                    Value = "All chemistry parameters are within normal limits for age and sex.",
                    Units = null,
                    ReferenceRange = null,
                    AbnormalFlag = null,
                    ObservationDateTime = new DateTimeOffset(2019, 12, 02, 07, 50, 00, TimeSpan.FromHours(10)).Subtract(TimeSpan.FromMinutes(5)),
                    Status = ResultStatusType.Final,
                    ChildResultList = null
                  }
                }
              }
            }
          }
        }
      };
    }
    public PathologyReportContainer GetFBC()
    {
      return new PathologyReportContainer()
      {
        PathologyReport = new PathologyReport()
        {
          PerformingLaboratory = LaboratoryFactory.GetPITUSLaboratory(),
          Patient = PatientFactory.GetPatient(PatientType.TessaCITIZEN),
          Request = new Request()
          {
            RequestedDate = new DateTimeOffset(2019, 09, 23, 00, 00, 00, TimeSpan.FromHours(10)),
            OrderNumber = "00000004",
            RequestingFacility = new Organisation()
            {
              Name = "Coagulation & Thrombosis Clinic",
              Identifier = new Identifier()
              {
                Value = "264C8EF6-868F-49B0-A532-B47D03F1A8D7",
                Type = IdentifierType.GUID
              }
            },
            RequestingApplication = "Best Practice 1.8.5.743",
            RequestingProvider = ProviderFactory.GetTrishFamilyDr(MedicareProviderNumber: "283530KX"),
            ClinicalNotes = "Warfarin 6mg per day, Family Hx Diabetes",
            CallBackPhoneNumber = "07302308594",
            CopyToList = new List<Provider>()
            {
              ProviderFactory.GetCoagulationAndThrombosisClinic(),
              ProviderFactory.GetBillCardiologist()
            }
          },
          PdfFileName = "SPIA Exemplar Report FBC v1.6.pdf",
          ReportList = new List<Report>()
          {
            new Report()
            {              
              ReportId = "1978881888",              
              CollectionDateTime = new DateTimeOffset(2019, 08, 02, 10, 40, 00, TimeSpan.FromHours(10)),
              SpecimenReceivedDateTime = new DateTimeOffset(2019, 08, 02, 12, 00, 00, TimeSpan.FromHours(10)),
              ReportReleaseDateTime = new DateTimeOffset(2019, 08, 02, 12, 52, 00, TimeSpan.FromHours(10)),
              ReportType = new ReportType()
              {
                Local = new Code() { Term = "FBC", Description = "Full blood count" },
                Snomed = new Code() { Term = "26604007", Description = "Full blood count" }
              },
              ReportStatus =  ResultStatusType.Final,
              Department = DiagnosticService.Hematology,
              ReportingPathologist = ProviderFactory.GetMarissaPathologist(),              
              Panel = new Panel()
              {                
                ResultList = new List<Result>()
                {
                  new Result()
                  {
                    Type = new ResultType()
                    {
                      Local = new Code()
                      {
                        Term = "HB",
                        Description = "Hemoglobin"
                      },
                      Lonic = new Code()
                      {
                        Term = "718-7",
                        Description = "Hemoglobin"
                      }
                    },
                    DataType = "NM",
                    Value = "146",
                    Units = "g/L",
                    ReferenceRange = "135-185",
                    AbnormalFlag = "N",
                    ObservationDateTime = new DateTimeOffset(2019, 08, 02, 12, 52, 00, TimeSpan.FromHours(10)).Subtract(TimeSpan.FromMinutes(5)),
                    Status = ResultStatusType.Final,
                    ChildResultList = null
                  },
                  new Result()
                  {
                    Type = new ResultType()
                    {
                      Local = new Code()
                      {
                        Term = "HCT",
                        Description = "Haematocrit"
                      },
                      Lonic = new Code()
                      {
                        Term = "4544-3",
                        Description = "Haematocrit"
                      }
                    },
                    DataType = "NM",
                    Value = "0.47",
                    Units = "L/L",
                    ReferenceRange = "0.40-0.51",
                    AbnormalFlag = "N",
                    ObservationDateTime = new DateTimeOffset(2019, 08, 02, 12, 52, 00, TimeSpan.FromHours(10)).Subtract(TimeSpan.FromMinutes(5)),
                    Status = ResultStatusType.Final,
                    ChildResultList = null
                  },
                  new Result()
                  {
                    Type = new ResultType()
                    {
                      Local = new Code()
                      {
                        Term = "RCC",
                        Description = "Red cell count"
                      },
                      Lonic = new Code()
                      {
                        Term = "789-8",
                        Description = "Red Cell Count"
                      }
                    },
                    DataType = "NM",
                    Value = "4.8",
                    Units = "10^12/L",
                    ReferenceRange = "4.0-5.8",
                    AbnormalFlag = "N",
                    ObservationDateTime = new DateTimeOffset(2019, 08, 02, 12, 52, 00, TimeSpan.FromHours(10)).Subtract(TimeSpan.FromMinutes(5)),
                    Status = ResultStatusType.Final,
                    ChildResultList = null
                  },
                  new Result()
                  {
                    Type = new ResultType()
                    {
                      Local = new Code()
                      {
                        Term = "WCC",
                        Description = "White cell count"
                      },
                      Lonic = new Code()
                      {
                        Term = "6690-2",
                        Description = "White cell count"
                      }
                    },
                    DataType = "NM",
                    Value = "8.6",
                    Units = "10^9/L",
                    ReferenceRange = "4.0-11.4",
                    AbnormalFlag = "N",
                    ObservationDateTime = new DateTimeOffset(2019, 08, 02, 12, 52, 00, TimeSpan.FromHours(10)).Subtract(TimeSpan.FromMinutes(5)),
                    Status = ResultStatusType.Final,
                    ChildResultList = null
                  },
                  new Result()
                  {
                    Type = new ResultType()
                    {
                      Local = new Code()
                      {
                        Term = "PLT",
                        Description = "Platelet count"
                      },
                      Lonic = new Code()
                      {
                        Term = "777-3",
                        Description = "Platelet count"
                      }
                    },
                    DataType = "NM",
                    Value = "278",
                    Units = "10^9/L",
                    ReferenceRange = "150-400",
                    AbnormalFlag = "N",
                    ObservationDateTime = new DateTimeOffset(2019, 08, 02, 12, 52, 00, TimeSpan.FromHours(10)).Subtract(TimeSpan.FromMinutes(5)),
                    Status = ResultStatusType.Final,
                    ChildResultList = null
                  },
                  new Result()
                  {
                    Type = new ResultType()
                    {
                      Local = new Code()
                      {
                        Term = "MCV",
                        Description = "MCV"
                      },
                      Lonic = new Code()
                      {
                        Term = "787-2",
                        Description = "Mean Cell Volume"
                      }
                    },
                    DataType = "NM",
                    Value = "97",
                    Units = "fL",
                    ReferenceRange = "80-100",
                    AbnormalFlag = "N",
                    ObservationDateTime = new DateTimeOffset(2019, 08, 02, 12, 52, 00, TimeSpan.FromHours(10)).Subtract(TimeSpan.FromMinutes(5)),
                    Status = ResultStatusType.Final,
                    ChildResultList = null
                  },
                  new Result()
                  {
                    Type = new ResultType()
                    {
                      Local = new Code()
                      {
                        Term = "MCH",
                        Description = "MCH"
                      },
                      Lonic = new Code()
                      {
                        Term = "785-6",
                        Description = "Mean cell haemoglobin"
                      }
                    },
                    DataType = "NM",
                    Value = "30",
                    Units = "pg",
                    ReferenceRange = "27-36",
                    AbnormalFlag = "N",
                    ObservationDateTime = new DateTimeOffset(2019, 08, 02, 12, 52, 00, TimeSpan.FromHours(10)).Subtract(TimeSpan.FromMinutes(5)),
                    Status = ResultStatusType.Final,
                    ChildResultList = null
                  },
                  new Result()
                  {
                    Type = new ResultType()
                    {
                      Local = new Code()
                      {
                        Term = "NEUT",
                        Description = "Neutrophils"
                      },
                      Lonic = new Code()
                      {
                        Term = "26499-4",
                        Description = "Neutrophils"
                      }
                    },
                    DataType = "NM",
                    Value = "2.9",
                    Units = "10^9/L",
                    ReferenceRange = "1.8-7.2",
                    AbnormalFlag = "N",
                    ObservationDateTime = new DateTimeOffset(2019, 08, 02, 12, 52, 00, TimeSpan.FromHours(10)).Subtract(TimeSpan.FromMinutes(5)),
                    Status = ResultStatusType.Final,
                    ChildResultList = null
                  },
                  new Result()
                  {
                    Type = new ResultType()
                    {
                      Local = new Code()
                      {
                        Term = "LYMP",
                        Description = "Lymphocytes"
                      },
                      Lonic = new Code()
                      {
                        Term = "26474-7",
                        Description = "Lymphocytes"
                      }
                    },
                    DataType = "NM",
                    Value = "1.6",
                    Units = "10^9/L",
                    ReferenceRange = "1.0-4.0",
                    AbnormalFlag = "N",
                    ObservationDateTime = new DateTimeOffset(2019, 08, 02, 12, 52, 00, TimeSpan.FromHours(10)).Subtract(TimeSpan.FromMinutes(5)),
                    Status = ResultStatusType.Final,
                    ChildResultList = null
                  },
                  new Result()
                  {
                    Type = new ResultType()
                    {
                      Local = new Code()
                      {
                        Term = "MONO",
                        Description = "Monocytes"
                      },
                      Lonic = new Code()
                      {
                        Term = "26484-6",
                        Description = "Monocytes"
                      }
                    },
                    DataType = "NM",
                    Value = "0.4",
                    Units = "10^9/L",
                    ReferenceRange = "0.1-1.0",
                    AbnormalFlag = "N",
                    ObservationDateTime = new DateTimeOffset(2019, 08, 02, 12, 52, 00, TimeSpan.FromHours(10)).Subtract(TimeSpan.FromMinutes(5)),
                    Status = ResultStatusType.Final,
                    ChildResultList = null
                  },
                  new Result()
                  {
                    Type = new ResultType()
                    {
                      Local = new Code()
                      {
                        Term = "EOS",
                        Description = "Eosinophils"
                      },
                      Lonic = new Code()
                      {
                        Term = "26449-9",
                        Description = "Eosinophils"
                      }
                    },
                    DataType = "NM",
                    Value = "0.2",
                    Units = "10^9/L",
                    ReferenceRange = "0.0-0.5",
                    AbnormalFlag = "N",
                    ObservationDateTime = new DateTimeOffset(2019, 08, 02, 12, 52, 00, TimeSpan.FromHours(10)).Subtract(TimeSpan.FromMinutes(5)),
                    Status = ResultStatusType.Final,
                    ChildResultList = null
                  },
                  new Result()
                  {
                    Type = new ResultType()
                    {
                      Local = new Code()
                      {
                        Term = "BASO",
                        Description = "Basophils"
                      },
                      Lonic = new Code()
                      {
                        Term = "26444-0",
                        Description = "Basophils"
                      }
                    },
                    DataType = "NM",
                    Value = "0.03",
                    Units = "10^9/L",
                    ReferenceRange = "0.0-0.20",
                    AbnormalFlag = "N",
                    ObservationDateTime = new DateTimeOffset(2019, 08, 02, 12, 52, 00, TimeSpan.FromHours(10)).Subtract(TimeSpan.FromMinutes(5)),
                    Status = ResultStatusType.Final,
                    ChildResultList = null
                  },
                  new Result()
                  {
                    Type = new ResultType()
                    {
                      Local = new Code()
                      {
                        Term = "INTER",
                        Description = "Interpretation"
                      },
                      Lonic = new Code()
                      {
                        Term = "18314-5",
                        Description = "Blood film examination"
                      }
                    },
                    DataType = "ST",
                    Value = "All haematology parameters are within normal limits for age and sex.",
                    Units = null,
                    ReferenceRange = null,
                    AbnormalFlag = null,
                    ObservationDateTime = new DateTimeOffset(2019, 08, 02, 12, 52, 00, TimeSpan.FromHours(10)).Subtract(TimeSpan.FromMinutes(5)),
                    Status = ResultStatusType.Final,
                    ChildResultList = null
                  }
                }
              }
            }
          }
        }
      };      
    }
    public PathologyReportContainer GetHFE()
    {
      return new PathologyReportContainer()
      {
        PathologyReport = new PathologyReport()
        {
          PerformingLaboratory = LaboratoryFactory.GetPITUSLaboratory(),
          Patient = PatientFactory.GetPatient(PatientType.GlennFERNIE),
          Request = new Request()
          {
            RequestedDate = new DateTimeOffset(2019, 10, 29, 00, 00, 00, TimeSpan.FromHours(10)),
            OrderNumber = "00000005",
            RequestingFacility = new Organisation()
            {
              Name = "Dermatology Clinic",
              Identifier = new Identifier()
              {
                Value = "D101F20B-1453-47A1-AD3F-A2845964A84E",
                Type = IdentifierType.GUID
              }
            },
            RequestingApplication = "Best Practice 1.8.5.743",
            RequestingProvider = ProviderFactory.GetTrishFamilyDr(MedicareProviderNumber: "9885728J"),
            ClinicalNotes = "? Hereditary haemochromatosis. Arthritis, increased iron stores.",
            CallBackPhoneNumber = "07302308594",
            CopyToList = new List<Provider>()
            {
              ProviderFactory.GetGeneticsClinic(),
              ProviderFactory.GetGeneticCounsellor()
            }
          },
          PdfFileName = "SPIA Exemplar Report Haemochromatosis genotyping v1.5.pdf",
          ReportList = new List<Report>()
          {
            new Report()
            {              
              ReportId = "1978881777",              
              CollectionDateTime = new DateTimeOffset(2019, 11, 29, 07, 40, 00, TimeSpan.FromHours(10)),
              SpecimenReceivedDateTime = new DateTimeOffset(2019, 11, 29, 12, 03, 00, TimeSpan.FromHours(10)),
              ReportReleaseDateTime = new DateTimeOffset(2019, 11, 29, 16, 38, 00, TimeSpan.FromHours(10)),
              ReportType = new ReportType()
              {
                Local = new Code() { Term = "HFE", Description = "Haemochromatosis Genotyping" },
                Snomed = new Code() { Term = "401085002", Description = "Haemochromatosis gene screening test" }
              },
              ReportStatus =  ResultStatusType.Final,
              Department = DiagnosticService.Genetics,
              ReportingPathologist = ProviderFactory.GetKondoPathologist(),              
              Panel = new Panel()
              {                
                ResultList = new List<Result>()
                {
                  new Result()
                  {
                    Type = new ResultType()
                    {
                      Local = new Code()
                      {
                        Term = "C28Y",
                        Description = "C28Y mutation analysis"
                      },
                      Lonic = null,
                    },
                    DataType = "ST",
                    Value = "Not Detected",
                    Units = null,
                    ReferenceRange = null,
                    AbnormalFlag = "N",
                    ObservationDateTime = new DateTimeOffset(2019, 11, 29, 16, 38, 00, TimeSpan.FromHours(10)).Subtract(TimeSpan.FromMinutes(5)),
                    Status = ResultStatusType.Final,
                    ChildResultList = null
                  },
                  new Result()
                  {
                    Type = new ResultType()
                    {
                      Local = new Code()
                      {
                        Term = "H63D",
                        Description = "H63D mutation analysis"
                      },
                      Lonic = null,
                    },
                    DataType = "ST",
                    Value = "Homozygous",
                    Units = null,
                    ReferenceRange = null,
                    AbnormalFlag = "N",
                    ObservationDateTime = new DateTimeOffset(2019, 11, 29, 16, 38, 00, TimeSpan.FromHours(10)).Subtract(TimeSpan.FromMinutes(5)),
                    Status = ResultStatusType.Final,
                    ChildResultList = null
                  },
                  new Result()
                  {
                    Type = new ResultType()
                    {
                      Local = new Code()
                      {
                        Term = "INTER",
                        Description = "Interpretation"
                      },
                      Lonic = null,
                    },
                    DataType = "FT",
                    Value = "\\H\\Results\\N\\\\.br\\" +
                             "Two copies of the p.His63Asp variant were detected in the patient. The p.Cys282Tyr variant was not detected.\\.br\\" +
                             "The diagnosis of the most common form of HFE-related hereditary haemochromatosis is excluded.\\.br\\" +
                             "\\.br\\" +
                             "\\H\\Interpretation\\N\\\\.br\\" +
                             "Hereditary haemochromatosis (HH) is a recessive genetic disorder of iron metabolism. Greater than 90% of HFE\\.br\\" +
                             "related hereditary haemochromatosis is associated with homozygosity for p.(Cys282Tyr) (c.845G>A; aka p.C282Y).\\.br\\" +
                             "Compound heterozygosity for p. (Cys282Tyr) and p. (His63Asp) (c.187C>G; aka p.H63D) may be a risk factor\\.br\\" +
                             "predisposing to mild to moderate forms of iron overload when in association with other risk factors. Both mutations\\.br\\" +
                             "are detected by real-time PCR amplification and fluorescent detection of alleles.",
                    Units = null,
                    ReferenceRange = null,
                    AbnormalFlag = "N",
                    ObservationDateTime = new DateTimeOffset(2019, 11, 29, 16, 38, 00, TimeSpan.FromHours(10)).Subtract(TimeSpan.FromMinutes(5)),
                    Status = ResultStatusType.Final,
                    ChildResultList = null
                  }
                }
              }
            }
          }
        }
      };      
    }
    public PathologyReportContainer GetHepBsAb()
    {
      return new PathologyReportContainer()
      {
        PathologyReport = new PathologyReport()
        {
          PerformingLaboratory = LaboratoryFactory.GetPITUSLaboratory(),          
          Patient = PatientFactory.GetPatient(PatientType.GeorginaROSSLAND),
          Request = new Request()
          {
            RequestedDate = new DateTimeOffset(2019, 09, 23, 00, 00, 00, TimeSpan.FromHours(10)),
            OrderNumber = "00000006",
            RequestingFacility = new Organisation()
            {
              Name = "Antenatal Clinic Sunrise Hospital",
              Identifier = new Identifier()
              {
                Value = "143569C9-8AFC-4BBD-A663-95079AE10B57",
                Type = IdentifierType.GUID
              }
            },
            RequestingApplication = "Best Practice 1.8.5.743",
            RequestingProvider = ProviderFactory.GetTrishFamilyDr(MedicareProviderNumber: "8203015Y"),
            ClinicalNotes = "First trimester antenatal screen, ~ 10 weeks pregnant (G1P0).",
            CallBackPhoneNumber = "07302308594",
            CopyToList = new List<Provider>()
            {
              ProviderFactory.GetBiancaMidwife()
            }
          },
          PdfFileName = "SPIA Exemplar Report HBsAb v1.5.pdf",
          ReportList = new List<Report>()
          {
            new Report()
            {              
              ReportId = "1978881874",              
              CollectionDateTime = new DateTimeOffset(2019, 09, 25, 07, 20, 00, TimeSpan.FromHours(10)),
              SpecimenReceivedDateTime = new DateTimeOffset(2019, 09, 25, 10, 24, 00, TimeSpan.FromHours(10)),
              ReportReleaseDateTime = new DateTimeOffset(2019, 09, 25, 07, 20, 00, TimeSpan.FromHours(10)),
              ReportType = new ReportType()
              {
                Local = new Code() { Term = "HepBsAb", Description = "Hepatitis B surface Ab" },
                Snomed = new Code() { Term = "315130004", Description = "Hepatitis B surface antibody level" }
              },
              ReportStatus =  ResultStatusType.Final,
              Department = DiagnosticService.Serology,
              ReportingPathologist = ProviderFactory.GetStanleyVirologist(),              
              Panel = new Panel()
              {                
                ResultList = new List<Result>()
                {
                  new Result()
                  {
                    Type = new ResultType()
                    {
                      Local = new Code()
                      {
                        Term = "HepBsAb",
                        Description = "Hepatitis B surface Ab"
                      },
                      Lonic = new Code()
                      {
                        Term = "16935-9",
                        Description = "Hepatitis B surface Ab"
                      }
                    },
                    DataType = "ST",
                    Value = "Positive",
                    Units = null,
                    ReferenceRange = null,
                    AbnormalFlag = "N",
                    ObservationDateTime = new DateTimeOffset(2019, 09, 25, 07, 20, 00, TimeSpan.FromHours(10)).Subtract(TimeSpan.FromMinutes(5)),
                    Status = ResultStatusType.Final,
                    ChildResultList = null
                  },
                  new Result()
                  {
                    Type = new ResultType()
                    {
                      Local = new Code()
                      {
                        Term = "INTER",
                        Description = "Interpretation"
                      },
                      Lonic = null
                    },
                    DataType = "FT",
                    Value = "\\H\\Interpretation\\N\\\\.br\\Positive HBsAb result indicates sufficient Hepatitis B immunity.",
                    Units = null,
                    ReferenceRange = null,
                    AbnormalFlag = "N",
                    ObservationDateTime = new DateTimeOffset(2019, 09, 25, 07, 20, 00, TimeSpan.FromHours(10)).Subtract(TimeSpan.FromMinutes(5)),
                    Status = ResultStatusType.Final,
                    ChildResultList = null
                  }
                }
              }
            }
          }
        }
      };

    }
    public PathologyReportContainer GetImmunoglobulinE()
    {
      return new PathologyReportContainer()
      {
        PathologyReport = new PathologyReport()
        {
          PerformingLaboratory = LaboratoryFactory.GetPITUSLaboratory(),
          Patient = PatientFactory.GetPatient(PatientType.GloriaNELSON),
          Request = new Request()
          {
            RequestedDate = new DateTimeOffset(2019, 11, 23, 00, 00, 00, TimeSpan.FromHours(10)),
            OrderNumber = "00000007",
            RequestingFacility = new Organisation()
            {
              Name = "Sunrise Hospital Medical Records",
              Identifier = new Identifier()
              {
                Value = "0FECFC6C-C98F-4625-B58B-ECB27063DAF1",
                Type = IdentifierType.GUID
              }
            },
            RequestingApplication = "Best Practice 1.8.5.743",
            RequestingProvider = ProviderFactory.GetTrishFamilyDr(MedicareProviderNumber: "1783879L"),
            ClinicalNotes = "Mild reaction to bee sting. Asthma.",
            CallBackPhoneNumber = "07302308594",
            CopyToList = new List<Provider>()
            {
              ProviderFactory.GetAllergyClinic(),
              ProviderFactory.GetBeulaImmunologist()
            }
          },
          PdfFileName = "SPIA Exemplar Report Immunoglobulin E v1.5.pdf",
          ReportList = new List<Report>()
          {
            new Report()
            {              
              ReportId = "1978881822",              
              CollectionDateTime = new DateTimeOffset(2019, 12, 02, 07, 20, 00, TimeSpan.FromHours(10)),
              SpecimenReceivedDateTime = new DateTimeOffset(2019, 12, 02, 11, 04, 00, TimeSpan.FromHours(10)),
              ReportReleaseDateTime = new DateTimeOffset(2019, 12, 02, 07, 50, 00, TimeSpan.FromHours(10)),
              ReportType = new ReportType()
              {
                Local = new Code() { Term = "ImmunoIgG", Description = "Immunoglobulin E" },
                Snomed = new Code() { Term = "41960005", Description = "IgE measurement" }
              },
              ReportStatus =  ResultStatusType.Final,
              Department = DiagnosticService.Immunology,
              ReportingPathologist = ProviderFactory.GetBertramPathologist(),             
              Panel = new Panel()
              {                
                ResultList = new List<Result>()
                {
                  new Result()
                  {
                    Type = new ResultType()
                    {
                      Local = new Code()
                      {
                        Term = "IMME",
                        Description = "Immunoglobulin E"
                      },
                      Lonic = new Code()
                      {
                        Term = "19113-0",
                        Description = "Immunoglobulin E IgE"
                      }
                    },
                    DataType = "NM",
                    Value = "320",
                    Units = "kIU/L",
                    ReferenceRange = "2.0-300",
                    AbnormalFlag = "H",
                    ObservationDateTime = new DateTimeOffset(2019, 12, 02, 07, 50, 00, TimeSpan.FromHours(10)).Subtract(TimeSpan.FromMinutes(5)),
                    Status = ResultStatusType.Final,
                    ChildResultList = null
                  },
                  new Result()
                  {
                    Type = new ResultType()
                    {
                      Local = new Code()
                      {
                        Term = "INTER",
                        Description = "Interpretation"
                      },
                      Lonic = null,
                    },
                    DataType = "FT",
                    Value = "\\H\\Interpretation\\N\\\\.br\\" +
                    "Immunoprotein parameters indicate an increasing level of sensitization. Specific IgE testing\\.br\\" +
                    "recommended for honey bee and common wasp venoms, also tryptase levels.",
                    Units = null,
                    ReferenceRange = null,
                    AbnormalFlag = null,
                    ObservationDateTime = new DateTimeOffset(2019, 12, 02, 07, 50, 00, TimeSpan.FromHours(10)).Subtract(TimeSpan.FromMinutes(5)),
                    Status = ResultStatusType.Final,
                    ChildResultList = null
                  }
                }
              }
            }
          }
        }
      };
    }
    public PathologyReportContainer GetINR()
    {
      return new PathologyReportContainer()
      {
        PathologyReport = new PathologyReport()
        {
          PerformingLaboratory = LaboratoryFactory.GetPITUSLaboratory(),
          Patient = PatientFactory.GetPatient(PatientType.TessaCITIZEN),
          Request = new Request()
          {
            RequestedDate = new DateTimeOffset(2019, 07, 29, 00, 00, 00, TimeSpan.FromHours(10)),
            OrderNumber = "00000009",
            RequestingFacility = new Organisation()
            {
              Name = "Coagulation & Thrombosis Clinic",
              Identifier = new Identifier()
              {
                Value = "ACA30A38-811E-4E0D-B55C-774D38B8E171",
                Type = IdentifierType.GUID
              }
            },
            RequestingApplication = "Best Practice 1.8.5.743",
            RequestingProvider = ProviderFactory.GetTrishFamilyDr(MedicareProviderNumber: "283530KX"),
            ClinicalNotes = "Warfarin 6mg per day, Family Hx Diabetes.",
            CallBackPhoneNumber = "07302308594",
            CopyToList = new List<Provider>()
            {
              ProviderFactory.GetCoagulationAndThrombosisClinic(),
              ProviderFactory.GetBillCardiologist()
            }
          },
          PdfFileName = "SPIA Exemplar Report INR v1.7.pdf",
          ReportList = new List<Report>()
          {
            new Report()
            {              
              ReportId = "1878881888",              
              CollectionDateTime = new DateTimeOffset(2019, 08, 02, 11, 30, 00, TimeSpan.FromHours(10)),
              SpecimenReceivedDateTime = new DateTimeOffset(2019, 08, 02, 12, 00, 00, TimeSpan.FromHours(10)),
              ReportReleaseDateTime = new DateTimeOffset(2019, 08, 02, 14, 32, 00, TimeSpan.FromHours(10)),
              ReportType = new ReportType()
              {
                Local = new Code() { Term = "INR", Description = "INR" },
                Snomed = new Code() { Term = "440685005", Description = "Calculation of international normalised ratio" }
              },
              ReportStatus =  ResultStatusType.Final,
              Department = DiagnosticService.Hematology,
              ReportingPathologist = ProviderFactory.GetMarissaPathologist(),              
              Panel = new Panel()
              {
                ResultList = new List<Result>()
                {
                  new Result()
                  {
                    Type = new ResultType()
                    {
                      Local = new Code()
                      {
                        Term = "WARF",
                        Description = "Warfarin dose"
                      },
                      Lonic = new Code()
                      {
                        Term = "4461-0",
                        Description = "Warfarin dose"
                      }
                    },
                    DataType = "NM",
                    Value = "6",
                    Units = "mg",
                    ReferenceRange = null,
                    AbnormalFlag = null,
                    ObservationDateTime = new DateTimeOffset(2019, 08, 02, 14, 32, 00, TimeSpan.FromHours(10)).Subtract(TimeSpan.FromMinutes(5)),
                    Status = ResultStatusType.Final,
                    ChildResultList = null
                  },
                  new Result()
                  {
                    Type = new ResultType()
                    {
                      Local = new Code()
                      {
                        Term = "INR",
                        Description = "INR"
                      },
                      Lonic = new Code()
                      {
                        Term = "6301-6",
                        Description = "INR"
                      }
                    },
                    DataType = "NM",
                    Value = "3.1",
                    Units = null,
                    ReferenceRange = "2.0-3.0",
                    AbnormalFlag = "H",
                    ObservationDateTime = new DateTimeOffset(2019, 08, 02, 14, 32, 00, TimeSpan.FromHours(10)).Subtract(TimeSpan.FromMinutes(5)),
                    Status = ResultStatusType.Final,
                    ChildResultList = null
                  },
                  new Result()
                  {
                    Type = new ResultType()
                    {
                      Local = new Code()
                      {
                        Term = "INTER",
                        Description = "Interpretation"
                      },
                      Lonic = null
                    },
                    DataType = "FT",
                    Value = "\\H\\Interpretation\\N\\\\.br\\" +
                    "INR is higher than therapeutic range. Recommend Warfarin dose be reduced to 5 mg per day and test repeated in\\.br\\" +
                    "7 days.",
                    Units = null,
                    ReferenceRange = null,
                    AbnormalFlag = null,
                    ObservationDateTime = new DateTimeOffset(2019, 08, 02, 14, 32, 00, TimeSpan.FromHours(10)).Subtract(TimeSpan.FromMinutes(5)),
                    Status = ResultStatusType.Final,
                    ChildResultList = null
                  }
                }
              }
            }
          }
        }
      };
    }
    public PathologyReportContainer GetMSU()
    {
      return new PathologyReportContainer()
      {
        PathologyReport = new PathologyReport()
        {
          PerformingLaboratory = LaboratoryFactory.GetPITUSLaboratory(),
          Patient = PatientFactory.GetPatient(PatientType.GloriaNELSON),
          Request = new Request()
          {
            RequestedDate = new DateTimeOffset(2020, 01, 22, 00, 00, 00, TimeSpan.FromHours(10)),
            OrderNumber = "00000012",
            RequestingFacility = new Organisation()
            {
              Name = "Doctor In The House Surgery",
              Identifier = new Identifier()
              {
                Value = "191394B8-6AB0-4EF5-BC7D-19A5B37FA60F",
                Type = IdentifierType.GUID
              }
            },
            RequestingApplication = "Best Practice 1.8.5.743",
            RequestingProvider = ProviderFactory.GetTrishFamilyDr(MedicareProviderNumber: "8264815W"),
            ClinicalNotes = "Diabetic with recurring UTI. Last course Trimethoprim (100 mg bd) completed 27 Nov 2019.",
            CallBackPhoneNumber = "07302308594",
            CopyToList = new List<Provider>()
            {
              ProviderFactory.GetRheumatologyClinic(),
              ProviderFactory.GetRebeccaRheumatologist()
            }
          },
          PdfFileName = "SPIA Exemplar Report MCS Urine v1.6.pdf",
          ReportList = new List<Report>()
          {
            new Report()
            {              
              ReportId = "2001277757",              
              CollectionDateTime = new DateTimeOffset(2020, 01, 22, 07, 40, 00, TimeSpan.FromHours(10)),
              SpecimenReceivedDateTime = new DateTimeOffset(2020, 01, 22, 10, 10, 00, TimeSpan.FromHours(10)),              
              ReportReleaseDateTime = new DateTimeOffset(2020, 01, 24, 14, 40, 00, TimeSpan.FromHours(10)),
              ReportType = new ReportType()
              {
                Local = new Code() { Term = "UMCS", Description = "MCS urine" },
                Snomed = new Code() { Term = "401324008", Description = "Urine microscopy, culture and sensitivities" }
              },
              ReportStatus =  ResultStatusType.Final,
              Department = DiagnosticService.Microbiology,
              ReportingPathologist = ProviderFactory.GetEvannaPathologist(),              
              Panel = new Panel()
              {                
                ResultList = new List<Result>()
                {
                  new Result()
                  {
                    Type = new ResultType()
                    {
                      Local = new Code()
                      {
                        Term = "GLUC",
                        Description = "Glucose"
                      },
                      Lonic = new Code()
                      {
                        Term = "25428-4",
                        Description = "Glucose"
                      }
                    },
                    DataType = "ST",
                    Value = "3+",
                    Units = null,
                    ReferenceRange = "Negative",
                    AbnormalFlag = "A",
                    ObservationDateTime = new DateTimeOffset(2020, 01, 24, 14, 40, 00, TimeSpan.FromHours(10)).Subtract(TimeSpan.FromMinutes(5)),
                    Status = ResultStatusType.Final,
                    ChildResultList = null
                  },
                  new Result()
                  {
                    Type = new ResultType()
                    {
                      Local = new Code()
                      {
                        Term = "BILI",
                        Description = "Bilirubin"
                      },
                      Lonic = new Code()
                      {
                        Term = "5770-3",
                        Description = "Bilirubin"
                      }
                    },
                    DataType = "ST",
                    Value = "1+",
                    Units = null,
                    ReferenceRange = "Negative",
                    AbnormalFlag = "A",
                    ObservationDateTime = new DateTimeOffset(2020, 01, 24, 14, 40, 00, TimeSpan.FromHours(10)).Subtract(TimeSpan.FromMinutes(5)),
                    Status = ResultStatusType.Final,
                    ChildResultList = null
                  },
                  new Result()
                  {
                    Type = new ResultType()
                    {
                      Local = new Code()
                      {
                        Term = "KETO",
                        Description = "Ketones"
                      },
                      Lonic = new Code()
                      {
                        Term = "2514-8",
                        Description = "Ketones"
                      }
                    },
                    DataType = "ST",
                    Value = "1+",
                    Units = null,
                    ReferenceRange = "Negative",
                    AbnormalFlag = "A",
                    ObservationDateTime = new DateTimeOffset(2020, 01, 24, 14, 40, 00, TimeSpan.FromHours(10)).Subtract(TimeSpan.FromMinutes(5)),
                    Status = ResultStatusType.Final,
                    ChildResultList = null
                  },
                  new Result()
                  {
                    Type = new ResultType()
                    {
                      Local = new Code()
                      {
                        Term = "SPECGRA",
                        Description = "Specific Gravity"
                      },
                      Lonic = new Code()
                      {
                        Term = "5811-5",
                        Description = "Specific Gravity"
                      }
                    },
                    DataType = "NM",
                    Value = "1.034",
                    Units = null,
                    ReferenceRange = "1.003-1.035",
                    AbnormalFlag = "N",
                    ObservationDateTime = new DateTimeOffset(2020, 01, 24, 14, 40, 00, TimeSpan.FromHours(10)).Subtract(TimeSpan.FromMinutes(5)),
                    Status = ResultStatusType.Final,
                    ChildResultList = null
                  },
                  new Result()
                  {
                    Type = new ResultType()
                    {
                      Local = new Code()
                      {
                        Term = "PH",
                        Description = "pH"
                      },
                      Lonic = new Code()
                      {
                        Term = "5803-2",
                        Description = "pH"
                      }
                    },
                    DataType = "NM",
                    Value = "8.3",
                    Units = null,
                    ReferenceRange = "5.0-8.0",
                    AbnormalFlag = "H",
                    ObservationDateTime = new DateTimeOffset(2020, 01, 24, 14, 40, 00, TimeSpan.FromHours(10)).Subtract(TimeSpan.FromMinutes(5)),
                    Status = ResultStatusType.Final,
                    ChildResultList = null
                  },
                  new Result()
                  {
                    Type = new ResultType()
                    {
                      Local = new Code()
                      {
                        Term = "PROT",
                        Description = "Protein"
                      },
                      Lonic = new Code()
                      {
                        Term = "20454-5",
                        Description = "Protein"
                      }
                    },
                    DataType = "ST",
                    Value = "2+",
                    Units = null,
                    ReferenceRange = "Negative",
                    AbnormalFlag = "A",
                    ObservationDateTime = new DateTimeOffset(2020, 01, 24, 14, 40, 00, TimeSpan.FromHours(10)).Subtract(TimeSpan.FromMinutes(5)),
                    Status = ResultStatusType.Final,
                    ChildResultList = null
                  },
                  new Result()
                  {
                    Type = new ResultType()
                    {
                      Local = new Code()
                      {
                        Term = "UROB",
                        Description = "Urobilinogen"
                      },
                      Lonic = new Code()
                      {
                        Term = "5818-0",
                        Description = "Urobilinogen"
                      }
                    },
                    DataType = "NM",
                    Value = "0.7",
                    Units = "mg/dL",
                    ReferenceRange = "0.1-1.0",
                    AbnormalFlag = "A",
                    ObservationDateTime = new DateTimeOffset(2020, 01, 24, 14, 40, 00, TimeSpan.FromHours(10)).Subtract(TimeSpan.FromMinutes(5)),
                    Status = ResultStatusType.Final,
                    ChildResultList = null
                  },
                  new Result()
                  {
                    Type = new ResultType()
                    {
                      Local = new Code()
                      {
                        Term = "NITR",
                        Description = "Nitrites"
                      },
                      Lonic = new Code()
                      {
                        Term = "5802-4",
                        Description = "Nitrites"
                      }
                    },
                    DataType = "ST",
                    Value = "Positive",
                    Units = null,
                    ReferenceRange = "Negative",
                    AbnormalFlag = "A",
                    ObservationDateTime = new DateTimeOffset(2020, 01, 24, 14, 40, 00, TimeSpan.FromHours(10)).Subtract(TimeSpan.FromMinutes(5)),
                    Status = ResultStatusType.Final,
                    ChildResultList = null
                  },
                  new Result()
                  {
                    Type = new ResultType()
                    {
                      Local = new Code()
                      {
                        Term = "HB",
                        Description = "Haemoglobin"
                      },
                      Lonic = new Code()
                      {
                        Term = "5794-3",
                        Description = "Haemoglobin"
                      }
                    },
                    DataType = "ST",
                    Value = "2+",
                    Units = null,
                    ReferenceRange = "Negative",
                    AbnormalFlag = "A",
                    ObservationDateTime = new DateTimeOffset(2020, 01, 24, 14, 40, 00, TimeSpan.FromHours(10)).Subtract(TimeSpan.FromMinutes(5)),
                    Status = ResultStatusType.Final,
                    ChildResultList = null
                  },
                  new Result()
                  {
                    Type = new ResultType()
                    {
                      Local = new Code()
                      {
                        Term = "LEUCOEST",
                        Description = "Leucocyte Esterase"
                      },
                      Lonic = new Code()
                      {
                        Term = "5799-2",
                        Description = "Leucocyte Esterase"
                      }
                    },
                    DataType = "ST",
                    Value = "Positive",
                    Units = null,
                    ReferenceRange = "Negative",
                    AbnormalFlag = "A",
                    ObservationDateTime = new DateTimeOffset(2020, 01, 24, 14, 40, 00, TimeSpan.FromHours(10)).Subtract(TimeSpan.FromMinutes(5)),
                    Status = ResultStatusType.Final,
                    ChildResultList = null
                  },
                  new Result()
                  {
                    Type = new ResultType()
                    {
                      Local = new Code()
                      {
                        Term = "WBC",
                        Description = "White blood cell count"
                      },
                      Lonic = new Code()
                      {
                        Term = "30405-5",
                        Description = "White blood cell count"
                      }
                    },
                    DataType = "NR",
                    Value = "10^25",
                    Units = "Erythrocytes/hpf",
                    ReferenceRange = "None seen",
                    AbnormalFlag = "A",
                    ObservationDateTime = new DateTimeOffset(2020, 01, 24, 14, 40, 00, TimeSpan.FromHours(10)).Subtract(TimeSpan.FromMinutes(5)),
                    Status = ResultStatusType.Final,
                    ChildResultList = null
                  },
                  new Result()
                  {
                    Type = new ResultType()
                    {
                      Local = new Code()
                      {
                        Term = "RBC",
                        Description = "Red blood cell count"
                      },
                      Lonic = new Code()
                      {
                        Term = "30391-7",
                        Description = "Red blood cell count"
                      }
                    },
                    DataType = "SN",
                    Value = ">^60",
                    Units = "Leucocytes/hpf",
                    ReferenceRange = "None seen",
                    AbnormalFlag = "A",
                    ObservationDateTime = new DateTimeOffset(2020, 01, 24, 14, 40, 00, TimeSpan.FromHours(10)).Subtract(TimeSpan.FromMinutes(5)),
                    Status = ResultStatusType.Final,
                    ChildResultList = null
                  },
                  new Result()
                  {
                    Type = new ResultType()
                    {
                      Local = new Code()
                      {
                        Term = "RBCM",
                        Description = "Red blood cell morphology"
                      },
                      Lonic = new Code()
                      {
                        Term = "53974-2",
                        Description = "Red blood cell morphology"
                      }
                    },
                    DataType = "ST",
                    Value = "crenated",
                    Units = null,
                    ReferenceRange = null,
                    AbnormalFlag = "A",
                    ObservationDateTime = new DateTimeOffset(2020, 01, 24, 14, 40, 00, TimeSpan.FromHours(10)).Subtract(TimeSpan.FromMinutes(5)),
                    Status = ResultStatusType.Final,
                    ChildResultList = null
                  },
                  new Result()
                  {
                    Type = new ResultType()
                    {
                      Local = new Code()
                      {
                        Term = "EPITH",
                        Description = "Epithelial cells"
                      },
                      Lonic = new Code()
                      {
                        Term = "30383-4",
                        Description = "Epithelial cells"
                      }
                    },
                    DataType = "NR",
                    Value = "1^5",
                    Units = "Epithelial cells/hpf",
                    ReferenceRange = "None seen",
                    AbnormalFlag = "A",
                    ObservationDateTime = new DateTimeOffset(2020, 01, 24, 14, 40, 00, TimeSpan.FromHours(10)).Subtract(TimeSpan.FromMinutes(5)),
                    Status = ResultStatusType.Final,
                    ChildResultList = null
                  },
                  new Result()
                  {
                    Type = new ResultType()
                    {
                      Local = new Code()
                      {
                        Term = "CASTS",
                        Description = "Casts present"
                      },
                      Lonic = new Code()
                      {
                        Term = "24124-0",
                        Description = "Casts"
                      }
                    },
                    DataType = "ST",
                    Value = "None seen",
                    Units = "Casts/hpf",
                    ReferenceRange = "None seen",
                    AbnormalFlag = "N",
                    ObservationDateTime = new DateTimeOffset(2020, 01, 24, 14, 40, 00, TimeSpan.FromHours(10)).Subtract(TimeSpan.FromMinutes(5)),
                    Status = ResultStatusType.Final,
                    ChildResultList = null
                  },
                  new Result()
                  {
                    Type = new ResultType()
                    {
                      Local = new Code()
                      {
                        Term = "CRYST",
                        Description = "Crystals"
                      },
                      Lonic = new Code()
                      {
                        Term = "49755-2",
                        Description = "Crystals"
                      }
                    },
                    DataType = "ST",
                    Value = "None seen",
                    Units = "Crystals/hpf",
                    ReferenceRange = "None seen",
                    AbnormalFlag = "N",
                    ObservationDateTime = new DateTimeOffset(2020, 01, 24, 14, 40, 00, TimeSpan.FromHours(10)).Subtract(TimeSpan.FromMinutes(5)),
                    Status = ResultStatusType.Final,
                    ChildResultList = null
                  },
                  new Result()
                  {
                    Type = new ResultType()
                    {
                      Local = new Code()
                      {
                        Term = "CULTURE",
                        Description = "Culture result/Organism"
                      },
                      Lonic = new Code()
                      {
                        Term = "630-4",
                        Description = "Bacteria identified"
                      }
                    },
                    DataType = "ST",
                    Value = "Escherichia coli 20,000 cfu/mL",
                    Units = null,
                    ReferenceRange = "No growth",
                    AbnormalFlag = "N",
                    ObservationDateTime = new DateTimeOffset(2020, 01, 24, 14, 40, 00, TimeSpan.FromHours(10)).Subtract(TimeSpan.FromMinutes(5)),
                    Status = ResultStatusType.Final,
                    ChildResultList = new List<Result>()
                    {
                      new Result()
                      {
                        Type = new ResultType()
                        {
                          Local = new Code()
                          {
                            Term = "Amikacin",
                            Description = "Amikacin"
                          },
                          Lonic = new Code()
                          {
                            Term = "18860-7",
                            Description = "Amikacin"
                          }
                        },
                        DataType = "ST",
                        Value = "I",
                        Units = null,
                        ReferenceRange = null,
                        AbnormalFlag = "I",
                        ObservationDateTime = new DateTimeOffset(2020, 01, 24, 14, 40, 00, TimeSpan.FromHours(10)).Subtract(TimeSpan.FromMinutes(5)),
                        Status = ResultStatusType.Final,
                        ChildResultList = null
                      },
                      new Result()
                      {
                        Type = new ResultType()
                        {
                          Local = new Code()
                          {
                            Term = "Amoxicillin",
                            Description = "Amoxicillin"
                          },
                          Lonic = new Code()
                          {
                            Term = "18861-5",
                            Description = "Amoxicillin"
                          }
                        },
                        DataType = "ST",
                        Value = "I",
                        Units = null,
                        ReferenceRange = null,
                        AbnormalFlag = "I",
                        ObservationDateTime = new DateTimeOffset(2020, 01, 24, 14, 40, 00, TimeSpan.FromHours(10)).Subtract(TimeSpan.FromMinutes(5)),
                        Status = ResultStatusType.Final,
                        ChildResultList = null
                      },
                      new Result()
                      {
                        Type = new ResultType()
                        {
                          Local = new Code()
                          {
                            Term = "Cefuroxime",
                            Description = "Cefuroxime"
                          },
                          Lonic = new Code()
                          {
                            Term = "18862-3",
                            Description = "Cefuroxime"
                          }
                        },
                        DataType = "ST",
                        Value = "R",
                        Units = null,
                        ReferenceRange = null,
                        AbnormalFlag = "R",
                        ObservationDateTime = new DateTimeOffset(2020, 01, 24, 14, 40, 00, TimeSpan.FromHours(10)).Subtract(TimeSpan.FromMinutes(5)),
                        Status = ResultStatusType.Final,
                        ChildResultList = null
                      },
                      new Result()
                      {
                        Type = new ResultType()
                        {
                          Local = new Code()
                          {
                            Term = "Gentamicin",
                            Description = "Gentamicin"
                          },
                          Lonic = new Code()
                          {
                            Term = "18928-2",
                            Description = "Gentamicin"
                          }
                        },
                        DataType = "ST",
                        Value = "S",
                        Units = null,
                        ReferenceRange = null,
                        AbnormalFlag = "S",
                        ObservationDateTime = new DateTimeOffset(2020, 01, 24, 14, 40, 00, TimeSpan.FromHours(10)).Subtract(TimeSpan.FromMinutes(5)),
                        Status = ResultStatusType.Final,
                        ChildResultList = null
                      },
                      new Result()
                      {
                        Type = new ResultType()
                        {
                          Local = new Code()
                          {
                            Term = "Co-trimoxazole",
                            Description = "Co-trimoxazole"
                          },
                          Lonic = new Code()
                          {
                            Term = "18998-5",
                            Description = "Co-trimoxazole"
                          }
                        },
                        DataType = "ST",
                        Value = "R",
                        Units = null,
                        ReferenceRange = null,
                        AbnormalFlag = "R",
                        ObservationDateTime = new DateTimeOffset(2020, 01, 24, 14, 40, 00, TimeSpan.FromHours(10)).Subtract(TimeSpan.FromMinutes(5)),
                        Status = ResultStatusType.Final,
                        ChildResultList = null
                      },
                      new Result()
                      {
                        Type = new ResultType()
                        {
                          Local = new Code()
                          {
                            Term = "Trimethoprim",
                            Description = "Trimethoprim"
                          },
                          Lonic = new Code()
                          {
                            Term = "18997-7",
                            Description = "Trimethoprim"
                          }
                        },
                        DataType = "ST",
                        Value = "I",
                        Units = null,
                        ReferenceRange = null,
                        AbnormalFlag = "I",
                        ObservationDateTime = new DateTimeOffset(2020, 01, 24, 14, 40, 00, TimeSpan.FromHours(10)).Subtract(TimeSpan.FromMinutes(5)),
                        Status = ResultStatusType.Final,
                        ChildResultList = null
                      }
                    }
                  },
                  new Result()
                  {
                    Type = new ResultType()
                    {
                      Local = new Code()
                      {
                        Term = "INTERP",
                        Description = "Interpretation"
                      },
                      Lonic = null
                    },
                    DataType = "ST",
                    Value = "Interpretation – Previous treatment with trimethoprim ineffective for recurring UTI.",
                    Units = null,
                    ReferenceRange = null,
                    AbnormalFlag = null,
                    ObservationDateTime = new DateTimeOffset(2020, 01, 24, 14, 40, 00, TimeSpan.FromHours(10)).Subtract(TimeSpan.FromMinutes(5)),
                    Status = ResultStatusType.Final,
                    ChildResultList = null
                  },
                }
              }
            }
          }
        }
      };
    }
    public PathologyReportContainer GetKaryotyping()
    {
      return new PathologyReportContainer()
      {
        PathologyReport = new PathologyReport()
        {
          PerformingLaboratory = LaboratoryFactory.GetPITUSLaboratory(),
          Patient = PatientFactory.GetPatient(PatientType.LarissaFERNIE),
          Request = new Request()
          {
            RequestedDate = new DateTimeOffset(2019, 11, 20, 00, 00, 00, TimeSpan.FromHours(10)),
            OrderNumber = "00000010",
            RequestingFacility = new Organisation()
            {
              Name = "Infertility Clinic",
              Identifier = new Identifier()
              {
                Value = "501970A0-1E1F-40D8-9656-61899527203F",
                Type = IdentifierType.GUID
              }
            },
            RequestingApplication = "Best Practice 1.8.5.743",
            RequestingProvider = ProviderFactory.GetTrishFamilyDr(MedicareProviderNumber: "951577QT"),
            ClinicalNotes = "Primary infertility",
            CallBackPhoneNumber = "07302308594",
            CopyToList = new List<Provider>()
            {
              ProviderFactory.GetGeneticCounsellingClinicCoordinator(),
              ProviderFactory.GetSarsgaardGeneticCounsellor(),
            }
          },
          PdfFileName = "SPIA Exemplar Report Karyotyping v1.4.pdf",
          ReportList = new List<Report>()
          {
            new Report()
            {              
              ReportId = "1978881774",              
              CollectionDateTime = new DateTimeOffset(2019, 11, 29, 10, 40, 00, TimeSpan.FromHours(10)),
              SpecimenReceivedDateTime = new DateTimeOffset(2019, 11, 29, 12, 05, 00, TimeSpan.FromHours(10)),
              ReportReleaseDateTime = new DateTimeOffset(2019, 11, 30, 12, 55, 00, TimeSpan.FromHours(10)),
              ReportType = new ReportType()
              {
                Local = new Code() { Term = "KARYO", Description = "Karyotype analysis" },
                Snomed = new Code() { Term = "1308381000168103", Description = "Whole blood cytogenetic analysis" }
              },
              ReportStatus =  ResultStatusType.Final,
              Department = DiagnosticService.Hematology,
              ReportingPathologist = ProviderFactory.GetKondoPathologist(),              
              Panel = new Panel()
              {                
                ResultList = new List<Result>()
                {
                  new Result()
                  {
                    Type = new ResultType()
                    {
                      Local = new Code()
                      {
                        Term = "KARYO",
                        Description = "Karyotype"
                      },
                      Lonic = null
                    },
                    DataType = "ST",
                    Value = "46,X,del(X)(q11.2)",
                    Units = null,
                    ReferenceRange = null,
                    AbnormalFlag = "N",
                    ObservationDateTime = new DateTimeOffset(2019, 11, 30, 12, 55, 00, TimeSpan.FromHours(10)).Subtract(TimeSpan.FromMinutes(5)),
                    Status = ResultStatusType.Final,
                    ChildResultList = null
                  },
                  new Result()
                  {
                    Type = new ResultType()
                    {
                      Local = new Code()
                      {
                        Term = "RES",
                        Description = "Results"
                      },
                      Lonic = null
                    },
                    DataType = "FT",
                    Value = "An abnormal female result showing a deletion of the long arm of the X-chromosome at band q11.2. This aberration\\.br\\" +
                            "was observed in all cells analysed.\\.br\\" +
                            "GTG banded analysis was performed on 5 cells analysed and 10 cells counted at a resolution of 550 bands.",
                    Units = null,
                    ReferenceRange = null,
                    AbnormalFlag = "N",
                    ObservationDateTime = new DateTimeOffset(2019, 11, 30, 12, 55, 00, TimeSpan.FromHours(10)).Subtract(TimeSpan.FromMinutes(5)),
                    Status = ResultStatusType.Final,
                    ChildResultList = null
                  },
                  new Result()
                  {
                    Type = new ResultType()
                    {
                      Local = new Code()
                      {
                        Term = "INTERP",
                        Description = "Interpretation"
                      },
                      Lonic = null
                    },
                    DataType = "FT",
                    Value = "Deletions of the long arm of the X-chromosome are associated with a variable phenotype in females. Common\\.br\\" +
                            "findings include short stature, gonadal dysgenesis and premature ovarian failure. It is likely that this aberration is the\\.br\\" +
                            "cause of the primary infertility observed in this patient. Genetic counselling is recommended.",
                    Units = null,
                    ReferenceRange = null,
                    AbnormalFlag = "N",
                    ObservationDateTime = new DateTimeOffset(2019, 11, 30, 12, 55, 00, TimeSpan.FromHours(10)).Subtract(TimeSpan.FromMinutes(5)),
                    Status = ResultStatusType.Final,
                    ChildResultList = null
                  }
                }
              }
            }
          }
        }
      };
    }
    public PathologyReportContainer GetLipids()
    {
      return new PathologyReportContainer()
      {
        PathologyReport = new PathologyReport()
        {
          PerformingLaboratory = LaboratoryFactory.GetPITUSLaboratory(),
          Patient = PatientFactory.GetPatient(PatientType.GregoryBLACKCOMB),
          Request = new Request()
          {
            RequestedDate = new DateTimeOffset(2019, 11, 09, 00, 00, 00, TimeSpan.FromHours(10)),
            OrderNumber = "00000011",
            RequestingFacility = new Organisation()
            {
              Name = "Cardiology Clinic",
              Identifier = new Identifier()
              {
                Value = "55023B97-61F8-4445-8590-4F18AD68E9AD",
                Type = IdentifierType.GUID
              }
            },
            RequestingApplication = "Best Practice 1.8.5.743",
            RequestingProvider = ProviderFactory.GetTrishFamilyDr(MedicareProviderNumber: "3243890Y"),
            ClinicalNotes = "Knee surgery 12-Sept-19. Weight gain becoming problematic.",
            CallBackPhoneNumber = null,
            CopyToList = new List<Provider>()
            {
              ProviderFactory.GetCardiologyClinicSunshineHospital(),
              ProviderFactory.GetGBastien()
            }
          },
          PdfFileName = "SPIA Exemplar Report Lipids v1.6.pdf",
          ReportList = new List<Report>()
          {
            new Report()
            {              
              ReportId = "1978881822",              
              CollectionDateTime = new DateTimeOffset(2019, 12, 04, 07, 36, 00, TimeSpan.FromHours(10)),
              SpecimenReceivedDateTime = new DateTimeOffset(2019, 12, 04, 10, 14, 00, TimeSpan.FromHours(10)),
              ReportReleaseDateTime = new DateTimeOffset(2019, 12, 04, 14, 32, 00, TimeSpan.FromHours(10)),
              ReportType = new ReportType()
              {
                Local = new Code() { Term = "LIPIDS", Description = "Lipids" },
                Snomed = new Code() { Term = "252150008", Description = "Fasting lipid profile" }
              },
              ReportStatus =  ResultStatusType.Final,
              Department = DiagnosticService.Chemistry,
              ReportingPathologist = ProviderFactory.GetManuelDelPathologist(),              
              Panel = new Panel()
              {                
                ResultList = new List<Result>()
                {
                  new Result()
                  {
                    Type = new ResultType()
                    {
                      Local = new Code()
                      {
                        Term = "CHOL",
                        Description = "Cholesterol"
                      },
                      Lonic = new Code()
                      {
                        Term = "14647-2",
                        Description = "Cholesterol"
                      }
                    },
                    DataType = "NM",
                    Value = "6.2",
                    Units = "mmol/L",
                    ReferenceRange = "< 5.5",
                    AbnormalFlag = "H",
                    ObservationDateTime = new DateTimeOffset(2019, 12, 04, 14, 32, 00, TimeSpan.FromHours(10)).Subtract(TimeSpan.FromMinutes(5)),
                    Status = ResultStatusType.Final,
                    ChildResultList = null
                  },
                  new Result()
                  {
                    Type = new ResultType()
                    {
                      Local = new Code()
                      {
                        Term = "TRIGF",
                        Description = "Triglycerides fasting"
                      },
                      Lonic = new Code()
                      {
                        Term = "30524-3",
                        Description = "Triglycerides fasting"
                      }
                    },
                    DataType = "NM",
                    Value = "2.0",
                    Units = "mmol/L",
                    ReferenceRange = "< 1.7",
                    AbnormalFlag = "H",
                    ObservationDateTime = new DateTimeOffset(2019, 12, 04, 14, 32, 00, TimeSpan.FromHours(10)).Subtract(TimeSpan.FromMinutes(5)),
                    Status = ResultStatusType.Final,
                    ChildResultList = null
                  },
                  new Result()
                  {
                    Type = new ResultType()
                    {
                      Local = new Code()
                      {
                        Term = "HDL",
                        Description = "HDL cholesterol"
                      },
                      Lonic = new Code()
                      {
                        Term = "14646-4",
                        Description = "HDL cholesterol"
                      }
                    },
                    DataType = "NM",
                    Value = "3.2",
                    Units = "mmol/L",
                    ReferenceRange = "> 1.0",
                    AbnormalFlag = "N",
                    ObservationDateTime = new DateTimeOffset(2019, 12, 04, 14, 32, 00, TimeSpan.FromHours(10)).Subtract(TimeSpan.FromMinutes(5)),
                    Status = ResultStatusType.Final,
                    ChildResultList = null
                  },
                  new Result()
                  {
                    Type = new ResultType()
                    {
                      Local = new Code()
                      {
                        Term = "HDL",
                        Description = "LDL Cholesterol"
                      },
                      Lonic = new Code()
                      {
                        Term = "22748-8",
                        Description = "LDL Cholesterol"
                      }
                    },
                    DataType = "NM",
                    Value = "2.1",
                    Units = "mmol/L",
                    ReferenceRange = "< 3.0",
                    AbnormalFlag = "N",
                    ObservationDateTime = new DateTimeOffset(2019, 12, 04, 14, 32, 00, TimeSpan.FromHours(10)).Subtract(TimeSpan.FromMinutes(5)),
                    Status = ResultStatusType.Final,
                    ChildResultList = null
                  },
                  new Result()
                  {
                    Type = new ResultType()
                    {
                      Local = new Code()
                      {
                        Term = "CHRATIO",
                        Description = "Cholesterol/HDL cholesterol"
                      },
                      Lonic = new Code()
                      {
                        Term = "32309-7",
                        Description = "Chol/HDL Ratio"
                      }
                    },
                    DataType = "NM",
                    Value = "3.0",
                    Units = "mmol/L",
                    ReferenceRange = "< 3.5",
                    AbnormalFlag = "N",
                    ObservationDateTime = new DateTimeOffset(2019, 12, 04, 14, 32, 00, TimeSpan.FromHours(10)).Subtract(TimeSpan.FromMinutes(5)),
                    Status = ResultStatusType.Final,
                    ChildResultList = null
                  },
                  new Result()
                  {
                    Type = new ResultType()
                    {
                      Local = new Code()
                      {
                        Term = "INTERP",
                        Description = "Interpretation"
                      },
                      Lonic = new Code()
                      {
                        Term = "49262-9",
                        Description = "Fatty acids pattern Nar [Interp]"
                      }
                    },
                    DataType = "FT",
                    Value = "Absolute cardiovascular risk assessment should be performed on all adults aged between 45-75\\.br\\" +
                    "years without existing CVD or not already known to be at increased risk of CVD.\\.br\\" +
                    "A CBD risk calculator is provided at www.cvdcheck.org.au.",
                    Units = null,
                    ReferenceRange = null,
                    AbnormalFlag = null,
                    ObservationDateTime = new DateTimeOffset(2019, 12, 04, 14, 32, 00, TimeSpan.FromHours(10)).Subtract(TimeSpan.FromMinutes(5)),
                    Status = ResultStatusType.Final,
                    ChildResultList = null
                  }
                }
              }
            }
          }
        }
      };
    }
    public PathologyReportContainer GetProteinElectrophoresis()
    {
      return new PathologyReportContainer()
      {
        PathologyReport = new PathologyReport()
        {
          PerformingLaboratory = LaboratoryFactory.GetPITUSLaboratory(),
          Patient = PatientFactory.GetPatient(PatientType.GlennFERNIE),
          Request = new Request()
          {
            RequestedDate = new DateTimeOffset(2019, 11, 3, 00, 00, 00, TimeSpan.FromHours(10)),
            OrderNumber = "00000013",
            RequestingFacility = new Organisation()
            {
              Name = "Immunology Clinic",
              Identifier = new Identifier()
              {
                Value = "D220F4D1-D62C-4EE3-8356-F5DA1484370E",
                Type = IdentifierType.GUID
              }
            },
            RequestingApplication = "Best Practice 1.8.5.743",
            RequestingProvider = ProviderFactory.GetTrishFamilyDr(MedicareProviderNumber: "8264815W"),
            ClinicalNotes = "? Multiple myeloma",
            CallBackPhoneNumber = null,
            CopyToList = new List<Provider>()
            {
              ProviderFactory.GetImmunologyClinic(),
              ProviderFactory.GetArmandeImmunologist()
            }
          },
          PdfFileName = "SPIA Exemplar Report Protein SPEP core v1.5.pdf",
          ReportList = new List<Report>()
          {
            new Report()
            {              
              ReportId = "19118881777",              
              CollectionDateTime = new DateTimeOffset(2019, 12, 02, 07, 50, 00, TimeSpan.FromHours(10)),
              SpecimenReceivedDateTime = new DateTimeOffset(2019, 12, 02, 11, 04, 00, TimeSpan.FromHours(10)),
              ReportReleaseDateTime = new DateTimeOffset(2019, 12, 02, 15, 50, 00, TimeSpan.FromHours(10)),
              ReportType = new ReportType()
              {
                Local = new Code() { Term = "PROTELECT", Description = "Protein electrophoresis" },
                Snomed = new Code() { Term = "4903000", Description = "Serum protein electrophoresis" }
              },
              ReportStatus =  ResultStatusType.Final,
              Department = DiagnosticService.Immunology,
              ReportingPathologist = ProviderFactory.GetBertramPathologist(),              
              Panel = new Panel()
              {                
                ResultList = new List<Result>()
                {
                  new Result()
                  {
                    Type = new ResultType()
                    {
                      Local = new Code()
                      {
                        Term = "PROT",
                        Description = "Total Protein"
                      },
                      Lonic = new Code()
                      {
                        Term = "2885-2",
                        Description = "Total Protein"
                      }
                    },
                    DataType = "NM",
                    Value = "78",
                    Units = "g/L",
                    ReferenceRange = "35-175",
                    AbnormalFlag = "N",
                    ObservationDateTime = new DateTimeOffset(2019, 12, 02, 15, 50, 00, TimeSpan.FromHours(10)).Subtract(TimeSpan.FromMinutes(5)),
                    Status = ResultStatusType.Final,
                    ChildResultList = null
                  },
                  new Result()
                  {
                    Type = new ResultType()
                    {
                      Local = new Code()
                      {
                        Term = "ALB",
                        Description = "Albumin"
                      },
                      Lonic = new Code()
                      {
                        Term = "2862-1",
                        Description = "Albumin"
                      }
                    },
                    DataType = "NM",
                    Value = "22",
                    Units = "g/L",
                    ReferenceRange = "20-42",
                    AbnormalFlag = "N",
                    ObservationDateTime = new DateTimeOffset(2019, 12, 02, 15, 50, 00, TimeSpan.FromHours(10)).Subtract(TimeSpan.FromMinutes(5)),
                    Status = ResultStatusType.Final,
                    ChildResultList = null
                  },
                  new Result()
                  {
                    Type = new ResultType()
                    {
                      Local = new Code()
                      {
                        Term = "Globulin",
                        Description = "Globulin"
                      },
                      Lonic = new Code()
                      {
                        //ToDo: Do we need a Lonic term for Globulin in the Protein SPEP core report
                        Term = "??",
                        Description = "Globulin"
                      }
                    },
                    DataType = "NM",
                    Value = "61",
                    Units = "g/L",
                    ReferenceRange = "60-74",
                    AbnormalFlag = "N",
                    ObservationDateTime = new DateTimeOffset(2019, 12, 02, 15, 50, 00, TimeSpan.FromHours(10)).Subtract(TimeSpan.FromMinutes(5)),
                    Status = ResultStatusType.Final,
                    ChildResultList = null
                  },
                  new Result()
                  {
                    Type = new ResultType()
                    {
                      Local = new Code()
                      {
                        Term = "PARAP",
                        Description = "Paraprotein"
                      },
                      Lonic = new Code()
                      {
                        //ToDo: Do we need a Lonic term for Globulin in the Protein SPEP core report
                        Term = "??",
                        Description = "Paraprotein"
                      }
                    },
                    DataType = "ST",
                    Value = "Detected",
                    Units = null,
                    ReferenceRange = null,
                    AbnormalFlag = "N",
                    ObservationDateTime = new DateTimeOffset(2019, 12, 02, 15, 50, 00, TimeSpan.FromHours(10)).Subtract(TimeSpan.FromMinutes(5)),
                    Status = ResultStatusType.Final,
                    ChildResultList = null
                  },
                  new Result()
                  {
                    Type = new ResultType()
                    {
                      Local = new Code()
                      {
                        Term = "ALPHAGLOB",
                        Description = "Alpha globulin"
                      },
                      Lonic = new Code()
                      {
                        //ToDo: Do we need a Lonic term for Alpha globulin in the Protein SPEP core report
                        Term = "??",
                        Description = "Alpha globulin"
                      }
                    },
                    DataType = "NM",
                    Value = "5",
                    Units = "g/L",
                    ReferenceRange = "2-6",
                    AbnormalFlag = "N",
                    ObservationDateTime = new DateTimeOffset(2019, 12, 02, 15, 50, 00, TimeSpan.FromHours(10)).Subtract(TimeSpan.FromMinutes(5)),
                    Status = ResultStatusType.Final,
                    ChildResultList = null
                  },
                  new Result()
                  {
                    Type = new ResultType()
                    {
                      Local = new Code()
                      {
                        Term = "BETAGLOB",
                        Description = "Beta globulin"
                      },
                      Lonic = new Code()
                      {
                        //ToDo: Do we need a Lonic term for Beta globulin in the Protein SPEP core report
                        Term = "??",
                        Description = "Beta globulin"
                      }
                    },
                    DataType = "NM",
                    Value = "5",
                    Units = "g/L",
                    ReferenceRange = "2-6",
                    AbnormalFlag = "N",
                    ObservationDateTime = new DateTimeOffset(2019, 12, 02, 15, 50, 00, TimeSpan.FromHours(10)).Subtract(TimeSpan.FromMinutes(5)),
                    Status = ResultStatusType.Final,
                    ChildResultList = null
                  },
                  new Result()
                  {
                    Type = new ResultType()
                    {
                      Local = new Code()
                      {
                        Term = "GAMMA",
                        Description = "Gamma globulin"
                      },
                      Lonic = new Code()
                      {
                        Term = "2874-6",
                        Description = "Gamma"
                      }
                    },
                    DataType = "NM",
                    Value = "7",
                    Units = "g/L",
                    ReferenceRange = "6-15",
                    AbnormalFlag = "N",
                    ObservationDateTime = new DateTimeOffset(2019, 12, 02, 15, 50, 00, TimeSpan.FromHours(10)).Subtract(TimeSpan.FromMinutes(5)),
                    Status = ResultStatusType.Final,
                    ChildResultList = null
                  },
                  new Result()
                  {
                    Type = new ResultType()
                    {
                      Local = new Code()
                      {
                        Term = "IMMGLOB",
                        Description = "Immunoglobulins"
                      },
                      Lonic = new Code()
                      {
                        //ToDo: Do we need a Lonic term for Immunoglobulins in the Protein SPEP core report
                        Term = "??",
                        Description = "Immunoglobulins"
                      }
                    },
                    DataType = "ST",
                    Value = null,
                    Units = null,
                    ReferenceRange = null,
                    AbnormalFlag = null,
                    ObservationDateTime = new DateTimeOffset(2019, 12, 02, 15, 50, 00, TimeSpan.FromHours(10)).Subtract(TimeSpan.FromMinutes(5)),
                    Status = ResultStatusType.Final,
                    ChildResultList = new List<Result>()
                    {
                      new Result()
                      {
                        Type = new ResultType()
                        {
                          Local = new Code()
                          {
                            Term = "IGGMON",
                            Description = "IgG monoclonal"
                          },
                          Lonic = new Code()
                          {
                            //ToDo: Do we need a Lonic term for IgG monoclonal in the Protein SPEP core report
                            Term = "??",
                            Description = "IgG monoclonal"
                          }
                        },
                        DataType = "NM",
                        Value = "4",
                        Units = "g/L",
                        ReferenceRange = "10-40",
                        AbnormalFlag = "L",
                        ObservationDateTime = new DateTimeOffset(2019, 12, 02, 15, 50, 00, TimeSpan.FromHours(10)).Subtract(TimeSpan.FromMinutes(5)),
                        Status = ResultStatusType.Final,
                        ChildResultList = null
                      },
                      new Result()
                      {
                        Type = new ResultType()
                        {
                          Local = new Code()
                          {
                            Term = "IGAMON",
                            Description = "IgA monoclonal"
                          },
                          Lonic = new Code()
                          {
                            //ToDo: Do we need a Lonic term for IgA monoclonal in the Protein SPEP core report
                            Term = "??",
                            Description = "IgA monoclonal"
                          }
                        },
                        DataType = "NM",
                        Value = "7.0",
                        Units = "g/L",
                        ReferenceRange = "0.5-9.9",
                        AbnormalFlag = "N",
                        ObservationDateTime = new DateTimeOffset(2019, 12, 02, 15, 50, 00, TimeSpan.FromHours(10)).Subtract(TimeSpan.FromMinutes(5)),
                        Status = ResultStatusType.Final,
                        ChildResultList = null
                      },
                      new Result()
                      {
                        Type = new ResultType()
                        {
                          Local = new Code()
                          {
                            Term = "IGMMON",
                            Description = "IgM monoclonal"
                          },
                          Lonic = new Code()
                          {
                            //ToDo: Do we need a Lonic term for IgM monoclonal in the Protein SPEP core report
                            Term = "??",
                            Description = "IgM monoclonal"
                          }
                        },
                        DataType = "NM",
                        Value = "2.0",
                        Units = "g/L",
                        ReferenceRange = "0.2-3.9",
                        AbnormalFlag = "N",
                        ObservationDateTime = new DateTimeOffset(2019, 12, 02, 15, 50, 00, TimeSpan.FromHours(10)).Subtract(TimeSpan.FromMinutes(5)),
                        Status = ResultStatusType.Final,
                        ChildResultList = null
                      }
                    }
                  },                  
                  new Result()
                  {
                    Type = new ResultType()
                    {
                      Local = new Code()
                      {
                        Term = "INTERP",
                        Description = "Protein electrophoresis pattern comments"
                      },
                      Lonic = new Code()
                      {
                        Term = "49298-3",
                        Description = "Protein Fractions Nar [Interp]"
                      }
                    },
                    DataType = "FT",
                    Value = "Decreased residual gammaglobulins. Suggest urine protein electrophoresis and immunofixation.",
                    Units = null,
                    ReferenceRange = null,
                    AbnormalFlag = null,
                    ObservationDateTime = new DateTimeOffset(2019, 12, 02, 15, 50, 00, TimeSpan.FromHours(10)).Subtract(TimeSpan.FromMinutes(5)),
                    Status = ResultStatusType.Final,
                    ChildResultList = null
                  }
                }
              }
            }
          }
        }
      };
    }
    public PathologyReportContainer GetBloodGasArterial()
    {
      return new PathologyReportContainer()
      {
        PathologyReport = new PathologyReport()
        {
          PerformingLaboratory = LaboratoryFactory.GetPITUSLaboratory(),
          Patient = PatientFactory.GetPatient(PatientType.GlennFERNIE),
          Request = new Request()
          {
            RequestedDate = new DateTimeOffset(2019, 11, 09, 00, 00, 00, TimeSpan.FromHours(10)),
            OrderNumber = "00000014",
            RequestingFacility = new Organisation()
            {
              Name = "Sunshine Hospital Emergency Dept",
              Identifier = new Identifier()
              {
                Value = "143569C9-8AFC-4BBD-A663-95079AE10B57",
                Type = IdentifierType.GUID
              }
            },
            RequestingApplication = "Best Practice 1.8.5.743",
            RequestingProvider = ProviderFactory.GetTrishFamilyDr(MedicareProviderNumber: "873721DH"),
            ClinicalNotes = "Recent history uncontrolled diabetes; Shortness of breath",
            CallBackPhoneNumber = null,
            CopyToList = new List<Provider>()
            {
              ProviderFactory.GetDiabetesClinicSunshineHospital(),
              ProviderFactory.GetTrishFamilyDr("930481AT")
            }
          },
          PdfFileName = "SPIA Exemplar Report Blood Gas Arterial & Venous v0.8.pdf",
          ReportList = new List<Report>()
          {
            new Report()
            {              
              ReportId = "1978881828",
              CollectionDateTime = new DateTimeOffset(2019, 11, 09, 07, 40, 00, TimeSpan.FromHours(10)),
              SpecimenReceivedDateTime = new DateTimeOffset(2019, 12, 09, 11, 00, 00, TimeSpan.FromHours(10)),
              ReportReleaseDateTime = new DateTimeOffset(2019, 11, 09, 07, 50, 00, TimeSpan.FromHours(10)),
              ReportType = new ReportType()
              {
                //As of meeting on 10/11/2020 @ 2:00PM chnaged this to only Blood gases
                Local = new Code() { Term = "BGAS", Description = "Blood gases" },
                Snomed = null
                //Local = new Code() { Term = "BGASA", Description = "Blood gas arterial" },
                //Snomed = new Code() { Term = "91308007", Description = "Blood gases, arterial measurement" }
              },
              ReportStatus =  ResultStatusType.Final,
              Department = DiagnosticService.Chemistry,
              ReportingPathologist = ProviderFactory.GetMarioPathologist(),              
              Panel = new Panel()
              {                
                ResultList = new List<Result>()
                {
                  new Result()
                  {
                    Type = new ResultType()
                    {
                      Local = new Code()
                      {
                        Term = "OIF",                        
                        Description = "Oxygen inspired fraction"
                      },
                      Lonic = new Code()
                      {                        
                        Term = "3151-8",
                        Description = "Oxygen inspired fraction"
                      }
                    },
                    DataType = "NM",
                    Value = "0.5",
                    //Confirmed that units are not required Meeting on the 10/11/2020 @ 2:00PM
                    Units = null,
                    ReferenceRange = null,
                    AbnormalFlag = "N",
                    ObservationDateTime = new DateTimeOffset(2019, 11, 09, 07, 50, 00, TimeSpan.FromHours(10)).Subtract(TimeSpan.FromMinutes(5)),
                    Status = ResultStatusType.Final,
                    ChildResultList = null
                  },
                  new Result()
                  {
                    Type = new ResultType()
                    {
                      Local = new Code()
                      {
                        Term = "BTEMP",
                        Description = "Body temperature"
                      },
                      Lonic = new Code()
                      {
                        Term = "8310-5",
                        Description = "Body temperature"
                      }
                    },
                    DataType = "NM",
                    Value = "37.2",
                    Units = "Cel",
                    ReferenceRange = "36.5-37.5",
                    AbnormalFlag = "N",
                    ObservationDateTime = new DateTimeOffset(2019, 11, 09, 07, 50, 00, TimeSpan.FromHours(10)).Subtract(TimeSpan.FromMinutes(5)),
                    Status = ResultStatusType.Final,
                    ChildResultList = null
                  },
                  new Result()
                  {
                    Type = new ResultType()
                    {
                      Local = new Code()
                      {
                        Term = "PHART",
                        Description = "pH arterial"
                      },
                      Lonic = new Code()
                      {
                        Term = "2744-1",
                        Description = "pH arterial"
                      }
                    },
                    DataType = "NM",
                    Value = "7.50",
                    Units = "pH",
                    ReferenceRange = "7.35-7.45",
                    AbnormalFlag = "H",
                    ObservationDateTime = new DateTimeOffset(2019, 11, 09, 07, 50, 00, TimeSpan.FromHours(10)).Subtract(TimeSpan.FromMinutes(5)),
                    Status = ResultStatusType.Final,
                    ChildResultList = null
                  },                  
                  new Result()
                  {
                    Type = new ResultType()
                    {
                      Local = new Code()
                      {
                        Term = "PCO2",
                        Description = "pCO2 arterial"
                      },
                      Lonic = new Code()
                      {
                        Term = "2019-8",
                        Description = "pCO2 arterial"
                      }
                    },
                    DataType = "NM",
                    Value = "30",
                    Units = "mmHg",
                    ReferenceRange = "32-45",
                    AbnormalFlag = "L",
                    ObservationDateTime = new DateTimeOffset(2019, 11, 09, 07, 50, 00, TimeSpan.FromHours(10)).Subtract(TimeSpan.FromMinutes(5)),
                    Status = ResultStatusType.Final,
                    ChildResultList = null
                  },
                  new Result()
                  {
                    Type = new ResultType()
                    {
                      Local = new Code()
                      {
                        Term = "PO2A",                        
                        Description = "pO2 arterial??"
                      },
                      Lonic = new Code()
                      {
                        Term = "2019-8",
                        Description = "pO2 arterial???"
                      }
                    },
                    DataType = "NM",
                    Value = "61",
                    Units = "mmHg",
                    ReferenceRange = "83-108",
                    AbnormalFlag = "L",
                    ObservationDateTime = new DateTimeOffset(2019, 11, 09, 07, 50, 00, TimeSpan.FromHours(10)).Subtract(TimeSpan.FromMinutes(5)),
                    Status = ResultStatusType.Final,
                    ChildResultList = null
                  },
                  new Result()
                  {
                    Type = new ResultType()
                    {
                      Local = new Code()
                      {
                        Term = "OXYCA",                        
                        Description = "Oxygen content arterial"
                      },
                      Lonic = new Code()
                      {
                        Term = "19218-7",
                        Description = "Oxygen content arterial"
                      }
                    },
                    DataType = "NM",
                    Value = "89",
                    Units = "mol/L",
                    ReferenceRange = "94-98",
                    AbnormalFlag = "L",
                    ObservationDateTime = new DateTimeOffset(2019, 11, 09, 07, 50, 00, TimeSpan.FromHours(10)).Subtract(TimeSpan.FromMinutes(5)),
                    Status = ResultStatusType.Final,
                    ChildResultList = null
                  },
                  new Result()
                  {
                    Type = new ResultType()
                    {
                      Local = new Code()
                      {
                        Term = "BICART",                   
                        Description = "Bicarbonate arterial"
                      },
                      Lonic = new Code()
                      {
                        Term = "1960-4",
                        Description = "Bicarbonate arterial"
                      }
                    },
                    DataType = "NM",
                    Value = "27",
                    Units = "mmol/L",
                    ReferenceRange = "22-31",
                    AbnormalFlag = "L",
                    ObservationDateTime = new DateTimeOffset(2019, 11, 09, 07, 50, 00, TimeSpan.FromHours(10)).Subtract(TimeSpan.FromMinutes(5)),
                    Status = ResultStatusType.Final,
                    ChildResultList = null
                  },
                  new Result()
                  {
                    Type = new ResultType()
                    {
                      Local = new Code()
                      {
                        Term = "BEART",
                        Description = "Base excess arterial"
                      },
                      Lonic = new Code()
                      {
                        Term = "1925-7",
                        Description = "Base excess arterial"
                      }
                    },
                    DataType = "NM",
                    Value = "4.1",
                    Units = "mmol/L",
                    ReferenceRange = "-3.0-3.0",
                    AbnormalFlag = "H",
                    ObservationDateTime = new DateTimeOffset(2019, 11, 09, 07, 50, 00, TimeSpan.FromHours(10)).Subtract(TimeSpan.FromMinutes(5)),
                    Status = ResultStatusType.Final,
                    ChildResultList = null
                  },
                  new Result()
                  {
                    Type = new ResultType()
                    {
                      Local = new Code()
                      {
                        Term = "OXSART",
                        Description = "Oxygen saturation arterial"
                      },
                      Lonic = new Code()
                      {
                        Term = "51733-4 ",
                        Description = "Oxygen saturation arterial"
                      }
                    },
                    DataType = "NM",
                    Value = "92",
                    Units = "%",
                    ReferenceRange = "94-98",
                    AbnormalFlag = "L",
                    ObservationDateTime = new DateTimeOffset(2019, 11, 09, 07, 50, 00, TimeSpan.FromHours(10)).Subtract(TimeSpan.FromMinutes(5)),
                    Status = ResultStatusType.Final,
                    ChildResultList = null
                  },
                  new Result()
                  {
                    Type = new ResultType()
                    {
                      Local = new Code()
                      {
                        Term = "OXSART",
                        Description = "Sodium blood"
                      },
                      Lonic = new Code()
                      {
                        Term = "2947-0",
                        Description = "Sodium blood"
                      }
                    },
                    DataType = "NM",
                    Value = "137",
                    Units = "mmol/L",
                    ReferenceRange = "135-145",
                    AbnormalFlag = "L",
                    ObservationDateTime = new DateTimeOffset(2019, 11, 09, 07, 50, 00, TimeSpan.FromHours(10)).Subtract(TimeSpan.FromMinutes(5)),
                    Status = ResultStatusType.Final,
                    ChildResultList = null
                  },
                  new Result()
                  {
                    Type = new ResultType()
                    {
                      Local = new Code()
                      {
                        Term = "KBLOOD",
                        Description = "Potassium blood"
                      },
                      Lonic = new Code()
                      {
                        Term = "6298-4",
                        Description = "Potassium blood"
                      }
                    },
                    DataType = "NM",
                    Value = "3.5",
                    Units = "mmol/L",
                    ReferenceRange = "3.5-5.2",
                    AbnormalFlag = "N",
                    ObservationDateTime = new DateTimeOffset(2019, 11, 09, 07, 50, 00, TimeSpan.FromHours(10)).Subtract(TimeSpan.FromMinutes(5)),
                    Status = ResultStatusType.Final,
                    ChildResultList = null
                  },
                  new Result()
                  {
                    Type = new ResultType()
                    {
                      Local = new Code()
                      {
                        Term = "CLBLOOD",
                        Description = "Chloride blood"
                      },
                      Lonic = new Code()
                      {
                        Term = "2069-3",
                        Description = "Chloride blood"
                      }
                    },
                    DataType = "NM",
                    Value = "103",
                    Units = "mmol/L",
                    ReferenceRange = "95-110",
                    AbnormalFlag = "N",
                    ObservationDateTime = new DateTimeOffset(2019, 11, 09, 07, 50, 00, TimeSpan.FromHours(10)).Subtract(TimeSpan.FromMinutes(5)),
                    Status = ResultStatusType.Final,
                    ChildResultList = null
                  },
                  new Result()
                  {
                    Type = new ResultType()
                    {
                      Local = new Code()
                      {
                        Term = "UREAB",
                        Description = "Urea blood"
                      },
                      Lonic = new Code()
                      {
                        Term = "72903-8",
                        Description = "Urea blood"
                      }
                    },
                    DataType = "NM",
                    Value = "4.9",
                    Units = "mmol/L",
                    ReferenceRange = "3.8-8.5",
                    AbnormalFlag = "N",
                    ObservationDateTime = new DateTimeOffset(2019, 11, 09, 07, 50, 00, TimeSpan.FromHours(10)).Subtract(TimeSpan.FromMinutes(5)),
                    Status = ResultStatusType.Final,
                    ChildResultList = null
                  },
                  new Result()
                  {
                    Type = new ResultType()
                    {
                      Local = new Code()
                      {
                        Term = "CREATB",
                        Description = "Creatinine blood"
                      },
                      Lonic = new Code()
                      {
                        Term = "59826-8",
                        Description = "Creatinine blood"
                      }
                    },
                    DataType = "NM",
                    Value = "61",
                    Units = "umol/L",
                    ReferenceRange = "60-110",
                    AbnormalFlag = "N",
                    ObservationDateTime = new DateTimeOffset(2019, 11, 09, 07, 50, 00, TimeSpan.FromHours(10)).Subtract(TimeSpan.FromMinutes(5)),
                    Status = ResultStatusType.Final,
                    ChildResultList = null
                  },
                  new Result()
                  {
                    Type = new ResultType()
                    {
                      Local = new Code()
                      {
                        Term = "AGAPB",
                        Description = "Anion gap blood"
                      },
                      Lonic = new Code()
                      {
                        Term = "41276-7",
                        Description = "Anion gap blood"
                      }
                    },
                    DataType = "NM",
                    Value = "7",
                    Units = "mmol/L",
                    ReferenceRange = "4-13",
                    AbnormalFlag = "N",
                    ObservationDateTime = new DateTimeOffset(2019, 11, 09, 07, 50, 00, TimeSpan.FromHours(10)).Subtract(TimeSpan.FromMinutes(5)),
                    Status = ResultStatusType.Final,
                    ChildResultList = null
                  },
                  new Result()
                  {
                    Type = new ResultType()
                    {
                      Local = new Code()
                      {
                        Term = "GLUCB",
                        Description = "Glucose blood"
                      },
                      Lonic = new Code()
                      {
                        Term = "15074-8",
                        Description = "Glucose blood"
                      }
                    },
                    DataType = "NM",
                    Value = "9.1",
                    Units = "mmol/L",
                    ReferenceRange = "3.0-7.8",
                    AbnormalFlag = "H",
                    ObservationDateTime = new DateTimeOffset(2019, 11, 09, 07, 50, 00, TimeSpan.FromHours(10)).Subtract(TimeSpan.FromMinutes(5)),
                    Status = ResultStatusType.Final,
                    ChildResultList = null
                  },
                  new Result()
                  {
                    Type = new ResultType()
                    {
                      Local = new Code()
                      {
                        Term = "LACTB",
                        Description = "Lactate blood "
                      },
                      Lonic = new Code()
                      {
                        Term = "32693-4",
                        Description = "Lactate blood "
                      }
                    },
                    DataType = "NM",
                    Value = "0.8",
                    Units = "mmol/L",
                    ReferenceRange = "< 1.0",
                    AbnormalFlag = "N",
                    ObservationDateTime = new DateTimeOffset(2019, 11, 09, 07, 50, 00, TimeSpan.FromHours(10)).Subtract(TimeSpan.FromMinutes(5)),
                    Status = ResultStatusType.Final,
                    ChildResultList = null
                  },
                  new Result()
                  {
                    Type = new ResultType()
                    {
                      Local = new Code()
                      {
                        Term = "CAIB",
                        Description = "Calcium ionised blood"
                      },
                      Lonic = new Code()
                      {
                        Term = "1994-3",
                        Description = "Calcium ionised blood"
                      }
                    },
                    DataType = "NM",
                    Value = "1.15",
                    Units = "mmol/L",
                    ReferenceRange = "1.15-1.32",
                    AbnormalFlag = "N",
                    ObservationDateTime = new DateTimeOffset(2019, 11, 09, 07, 50, 00, TimeSpan.FromHours(10)).Subtract(TimeSpan.FromMinutes(5)),
                    Status = ResultStatusType.Final,
                    ChildResultList = null
                  },
                  new Result()
                  {
                    Type = new ResultType()
                    {
                      Local = new Code()
                      {
                        Term = "HBCALC",
                        Description = "Haemoglobin calculated"
                      },
                      Lonic = new Code()
                      {
                        Term = "20509-6",
                        Description = "Haemoglobin calculated"
                      }
                    },
                    DataType = "NM",
                    Value = "148",
                    Units = "g/L",
                    ReferenceRange = "135-180",
                    AbnormalFlag = "N",
                    ObservationDateTime = new DateTimeOffset(2019, 11, 09, 07, 50, 00, TimeSpan.FromHours(10)).Subtract(TimeSpan.FromMinutes(5)),
                    Status = ResultStatusType.Final,
                    ChildResultList = null
                  },
                  new Result()
                  {
                    Type = new ResultType()
                    {
                      Local = new Code()
                      {
                        Term = "OXYHBART",
                        Description = "Oxyhaemoglobin arterial"
                      },
                      Lonic = new Code()
                      {
                        Term = "2714-4",
                        Description = "Oxyhaemoglobin arterial"
                      }
                    },
                    DataType = "NM",
                    Value = "98",
                    Units = "%",
                    ReferenceRange = "94-98",
                    AbnormalFlag = "N",
                    ObservationDateTime = new DateTimeOffset(2019, 11, 09, 07, 50, 00, TimeSpan.FromHours(10)).Subtract(TimeSpan.FromMinutes(5)),
                    Status = ResultStatusType.Final,
                    ChildResultList = null
                  },
                  new Result()
                  {
                    Type = new ResultType()
                    {
                      Local = new Code()
                      {
                        Term = "CARBHB",
                        Description = "Carboxyhaemoglobin"
                      },
                      Lonic = new Code()
                      {
                        Term = "20563-3",
                        Description = "Carboxyhaemoglobin"
                      }
                    },
                    DataType = "SN",
                    Value = "<^0.2",
                    Units = "%",
                    ReferenceRange = "< 1.5",
                    AbnormalFlag = "N",
                    ObservationDateTime = new DateTimeOffset(2019, 11, 09, 07, 50, 00, TimeSpan.FromHours(10)).Subtract(TimeSpan.FromMinutes(5)),
                    Status = ResultStatusType.Final,
                    ChildResultList = null
                  },
                  new Result()
                  {
                    Type = new ResultType()
                    {
                      Local = new Code()
                      {
                        Term = "METHHB",
                        Description = "Methaemoglobin"
                      },
                      Lonic = new Code()
                      {
                        Term = "2614-6",
                        Description = "Methaemoglobin"
                      }
                    },
                    DataType = "NM",
                    Value = "0.2",
                    Units = "%",
                    ReferenceRange = "< 1.1",
                    AbnormalFlag = "N",
                    ObservationDateTime = new DateTimeOffset(2019, 11, 09, 07, 50, 00, TimeSpan.FromHours(10)).Subtract(TimeSpan.FromMinutes(5)),
                    Status = ResultStatusType.Final,
                    ChildResultList = null
                  },
                  new Result()
                  {
                    Type = new ResultType()
                    {
                      Local = new Code()
                      {
                        Term = "INTERP",
                        Description = "Interpretation"
                      },
                      Lonic = null,
                    },
                    DataType = "FT",
                    Value = "Chemistry parameters are indicative of respiratory alkalosis.",
                    Units = null,
                    ReferenceRange = null,
                    AbnormalFlag = null,
                    ObservationDateTime = new DateTimeOffset(2019, 11, 09, 07, 50, 00, TimeSpan.FromHours(10)).Subtract(TimeSpan.FromMinutes(5)),
                    Status = ResultStatusType.Final,
                    ChildResultList = null
                  }
                }
              }
            }
          }
        }
      };
    }
    public PathologyReportContainer GetSARSCoV2NAT()
    {
      return new PathologyReportContainer()
      {
        PathologyReport = new PathologyReport()
        {
          PerformingLaboratory = LaboratoryFactory.GetPITUSLaboratory(),
          Patient = PatientFactory.GetPatient(PatientType.HaydenNORQUAY),
          Request = new Request()
          {
            RequestedDate = new DateTimeOffset(2020, 08, 20, 00, 00, 00, TimeSpan.FromHours(10)),
            OrderNumber = "00000015",
            RequestingFacility = new Organisation()
            {
              Name = "Fever Clinic Sunrise Hospital",
              Identifier = new Identifier()
              {
                Value = "3659F40F-8BDF-4CD6-BF46-38257CA6BB97",
                Type = IdentifierType.GUID
              }
            },
            RequestingApplication = "Best Practice 1.8.5.743",
            RequestingProvider = ProviderFactory.GetTrishFamilyDr(MedicareProviderNumber: "603107KW"),
            ClinicalNotes = "Sore throat & cough, works in hospitality industry",
            CallBackPhoneNumber = null,
            CopyToList = new List<Provider>()
            {
              ProviderFactory.GetMySpecialist()
            }
          },
          PdfFileName = "SPIA Exemplar Report SARS-CoV-2 NAT v0.4.pdf",
          ReportList = new List<Report>()
          {
            new Report()
            {              
              ReportId = "2078881879",              
              CollectionDateTime = new DateTimeOffset(2020, 08, 20, 11, 20, 00, TimeSpan.FromHours(10)),
              SpecimenReceivedDateTime = new DateTimeOffset(2020, 08, 20, 13, 24, 00, TimeSpan.FromHours(10)),
              ReportReleaseDateTime = new DateTimeOffset(2020, 08, 20, 20, 28, 00, TimeSpan.FromHours(10)),
              ReportType = new ReportType()
              {
                Local = new Code() { Term = "COVID2", Description = "SARS-CoV-2 nucleic acid" },
                Snomed = new Code() { Term = "1445431000168101", Description = "Oropharyngeal swab COVID-19 nucleic acid assay" }
              },
              ReportStatus =  ResultStatusType.Final,
              Department = DiagnosticService.Microbiology,
              ReportingPathologist = ProviderFactory.GetBellaPathologist(),              
              Panel = new Panel()
              {                
                ResultList = new List<Result>()
                {
                  new Result()
                  {
                    Type = new ResultType()
                    {
                      Local = new Code()
                      {
                        Term = "COVI2DRNA",
                        Description = "SARS-CoV-2 RNA"
                      },
                      Lonic = new Code()
                      {
                        Term = "94309-2",
                        Description = "SARS-CoV-2 (COVID-19) RNA NAA+probe Ql (Unsp spec)"
                      }
                    },
                    DataType = "ST",
                    Value = "Not Detected",
                    Units = null,
                    ReferenceRange = null,
                    AbnormalFlag = "N",
                    ObservationDateTime = new DateTimeOffset(2020, 08, 20, 20, 28, 00, TimeSpan.FromHours(10)).Subtract(TimeSpan.FromMinutes(5)),
                    Status = ResultStatusType.Final,
                    ChildResultList = null
                  },
                  new Result()
                  {
                    Type = new ResultType()
                    {
                      Local = new Code()
                      {
                        Term = "INTERP",
                        Description = "Interpretation"
                      },
                      Lonic = null,
                    },
                    DataType = "FT",
                    Value = "Failure to detect virus specific nucleic acid does not always exclude SARS-CoV-2. Repeat testing\\.br\\" +
                            "including a sputum sample if available, may be indicated if clinical suspicion is high.\\.br\\" +
                            "This assay is designed to detect the E gene of SARS-CoV-2, the causative agent of COVID-19 using\\.br\\" +
                            "nucleic acid amplification. On occasions, a second assay targeting the N gene is also utilised.\\.br\\" +
                            "\\.br\\" +
                            "For further requires regarding these results, please contact the Medical Virologist on (07) 5454 0387.\\.br\\" +
                            "Testing for SARS-CoV-2 is notifiable on request irrespective of test results.\\.br\\",
                    Units = null,
                    ReferenceRange = null,
                    AbnormalFlag = null,
                    ObservationDateTime = new DateTimeOffset(2020, 08, 20, 20, 28, 00, TimeSpan.FromHours(10)).Subtract(TimeSpan.FromMinutes(5)),
                    Status = ResultStatusType.Final,
                    ChildResultList = null
                  }
                }
              }
            }
          }
        }
      };
    }
    public PathologyReportContainer GetSARSCoV2Serology()
    {
      return new PathologyReportContainer()
      {
        PathologyReport = new PathologyReport()
        {
          PerformingLaboratory = LaboratoryFactory.GetPITUSLaboratory(),
          Patient = PatientFactory.GetPatient(PatientType.GregoryBLACKCOMB),
          Request = new Request()
          {
            RequestedDate = new DateTimeOffset(2020, 7, 24, 00, 00, 00, TimeSpan.FromHours(10)),
            OrderNumber = "00000016",
            RequestingFacility = new Organisation()
            {
              Name = "Respiratory Clinic Sunrise Hospital",
              Identifier = new Identifier()
              {
                Value = "13A8DC14-A1E9-475C-9B4C-DA19866E020A",
                Type = IdentifierType.GUID
              }
            },
            RequestingApplication = "Best Practice 1.8.5.743",
            RequestingProvider = ProviderFactory.GetTrishFamilyDr(MedicareProviderNumber: "067709AH"),
            ClinicalNotes = "Shortness of breath, sore throat",
            CallBackPhoneNumber = null,
            CopyToList = new List<Provider>()
            {
              ProviderFactory.GetMyPhysio()
            }
          },
          PdfFileName = "SPIA Exemplar Report SARS-CoV-2 serology v0.3.pdf",
          ReportList = new List<Report>()
          {
            new Report()
            {              
              ReportId = "1978881814",              
              CollectionDateTime = new DateTimeOffset(2020, 7, 25, 08, 20, 00, TimeSpan.FromHours(10)),
              SpecimenReceivedDateTime = new DateTimeOffset(2020, 7, 25, 10, 02, 00, TimeSpan.FromHours(10)),
              ReportReleaseDateTime = new DateTimeOffset(2020, 07, 25, 19, 20, 00, TimeSpan.FromHours(10)),
              ReportType = new ReportType()
              {
                Local = new Code() { Term = "COVID2SER", Description = "SARS-CoV-2 Serology" },
                Snomed = new Code() { Term = "1454651000168108", Description = "SARS-CoV-2 serology" }
              },
              ReportStatus =  ResultStatusType.Final,
              Department = DiagnosticService.Serology,
              ReportingPathologist = ProviderFactory.GetBellaPathologist(),              
              Panel = new Panel()
              {                
                ResultList = new List<Result>()
                {
                  new Result()
                  {
                    Type = new ResultType()
                    {
                      Local = new Code()
                      {
                        Term = "COVID2IGA",
                        Description = "SARS-CoV-2 IgA IF"
                      },
                      Lonic = new Code()
                      {
                        Term = "95427-1",
                        Description = "SARS-CoV-2 (COVID-19) IgA IF [Titer]"
                      }
                    },
                    DataType = "NM",
                    Value = "20",
                    //ToDo: Need units or answer as to whether they are needed
                    Units = "??",
                    //ToDo: Need units or answer as to whether they are needed
                    ReferenceRange = "??",
                    AbnormalFlag = "N",
                    ObservationDateTime = new DateTimeOffset(2020, 07, 25, 19, 20, 00, TimeSpan.FromHours(10)).Subtract(TimeSpan.FromMinutes(5)),
                    Status = ResultStatusType.Final,
                    ChildResultList = null
                  },
                  new Result()
                  {
                    Type = new ResultType()
                    {
                      Local = new Code()
                      {
                        Term = "COVID2IGM",
                        Description = "SARS-CoV-2 IgM IF"
                      },
                      Lonic = new Code()
                      {
                        Term = "95429-7",
                        Description = "SARS-CoV-2 (COVID-19) IgG IF [Titer]"
                      }
                    },
                    DataType = "NM",
                    Value = "10",
                    //ToDo: Need units or answer as to whether they are needed
                    Units = "??",
                    //ToDo: Need units or answer as to whether they are needed
                    ReferenceRange = "??",
                    AbnormalFlag = "N",
                    ObservationDateTime = new DateTimeOffset(2020, 07, 25, 19, 20, 00, TimeSpan.FromHours(10)).Subtract(TimeSpan.FromMinutes(5)),
                    Status = ResultStatusType.Final,
                    ChildResultList = null
                  },
                  new Result()
                  {
                    Type = new ResultType()
                    {
                      Local = new Code()
                      {
                        Term = "COVID2IGG",
                        Description = "SARS-CoV-2 IgG IF"
                      },
                      Lonic = new Code()
                      {
                        Term = "95428-9",
                        Description = "SARS-CoV-2 (COVID-19) IgM IF [Titer]"
                      }
                    },
                    DataType = "NM",
                    Value = "320",
                    //ToDo: Need units or answer as to whether they are needed
                    Units = "??",
                    //ToDo: Need units or answer as to whether they are needed
                    ReferenceRange = "??",
                    AbnormalFlag = "N",
                    ObservationDateTime = new DateTimeOffset(2020, 07, 25, 19, 20, 00, TimeSpan.FromHours(10)).Subtract(TimeSpan.FromMinutes(5)),
                    Status = ResultStatusType.Final,
                    ChildResultList = null
                  },
                  new Result()
                  {
                    Type = new ResultType()
                    {
                      Local = new Code()
                      {
                        Term = "INTERP",
                        Description = "Interpretation"
                      },
                      Lonic = null,
                    },
                    DataType = "FT",
                    Value = "These results suggest resolving SARS-CoV-2 infection. For further information please contact the medical virologist on(07) 5454 0387.\\.br\\" +                    
                            "virologist on (07) 5454 0387.\\.br\\" +
                            "\\.br\\" +
                            "N.B. SARS-Co-V-2 is the cause of coronavirus disease (CoVID-19).\\.br\\" +
                            "The Immunofluorescent tests used have been evaluated and validated but not yet registered with NATA.\\.br\\" +
                            "Results have been issued to the serious threat to public health from CoVID-19.\\.br\\" +
                            "These results have been electronically notified to the Ministry of Health.\\.br\\",                    
                    Units = null,                    
                    ReferenceRange = null,
                    AbnormalFlag = null,
                    ObservationDateTime = new DateTimeOffset(2020, 07, 25, 19, 20, 00, TimeSpan.FromHours(10)).Subtract(TimeSpan.FromMinutes(5)),
                    Status =  ResultStatusType.Final,
                    ChildResultList = null
                  }
                }
              }
            }
          }
        }
      };
    }

  }
}

