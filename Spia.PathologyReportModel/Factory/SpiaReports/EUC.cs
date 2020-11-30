using Spia.PathologyReportModel.Model;
using System;
using System.Collections.Generic;

namespace Spia.PathologyReportModel.Factory.SpiaReports
{
  public class EUC : IReportFactory
  {
    private readonly SpiaPatientFactory PatientFactory;
    private readonly SpiaProviderFactory ProviderFactory;
    private readonly SpiaLaboratoryFactory LaboratoryFactory;
    public EUC(SpiaPatientFactory SpiaPatientFactory, SpiaProviderFactory SpiaProviderFactory, SpiaLaboratoryFactory SpiaLaboratoryFactory)
    {
      this.PatientFactory = SpiaPatientFactory;
      this.ProviderFactory = SpiaProviderFactory;
      this.LaboratoryFactory = SpiaLaboratoryFactory;
    }

    public PathologyReportContainer GetReport()
    {
      var RequestedDate =            new DateTimeOffset(2019, 11, 08, 00, 00, 00, TimeSpan.FromHours(10));
      var CollectionDateTime =       new DateTimeOffset(2019, 11, 09, 07, 40, 00, TimeSpan.FromHours(10));
      var SpecimenReceivedDateTime = new DateTimeOffset(2019, 11, 09, 09, 05, 00, TimeSpan.FromHours(10));
      var ReportReleaseDateTime =    new DateTimeOffset(2019, 11, 09, 10, 25, 00, TimeSpan.FromHours(10));
      var ObservationDateTime = ReportReleaseDateTime.Subtract(TimeSpan.FromMinutes(5));

      return new PathologyReportContainer()
      {
        PathologyReport = new PathologyReport()
        {
          PerformingLaboratory = LaboratoryFactory.GetPITUSLaboratory(),
          Patient = PatientFactory.GetGordonSEYMOUR(),
          Request = new Request()
          {
            RequestedDate = RequestedDate,
            OrderNumber = "00000002",
            RequestingFacility = new Organisation()
            {
              Name = "Sunrise Hospital Arthritis Clinic",
              Identifier = new Identifier()
              {
                Value = "9FD7F9F2-AB17-4FE7-9954-7FE285287122",
                Type = IdentifierType.GUID
              }
            },
            RequestingApplication = "Best Practice 1.8.5.743",
            RequestingProvider = ProviderFactory.GetTrishFamilyDr(MedicareProviderNumber: "9316007J"),
            ClinicalNotes = "Previous episodes of gout; colchicine 1.2 mg May 2018",
            CallBackPhoneNumber = null,
            CopyToList = null,
          },
          PdfFileName = "Exemplar Report EUC v1.6.pdf",
          ReportList = new List<Report>()
          {
            new Report()
            {
              ReportId = "1978881856",
              CollectionDateTime = CollectionDateTime,
              SpecimenReceivedDateTime = SpecimenReceivedDateTime,
              ReportReleaseDateTime = ReportReleaseDateTime,
              ReportType = new ReportType()
              {
                //As of meeting on the 10/11/2020 @ 2:00pm, chnaged this to just Biochemistry - General
                Local = new Code() { Term = "BGEN", Description = "Biochemistry - General" },
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
                    Value = "138",
                    Units = "mmol/L",
                    ReferenceRange = "135-142",
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
                    Value = "4.3",
                    Units = "mmol/L",
                    ReferenceRange = "3.8-4.8",
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
                    Value = "107",
                    Units = "mmol/L",
                    ReferenceRange = "100-110",
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
                    Value = "32",
                    Units = "mmol/L",
                    ReferenceRange = "25-35",
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
                    Value = "104",
                    Units = "mmol/L",
                    ReferenceRange = "83-108",
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
                    Value = "101",
                    Units = "mmol/L",
                    ReferenceRange = "70-100",
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
                        Term = "EGFR",
                        Description = "Glomerular filtration rate estimated"
                      },
                      Lonic = new Code()
                      {
                        Term = "50210-4",
                        Description = "eGFR"
                      }
                    },
                    DataType = "NM",
                    Value = "113",
                    Units = "mL/min/1.73m^2",
                    ReferenceRange = "90-120",
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
                        Term = "INTERP",
                        Description = "Interpretation"
                      },
                      Lonic = null,
                    },
                    DataType = "ST",
                    Value = "Chemistry parameters indicate colchicine treatment effective in managing gout.",
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
