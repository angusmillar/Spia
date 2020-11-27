using Spia.PathologyReportModel.Model;
using System;
using System.Collections.Generic;

namespace Spia.PathologyReportModel.Factory.SpiaReports
{
  public class HaemochromatosisGeneScreening : IReportFactory
  {
    private readonly SpiaPatientFactory PatientFactory;
    private readonly SpiaProviderFactory ProviderFactory;
    private readonly SpiaLaboratoryFactory LaboratoryFactory;
    public HaemochromatosisGeneScreening(SpiaPatientFactory SpiaPatientFactory, SpiaProviderFactory SpiaProviderFactory, SpiaLaboratoryFactory SpiaLaboratoryFactory)
    {
      this.PatientFactory = SpiaPatientFactory;
      this.ProviderFactory = SpiaProviderFactory;
      this.LaboratoryFactory = SpiaLaboratoryFactory;
    }
    public PathologyReportContainer GetReport()
    {
      var RequestedDate =            new DateTimeOffset(2020, 09, 19, 00, 00, 00, TimeSpan.FromHours(10));
      var CollectionDateTime =       new DateTimeOffset(2020, 09, 20, 08, 20, 00, TimeSpan.FromHours(10));
      var SpecimenReceivedDateTime = new DateTimeOffset(2020, 09, 20, 09, 35, 00, TimeSpan.FromHours(10));
      var ReportReleaseDateTime =    new DateTimeOffset(2020, 09, 22, 11, 15, 00, TimeSpan.FromHours(10));
      var ObservationDateTime = ReportReleaseDateTime.Subtract(TimeSpan.FromMinutes(5));
      return new PathologyReportContainer()
      {
        PathologyReport = new PathologyReport()
        {
          PerformingLaboratory = LaboratoryFactory.GetPITUSLaboratory(),
          Patient = PatientFactory.GetNormanMANNING(),
          Request = new Request()
          {
            RequestedDate = RequestedDate,
            OrderNumber = "00000005",
            RequestingFacility = new Organisation()
            {
              Name = "Sunrise Hospital Metabolic Clinic",
              Identifier = new Identifier()
              {
                Value = "75C38DF6-BE0B-4287-8D46-6395CACAAD7A",
                Type = IdentifierType.GUID
              }
            },
            RequestingApplication = "Best Practice 1.8.5.743",
            RequestingProvider = ProviderFactory.GetTrishFamilyDr(MedicareProviderNumber: "2359622W"),
            ClinicalNotes = "? Hereditary haemochromatosis. Arthritis, increased iron stores",
            CallBackPhoneNumber = null,
            CopyToList = new List<Provider>()
            {
              ProviderFactory.GetGeneticsClinic(),
              ProviderFactory.GetBjornGeneticCounsellor()
            }
          },
          PdfFileName = "Exemplar Report Haemochromatosis gene screening v1.4.pdf",
          ReportList = new List<Report>()
          {
            new Report()
            {
              ReportId = "1978881777",
              CollectionDateTime = CollectionDateTime,
              SpecimenReceivedDateTime = SpecimenReceivedDateTime,
              ReportReleaseDateTime = ReportReleaseDateTime,
              ReportType = new ReportType()
              {
                Local = new Code() { Term = "HFE", Description = "Haemochromatosis gene screening" },
                Snomed = new Code() { Term = "401085002", Description = "Haemochromatosis gene screening test" }
              },
              ReportStatus =  ResultStatusType.Final,
              Department = DiagnosticService.Genetics,
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
                        Term = "C28Y",
                        Description = "C28Y mutation analysis"
                      },
                      Lonic = null,
                    },
                    DataType = "ST",
                    Value = "Not Detected",
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
                        Term = "H63D",
                        Description = "H63D mutation analysis"
                      },
                      Lonic = null,
                    },
                    DataType = "ST",
                    Value = "Homozygous",
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
                      Lonic = null,
                    },
                    DataType = "FT",
                    Value = "Two copies of the p.His63Asp variant were detected in the patient. The p.Cys282Tyr variant was not detected.\\.br\\" +
                             "The diagnosis of the most common form of HFE-related hereditary haemochromatosis is excluded.\\.br\\",
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
                        Term = "INTER",
                        Description = "Interpretation"
                      },
                      Lonic = null,
                    },
                    DataType = "FT",
                    Value = "Hereditary haemochromatosis (HH) is a recessive genetic disorder of iron metabolism. Greater than 90% of HFE\\.br\\" +
                             "related hereditary haemochromatosis is associated with homozygosity for p.(Cys282Tyr) (c.845G>A; aka p.C282Y).\\.br\\" +
                             "Compound heterozygosity for p. (Cys282Tyr) and p. (His63Asp) (c.187C>G; aka p.H63D) may be a risk factor\\.br\\" +
                             "predisposing to mild to moderate forms of iron overload when in association with other risk factors. Both mutations\\.br\\" +
                             "are detected by real-time PCR amplification and fluorescent detection of alleles.",
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
