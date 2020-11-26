using Spia.PathologyReportModel.Model;
using System;
using System.Collections.Generic;

namespace Spia.PathologyReportModel.Factory.SpiaReports
{
  public class HepBsAb : IReportFactory
  {
    private readonly SpiaPatientFactory PatientFactory;
    private readonly SpiaProviderFactory ProviderFactory;
    private readonly SpiaLaboratoryFactory LaboratoryFactory;
    public HepBsAb(SpiaPatientFactory SpiaPatientFactory, SpiaProviderFactory SpiaProviderFactory, SpiaLaboratoryFactory SpiaLaboratoryFactory)
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
          Patient = PatientFactory.GetPatient(PatientType.GraceROSSLAND),
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
  }
}
