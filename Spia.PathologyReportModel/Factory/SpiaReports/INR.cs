using Spia.PathologyReportModel.Model;
using System;
using System.Collections.Generic;

namespace Spia.PathologyReportModel.Factory.SpiaReports
{
  public class INR : IReportFactory
  {
    private readonly SpiaPatientFactory PatientFactory;
    private readonly SpiaProviderFactory ProviderFactory;
    private readonly SpiaLaboratoryFactory LaboratoryFactory;
    public INR(SpiaPatientFactory SpiaPatientFactory, SpiaProviderFactory SpiaProviderFactory, SpiaLaboratoryFactory SpiaLaboratoryFactory)
    {
      this.PatientFactory = SpiaPatientFactory;
      this.ProviderFactory = SpiaProviderFactory;
      this.LaboratoryFactory = SpiaLaboratoryFactory;
    }

    public PathologyReportContainer GetReport()
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
  }
}
