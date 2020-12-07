using Spia.PathologyReportModel.Model;
using System;
using System.Collections.Generic;

namespace Spia.PathologyReportModel.Factory.SpiaReports
{
  public class BloodGasArterial : IReportFactory
  {
    private readonly SpiaPatientFactory PatientFactory;
    private readonly SpiaProviderFactory ProviderFactory;
    private readonly SpiaLaboratoryFactory LaboratoryFactory;
    public BloodGasArterial(SpiaPatientFactory SpiaPatientFactory, SpiaProviderFactory SpiaProviderFactory, SpiaLaboratoryFactory SpiaLaboratoryFactory)
    {
      this.PatientFactory = SpiaPatientFactory;
      this.ProviderFactory = SpiaProviderFactory;
      this.LaboratoryFactory = SpiaLaboratoryFactory;
    }

    public PathologyReportContainer GetReport()
    {
      var RequestedDate =            new DateTimeOffset(2019, 11, 09, 00, 00, 00, TimeSpan.FromHours(10));
      var CollectionDateTime =       new DateTimeOffset(2019, 11, 09, 07, 40, 00, TimeSpan.FromHours(10));
      var SpecimenReceivedDateTime = new DateTimeOffset(2019, 11, 09, 07, 41, 00, TimeSpan.FromHours(10));
      var ReportReleaseDateTime =    new DateTimeOffset(2019, 11, 10, 07, 45, 00, TimeSpan.FromHours(10));
      var ObservationDateTime = ReportReleaseDateTime.Subtract(TimeSpan.FromMinutes(5));

      return new PathologyReportContainer()
      {
        PathologyReport = new PathologyReport()
        {
          PerformingLaboratory = LaboratoryFactory.GetPITUSLaboratory(),
          Patient = PatientFactory.GetGlennFERNIE(),
          Request = new Request()
          {
            RequestedDate = RequestedDate,
            OrderNumber = "00000014",
            RequestingFacility = new Organisation()
            {
              Name = "Sunshine Hospital Emergency Dept",
              Identifier = new Identifier()
              {
                Value = "143569C9-8AFC-4BBD-A663-95079AE10B57",
                Type = IdentifierType.GUID
              }
            },
            RequestingApplication = "Best Practice 1.8.5.743",
            RequestingProvider = ProviderFactory.GetTrishFamilyDr(MedicareProviderNumber: "873721DH"),
            ClinicalNotes = "Recent history uncontrolled diabetes; Shortness of breath",
            CallBackPhoneNumber = null,
            CopyToList = new List<Provider>()
            {
              ProviderFactory.GetDiabetesClinicSunshineHospital(),             
            }
          },
          PdfFileName = "SPIA Exemplar Report Blood Gases v1.3.pdf",
          ReportList = new List<Report>()
          {
            new Report()
            {
              ReportId = "2078881880",
              CollectionDateTime = CollectionDateTime,
              SpecimenReceivedDateTime = SpecimenReceivedDateTime,
              ReportReleaseDateTime = ReportReleaseDateTime,
              ReportType = new ReportType()
              {
                //As of meeting on 10/11/2020 @ 2:00PM changed this to only Blood gases
                Local = new Code() { Term = "BGAS", Description = "Blood gases" },
                Snomed = null
                //Local = new Code() { Term = "BGASA", Description = "Blood gas arterial" },
                //Snomed = new Code() { Term = "91308007", Description = "Blood gases, arterial measurement" }
              },
              ReportStatus =  ResultStatusType.Final,
              Department = DiagnosticService.Chemistry,
              ReportingPathologist = ProviderFactory.GetMarioPathologist(),
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
                        Term = "OIF",
                        Description = "Oxygen inspired fraction"
                      },
                      Lonic = new Code()
                      {
                        Term = "3151-8",
                        Description = "Oxygen inspired fraction"
                      }
                    },
                    DataType = "NM",
                    Value = "0.5",
                    //Confirmed that units are not required Meeting on the 10/11/2020 @ 2:00PM
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
                        Term = "BTEMP",
                        Description = "Body temperature"
                      },
                      Lonic = new Code()
                      {
                        Term = "8310-5",
                        Description = "Body temperature"
                      }
                    },
                    DataType = "NM",
                    Value = "37.2",
                    Units = "Cel", //Cel, Cel, UCUM
                    ReferenceRange = "36.5-37.5",
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
                        Term = "PHBLOOD",
                        Description = "pH blood"
                      },
                      Lonic = new Code()
                      {
                        Term = "2744-1",
                        Description = "pH arterial"
                      }
                    },
                    DataType = "NM",
                    Value = "7.50",
                    Units = "pH",// [pH], pH, UCUM
                    ReferenceRange = "7.30-7.40",
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
                        Term = "PCO2",
                        Description = "pCO2 arterial"
                      },
                      Lonic = new Code()
                      {
                        Term = "2019-8",
                        Description = "pCO2 arterial"
                      }
                    },
                    DataType = "NM",
                    Value = "30",
                    Units = "mmHg",// mm[Hg], mmHg, UCUM
                    ReferenceRange = "32-45",
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
                        Term = "PO2A",
                        Description = "pO2 arterial"
                      },
                      Lonic = new Code()
                      {
                        Term = "2019-8",
                        Description = "pO2 arterial"
                      }
                    },
                    DataType = "NM",
                    Value = "61",
                    Units = "mmHg",// mm[Hg], mmHg, UCUM
                    ReferenceRange = "83-108",
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
                        Term = "OXYCA",
                        Description = "Oxygen content arterial"
                      },
                      Lonic = new Code()
                      {
                        Term = "19218-7",
                        Description = "Oxygen content arterial"
                      }
                    },
                    DataType = "NM",
                    Value = "89",
                    Units = "mol/L",// mol/L, mol/L, UCUM
                    ReferenceRange = "94-98",
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
                        Term = "BICART",
                        Description = "Bicarbonate blood"
                      },
                      Lonic = new Code()
                      {
                        Term = "1960-4",
                        Description = "Bicarbonate arterial"
                      }
                    },
                    DataType = "NM",
                    Value = "27",
                    Units = "mmol/L",// mmol/L, mmol/L, UCUM
                    ReferenceRange = "22-30",
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
                        Term = "BEART",
                        Description = "Base excess blood"
                      },
                      Lonic = new Code()
                      {
                        Term = "1925-7",
                        Description = "Base excess arterial"
                      }
                    },
                    DataType = "NM",
                    Value = "4.1",
                    Units = "mmol/L",// mmol/L, mmol/L, UCUM
                    ReferenceRange = "-3.0-3.0",
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
                        Term = "OXSART",
                        Description = "Oxygen saturation arterial"
                      },
                      Lonic = new Code()
                      {
                        Term = "51733-4 ",
                        Description = "Oxygen saturation arterial"
                      }
                    },
                    DataType = "NM",
                    Value = "92",
                    Units = "%",// %, %, UCUM
                    ReferenceRange = "94-98",
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
                        Term = "OXSART",
                        Description = "Sodium blood"
                      },
                      Lonic = new Code()
                      {
                        Term = "2947-0",
                        Description = "Sodium blood"
                      }
                    },
                    DataType = "NM",
                    Value = "137",
                    Units = "mmol/L",// mmol/L, mmol/L, UCUM
                    ReferenceRange = "135-145",
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
                        Term = "KBLOOD",
                        Description = "Potassium blood"
                      },
                      Lonic = new Code()
                      {
                        Term = "6298-4",
                        Description = "Potassium blood"
                      }
                    },
                    DataType = "NM",
                    Value = "3.5",
                    Units = "mmol/L",// mmol/L, mmol/L, UCUM
                    ReferenceRange = "3.5-5.2",
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
                        Term = "CLBLOOD",
                        Description = "Chloride blood"
                      },
                      Lonic = new Code()
                      {
                        Term = "2069-3",
                        Description = "Chloride blood"
                      }
                    },
                    DataType = "NM",
                    Value = "103",
                    Units = "mmol/L",// mmol/L, mmol/L, UCUM
                    ReferenceRange = "95-110",
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
                        Term = "UREAB",
                        Description = "Urea blood"
                      },
                      Lonic = new Code()
                      {
                        Term = "72903-8",
                        Description = "Urea blood"
                      }
                    },
                    DataType = "NM",
                    Value = "4.9",
                    Units = "mmol/L",// mmol/L, mmol/L, UCUM
                    ReferenceRange = "3.8-8.5",
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
                        Term = "CREATB",
                        Description = "Creatinine blood"
                      },
                      Lonic = new Code()
                      {
                        Term = "59826-8",
                        Description = "Creatinine blood"
                      }
                    },
                    DataType = "NM",
                    Value = "61",
                    Units = "umol/L",// umol/L, umol/L, UCUM
                    ReferenceRange = "60-110",
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
                        Term = "AGAPB",
                        Description = "Anion gap blood"
                      },
                      Lonic = new Code()
                      {
                        Term = "41276-7",
                        Description = "Anion gap blood"
                      }
                    },
                    DataType = "NM",
                    Value = "7",
                    Units = "mmol/L",// mmol/L, mmol/L, UCUM
                    ReferenceRange = "4-13",
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
                        Term = "GLUCB",
                        Description = "Glucose blood"
                      },
                      Lonic = new Code()
                      {
                        Term = "15074-8",
                        Description = "Glucose blood"
                      }
                    },
                    DataType = "NM",
                    Value = "9.1",
                    Units = "mmol/L",// mmol/L, mmol/L, UCUM
                    ReferenceRange = "3.0-7.8",
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
                        Term = "LACTB",
                        Description = "Lactate blood "
                      },
                      Lonic = new Code()
                      {
                        Term = "32693-4",
                        Description = "Lactate blood "
                      }
                    },
                    DataType = "NM",
                    Value = "0.8",
                    Units = "mmol/L",// mmol/L, mmol/L, UCUM
                    ReferenceRange = "< 1.0",
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
                        Term = "CAIB",
                        Description = "Calcium ionised blood"
                      },
                      Lonic = new Code()
                      {
                        Term = "1994-3",
                        Description = "Calcium ionised blood"
                      }
                    },
                    DataType = "NM",
                    Value = "1.15",
                    Units = "mmol/L",// mmol/L, mmol/L, UCUM
                    ReferenceRange = "1.15-1.32",
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
                        Term = "HBCALC",
                        Description = "Haemoglobin calculated"
                      },
                      Lonic = new Code()
                      {
                        Term = "20509-6",
                        Description = "Haemoglobin calculated"
                      }
                    },
                    DataType = "NM",
                    Value = "148",
                    Units = "g/L",// g/L, g/L, UCUM
                    ReferenceRange = "135-180",
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
                        Term = "OXYHBART",
                        Description = "Oxyhaemoglobin arterial"
                      },
                      Lonic = new Code()
                      {
                        Term = "2714-4",
                        Description = "Oxyhaemoglobin arterial"
                      }
                    },
                    DataType = "NM",
                    Value = "98",
                    Units = "%",// %, %, UCUM
                    ReferenceRange = "94-98",
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
                        Term = "CARBHB",
                        Description = "Carboxyhaemoglobin"
                      },
                      Lonic = new Code()
                      {
                        Term = "20563-3",
                        Description = "Carboxyhaemoglobin"
                      }
                    },
                    DataType = "SN",
                    Value = "<^0.2",
                    Units = "%",// %, %, UCUM
                    ReferenceRange = "< 1.5",
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
                        Term = "METHHB",
                        Description = "Methaemoglobin"
                      },
                      Lonic = new Code()
                      {
                        Term = "2614-6",
                        Description = "Methaemoglobin"
                      }
                    },
                    DataType = "NM",
                    Value = "0.2",
                    Units = "%",// %, %, UCUM
                    ReferenceRange = "< 1.1",
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
                    Value = "Chemistry parameters are indicative of respiratory alkalosis.",
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
