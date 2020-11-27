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
      var RequestedDate =            new DateTimeOffset(2019, 07, 25, 00, 00, 00, TimeSpan.FromHours(10));
      var CollectionDateTime =       new DateTimeOffset(2019, 07, 25, 16, 32, 00, TimeSpan.FromHours(10));
      var SpecimenReceivedDateTime = new DateTimeOffset(2019, 07, 25, 17, 48, 00, TimeSpan.FromHours(10));
      var ReportReleaseDateTime =    new DateTimeOffset(2019, 07, 25, 20, 32, 00, TimeSpan.FromHours(10));
      var ObservationDateTime = ReportReleaseDateTime.Subtract(TimeSpan.FromMinutes(5));

      return new PathologyReportContainer()
      {
        PathologyReport = new PathologyReport()
        {
          PerformingLaboratory = LaboratoryFactory.GetPITUSLaboratory(),
          Patient = PatientFactory.GetChristinaSELKIRK(),
          Request = new Request()
          {
            RequestedDate = RequestedDate,
            OrderNumber = "00000009",
            RequestingFacility = new Organisation()
            {
              Name = "Sunrise Hospital Coagulation & Thrombosis Clinic",
              Identifier = new Identifier()
              {
                Value = "ACA30A38-811E-4E0D-B55C-774D38B8E171",
                Type = IdentifierType.GUID
              }
            },
            RequestingApplication = "Best Practice 1.8.5.743",
            RequestingProvider = ProviderFactory.GetTrishFamilyDr(MedicareProviderNumber: "283530KX"),
            ClinicalNotes = "Warfarin 6mg per day; monitoring new dose",
            CallBackPhoneNumber = null,
            CopyToList = new List<Provider>()
            {
              ProviderFactory.GetCoagulationAndThrombosisClinic(),
              ProviderFactory.GetBillCardiologist()
            }
          },
          PdfFileName = "Exemplar Report INR v1.6.pdf",
          ReportList = new List<Report>()
          {
            new Report()
            {
              ReportId = "1878881888",
              CollectionDateTime = CollectionDateTime,
              SpecimenReceivedDateTime = SpecimenReceivedDateTime,
              ReportReleaseDateTime = ReportReleaseDateTime,
              ReportType = new ReportType()
              {
                Local = new Code() { Term = "COAG", Description = "Coagulation" },
                //Snomed = new Code() { Term = "440685005", Description = "Calculation of international normalised ratio" }
                Snomed = null
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
                      Lonic = null
                    },
                    DataType = "FT",
                    Value = "INR is higher than therapeutic range. Recommend Warfarin dose be reduced to 5 mg per day, repeat test in 7 days.\\.br\\",
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
