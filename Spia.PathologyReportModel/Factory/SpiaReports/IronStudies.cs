using Spia.PathologyReportModel.Model;
using System;
using System.Collections.Generic;

namespace Spia.PathologyReportModel.Factory.SpiaReports
{
  public class IronStudies : IReportFactory
  {
    private readonly SpiaPatientFactory PatientFactory;
    private readonly SpiaProviderFactory ProviderFactory;
    private readonly SpiaLaboratoryFactory LaboratoryFactory;
    public IronStudies(SpiaPatientFactory SpiaPatientFactory, SpiaProviderFactory SpiaProviderFactory, SpiaLaboratoryFactory SpiaLaboratoryFactory)
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
          Patient = PatientFactory.GetIrisREVELSTOKE(),
          Request = new Request()
          {
            RequestedDate = RequestedDate,
            OrderNumber = "00000018",
            RequestingFacility = new Organisation()
            {
              Name = "Sunrise Hospital Infusion Clinic",
              Identifier = new Identifier()
              {
                Value = "48B99BF1-DAD8-42E1-81E6-9FB3475D60E0",
                Type = IdentifierType.GUID
              }
            },
            RequestingApplication = "Best Practice 1.8.5.743",
            RequestingProvider = ProviderFactory.GetTrishFamilyDr(MedicareProviderNumber: "3226893T"),
            ClinicalNotes = "Lethargy, dizziness ? Iron deficient",
            CallBackPhoneNumber = null,
            CopyToList = new List<Provider>()
            {
              ProviderFactory.GetInfusionClinic()
            }
          },
          PdfFileName = "SPIA Exemplar Report Iron studies v0.2.pdf",
          ReportList = new List<Report>()
          {
            new Report()
            {
              ReportId = "1978881829",
              CollectionDateTime =       CollectionDateTime,
              SpecimenReceivedDateTime = SpecimenReceivedDateTime,
              ReportReleaseDateTime =    ReportReleaseDateTime,
              ReportType = new ReportType()
              {
                Local = new Code() { Term = "IS", Description = "Iron studies" },
                Snomed = new Code() { Term = "269820002", Description = "Serum iron tests" }
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
                        Term = "FERR",
                        Description = "Ferritin"
                      },
                      Lonic = new Code()
                      {
                        Term = "2276-4",
                        Description = "Ferritin"
                      }
                    },
                    DataType = "NM",
                    Value = "27",
                    Units = "ug/L",
                    ReferenceRange = "30-120",
                    AbnormalFlag = "L",
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
                        Term = "IRON",
                        Description = "Iron"
                      },
                      Lonic = new Code()
                      {
                        Term = "14798-3",
                        Description = "Iron"
                      }
                    },
                    DataType = "NM",
                    Value = "8.7",
                    Units = "umol/L",
                    ReferenceRange = "10.0-30.0",
                    AbnormalFlag = "L",
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
                        Term = "TRANFERR",
                        Description = "Transferrin"
                      },
                      Lonic = new Code()
                      {
                        Term = "3034-6",
                        Description = "Transferrin"
                      }
                    },
                    DataType = "NM",
                    Value = "4.1",
                    Units = "g/L",
                    ReferenceRange = "2.10-3.80",
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
                        Term = "TRANFERRSAT",
                        Description = "Transferrin saturation"
                      },
                      Lonic = new Code()
                      {
                        Term = "14801-5",
                        Description = "Transferrin saturation"
                      }
                    },
                    DataType = "NM",
                    Value = "14",
                    Units = "%",
                    ReferenceRange = "15-45",
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
                        Term = "INTERP",
                        Description = "Interpretation"
                      },
                      Lonic = new Code()
                      {
                        Term = "14797-5",
                        Description = "Iron [Interpretation] in Serum or Plasma"
                      }
                    },
                    DataType = "FT",
                    Value = "Chemistry parameters are indicative of iron deficiency.",
                    Units = null,
                    ReferenceRange = null,
                    AbnormalFlag = null,
                    ObservationDateTime = ObservationDateTime,
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
