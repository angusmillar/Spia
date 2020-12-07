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
      var RequestedDate =            new DateTimeOffset(2019, 11, 09, 00, 00, 00, TimeSpan.FromHours(10));
      var CollectionDateTime =       new DateTimeOffset(2019, 11, 09, 07, 40, 00, TimeSpan.FromHours(10));
      var SpecimenReceivedDateTime = new DateTimeOffset(2019, 11, 09, 08, 17, 00, TimeSpan.FromHours(10));
      var ReportReleaseDateTime =    new DateTimeOffset(2019, 11, 09, 10, 25, 00, TimeSpan.FromHours(10));
      var ObservationDateTime = ReportReleaseDateTime.Subtract(TimeSpan.FromMinutes(5));

      return new PathologyReportContainer()
      {
        PathologyReport = new PathologyReport()
        {
          PerformingLaboratory = LaboratoryFactory.GetPITUSLaboratory(),
          Patient = PatientFactory.GetReaganPHOENIX(),
          Request = new Request()
          {
            RequestedDate = RequestedDate,
            OrderNumber = "00000011",
            RequestingFacility = new Organisation()
            {
              Name = "Sunrise Hospital Cardiology Clinic",
              Identifier = new Identifier()
              {
                Value = "55023B97-61F8-4445-8590-4F18AD68E9AD",
                Type = IdentifierType.GUID
              }
            },
            RequestingApplication = "Best Practice 1.8.5.743",
            RequestingProvider = ProviderFactory.GetTrishFamilyDr(MedicareProviderNumber: "3243890Y"),
            ClinicalNotes = "Knee surgery 12-Sep-19; Increasing weight gain",
            CallBackPhoneNumber = null,
            CopyToList = new List<Provider>()
            {
              ProviderFactory.GetCardiologyClinicSunshineHospital(),
              ProviderFactory.GetGBastien()
            }
          },
          PdfFileName = "SPIA Exemplar Report Lipids v1.5.pdf",
          ReportList = new List<Report>()
          {
            new Report()
            {
              ReportId = "1978881822",
              CollectionDateTime = CollectionDateTime,
              SpecimenReceivedDateTime = SpecimenReceivedDateTime,
              ReportReleaseDateTime = ReportReleaseDateTime,
              ReportType = new ReportType()
              {
                Local = new Code() { Term = "LIPIDS", Description = "Lipid profile" },
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
                    Units = "mmol/L",// mmol/L, mmol/L, UCUM
                    ReferenceRange = "< 5.5",
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
                    Units = "mmol/L",// mmol/L, mmol/L, UCUM
                    ReferenceRange = "> 1.2",
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
                        Term = "TRIGR",
                        Description = "Triglycerides random"
                      },                      
                      Lonic = new Code()
                      {
                        Term = "14927-8",
                        Description = "Triglycerides random"
                      }
                    },
                    DataType = "NM",
                    Value = "2.3",
                    Units = "mmol/L",// mmol/L, mmol/L, UCUM
                    ReferenceRange = "< 2.0",
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
                    Units = "mmol/L",// mmol/L, mmol/L, UCUM
                    ReferenceRange = "< 3.0",
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
                    Value = "1.9",
                    Units = null,
                    ReferenceRange = "< 3.5",
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
                        Term = "NHDLC",
                        Description = "Non HDL Cholesterol"
                      },                    
                      Lonic = new Code()
                      {
                        Term = "70204-3",
                        Description = "Non HDL Cholesterol "
                      }
                    },
                    DataType = "NM",
                    Value = "2.1",
                    Units = "mmol/L",// mmol/L, mmol/L, UCUM
                    ReferenceRange = "< 4.0",
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
