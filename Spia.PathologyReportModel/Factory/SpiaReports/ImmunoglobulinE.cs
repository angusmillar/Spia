using Spia.PathologyReportModel.Model;
using System;
using System.Collections.Generic;

namespace Spia.PathologyReportModel.Factory.SpiaReports
{
  public class ImmunoglobulinE : IReportFactory
  {
    private readonly SpiaPatientFactory PatientFactory;
    private readonly SpiaProviderFactory ProviderFactory;
    private readonly SpiaLaboratoryFactory LaboratoryFactory;
    public ImmunoglobulinE(SpiaPatientFactory SpiaPatientFactory, SpiaProviderFactory SpiaProviderFactory, SpiaLaboratoryFactory SpiaLaboratoryFactory)
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
  }
}
