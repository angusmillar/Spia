using Spia.PathologyReportModel.Model;
using System;
using System.Collections.Generic;

namespace Spia.PathologyReportModel.Factory.SpiaReports
{
  public class FBC : IReportFactory
  {
    private readonly SpiaPatientFactory PatientFactory;
    private readonly SpiaProviderFactory ProviderFactory;
    private readonly SpiaLaboratoryFactory LaboratoryFactory;
    public FBC(SpiaPatientFactory SpiaPatientFactory, SpiaProviderFactory SpiaProviderFactory, SpiaLaboratoryFactory SpiaLaboratoryFactory)
    {
      this.PatientFactory = SpiaPatientFactory;
      this.ProviderFactory = SpiaProviderFactory;
      this.LaboratoryFactory = SpiaLaboratoryFactory;
    }
    public PathologyReportContainer GetReport()
    {
      var RequestedDate =            new DateTimeOffset(2019, 08, 02, 00, 00, 00, TimeSpan.FromHours(10));
      var CollectionDateTime =       new DateTimeOffset(2019, 08, 02, 07, 43, 00, TimeSpan.FromHours(10));
      var SpecimenReceivedDateTime = new DateTimeOffset(2019, 08, 02, 09, 03, 00, TimeSpan.FromHours(10));
      var ReportReleaseDateTime =    new DateTimeOffset(2019, 08, 02, 10, 50, 00, TimeSpan.FromHours(10));
      var ObservationDateTime = ReportReleaseDateTime.Subtract(TimeSpan.FromMinutes(5));

      return new PathologyReportContainer()
      {
        PathologyReport = new PathologyReport()
        {
          PerformingLaboratory = LaboratoryFactory.GetPITUSLaboratory(),
          Patient = PatientFactory.GetTessaCITIZEN(),
          Request = new Request()
          {
            RequestedDate = RequestedDate,
            OrderNumber = "00000004",
            RequestingFacility = new Organisation()
            {
              Name = "Sunrise Hospital Coagulation & Thrombosis Clinic",
              Identifier = new Identifier()
              {
                Value = "264C8EF6-868F-49B0-A532-B47D03F1A8D7",
                Type = IdentifierType.GUID
              }
            },
            RequestingApplication = "Best Practice 1.8.5.743",
            RequestingProvider = ProviderFactory.GetTrishFamilyDr(MedicareProviderNumber: "283530KX"),
            ClinicalNotes = "Warfarin 6mg per day, Family Hx Diabetes",
            CallBackPhoneNumber = null,
            CopyToList = new List<Provider>()
            {              
              ProviderFactory.GetBillHaematologist()
            }
          },
          PdfFileName = "Exemplar Report FBC v1.5.pdf",
          ReportList = new List<Report>()
          {
            new Report()
            {
              ReportId = "1978881888",
              CollectionDateTime = CollectionDateTime,
              SpecimenReceivedDateTime = SpecimenReceivedDateTime,
              ReportReleaseDateTime = ReportReleaseDateTime,
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
                    ReferenceRange = "135-165",
                    AbnormalFlag = "N",
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
                    ObservationDateTime = ObservationDateTime,
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
  }
}
