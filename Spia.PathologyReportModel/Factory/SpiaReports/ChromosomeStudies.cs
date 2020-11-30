using Spia.PathologyReportModel.Model;
using System;
using System.Collections.Generic;

namespace Spia.PathologyReportModel.Factory.SpiaReports
{
  public class ChromosomeStudies : IReportFactory
  {
    private readonly SpiaPatientFactory PatientFactory;
    private readonly SpiaProviderFactory ProviderFactory;
    private readonly SpiaLaboratoryFactory LaboratoryFactory;
    public ChromosomeStudies(SpiaPatientFactory SpiaPatientFactory, SpiaProviderFactory SpiaProviderFactory, SpiaLaboratoryFactory SpiaLaboratoryFactory)
    {
      this.PatientFactory = SpiaPatientFactory;
      this.ProviderFactory = SpiaProviderFactory;
      this.LaboratoryFactory = SpiaLaboratoryFactory;
    }

    public PathologyReportContainer GetReport()
    {
      var RequestedDate =            new DateTimeOffset(2019, 09, 23, 00, 00, 00, TimeSpan.FromHours(10));
      var CollectionDateTime =       new DateTimeOffset(2019, 09, 25, 08, 20, 00, TimeSpan.FromHours(10));
      var SpecimenReceivedDateTime = new DateTimeOffset(2019, 09, 25, 11, 15, 00, TimeSpan.FromHours(10));
      var ReportReleaseDateTime =    new DateTimeOffset(2019, 09, 26, 10, 00, 00, TimeSpan.FromHours(10));
      var ObservationDateTime = ReportReleaseDateTime.Subtract(TimeSpan.FromMinutes(5));

      return new PathologyReportContainer()
      {
        PathologyReport = new PathologyReport()
        {
          PerformingLaboratory = LaboratoryFactory.GetPITUSLaboratory(),
          Patient = PatientFactory.GetLarissaFERNIE(),
          Request = new Request()
          {
            RequestedDate = RequestedDate,
            OrderNumber = "00000010",
            RequestingFacility = new Organisation()
            {
              Name = "Sunrise Hospital Fertility Clinic",
              Identifier = new Identifier()
              {
                Value = "501970A0-1E1F-40D8-9656-61899527203F",
                Type = IdentifierType.GUID
              }
            },
            RequestingApplication = "Best Practice 1.8.5.743",
            RequestingProvider = ProviderFactory.GetTrishFamilyDr(MedicareProviderNumber: "951577QT"),
            ClinicalNotes = "Primary infertility",
            CallBackPhoneNumber = null,
            CopyToList = new List<Provider>()
            {
              ProviderFactory.GetGeneticCounsellingClinicCoordinator(),
              ProviderFactory.GetSarsgaardGeneticCounsellor(),
            }
          },
          PdfFileName = "Exemplar Report Chromosome studies v1.4.pdf",
          ReportList = new List<Report>()
          {
            new Report()
            {
              ReportId = "1978881874",
              CollectionDateTime = CollectionDateTime,
              SpecimenReceivedDateTime = SpecimenReceivedDateTime,
              ReportReleaseDateTime = ReportReleaseDateTime,
              ReportType = new ReportType()
              {
                Local = new Code() { Term = "CHROMOSTUD", Description = "Chromosome studies" },
                Snomed = new Code() { Term = "1308381000168103", Description = "Whole blood cytogenetic analysis" }
              },
              ReportStatus =  ResultStatusType.Final,
              Department = DiagnosticService.Hematology,
              ReportingPathologist = ProviderFactory.GetKondoPathologist(),
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
                        Term = "KARYO",
                        Description = "Cytogenetic karyotyping"
                      },
                      Lonic = null
                    },
                    DataType = "ST",
                    Value = "46,X,del(X)(q11.2)",
                    Units = null,
                    ReferenceRange = null,
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
                        Term = "RES",
                        Description = "Results"
                      },
                      Lonic = null
                    },
                    DataType = "FT",
                    Value = "An abnormal female result showing a deletion of the long arm of the X-chromosome at band q11.2.\\.br\\" +
                            "This aberration was observed in all cells analysed.\\.br\\" +
                            "GTG banded analysis was performed on 5 cells analysed and 10 cells counted at a resolution of 550 bands.",
                    Units = null,
                    ReferenceRange = null,
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
                      Lonic = null
                    },
                    DataType = "FT",
                    Value = "Deletions of the long arm of the X-chromosome are associated with a variable phenotype in females. Common\\.br\\" +
                            "findings include short stature, gonadal dysgenesis and premature ovarian failure. It is likely that this aberration is the\\.br\\" +
                            "cause of the primary infertility observed in this patient. Genetic counselling is recommended.",
                    Units = null,
                    ReferenceRange = null,
                    AbnormalFlag = "N",
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
