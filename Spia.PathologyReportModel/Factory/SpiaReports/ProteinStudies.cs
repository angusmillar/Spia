using Spia.PathologyReportModel.Model;
using System;
using System.Collections.Generic;

namespace Spia.PathologyReportModel.Factory.SpiaReports
{
  public class ProteinStudies : IReportFactory
  {
    private readonly SpiaPatientFactory PatientFactory;
    private readonly SpiaProviderFactory ProviderFactory;
    private readonly SpiaLaboratoryFactory LaboratoryFactory;
    public ProteinStudies(SpiaPatientFactory SpiaPatientFactory, SpiaProviderFactory SpiaProviderFactory, SpiaLaboratoryFactory SpiaLaboratoryFactory)
    {
      this.PatientFactory = SpiaPatientFactory;
      this.ProviderFactory = SpiaProviderFactory;
      this.LaboratoryFactory = SpiaLaboratoryFactory;
    }

    public PathologyReportContainer GetReport()
    {
      var RequestedDate =            new DateTimeOffset(2019, 11, 08, 00, 00, 00, TimeSpan.FromHours(10));
      var CollectionDateTime =       new DateTimeOffset(2019, 11, 09, 10, 25, 00, TimeSpan.FromHours(10));
      var SpecimenReceivedDateTime = new DateTimeOffset(2019, 11, 09, 11, 15, 00, TimeSpan.FromHours(10));
      var ReportReleaseDateTime =    new DateTimeOffset(2019, 11, 10, 09, 35, 00, TimeSpan.FromHours(10));
      var ObservationDateTime = ReportReleaseDateTime.Subtract(TimeSpan.FromMinutes(5));

      return new PathologyReportContainer()
      {
        PathologyReport = new PathologyReport()
        {
          PerformingLaboratory = LaboratoryFactory.GetPITUSLaboratory(),
          Patient = PatientFactory.GetEricaPURCELL(),
          Request = new Request()
          {
            RequestedDate = RequestedDate,
            OrderNumber = "00000013",
            RequestingFacility = new Organisation()
            {
              Name = "Sunrise Hospital Immunology Clinic",
              Identifier = new Identifier()
              {
                Value = "D220F4D1-D62C-4EE3-8356-F5DA1484370E",
                Type = IdentifierType.GUID
              }
            },
            RequestingApplication = "Best Practice 1.8.5.743",
            RequestingProvider = ProviderFactory.GetTrishFamilyDr(MedicareProviderNumber: "8264815W"),
            ClinicalNotes = "Investigations for Multiple Myeloma",
            CallBackPhoneNumber = null,
            CopyToList = new List<Provider>()
            {              
              ProviderFactory.GetArmandeImmunologist()
            }
          },
          PdfFileName = "Exemplar Report EPP v1.5.pdf",
          ReportList = new List<Report>()
          {
            new Report()
            {
              ReportId = "1978881828",
              CollectionDateTime = CollectionDateTime,
              SpecimenReceivedDateTime = SpecimenReceivedDateTime,
              ReportReleaseDateTime = ReportReleaseDateTime,
              ReportType = new ReportType()
              {
                Local = new Code() { Term = "PROTSTUDY", Description = "Protein studies" },
                Snomed = new Code() { Term = "4903000", Description = "Serum protein electrophoresis" }
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
                        Term = "PROT",
                        Description = "Total Protein"
                      },
                      Lonic = new Code()
                      {
                        Term = "2885-2",
                        Description = "Total Protein"
                      }
                    },
                    DataType = "NM",
                    Value = "174",
                    Units = "g/L",
                    ReferenceRange = "69-75",
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
                        Term = "ALB",
                        Description = "Albumin"
                      },
                      Lonic = new Code()
                      {
                        Term = "2862-1",
                        Description = "Albumin"
                      }
                    },
                    DataType = "NM",
                    Value = "30",
                    Units = "g/L",
                    ReferenceRange = "40-50",
                    AbnormalFlag = "N",
                    ObservationDateTime = ObservationDateTime,
                    Status = ResultStatusType.Final,
                    ChildResultList = null
                  },
                  //new Result()
                  //{
                  //  Type = new ResultType()
                  //  {
                  //    Local = new Code()
                  //    {
                  //      Term = "Globulin",
                  //      Description = "Globulin"
                  //    },
                  //    Lonic = new Code()
                  //    {
                  //      //ToDo: Do we need a Lonic term for Globulin in the Protein SPEP core report
                  //      Term = "10834-0",
                  //      Description = "Globulin"
                  //    }
                  //  },
                  //  DataType = "NM",
                  //  Value = null,
                  //  Units = null,
                  //  ReferenceRange = "28-37",
                  //  AbnormalFlag = "N",
                  //  ObservationDateTime = ObservationDateTime,
                  //  Status = ResultStatusType.Final,
                  //  ChildResultList = null
                  //},                  
                  new Result()
                  {
                    Type = new ResultType()
                    {
                      Local = new Code()
                      {
                        Term = "ALPHAGLOB1",
                        Description = "Alpha-1 globulin"
                      },
                      //ToDo: Do we need a LONIC term for Alpha-1 globulin, is it still 12728-2 which was for Alpha globulin
                      Lonic = null
                    },
                    DataType = "NM",
                    Value = "3",
                    Units = "g/L",
                    ReferenceRange = "3-6",
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
                        Term = "ALPHAGLOB2",
                        Description = "Alpha-2 globuin"
                      },
                      //ToDo: Do we need a LONIC term for Alpha-1 globulin, is it still 12728-2 which was for Alpha globulin
                      Lonic = null
                    },
                    DataType = "NM",
                    Value = "8",
                    Units = "g/L",
                    ReferenceRange = "4-10",
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
                        Term = "BETA1GLOB",
                        Description = "Beta-1 globulin"
                      },
                      //ToDo: Do we need a LONIC term for Beta-1 globulin
                      Lonic = null
                    },
                    DataType = "NM",
                    Value = "5",
                    Units = "g/L",
                    ReferenceRange = "2-6",
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
                        Term = "BETA2GLOB",
                        Description = "Beta-2 globulin"
                      },
                      //ToDo: Do we need a LONIC term for Beta-2 globulin
                      Lonic = null
                    },
                    DataType = "NM",
                    Value = "5",
                    Units = "g/L",
                    ReferenceRange = "2-5",
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
                        Term = "GAMMA",
                        Description = "Gamma globulin"
                      },
                      Lonic = new Code()
                      {
                        Term = "2874-6",
                        Description = "Gamma"
                      }
                    },
                    DataType = "NM",
                    Value = "4",
                    Units = "g/L",
                    ReferenceRange = "6-15",
                    AbnormalFlag = "L",
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
                        Term = "PARAP",
                        Description = "Paraprotein"
                      },
                      Lonic = new Code()
                      {
                        //ToDo: Do we need a Lonic term for Paraprotein in the Protein SPEP core report
                        Term = "??",
                        Description = "Paraprotein"
                      }
                    },
                    DataType = "ST",
                    Value = "Detected",
                    Units = null,
                    ReferenceRange = null,
                    AbnormalFlag = null,
                    ObservationDateTime = ObservationDateTime,
                    Status = ResultStatusType.Final,
                    ChildResultList = new List<Result>()
                    {
                      new Result()
                      {
                        Type = new ResultType()
                        {
                          Local = new Code()
                          {
                            Term = "TYPE",
                            Description = "Type"
                          },
                          //ToDo: Do we need a lonic for the Paraprotein type?
                          Lonic = null
                        },
                        DataType = "ST",
                        Value = "IgM Kappa",
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
                            Term = "CONCENT",
                            Description = "Concentration"
                          },
                          //ToDo: Do we need a lonic for the Paraprotein Concentration?
                          Lonic = null
                        },
                        DataType = "NM",
                        Value = "8",
                        Units = "g/L",
                        ReferenceRange = null,
                        AbnormalFlag = null,
                        ObservationDateTime = ObservationDateTime,
                        Status = ResultStatusType.Final,
                        ChildResultList = null
                      }
                    }
                  },
                  new Result()
                  {
                    Type = new ResultType()
                    {
                      Local = new Code()
                      {
                        Term = "IMMGLOB",
                        Description = "Immunoglobulins"
                      },
                      Lonic = null
                    },
                    DataType = "ST",
                    Value = null,
                    Units = null,
                    ReferenceRange = null,
                    AbnormalFlag = null,
                    ObservationDateTime = ObservationDateTime,
                    Status = ResultStatusType.Final,
                    ChildResultList = new List<Result>()
                    {
                      new Result()
                      {
                        Type = new ResultType()
                        {
                          Local = new Code()
                          {
                            Term = "IMMG",
                            Description = "Immunoglobulin G"
                          },
                          Lonic = new Code()
                          {
                            //ToDo: Is this still the correct Lonic for the named changed Immunoglobulin G
                            Term = "44597-3",
                            Description = "IgG monoclonal"
                          }
                        },
                        DataType = "NM",
                        Value = "4",
                        Units = "g/L",
                        ReferenceRange = "5-16",
                        AbnormalFlag = "L",
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
                            Term = "IMMGA",
                            Description = "Immunoglobulin A"
                          },
                          Lonic = new Code()
                          {
                            //ToDo: Is this still the correct Lonic for the named changed Immunoglobulin A
                            Term = "44587-4",
                            Description = "IgA monoclonal"
                          }
                        },
                        DataType = "NM",
                        Value = "7.0",
                        Units = "g/L",
                        ReferenceRange = "0.5-3.5",
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
                            Term = "IMMGAM",
                            Description = "Immunoglobulin M"
                          },
                          Lonic = new Code()
                          {
                            //ToDo: Is this still the correct Lonic for the named changed Immunoglobulin M
                            Term = "44601-3",
                            Description = "IgM monoclonal"
                          }
                        },
                        DataType = "NM",
                        Value = "8.9",
                        Units = "g/L",
                        ReferenceRange = "0.2-3.9",
                        AbnormalFlag = "N",
                        ObservationDateTime = ObservationDateTime,
                        Status = ResultStatusType.Final,
                        ChildResultList = null
                      }
                    }
                  },
                  new Result()
                  {
                    Type = new ResultType()
                    {
                      Local = new Code()
                      {
                        Term = "INTERP",
                        Description = "Protein electrophoresis pattern comments"
                      },
                      Lonic = new Code()
                      {
                        Term = "49298-3",
                        Description = "Protein Fractions Nar [Interp]"
                      }
                    },
                    DataType = "FT",
                    Value = "Decreased residual gammaglobulins. Suggest urine protein electrophoresis and immunofixation.",
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
