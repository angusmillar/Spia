using Spia.PathologyReportModel.Model;
using System;
using System.Collections.Generic;

namespace Spia.PathologyReportModel.Factory.SpiaReports
{
  public class SARSCoV2NAT : IReportFactory
  {
    private readonly SpiaPatientFactory PatientFactory;
    private readonly SpiaProviderFactory ProviderFactory;
    private readonly SpiaLaboratoryFactory LaboratoryFactory;
    public SARSCoV2NAT(SpiaPatientFactory SpiaPatientFactory, SpiaProviderFactory SpiaProviderFactory, SpiaLaboratoryFactory SpiaLaboratoryFactory)
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
          Patient = PatientFactory.GetPatient(PatientType.HaydenNORQUAY),
          Request = new Request()
          {
            RequestedDate = new DateTimeOffset(2020, 08, 20, 00, 00, 00, TimeSpan.FromHours(10)),
            OrderNumber = "00000015",
            RequestingFacility = new Organisation()
            {
              Name = "Fever Clinic Sunrise Hospital",
              Identifier = new Identifier()
              {
                Value = "3659F40F-8BDF-4CD6-BF46-38257CA6BB97",
                Type = IdentifierType.GUID
              }
            },
            RequestingApplication = "Best Practice 1.8.5.743",
            RequestingProvider = ProviderFactory.GetTrishFamilyDr(MedicareProviderNumber: "603107KW"),
            ClinicalNotes = "Sore throat & cough, works in hospitality industry",
            CallBackPhoneNumber = null,
            CopyToList = new List<Provider>()
            {
              ProviderFactory.GetMySpecialist()
            }
          },
          PdfFileName = "SPIA Exemplar Report SARS-CoV-2 NAT v0.4.pdf",
          ReportList = new List<Report>()
          {
            new Report()
            {
              ReportId = "2078881879",
              CollectionDateTime = new DateTimeOffset(2020, 08, 20, 11, 20, 00, TimeSpan.FromHours(10)),
              SpecimenReceivedDateTime = new DateTimeOffset(2020, 08, 20, 13, 24, 00, TimeSpan.FromHours(10)),
              ReportReleaseDateTime = new DateTimeOffset(2020, 08, 20, 20, 28, 00, TimeSpan.FromHours(10)),
              ReportType = new ReportType()
              {
                Local = new Code() { Term = "COVID2", Description = "SARS-CoV-2 nucleic acid" },
                Snomed = new Code() { Term = "1445431000168101", Description = "Oropharyngeal swab COVID-19 nucleic acid assay" }
              },
              ReportStatus =  ResultStatusType.Final,
              Department = DiagnosticService.Microbiology,
              ReportingPathologist = ProviderFactory.GetBellaPathologist(),
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
                        Term = "COVI2DRNA",
                        Description = "SARS-CoV-2 RNA"
                      },
                      Lonic = new Code()
                      {
                        Term = "94309-2",
                        Description = "SARS-CoV-2 (COVID-19) RNA NAA+probe Ql (Unsp spec)"
                      }
                    },
                    DataType = "ST",
                    Value = "Not Detected",
                    Units = null,
                    ReferenceRange = null,
                    AbnormalFlag = "N",
                    ObservationDateTime = new DateTimeOffset(2020, 08, 20, 20, 28, 00, TimeSpan.FromHours(10)).Subtract(TimeSpan.FromMinutes(5)),
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
                    DataType = "FT",
                    Value = "Failure to detect virus specific nucleic acid does not always exclude SARS-CoV-2. Repeat testing\\.br\\" +
                            "including a sputum sample if available, may be indicated if clinical suspicion is high.\\.br\\" +
                            "This assay is designed to detect the E gene of SARS-CoV-2, the causative agent of COVID-19 using\\.br\\" +
                            "nucleic acid amplification. On occasions, a second assay targeting the N gene is also utilised.\\.br\\" +
                            "\\.br\\" +
                            "For further requires regarding these results, please contact the Medical Virologist on (07) 5454 0387.\\.br\\" +
                            "Testing for SARS-CoV-2 is notifiable on request irrespective of test results.\\.br\\",
                    Units = null,
                    ReferenceRange = null,
                    AbnormalFlag = null,
                    ObservationDateTime = new DateTimeOffset(2020, 08, 20, 20, 28, 00, TimeSpan.FromHours(10)).Subtract(TimeSpan.FromMinutes(5)),
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
