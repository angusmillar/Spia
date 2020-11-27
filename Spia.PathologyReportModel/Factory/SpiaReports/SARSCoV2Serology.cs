using Spia.PathologyReportModel.Model;
using System;
using System.Collections.Generic;

namespace Spia.PathologyReportModel.Factory.SpiaReports
{
  public class SARSCoV2Serology : IReportFactory
  {
    private readonly SpiaPatientFactory PatientFactory;
    private readonly SpiaProviderFactory ProviderFactory;
    private readonly SpiaLaboratoryFactory LaboratoryFactory;
    public SARSCoV2Serology(SpiaPatientFactory SpiaPatientFactory, SpiaProviderFactory SpiaProviderFactory, SpiaLaboratoryFactory SpiaLaboratoryFactory)
    {
      this.PatientFactory = SpiaPatientFactory;
      this.ProviderFactory = SpiaProviderFactory;
      this.LaboratoryFactory = SpiaLaboratoryFactory;
    }

    public PathologyReportContainer GetReport()
    {
      var RequestedDate =            new DateTimeOffset(2020, 07, 25, 00, 00, 00, TimeSpan.FromHours(10));
      var CollectionDateTime =       new DateTimeOffset(2020, 07, 25, 18, 20, 00, TimeSpan.FromHours(10));
      var SpecimenReceivedDateTime = new DateTimeOffset(2020, 07, 25, 19, 32, 00, TimeSpan.FromHours(10));
      var ReportReleaseDateTime =    new DateTimeOffset(2020, 07, 26, 10, 36, 00, TimeSpan.FromHours(10));
      var ObservationDateTime = ReportReleaseDateTime.Subtract(TimeSpan.FromMinutes(5));

      return new PathologyReportContainer()
      {
        PathologyReport = new PathologyReport()
        {
          PerformingLaboratory = LaboratoryFactory.GetPITUSLaboratory(),
          Patient = PatientFactory.GetGeorgeWHITEWATER(),
          Request = new Request()
          {
            RequestedDate = RequestedDate,
            OrderNumber = "00000016",
            RequestingFacility = new Organisation()
            {
              Name = "Sunrise Hospital Respiratory Clinic",
              Identifier = new Identifier()
              {
                Value = "13A8DC14-A1E9-475C-9B4C-DA19866E020A",
                Type = IdentifierType.GUID
              }
            },
            RequestingApplication = "Best Practice 1.8.5.743",
            RequestingProvider = ProviderFactory.GetTrishFamilyDr(MedicareProviderNumber: "067709AH"),
            ClinicalNotes = "Sore throat & cough, works in hospitality",
            CallBackPhoneNumber = null,
            CopyToList = new List<Provider>()
            {
              ProviderFactory.GetMyPhysio()
            }
          },
          PdfFileName = "Exemplar Report SARS-CoV-2 Serology v0.3.pdf",
          ReportList = new List<Report>()
          {
            new Report()
            {
              ReportId = "1978881860",
              CollectionDateTime = CollectionDateTime,
              SpecimenReceivedDateTime = SpecimenReceivedDateTime,
              ReportReleaseDateTime = ReportReleaseDateTime,
              ReportType = new ReportType()
              {
                Local = new Code() { Term = "COVID2SER", Description = "SARS-CoV-2 Serology" },
                Snomed = new Code() { Term = "1454651000168108", Description = "SARS-CoV-2 serology" }
              },
              ReportStatus =  ResultStatusType.Final,
              Department = DiagnosticService.Serology,
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
                        Term = "COVID2IGA",
                        Description = "SARS-CoV-2 IgA IF"
                      },
                      Lonic = new Code()
                      {
                        Term = "95427-1",
                        Description = "SARS-CoV-2 (COVID-19) IgA IF [Titer]"
                      }
                    },
                    DataType = "NM",
                    Value = "20",
                    Units = null,
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
                        Term = "COVID2IGM",
                        Description = "SARS-CoV-2 IgM IF"
                      },
                      Lonic = new Code()
                      {
                        Term = "95429-7",
                        Description = "SARS-CoV-2 (COVID-19) IgG IF [Titer]"
                      }
                    },
                    DataType = "NM",
                    Value = "10",
                    Units = null,
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
                        Term = "COVID2IGG",
                        Description = "SARS-CoV-2 IgG IF"
                      },
                      Lonic = new Code()
                      {
                        Term = "95428-9",
                        Description = "SARS-CoV-2 (COVID-19) IgM IF [Titer]"
                      }
                    },
                    DataType = "NM",
                    Value = "320",
                    Units = null,
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
                        Term = "INTERP",
                        Description = "Interpretation"
                      },
                      Lonic = null,
                    },
                    DataType = "FT",
                    Value = "These results suggest resolving SARS-CoV-2 infection. For further information please contact the medical virologist on(07) 5454 0387.\\.br\\" +
                            "virologist on (07) 5454 0387.\\.br\\" +
                            "\\.br\\" +
                            "N.B. SARS-Co-V-2 is the cause of coronavirus disease (CoVID-19).\\.br\\" +
                            "The Immunofluorescent tests used have been evaluated and validated but not yet registered with NATA.\\.br\\" +
                            "Results have been issued to the serious threat to public health from CoVID-19.\\.br\\" +
                            "These results have been electronically notified to the Ministry of Health.\\.br\\",
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
