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
      var RequestedDate =             new DateTimeOffset(2019, 09, 24, 00, 00, 00, TimeSpan.FromHours(10));
      var CollectionDateTime =        new DateTimeOffset(2019, 09, 25, 08, 20, 00, TimeSpan.FromHours(10));
      var SpecimenReceivedDateTime =  new DateTimeOffset(2019, 09, 25, 10, 05, 00, TimeSpan.FromHours(10));
      var ReportReleaseDateTime =     new DateTimeOffset(2019, 09, 25, 11, 55, 00, TimeSpan.FromHours(10));
      var ObservationDateTime = ReportReleaseDateTime.Subtract(TimeSpan.FromMinutes(5));
      return new PathologyReportContainer()
      {
        PathologyReport = new PathologyReport()
        {
          PerformingLaboratory = LaboratoryFactory.GetPITUSLaboratory(),
          Patient = PatientFactory.GetGraceROSSLAND(),
          Request = new Request()
          {
            RequestedDate = RequestedDate,
            OrderNumber = "00000006",
            RequestingFacility = new Organisation()
            {
              Name = "Sunrise Hospital Antenatal Clinic",
              Identifier = new Identifier()
              {
                Value = "143569C9-8AFC-4BBD-A663-95079AE10B57",
                Type = IdentifierType.GUID
              }
            },
            RequestingApplication = "Best Practice 1.8.5.743",
            RequestingProvider = ProviderFactory.GetTrishFamilyDr(MedicareProviderNumber: "8203015Y"),
            ClinicalNotes = "First trimester antenatal screen, ~ 10 weeks pregnant (G1P0)",
            CallBackPhoneNumber = null,
            CopyToList = new List<Provider>()
            {
              ProviderFactory.GetBiancaMidwife()
            }
          },
          PdfFileName = "Exemplar Report HbsAb v1.5.pdf",
          ReportList = new List<Report>()
          {
            new Report()
            {
              ReportId = "1978881817",
              CollectionDateTime = CollectionDateTime,
              SpecimenReceivedDateTime = SpecimenReceivedDateTime,
              ReportReleaseDateTime = ReportReleaseDateTime,
              ReportType = new ReportType()
              {
                Local = new Code() { Term = "HepBSer", Description = "Hepatitis B serology" },
                Snomed = null
                //Snomed = new Code() { Term = "315130004", Description = "Hepatitis B surface antibody level" }
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
                        Term = "INTER",
                        Description = "Interpretation"
                      },
                      Lonic = null
                    },
                    DataType = "FT",
                    Value = "Positive HBsAb result indicates sufficient Hepatitis B immunity.",
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
