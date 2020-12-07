using Spia.PathologyReportModel.Model;
using System;
using System.Collections.Generic;

namespace Spia.PathologyReportModel.Factory.SpiaReports
{
  //This HistoColorectal is not finished and by commenting out the IReportFactory interface it will not be included in the output
  public class HistoColorectal //: IReportFactory
  {
    private readonly SpiaPatientFactory PatientFactory;
    private readonly SpiaProviderFactory ProviderFactory;
    private readonly SpiaLaboratoryFactory LaboratoryFactory;
    public HistoColorectal(SpiaPatientFactory SpiaPatientFactory, SpiaProviderFactory SpiaProviderFactory, SpiaLaboratoryFactory SpiaLaboratoryFactory)
    {
      this.PatientFactory = SpiaPatientFactory;
      this.ProviderFactory = SpiaProviderFactory;
      this.LaboratoryFactory = SpiaLaboratoryFactory;
    }

    public PathologyReportContainer GetReport()
    {
      var RequestedDate =            new DateTimeOffset(2019, 11, 28, 00, 00, 00, TimeSpan.FromHours(10));
      var CollectionDateTime =       new DateTimeOffset(2019, 11, 28, 06, 35, 00, TimeSpan.FromHours(10));
      var SpecimenReceivedDateTime = new DateTimeOffset(2019, 11, 28, 08, 22, 00, TimeSpan.FromHours(10));
      var ReportReleaseDateTime =    new DateTimeOffset(2019, 11, 29, 10, 25, 00, TimeSpan.FromHours(10));
      var ObservationDateTime = ReportReleaseDateTime.Subtract(TimeSpan.FromMinutes(5));

      return new PathologyReportContainer()
      {
        PathologyReport = new PathologyReport()
        {
          PerformingLaboratory = LaboratoryFactory.GetPITUSLaboratory(),
          Patient = PatientFactory.GetGrantWHISTLER(),
          Request = new Request()
          {
            RequestedDate = RequestedDate,
            OrderNumber = "00000019",
            RequestingFacility = new Organisation()
            {
              Name = "Sunrise Hospital GI Clinic",
              Identifier = new Identifier()
              {
                Value = "EF539BC7-F6C2-4637-A12F-4EC6FA5CFA4B",
                Type = IdentifierType.GUID
              }
            },
            RequestingApplication = "Best Practice 1.8.5.743",
            RequestingProvider = ProviderFactory.GetTrishFamilyDr(MedicareProviderNumber: "449738MF"),
            ClinicalNotes = "Positive FOB screen 18-Sep-19. Short course radiotherapy ceased 19 - Nov - 19",
            CallBackPhoneNumber = null,
            CopyToList = new List<Provider>()
            {             
            }
          },
          PdfFileName = "Exemplar Report Histo Colorectal SPRC v1.2.pdf",
          ReportList = new List<Report>()
          {
            new Report()
            {
              ReportId = "11/P28460",
              CollectionDateTime =       CollectionDateTime,
              SpecimenReceivedDateTime = SpecimenReceivedDateTime,
              ReportReleaseDateTime =    ReportReleaseDateTime,
              ReportType = new ReportType()
              {
                Local = new Code() { Term = "IS", Description = "Colorectal Cancer Structured Report" },
                Snomed = new Code() { Term = "84907-5", Description = "Colorectal Cancer Structured Pathology Report" }
              },
              ReportStatus =  ResultStatusType.Final,
              Department = DiagnosticService.HistologyAndAnatomicalPathology,
              ReportingPathologist = ProviderFactory.GetArturoPathologist(),
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
                    Units = "ug/L",// ug/L, ug/L, UCUM
                    ReferenceRange = "30-120",
                    AbnormalFlag = "L",
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
