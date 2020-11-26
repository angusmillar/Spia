using Spia.PathologyReportModel.Model;
using System;
using System.Collections.Generic;

namespace Spia.PathologyReportModel.Factory.SpiaReports
{
  public class Lipids : IReportFactory
  {
    private readonly SpiaPatientFactory PatientFactory;
    private readonly SpiaProviderFactory ProviderFactory;
    private readonly SpiaLaboratoryFactory LaboratoryFactory;
    public Lipids(SpiaPatientFactory SpiaPatientFactory, SpiaProviderFactory SpiaProviderFactory, SpiaLaboratoryFactory SpiaLaboratoryFactory)
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
          Patient = PatientFactory.GetPatient(PatientType.GregoryBLACKCOMB),
          Request = new Request()
          {
            RequestedDate = new DateTimeOffset(2019, 11, 09, 00, 00, 00, TimeSpan.FromHours(10)),
            OrderNumber = "00000011",
            RequestingFacility = new Organisation()
            {
              Name = "Cardiology Clinic",
              Identifier = new Identifier()
              {
                Value = "55023B97-61F8-4445-8590-4F18AD68E9AD",
                Type = IdentifierType.GUID
              }
            },
            RequestingApplication = "Best Practice 1.8.5.743",
            RequestingProvider = ProviderFactory.GetTrishFamilyDr(MedicareProviderNumber: "3243890Y"),
            ClinicalNotes = "Knee surgery 12-Sept-19. Weight gain becoming problematic.",
            CallBackPhoneNumber = null,
            CopyToList = new List<Provider>()
            {
              ProviderFactory.GetCardiologyClinicSunshineHospital(),
              ProviderFactory.GetGBastien()
            }
          },
          PdfFileName = "SPIA Exemplar Report Lipids v1.6.pdf",
          ReportList = new List<Report>()
          {
            new Report()
            {
              ReportId = "1978881822",
              CollectionDateTime = new DateTimeOffset(2019, 12, 04, 07, 36, 00, TimeSpan.FromHours(10)),
              SpecimenReceivedDateTime = new DateTimeOffset(2019, 12, 04, 10, 14, 00, TimeSpan.FromHours(10)),
              ReportReleaseDateTime = new DateTimeOffset(2019, 12, 04, 14, 32, 00, TimeSpan.FromHours(10)),
              ReportType = new ReportType()
              {
                Local = new Code() { Term = "LIPIDS", Description = "Lipids" },
                Snomed = new Code() { Term = "252150008", Description = "Fasting lipid profile" }
              },
              ReportStatus =  ResultStatusType.Final,
              Department = DiagnosticService.Chemistry,
              ReportingPathologist = ProviderFactory.GetManuelDelPathologist(),
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
                        Term = "CHOL",
                        Description = "Cholesterol"
                      },
                      Lonic = new Code()
                      {
                        Term = "14647-2",
                        Description = "Cholesterol"
                      }
                    },
                    DataType = "NM",
                    Value = "6.2",
                    Units = "mmol/L",
                    ReferenceRange = "< 5.5",
                    AbnormalFlag = "H",
                    ObservationDateTime = new DateTimeOffset(2019, 12, 04, 14, 32, 00, TimeSpan.FromHours(10)).Subtract(TimeSpan.FromMinutes(5)),
                    Status = ResultStatusType.Final,
                    ChildResultList = null
                  },
                  new Result()
                  {
                    Type = new ResultType()
                    {
                      Local = new Code()
                      {
                        Term = "TRIGF",
                        Description = "Triglycerides fasting"
                      },
                      Lonic = new Code()
                      {
                        Term = "30524-3",
                        Description = "Triglycerides fasting"
                      }
                    },
                    DataType = "NM",
                    Value = "2.0",
                    Units = "mmol/L",
                    ReferenceRange = "< 1.7",
                    AbnormalFlag = "H",
                    ObservationDateTime = new DateTimeOffset(2019, 12, 04, 14, 32, 00, TimeSpan.FromHours(10)).Subtract(TimeSpan.FromMinutes(5)),
                    Status = ResultStatusType.Final,
                    ChildResultList = null
                  },
                  new Result()
                  {
                    Type = new ResultType()
                    {
                      Local = new Code()
                      {
                        Term = "HDL",
                        Description = "HDL cholesterol"
                      },
                      Lonic = new Code()
                      {
                        Term = "14646-4",
                        Description = "HDL cholesterol"
                      }
                    },
                    DataType = "NM",
                    Value = "3.2",
                    Units = "mmol/L",
                    ReferenceRange = "> 1.0",
                    AbnormalFlag = "N",
                    ObservationDateTime = new DateTimeOffset(2019, 12, 04, 14, 32, 00, TimeSpan.FromHours(10)).Subtract(TimeSpan.FromMinutes(5)),
                    Status = ResultStatusType.Final,
                    ChildResultList = null
                  },
                  new Result()
                  {
                    Type = new ResultType()
                    {
                      Local = new Code()
                      {
                        Term = "HDL",
                        Description = "LDL Cholesterol"
                      },
                      Lonic = new Code()
                      {
                        Term = "22748-8",
                        Description = "LDL Cholesterol"
                      }
                    },
                    DataType = "NM",
                    Value = "2.1",
                    Units = "mmol/L",
                    ReferenceRange = "< 3.0",
                    AbnormalFlag = "N",
                    ObservationDateTime = new DateTimeOffset(2019, 12, 04, 14, 32, 00, TimeSpan.FromHours(10)).Subtract(TimeSpan.FromMinutes(5)),
                    Status = ResultStatusType.Final,
                    ChildResultList = null
                  },
                  new Result()
                  {
                    Type = new ResultType()
                    {
                      Local = new Code()
                      {
                        Term = "CHRATIO",
                        Description = "Cholesterol/HDL cholesterol"
                      },
                      Lonic = new Code()
                      {
                        Term = "32309-7",
                        Description = "Chol/HDL Ratio"
                      }
                    },
                    DataType = "NM",
                    Value = "3.0",
                    Units = "mmol/L",
                    ReferenceRange = "< 3.5",
                    AbnormalFlag = "N",
                    ObservationDateTime = new DateTimeOffset(2019, 12, 04, 14, 32, 00, TimeSpan.FromHours(10)).Subtract(TimeSpan.FromMinutes(5)),
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
                      Lonic = new Code()
                      {
                        Term = "49262-9",
                        Description = "Fatty acids pattern Nar [Interp]"
                      }
                    },
                    DataType = "FT",
                    Value = "Absolute cardiovascular risk assessment should be performed on all adults aged between 45-75\\.br\\" +
                    "years without existing CVD or not already known to be at increased risk of CVD.\\.br\\" +
                    "A CBD risk calculator is provided at www.cvdcheck.org.au.",
                    Units = null,
                    ReferenceRange = null,
                    AbnormalFlag = null,
                    ObservationDateTime = new DateTimeOffset(2019, 12, 04, 14, 32, 00, TimeSpan.FromHours(10)).Subtract(TimeSpan.FromMinutes(5)),
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
