using Spia.PathologyReportModel.Model;
using System;
using System.Collections.Generic;

namespace Spia.PathologyReportModel.Factory.SpiaReports
{
  public class IgE : IReportFactory
  {
    private readonly SpiaPatientFactory PatientFactory;
    private readonly SpiaProviderFactory ProviderFactory;
    private readonly SpiaLaboratoryFactory LaboratoryFactory;
    public IgE(SpiaPatientFactory SpiaPatientFactory, SpiaProviderFactory SpiaProviderFactory, SpiaLaboratoryFactory SpiaLaboratoryFactory)
    {
      this.PatientFactory = SpiaPatientFactory;
      this.ProviderFactory = SpiaProviderFactory;
      this.LaboratoryFactory = SpiaLaboratoryFactory;
    }

    public PathologyReportContainer GetReport()
    {
      var RequestedDate =            new DateTimeOffset(2020, 01, 22, 00, 00, 00, TimeSpan.FromHours(10));
      var CollectionDateTime =       new DateTimeOffset(2020, 01, 22, 16, 32, 00, TimeSpan.FromHours(10));
      var SpecimenReceivedDateTime = new DateTimeOffset(2020, 01, 22, 17, 08, 00, TimeSpan.FromHours(10));
      var ReportReleaseDateTime =    new DateTimeOffset(2020, 01, 23, 08, 45, 00, TimeSpan.FromHours(10));
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
            OrderNumber = "00000007",
            RequestingFacility = new Organisation()
            {
              Name = "Sunrise Hospital Allergy Clinic",
              Identifier = new Identifier()
              {
                Value = "B515183F-04DC-4D4B-9417-DD1A6A6A8093",
                Type = IdentifierType.GUID
              }
            },
            RequestingApplication = "Best Practice 1.8.5.743",
            RequestingProvider = ProviderFactory.GetTrishFamilyDr(MedicareProviderNumber: "1783879L"),
            ClinicalNotes = "Moderate reaction to bee sting; asthmatic",
            CallBackPhoneNumber = null,
            CopyToList = new List<Provider>()
            {
              ProviderFactory.GetAllergyClinic(),
              ProviderFactory.GetBeulaImmunologist()
            }
          },
          PdfFileName = "Exemplar Report IgE v1.5.pdf",
          ReportList = new List<Report>()
          {
            new Report()
            {
              ReportId = "2078881822",
              CollectionDateTime = CollectionDateTime,
              SpecimenReceivedDateTime = SpecimenReceivedDateTime,
              ReportReleaseDateTime = ReportReleaseDateTime,
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
                    Value = "620",
                    Units = "kIU/L",
                    ReferenceRange = "2-300",
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
                      Lonic = null,
                    },
                    DataType = "FT",
                    Value = "Immunoprotein parameters indicate recent allergic reaction. Specific IgE testing recommended for honey bee and\\.br\\" +
                            "common wasp venoms, also tryptase levels to identify individual allergen(s).",
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
