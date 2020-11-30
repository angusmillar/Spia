using Spia.PathologyReportModel.Model;
using System;
using System.Collections.Generic;

namespace Spia.PathologyReportModel.Factory.SpiaReports
{
  public class MSU : IReportFactory
  {
    private readonly SpiaPatientFactory PatientFactory;
    private readonly SpiaProviderFactory ProviderFactory;
    private readonly SpiaLaboratoryFactory LaboratoryFactory;
    public MSU(SpiaPatientFactory SpiaPatientFactory, SpiaProviderFactory SpiaProviderFactory, SpiaLaboratoryFactory SpiaLaboratoryFactory)
    {
      this.PatientFactory = SpiaPatientFactory;
      this.ProviderFactory = SpiaProviderFactory;
      this.LaboratoryFactory = SpiaLaboratoryFactory;
    }

    public PathologyReportContainer GetReport()
    {
      var RequestedDate =            new DateTimeOffset(2019, 11, 09, 00, 00, 00, TimeSpan.FromHours(10));
      var CollectionDateTime =       new DateTimeOffset(2019, 11, 09, 08, 45, 00, TimeSpan.FromHours(10));
      var SpecimenReceivedDateTime = new DateTimeOffset(2019, 11, 09, 09, 12, 00, TimeSpan.FromHours(10));
      var ReportReleaseDateTime =    new DateTimeOffset(2019, 11, 09, 10, 25, 00, TimeSpan.FromHours(10));
      var ObservationDateTime = ReportReleaseDateTime.Subtract(TimeSpan.FromMinutes(5));

      return new PathologyReportContainer()
      {
        PathologyReport = new PathologyReport()
        {
          PerformingLaboratory = LaboratoryFactory.GetPITUSLaboratory(),
          Patient = PatientFactory.GetGloriaNELSON(),
          Request = new Request()
          {
            RequestedDate = RequestedDate,
            OrderNumber = "00000012",
            RequestingFacility = new Organisation()
            {
              Name = "Sunrise Hospital Emergency Dept",
              Identifier = new Identifier()
              {
                Value = "191394B8-6AB0-4EF5-BC7D-19A5B37FA60F",
                Type = IdentifierType.GUID
              }
            },
            RequestingApplication = "Best Practice 1.8.5.743",
            RequestingProvider = ProviderFactory.GetTrishFamilyDr(MedicareProviderNumber: "873721DH"),
            ClinicalNotes = "Diabetic with recurring UTI. Significant vaginal burning on micturition. Last course Trimethoprim (100 mg bd) completed 27- Dec-19.",
            CallBackPhoneNumber = null,
            CopyToList = new List<Provider>()
            {             
              ProviderFactory.GetRebeccaGP()
            }
          },
          PdfFileName = "SPIA Exemplar Report Urine MCS v1.6.pdf",
          ReportList = new List<Report>()
          {
            new Report()
            {
              ReportId = "2001277757",
              CollectionDateTime = CollectionDateTime,
              SpecimenReceivedDateTime = SpecimenReceivedDateTime,
              ReportReleaseDateTime = ReportReleaseDateTime,
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
                        Term = "COLOR",
                        Description = "Colour"
                      },
                      Lonic = new Code()
                      {
                        Term = "5778-6",
                        Description = "Colour"
                      }
                    },
                    DataType = "ST",
                    Value = "Pink stained",
                    Units = null,
                    ReferenceRange = null,
                    AbnormalFlag = null,
                    ObservationDateTime = ObservationDateTime,
                    Status = ResultStatusType.Final,
                    ChildResultList = null
                  },
                  new Result()
                  {
                    Type = new ResultType()
                    {
                      Local = new Code()
                      {
                        Term = "CLARITY",
                        Description = "Clarity"
                      },
                      Lonic = new Code()
                      {
                        Term = "32167-9",
                        Description = "Clarity"
                      }
                    },
                    DataType = "ST",
                    Value = "Turbid",
                    Units = null,
                    ReferenceRange = null,
                    AbnormalFlag = null,
                    ObservationDateTime = ObservationDateTime,
                    Status = ResultStatusType.Final,
                    ChildResultList = null
                  },
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
                    Units = "No units",
                    ReferenceRange = "Negative",
                    AbnormalFlag = "A",
                    ObservationDateTime = ObservationDateTime,
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
                    Units = "No units",
                    ReferenceRange = "Negative",
                    AbnormalFlag = "A",
                    ObservationDateTime = ObservationDateTime,
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
                    Units = "No units",
                    ReferenceRange = "Negative",
                    AbnormalFlag = "A",
                    ObservationDateTime = ObservationDateTime,
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
                    Units = "No units",
                    ReferenceRange = "1.003-1.035",
                    AbnormalFlag = null,
                    ObservationDateTime = ObservationDateTime,
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
                    Units = "No units",
                    ReferenceRange = "5.0-8.0",
                    AbnormalFlag = "H",
                    ObservationDateTime = ObservationDateTime,
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
                    Units = "No units",
                    ReferenceRange = "Negative",
                    AbnormalFlag = "A",
                    ObservationDateTime = ObservationDateTime,
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
                    ObservationDateTime = ObservationDateTime,
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
                    Units = "No units",
                    ReferenceRange = "Negative",
                    AbnormalFlag = "A",
                    ObservationDateTime = ObservationDateTime,
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
                    Units = "No units",
                    ReferenceRange = "Negative",
                    AbnormalFlag = "A",
                    ObservationDateTime = ObservationDateTime,
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
                    Units = "No units",
                    ReferenceRange = "Negative",
                    AbnormalFlag = "A",
                    ObservationDateTime = ObservationDateTime,
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
                    ObservationDateTime = ObservationDateTime,
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
                    ObservationDateTime = ObservationDateTime,
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
                    Units = "No units",
                    ReferenceRange = null,
                    AbnormalFlag = "A",
                    ObservationDateTime = ObservationDateTime,
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
                    ObservationDateTime = ObservationDateTime,
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
                    AbnormalFlag = null,
                    ObservationDateTime = ObservationDateTime,
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
                    AbnormalFlag = null,
                    ObservationDateTime = ObservationDateTime,
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
                    AbnormalFlag = null,
                    ObservationDateTime = ObservationDateTime,
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
                        ObservationDateTime = ObservationDateTime,
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
                        ObservationDateTime = ObservationDateTime,
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
                        ObservationDateTime = ObservationDateTime,
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
                        ObservationDateTime = ObservationDateTime,
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
                        ObservationDateTime = ObservationDateTime,
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
                        ObservationDateTime = ObservationDateTime,
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
                    Value = "Previous treatment with trimethoprim ineffective for recurring E. coli infection. Suggest\\.br\\" +
                            "intramuscular injection 160 mg gentamicin QD 14 days.",
                    Units = null,
                    ReferenceRange = null,
                    AbnormalFlag = null,
                    ObservationDateTime = ObservationDateTime,
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
  }
}
