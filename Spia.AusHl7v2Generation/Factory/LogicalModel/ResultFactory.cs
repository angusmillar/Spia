using Spia.AusHl7v2Generation.Model.Logical;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using PeterPiper.Hl7.V2.Model;

namespace Spia.AusHl7v2Generation.Factory.LogicalModel
{
  public static class ResultFactory
  {
    public static IEnumerable<Result> GetChlamydiaResultList(DateTimeOffset AnalyserDateTime, string PdfFileName)
    {
      var ObservationDateTime = AnalyserDateTime;
      //var ProducerId = new CodedElement(new Coded("OtherPath", "1234", "AUSNATA"), null);
      var ResultList = new List<Result>();

      //Chlamydia
      ResultList.Add(new Result(
        setId: (ResultList.Count + 1).ToString(CultureInfo.CurrentCulture),
        dataType: "ST",
        resultType: new CodedElement(
          local: new Coded("CHLY", "Chlamydia trachomatis DNA", $"NATA{LabInfo.NATAAccNumber}"),
          international: new Coded("21613-5", "Chlamydia trachomatis DNA", "LN")),
        value: Creator.Element("Negative"),
        status: "F")
      {
        Units = null,
        ReferenceRange = null,
        AbnormalFlag = "N",
        ObservationDateTime = ObservationDateTime,
        //ProducerId = ProducerId
      });

      ResultList.Add(PDFDisplaySegment(ResultList.Count + 1, ObservationDateTime, PdfFileName));

      return ResultList;
    }
    public static IEnumerable<Result> GetEUCResultList(DateTimeOffset AnalyserDateTime, string PdfFileName)
    {
      var ObservationDateTime = AnalyserDateTime;
      //var ProducerId = new CodedElement(new Coded("OtherPath", "1234", "AUSNATA"), null);
      var ResultList = new List<Result>();

      //Sodium
      ResultList.Add(new Result(
        setId: (ResultList.Count + 1).ToString(CultureInfo.CurrentCulture),
        dataType: "NM",
        resultType: new CodedElement(
          local: new Coded("Na", "Sodium", $"NATA{LabInfo.NATAAccNumber}"),
          international: new Coded("2951-2", "Sodium", "LN")),
        value: Creator.Element("136"),
        status: "F")
      {
        Units = "mmol/L",
        ReferenceRange = "135-145",
        AbnormalFlag = "N",
        ObservationDateTime = ObservationDateTime,
        //ProducerId = ProducerId
      });

      //Potassium
      ResultList.Add(new Result(
        setId: (ResultList.Count + 1).ToString(CultureInfo.CurrentCulture),
        dataType: "NM",
        resultType: new CodedElement(
          local: new Coded("K", "Potassium", $"NATA{LabInfo.NATAAccNumber}"),
          international: new Coded("2823-3", "Potassium", "LN")),
        value: Creator.Element("5.2"),
        status: "F")
      {
        Units = "mmol/L",
        ReferenceRange = "3.5-5.2",
        AbnormalFlag = "N",
        ObservationDateTime = ObservationDateTime,
        //ProducerId = ProducerId
      });

      //Chloride
      ResultList.Add(new Result(
        setId: (ResultList.Count + 1).ToString(CultureInfo.CurrentCulture),
        dataType: "NM",
        resultType: new CodedElement(
          local: new Coded("Cl", "Chloride", $"NATA{LabInfo.NATAAccNumber}"),
          international: new Coded("2075-0", "Chloride", "LN")),
        value: Creator.Element("9.6"),
        status: "F")
      {
        Units = "mmol/L",
        ReferenceRange = "95-110",
        AbnormalFlag = "N",
        ObservationDateTime = ObservationDateTime,
        //ProducerId = ProducerId
      });

      //Bicarbonate
      ResultList.Add(new Result(
        setId: (ResultList.Count + 1).ToString(CultureInfo.CurrentCulture),
        dataType: "NM",
        resultType: new CodedElement(
          local: new Coded("BICARB", "Bicarbonate", $"NATA{LabInfo.NATAAccNumber}"),
          international: new Coded("1963-8", "Bicarbonate", "LN")),
        value: Creator.Element("27"),
        status: "F")
      {
        Units = "mmol/L",
        ReferenceRange = "22-32",
        AbnormalFlag = "N",
        ObservationDateTime = ObservationDateTime,
        //ProducerId = ProducerId
      });

      //Urea
      ResultList.Add(new Result(
        setId: (ResultList.Count + 1).ToString(CultureInfo.CurrentCulture),
        dataType: "NM",
        resultType: new CodedElement(
          local: new Coded("UREA", "Urea", $"NATA{LabInfo.NATAAccNumber}"),
          international: new Coded("22664-7", "Urea", "LN")),
        value: Creator.Element("5.7"),
        status: "F")
      {
        Units = "mmol/L",
        ReferenceRange = "3.0-8.5",
        AbnormalFlag = "N",
        ObservationDateTime = ObservationDateTime,
        //ProducerId = ProducerId
      });

      //Creatinine
      ResultList.Add(new Result(
        setId: (ResultList.Count + 1).ToString(CultureInfo.CurrentCulture),
        dataType: "NM",
        resultType: new CodedElement(
          local: new Coded("CREAT", "Creatinine", $"NATA{LabInfo.NATAAccNumber}"),
          international: new Coded("14682-9", "Creatinine", "LN")),
        value: Creator.Element("87"),
        status: "F")
      {
        Units = "umol/L",
        ReferenceRange = "60-110",
        AbnormalFlag = "N",
        ObservationDateTime = ObservationDateTime,
        //ProducerId = ProducerId
      });

      //eGFR
      ResultList.Add(new Result(
        setId: (ResultList.Count + 1).ToString(CultureInfo.CurrentCulture),
        dataType: "NM",
        resultType: new CodedElement(
          local: new Coded("EGFR", "Glomerular filtration rate estimated", $"NATA{LabInfo.NATAAccNumber}"),
          international: new Coded("62238-1", "eGFR", "LN")),
        value: Creator.Element("88"),
        status: "F")
      {
        Units = "mL/min/1.73m^2",
        ReferenceRange = "60-120",
        AbnormalFlag = "N",
        ObservationDateTime = ObservationDateTime,
        //ProducerId = ProducerId
      });

      //Interpretation
      ResultList.Add(new Result(
        setId: (ResultList.Count + 1).ToString(CultureInfo.CurrentCulture),
        dataType: "ST",
        resultType: new CodedElement(
          local: new Coded("INTERP", "Interpretation", $"NATA{LabInfo.NATAAccNumber}"),
          international: null),
        value: Creator.Element("All chemistry parameters are within normal limits for age and sex."),
        status: "F")
      {
        Units = null,
        ReferenceRange = null,
        AbnormalFlag = null,
        ObservationDateTime = ObservationDateTime,
      });

      ResultList.Add(PDFDisplaySegment(ResultList.Count + 1, ObservationDateTime, PdfFileName));

      return ResultList;
    }
    public static IEnumerable<Result> GetFBCResultList(DateTimeOffset AnalyserDateTime, string PdfFileName)
    {
      var ObservationDateTime = AnalyserDateTime;
      //var ProducerId = new CodedElement(new Coded("OtherPath", "1234", "AUSNATA"), null);
      var ResultList = new List<Result>();

      //Hemoglobin
      ResultList.Add(new Result(
        setId: (ResultList.Count + 1).ToString(CultureInfo.CurrentCulture),
        dataType: "NM",
        resultType: new CodedElement(
          local: new Coded("HB", "Hemoglobin", $"NATA{LabInfo.NATAAccNumber}"),
          international: new Coded("718-7", "Hemoglobin", "LN")),
        value: Creator.Element("146"),
        status: "F")
      {
        Units = "g/L",
        ReferenceRange = "135-185",
        AbnormalFlag = "N",
        ObservationDateTime = ObservationDateTime,
        //ProducerId = ProducerId
      });

      //Haematocrit
      //OBX|13|NM|4544-3^Haematocrit^LN||0.47|L/L^^UCUM|0.40-0.51||||F
      ResultList.Add(new Result(
        setId: (ResultList.Count + 1).ToString(CultureInfo.CurrentCulture),
        dataType: "NM",
        resultType: new CodedElement(
          local: new Coded("HCT", "Haematocrit", $"NATA{LabInfo.NATAAccNumber}"),
          international: new Coded("4544-3", "Haematocrit", "LN")),
        value: Creator.Element("0.47"),
        status: "F")
      {
        Units = "L/L",
        ReferenceRange = "0.40-0.51",
        AbnormalFlag = "N",
        ObservationDateTime = ObservationDateTime,
        //ProducerId = ProducerId
      });


      //Red Cell Count
      //OBX|14|NM|789-8^Red Cell Count^LN||4.8|10*12/L^^UCUM|4.0-5.8||||F 
      ResultList.Add(new Result(
        setId: (ResultList.Count + 1).ToString(CultureInfo.CurrentCulture),
        dataType: "NM",
        resultType: new CodedElement(
          local: new Coded("RCC", "Red cell count", $"NATA{LabInfo.NATAAccNumber}"),
          international: new Coded("789-8", "Red Cell Count", "LN")),
        value: Creator.Element("4.8"),
        status: "F")
      {
        Units = "10^12/L",        
        ReferenceRange = "4.0-5.8",
        AbnormalFlag = "N",
        ObservationDateTime = ObservationDateTime,
        //ProducerId = ProducerId
      });

      //White cell count      
      ResultList.Add(new Result(
        setId: (ResultList.Count + 1).ToString(CultureInfo.CurrentCulture),
        dataType: "NM",
        resultType: new CodedElement(
          local: new Coded("WCC", "White cell count", $"NATA{LabInfo.NATAAccNumber}"),
          international: new Coded("6690-2", "White cell count", "LN")),
        value: Creator.Element("8.6"),
        status: "F")
      {
        Units = "10^9/L",
        ReferenceRange = "4.0-11.4",
        AbnormalFlag = "N",
        ObservationDateTime = ObservationDateTime,
        //ProducerId = ProducerId
      });

      //Platelet count   
      ResultList.Add(new Result(
        setId: (ResultList.Count + 1).ToString(CultureInfo.CurrentCulture),
        dataType: "NM",
        resultType: new CodedElement(
          local: new Coded("WCC", "Platelet count", $"NATA{LabInfo.NATAAccNumber}"),
          international: new Coded("777-3", "Platelet count", "LN")),
        value: Creator.Element("278"),
        status: "F")
      {
        Units = "10^9/L",
        ReferenceRange = "150-400",
        AbnormalFlag = "N",
        ObservationDateTime = ObservationDateTime,
        //ProducerId = ProducerId
      });

      //Mean Cell Volume      
      ResultList.Add(new Result(
        setId: (ResultList.Count + 1).ToString(CultureInfo.CurrentCulture),
        dataType: "NM",
        resultType: new CodedElement(
          local: new Coded("MCV", "MCV", $"NATA{LabInfo.NATAAccNumber}"),
          international: new Coded("787-2", "Mean Cell Volume", "LN")),
        value: Creator.Element("97"),
        status: "F")
      {
        Units = "fL",
        ReferenceRange = "80-100",
        AbnormalFlag = "N",
        ObservationDateTime = ObservationDateTime,
        //ProducerId = ProducerId
      });

      //Mean Cell Volume      
      ResultList.Add(new Result(
        setId: (ResultList.Count + 1).ToString(CultureInfo.CurrentCulture),
        dataType: "NM",
        resultType: new CodedElement(
          local: new Coded("MCH", "MCH", $"NATA{LabInfo.NATAAccNumber}"),
          international: new Coded("785-6", "Mean cell haemoglobin", "LN")),
        value: Creator.Element("30"),
        status: "F")
      {
        Units = "pg",
        ReferenceRange = "27-36",
        AbnormalFlag = "N",
        ObservationDateTime = ObservationDateTime,
        //ProducerId = ProducerId
      });

      //Neutrophils      
      ResultList.Add(new Result(
        setId: (ResultList.Count + 1).ToString(CultureInfo.CurrentCulture),
        dataType: "NM",
        resultType: new CodedElement(
          local: new Coded("NEUT", "Neutrophils", $"NATA{LabInfo.NATAAccNumber}"),
          international: new Coded("26499-4", "Neutrophils", "LN")),
        value: Creator.Element("2.9"),
        status: "F")
      {
        Units = "10^9/L",        
        ReferenceRange = "1.8-7.2",
        AbnormalFlag = "N",
        ObservationDateTime = ObservationDateTime,
        //ProducerId = ProducerId
      });

      //Lymphocytes      
      ResultList.Add(new Result(
        setId: (ResultList.Count + 1).ToString(CultureInfo.CurrentCulture),
        dataType: "NM",
        resultType: new CodedElement(
          local: new Coded("LYMP", "Lymphocytes", $"NATA{LabInfo.NATAAccNumber}"),
          international: new Coded("26474-7", "Lymphocytes", "LN")),
        value: Creator.Element("1.6"),
        status: "F")
      {
        Units = "10^9/L",
        ReferenceRange = "1.0-4.0",
        AbnormalFlag = "N",
        ObservationDateTime = ObservationDateTime,
        //ProducerId = ProducerId
      });

      //Monocytes    
      ResultList.Add(new Result(
        setId: (ResultList.Count + 1).ToString(CultureInfo.CurrentCulture),
        dataType: "NM",
        resultType: new CodedElement(
          local: new Coded("MONO", "Monocytes", $"NATA{LabInfo.NATAAccNumber}"),
          international: new Coded("26484-6", "Monocytes", "LN")),
        value: Creator.Element("0.4"),
        status: "F")
      {
        Units = "10^9/L",        
        ReferenceRange = "0.1-1.0",
        AbnormalFlag = "N",
        ObservationDateTime = ObservationDateTime,
        //ProducerId = ProducerId
      });

      //Eosinophils    
      ResultList.Add(new Result(
        setId: (ResultList.Count + 1).ToString(CultureInfo.CurrentCulture),
        dataType: "NM",
        resultType: new CodedElement(
          local: new Coded("EOS", "Eosinophils", $"NATA{LabInfo.NATAAccNumber}"),
          international: new Coded("26449-9", "Eosinophils", "LN")),
        value: Creator.Element("0.2"),
        status: "F")
      {
        Units = "10^9/L",        
        ReferenceRange = "0.0-0.5",
        AbnormalFlag = "N",
        ObservationDateTime = ObservationDateTime,
        //ProducerId = ProducerId
      });

      //Basophils    
      ResultList.Add(new Result(
        setId: (ResultList.Count + 1).ToString(CultureInfo.CurrentCulture),
        dataType: "NM",
        resultType: new CodedElement(
          local: new Coded("BASO", "Basophils", $"NATA{LabInfo.NATAAccNumber}"),
          international: new Coded("26444-0", "Basophils", "LN")),
        value: Creator.Element("0.03"),
        status: "F")
      {
        Units = "10^9/L",        
        ReferenceRange = "0.0-0.20",
        AbnormalFlag = "N",
        ObservationDateTime = ObservationDateTime,
        //ProducerId = ProducerId
      });

      //Interpretation    
      ResultList.Add(new Result(
        setId: (ResultList.Count + 1).ToString(CultureInfo.CurrentCulture),
        dataType: "ST",
        resultType: new CodedElement(
          local: new Coded("INTER", "Interpretation", $"NATA{LabInfo.NATAAccNumber}"),
          international: new Coded("18314-5", "Blood film examination", "LN")),
        value: Creator.Element("All haematology parameters are within normal limits for age and sex."),
        status: "F")
      {
        Units = null,
        ReferenceRange = null,
        AbnormalFlag = null,
        ObservationDateTime = ObservationDateTime,
        //ProducerId = ProducerId
      });

      ResultList.Add(PDFDisplaySegment(ResultList.Count + 1, ObservationDateTime, PdfFileName));

      return ResultList;
    }
    public static IEnumerable<Result> GetHFEResultList(DateTimeOffset AnalyserDateTime, string PdfFileName)
    {
      var ObservationDateTime = AnalyserDateTime;
      //var ProducerId = new CodedElement(new Coded("OtherPath", "1234", "AUSNATA"), null);
      var ResultList = new List<Result>();

      //C28Y mutation analysis
      ResultList.Add(new Result(
        setId: (ResultList.Count + 1).ToString(CultureInfo.CurrentCulture),
        dataType: "ST",
        resultType: new CodedElement(
          local: new Coded("C28Y", "C28Y mutation analysis", $"NATA{LabInfo.NATAAccNumber}"),
          international: null),
        value: Creator.Element("Not Detected"),
        status: "F")
      {
        Units = null,
        ReferenceRange = null,
        AbnormalFlag = "N",
        ObservationDateTime = ObservationDateTime,
        //ProducerId = ProducerId
      });

      //H63D mutation analysis 
      ResultList.Add(new Result(
        setId: (ResultList.Count + 1).ToString(CultureInfo.CurrentCulture),
        dataType: "ST",
        resultType: new CodedElement(
          local: new Coded("H63D", "H63D mutation analysis", $"NATA{LabInfo.NATAAccNumber}"),
          international: null),
        value: Creator.Element("Homozygous"),
        status: "F")
      {
        Units = null,
        ReferenceRange = null,
        AbnormalFlag = "N",
        ObservationDateTime = ObservationDateTime,
        //ProducerId = ProducerId
      });

      //Interpretation    
      ResultList.Add(new Result(
        setId: (ResultList.Count + 1).ToString(CultureInfo.CurrentCulture),
        dataType: "ST",
        resultType: new CodedElement(
          local: new Coded("INTER", "Interpretation", $"NATA{LabInfo.NATAAccNumber}"),
          international: null),
        value: Creator.Element("Two copies of the p.His63Asp variant were detected in the patient. The p.Cys282Tyr variant was not detected. The diagnosis of the most common form of HFE-related hereditary haemochromatosis is excluded."),
        status: "F")
      {
        Units = null,
        ReferenceRange = null,
        AbnormalFlag = null,
        ObservationDateTime = ObservationDateTime,
        //ProducerId = ProducerId
      });

      ResultList.Add(PDFDisplaySegment(ResultList.Count + 1, ObservationDateTime, PdfFileName));

      return ResultList;
    }
    public static IEnumerable<Result> GetHepBsAbResultList(DateTimeOffset AnalyserDateTime, string PdfFileName)
    {
      var ObservationDateTime = AnalyserDateTime;
      //var ProducerId = new CodedElement(new Coded("OtherPath", "1234", "AUSNATA"), null);
      var ResultList = new List<Result>();

      //C28Y mutation analysis
      ResultList.Add(new Result(
        setId: (ResultList.Count + 1).ToString(CultureInfo.CurrentCulture),
        dataType: "ST",
        resultType: new CodedElement(
          local: new Coded("HepBsAb", "Hepatitis B surface Ab", $"NATA{LabInfo.NATAAccNumber}"),
          international: new Coded("16935-9", "Hepatitis B surface Ab", "LN")),
        value: Creator.Element("Positive"),
        status: "F")
      {
        Units = null,
        ReferenceRange = null,
        AbnormalFlag = "N",
        ObservationDateTime = ObservationDateTime,
      });

      //Interpretation    
      ResultList.Add(new Result(
        setId: (ResultList.Count + 1).ToString(CultureInfo.CurrentCulture),
        dataType: "ST",
        resultType: new CodedElement(
          local: new Coded("INTER", "Interpretation", $"NATA{LabInfo.NATAAccNumber}"),
          international: null),
        value: Creator.Element("Two copies of the p.His63Asp variant were detected in the patient. The p.Cys282Tyr variant was not detected. The diagnosis of the most common form of HFE-related hereditary haemochromatosis is excluded."),
        status: "F")
      {
        Units = null,
        ReferenceRange = null,
        AbnormalFlag = null,
        ObservationDateTime = ObservationDateTime,
      });

      ResultList.Add(PDFDisplaySegment(ResultList.Count + 1, ObservationDateTime, PdfFileName));

      return ResultList;
    }
    public static IEnumerable<Result> GetImmunoglobulinEResultList(DateTimeOffset AnalyserDateTime, string PdfFileName)
    {
      var ObservationDateTime = AnalyserDateTime;
      //var ProducerId = new CodedElement(new Coded("OtherPath", "1234", "AUSNATA"), null);
      var ResultList = new List<Result>();

      //C28Y mutation analysis
      ResultList.Add(new Result(
        setId: (ResultList.Count + 1).ToString(CultureInfo.CurrentCulture),
        dataType: "NM",
        resultType: new CodedElement(
          local: new Coded("IMME", "Immunoglobulin E", $"NATA{LabInfo.NATAAccNumber}"),
          international: new Coded("19113-0", "Immunoglobulin E IgE", "LN")),
        value: Creator.Element("320"),
        status: "F")
      {
        Units = "kIU/L",
        ReferenceRange = "2.0-300",
        AbnormalFlag = "H",
        ObservationDateTime = ObservationDateTime,
      });

      //Interpretation    
      ResultList.Add(new Result(
        setId: (ResultList.Count + 1).ToString(CultureInfo.CurrentCulture),
        dataType: "ST",
        resultType: new CodedElement(
          local: new Coded("INTER", "Interpretation", $"NATA{LabInfo.NATAAccNumber}"),
          international: null),
        value: Creator.Element("Immunoprotein parameters indicate an increasing level of sensitization. Specific IgE testing recommended for honey bee and common wasp venoms, also tryptase levels."),
        status: "F")
      {
        Units = null,
        ReferenceRange = null,
        AbnormalFlag = null,
        ObservationDateTime = ObservationDateTime,
      });

      ResultList.Add(PDFDisplaySegment(ResultList.Count + 1, ObservationDateTime, PdfFileName));

      return ResultList;
    }
    public static IEnumerable<Result> GetINRResultList(DateTimeOffset AnalyserDateTime, string PdfFileName)
    {
      var ObservationDateTime = AnalyserDateTime;
      //var ProducerId = new CodedElement(new Coded("OtherPath", "1234", "AUSNATA"), null);
      var ResultList = new List<Result>();

      //Warfarin
      ResultList.Add(new Result(
        setId: (ResultList.Count + 1).ToString(CultureInfo.CurrentCulture),
        dataType: "NM",
        resultType: new CodedElement(
          local: new Coded("WARF", "Warfarin dose", "SUPER -LIS"),
          international: new Coded("4461-0", "Warfarin dose", "LN")),
        value: Creator.Element("6"),
        status: "F")
      {
        Units = "mg",
        ReferenceRange = null,
        AbnormalFlag = null,
        ObservationDateTime = ObservationDateTime,
      });

      //INR
      ResultList.Add(new Result(
        setId: (ResultList.Count + 1).ToString(CultureInfo.CurrentCulture),
        dataType: "NM",
        resultType: new CodedElement(
          local: new Coded("INR", "INR", "SUPER -LIS"),
          international: new Coded("6301-6", "INR", "LN")),
        value: Creator.Element("3.1"),
        status: "F")
      {
        Units = null,
        ReferenceRange = "2.0-3.0",
        AbnormalFlag = "H",
        ObservationDateTime = ObservationDateTime,
      });

      //Interpretation    
      ResultList.Add(new Result(
        setId: (ResultList.Count + 1).ToString(CultureInfo.CurrentCulture),
        dataType: "ST",
        resultType: new CodedElement(
          local: new Coded("INTER", "Interpretation", $"NATA{LabInfo.NATAAccNumber}"),
          international: null),
        value: Creator.Element("INR is higher than therapeutic range. Recommend Warfarin dose be reduced to 5 mg per day and repeat test in 7 days."),
        status: "F")
      {
        Units = null,
        ReferenceRange = null,
        AbnormalFlag = null,
        ObservationDateTime = ObservationDateTime,
      });

      ResultList.Add(PDFDisplaySegment(ResultList.Count + 1, ObservationDateTime, PdfFileName));

      return ResultList;
    }
    public static IEnumerable<Result> GetKaryotypingResultList(DateTimeOffset AnalyserDateTime, string PdfFileName)
    {
      var ObservationDateTime = AnalyserDateTime;
      //var ProducerId = new CodedElement(new Coded("OtherPath", "1234", "AUSNATA"), null);
      var ResultList = new List<Result>();

      //Karyotype
      ResultList.Add(new Result(
        setId: (ResultList.Count + 1).ToString(CultureInfo.CurrentCulture),
        dataType: "ST",
        resultType: new CodedElement(
          local: new Coded("KARYO", "Karyotype", $"NATA{LabInfo.NATAAccNumber}"),
          international: null),
        value: Creator.Element("46,X,del(X)(q11.2)"),
        status: "F")
      {
        Units = null,
        ReferenceRange = null,
        AbnormalFlag = "N",
        ObservationDateTime = ObservationDateTime,
        //ProducerId = ProducerId
      });
      
      //Results
      ResultList.Add(new Result(
        setId: (ResultList.Count + 1).ToString(CultureInfo.CurrentCulture),
        dataType: "ST",
        resultType: new CodedElement(
          local: new Coded("RES", "Results", $"NATA{LabInfo.NATAAccNumber}"),
          international: null),
        value: Creator.Element("An abnormal female result showing a deletion of the long arm of the X-chromosome at band q11.2. This aberration was observed in all cells analysed. GTG banded analysis was performed on 5 cells analysed and 10 cells counted at a resolution of 550 bands."),
        status: "F")
      {
        Units = null,
        ReferenceRange = null,
        AbnormalFlag = "N",
        ObservationDateTime = ObservationDateTime,
        //ProducerId = ProducerId
      });

      //Interpretation
      ResultList.Add(new Result(
        setId: (ResultList.Count + 1).ToString(CultureInfo.CurrentCulture),
        dataType: "ST",
        resultType: new CodedElement(
          local: new Coded("INTERP", "Interpretation", $"NATA{LabInfo.NATAAccNumber}"),
          international: null),
        value: Creator.Element("Deletions of the long arm of the X-chromosome are associated with a variable phenotype in females.Common findings include short stature, gonadal dysgenesis and premature ovarian failure.It is likely that this aberration is the cause of the primary infertility observed in this patient.Genetic counselling is recommended."),
        status: "F")
      {
        Units = null,
        ReferenceRange = null,
        AbnormalFlag = null,
        ObservationDateTime = ObservationDateTime,
      });

      ResultList.Add(PDFDisplaySegment(ResultList.Count + 1, ObservationDateTime, PdfFileName));

      return ResultList;
    }
    public static IEnumerable<Result> GetLipidsResultList(DateTimeOffset AnalyserDateTime, string PdfFileName)
    {
      var ObservationDateTime = AnalyserDateTime;
      //var ProducerId = new CodedElement(new Coded("OtherPath", "1234", "AUSNATA"), null);
      var ResultList = new List<Result>();

      //Cholesterol
      ResultList.Add(new Result(
        setId: (ResultList.Count + 1).ToString(CultureInfo.CurrentCulture),
        dataType: "NM",
        resultType: new CodedElement(
          local: new Coded("CHOL", "Cholesterol", $"NATA{LabInfo.NATAAccNumber}"),
          international: new Coded("14647-2", "Cholesterol", "LN")),
        value: Creator.Element("6.2"),
        status: "F")
      {
        Units = "mmol/L",
        ReferenceRange = "< 5.5",
        AbnormalFlag = "H",
        ObservationDateTime = ObservationDateTime,
        //ProducerId = ProducerId
      });

      //Triglycerides
      ResultList.Add(new Result(
        setId: (ResultList.Count + 1).ToString(CultureInfo.CurrentCulture),
        dataType: "NM",
        resultType: new CodedElement(
          local: new Coded("TRIGF", "Triglycerides fasting", $"NATA{LabInfo.NATAAccNumber}"),
          international: new Coded("30524-3", "Triglycerides fasting", "LN")),
        value: Creator.Element("2.0"),
        status: "F")
      {
        Units = "mmol/L",
        ReferenceRange = "< 1.7",
        AbnormalFlag = "H",
        ObservationDateTime = ObservationDateTime,
        //ProducerId = ProducerId
      });

      //HDL Cholesterol
      ResultList.Add(new Result(
        setId: (ResultList.Count + 1).ToString(CultureInfo.CurrentCulture),
        dataType: "NM",
        resultType: new CodedElement(
          local: new Coded("HDL", "HDL cholesterol", $"NATA{LabInfo.NATAAccNumber}"),
          international: new Coded("14646-4", "HDL Cholesterol", "LN")),
        value: Creator.Element("3.2"),
        status: "F")
      {
        Units = "mmol/L",
        ReferenceRange = "> 1.0",
        AbnormalFlag = "N",
        ObservationDateTime = ObservationDateTime,
        //ProducerId = ProducerId
      });

      //LDL Cholesterol
      ResultList.Add(new Result(
        setId: (ResultList.Count + 1).ToString(CultureInfo.CurrentCulture),
        dataType: "NM",
        resultType: new CodedElement(
          local: new Coded("LDL", "LDL Cholesterol", $"NATA{LabInfo.NATAAccNumber}"),
          international: new Coded("22748-8", "LDL Cholesterol", "LN")),
        value: Creator.Element("2.1"),
        status: "F")
      {
        Units = "mmol/L",
        ReferenceRange = "< 3.0",
        AbnormalFlag = "N",
        ObservationDateTime = ObservationDateTime,
        //ProducerId = ProducerId
      });

      //Chol/HDL Ratio
      ResultList.Add(new Result(
        setId: (ResultList.Count + 1).ToString(CultureInfo.CurrentCulture),
        dataType: "NM",
        resultType: new CodedElement(
          local: new Coded("CHRATIO", "Cholesterol/HDL cholesterol", $"NATA{LabInfo.NATAAccNumber}"),
          international: new Coded("32309-7", "Chol/HDL Ratio", "LN")),
        value: Creator.Element("3.0"),
        status: "F")
      {
        Units = null,
        ReferenceRange = "< 3.5",
        AbnormalFlag = "N",
        ObservationDateTime = ObservationDateTime,
        //ProducerId = ProducerId
      });

      //Interpretation
      ResultList.Add(new Result(
        setId: (ResultList.Count + 1).ToString(CultureInfo.CurrentCulture),
        dataType: "ST",
        resultType: new CodedElement(
          local: new Coded("INTERP", "Interpretation", $"NATA{LabInfo.NATAAccNumber}"),
          international: new Coded("49262-9", "Fatty acids pattern Nar [Interp]", "LN")), 
        value: Creator.Element("Absolute cardiovascular risk assessment should be performed on all adults aged between 45 - 75 years without existing CVD or not already known to be at increased risk of CVD. A CBD risk calculator is provided at www.cvdcheck.org.au."),
        status: "F")
      {
        Units = null,
        ReferenceRange = null,
        AbnormalFlag = null,
        ObservationDateTime = ObservationDateTime,
      });

      ResultList.Add(PDFDisplaySegment(ResultList.Count + 1, ObservationDateTime, PdfFileName));

      return ResultList;
    }
    public static IEnumerable<Result> GetMSUResultList(DateTimeOffset AnalyserDateTime, string PdfFileName)
    {
      var ObservationDateTime = AnalyserDateTime;
      //var ProducerId = new CodedElement(new Coded("OtherPath", "1234", "AUSNATA"), null);
      var ResultList = new List<Result>();

      //Glucose
      ResultList.Add(new Result(
        setId: (ResultList.Count + 1).ToString(CultureInfo.CurrentCulture),
        dataType: "ST",
        resultType: new CodedElement(
          local: new Coded("GLUC", "Glucose", $"NATA{LabInfo.NATAAccNumber}"),
          international: new Coded("25428-4", "Glucose", "LN")),
        value: Creator.Element("3+"),
        status: "F")
      {
        Units = null,
        ReferenceRange = "Negative",
        AbnormalFlag = "A",
        ObservationDateTime = ObservationDateTime,
      });

      //Bilirubin
      ResultList.Add(new Result(
        setId: (ResultList.Count + 1).ToString(CultureInfo.CurrentCulture),
        dataType: "ST",
        resultType: new CodedElement(
          local: new Coded("BILI", "Bilirubin", $"NATA{LabInfo.NATAAccNumber}"),
          international: new Coded("5770-3", "Bilirubin", "LN")),
        value: Creator.Element("1+"),
        status: "F")
      {
        Units = null,
        ReferenceRange = "Negative",
        AbnormalFlag = "A",
        ObservationDateTime = ObservationDateTime,
      });

      //Ketones
      ResultList.Add(new Result(
        setId: (ResultList.Count + 1).ToString(CultureInfo.CurrentCulture),
        dataType: "ST",
        resultType: new CodedElement(
          local: new Coded("KETO", "Ketones", $"NATA{LabInfo.NATAAccNumber}"),
          international: new Coded("2514-8", "Ketones", "LN")),
        value: Creator.Element("1+"),
        status: "F")
      {
        Units = null,
        ReferenceRange = "Negative",
        AbnormalFlag = "A",
        ObservationDateTime = ObservationDateTime,
      });

      //Specific Gravity
      ResultList.Add(new Result(
        setId: (ResultList.Count + 1).ToString(CultureInfo.CurrentCulture),
        dataType: "NM",
        resultType: new CodedElement(
          local: new Coded("KETO", "Specific Gravity", $"NATA{LabInfo.NATAAccNumber}"),
          international: new Coded("5811-5", "Specific Gravity", "LN")),
        value: Creator.Element("1.034"),
        status: "F")
      {
        Units = null,
        ReferenceRange = "1.003-1.035",
        AbnormalFlag = "N",
        ObservationDateTime = ObservationDateTime,
      });

      //pH
      ResultList.Add(new Result(
        setId: (ResultList.Count + 1).ToString(CultureInfo.CurrentCulture),
        dataType: "NM",
        resultType: new CodedElement(
          local: new Coded("PH", "pH", $"NATA{LabInfo.NATAAccNumber}"),
          international: new Coded("5803-2", "pH", "LN")),
        value: Creator.Element("8.3"),
        status: "F")
      {
        Units = null,
        ReferenceRange = "5.0-8.0",
        AbnormalFlag = "H",
        ObservationDateTime = ObservationDateTime,
      });

      //Protein
      ResultList.Add(new Result(
        setId: (ResultList.Count + 1).ToString(CultureInfo.CurrentCulture),
        dataType: "ST",
        resultType: new CodedElement(
          local: new Coded("PROT", "Protein", $"NATA{LabInfo.NATAAccNumber}"),
          international: new Coded("20454-5", "Protein", "LN")),
        value: Creator.Element("2+"),
        status: "F")
      {
        Units = null,
        ReferenceRange = "Negative",
        AbnormalFlag = "A",
        ObservationDateTime = ObservationDateTime,
      });

      //Urobilinogen
      ResultList.Add(new Result(
        setId: (ResultList.Count + 1).ToString(CultureInfo.CurrentCulture),
        dataType: "NM",
        resultType: new CodedElement(
          local: new Coded("UROB", "Urobilinogen", $"NATA{LabInfo.NATAAccNumber}"),
          international: new Coded("5818-0", "Urobilinogen", "LN")),
        value: Creator.Element("0.7"),
        status: "F")
      {
        Units = null,
        ReferenceRange = "0.1-1.0",
        AbnormalFlag = "N",
        ObservationDateTime = ObservationDateTime,
      });

      //Nitrites
      ResultList.Add(new Result(
        setId: (ResultList.Count + 1).ToString(CultureInfo.CurrentCulture),
        dataType: "ST",
        resultType: new CodedElement(
          local: new Coded("NITR", "Nitrites", $"NATA{LabInfo.NATAAccNumber}"),
          international: new Coded("5802-4", "Nitrites", "LN")),
        value: Creator.Element("Positive"),
        status: "F")
      {
        Units = null,
        ReferenceRange = "Negative",
        AbnormalFlag = "A",
        ObservationDateTime = ObservationDateTime,
      });

      //Haemoglobin
      ResultList.Add(new Result(
        setId: (ResultList.Count + 1).ToString(CultureInfo.CurrentCulture),
        dataType: "ST",
        resultType: new CodedElement(
          local: new Coded("HB", "Haemoglobin", $"NATA{LabInfo.NATAAccNumber}"),
          international: new Coded("5794-3", "Haemoglobin", "LN")),
        value: Creator.Element("2+"),
        status: "F")
      {
        Units = null,
        ReferenceRange = "Negative",
        AbnormalFlag = "A",
        ObservationDateTime = ObservationDateTime,
      });

      //Leucocyte Esterase
      ResultList.Add(new Result(
        setId: (ResultList.Count + 1).ToString(CultureInfo.CurrentCulture),
        dataType: "ST",
        resultType: new CodedElement(
          local: new Coded("LEUCOEST", "Leucocyte Esterase", $"NATA{LabInfo.NATAAccNumber}"),
          international: new Coded("5799-2", "Leucocyte Esterase", "LN")),
        value: Creator.Element("Positive"),
        status: "F")
      {
        Units = null,
        ReferenceRange = "Negative",
        AbnormalFlag = "A",
        ObservationDateTime = ObservationDateTime,
      });

      //White blood cell count
      ResultList.Add(new Result(
        setId: (ResultList.Count + 1).ToString(CultureInfo.CurrentCulture),
        dataType: "NR",
        resultType: new CodedElement(
          local: new Coded("WBC", "White blood cell count", $"NATA{LabInfo.NATAAccNumber}"),
          international: new Coded("30405-5", "White blood cell count", "LN")),
        value: Creator.Element("10^25"),
        status: "F")
      {
        Units = "Erythrocytes/hpf",
        ReferenceRange = "None seen",
        AbnormalFlag = "A",
        ObservationDateTime = ObservationDateTime,
      });

      //Red blood cell count 
      ResultList.Add(new Result(
        setId: (ResultList.Count + 1).ToString(CultureInfo.CurrentCulture),
        dataType: "SN",
        resultType: new CodedElement(
          local: new Coded("RBC", "Red blood cell count", $"NATA{LabInfo.NATAAccNumber}"),
          international: new Coded("30391-7", "Red blood cell count", "LN")),
        value: Creator.Element(">^60"),
        status: "F")
      {
        Units = "Leucocytes/hpf",
        ReferenceRange = "None seen",
        AbnormalFlag = "A",
        ObservationDateTime = ObservationDateTime,
      });

      //Red blood cell morphology
      ResultList.Add(new Result(
        setId: (ResultList.Count + 1).ToString(CultureInfo.CurrentCulture),
        dataType: "ST",
        resultType: new CodedElement(
          local: new Coded("RBCM", "Red blood cell morphology", $"NATA{LabInfo.NATAAccNumber}"),
          international: new Coded("53974-2", "Red blood cell morphology", "LN")),
        value: Creator.Element("crenated"),
        status: "F")
      {
        Units = null,
        ReferenceRange = null,
        AbnormalFlag = "A",
        ObservationDateTime = ObservationDateTime,
      });

      //Epithelial cells
      ResultList.Add(new Result(
        setId: (ResultList.Count + 1).ToString(CultureInfo.CurrentCulture),
        dataType: "NR",
        resultType: new CodedElement(
          local: new Coded("EPITH", "Epithelial cells", $"NATA{LabInfo.NATAAccNumber}"),
          international: new Coded("30383-4", "Epithelial cells", "LN")),
        value: Creator.Element("1^5"),
        status: "F")
      {
        Units = "Epithelial cells/hpf",
        ReferenceRange = "None seen",
        AbnormalFlag = "A",
        ObservationDateTime = ObservationDateTime,
      });

      //Casts
      ResultList.Add(new Result(
        setId: (ResultList.Count + 1).ToString(CultureInfo.CurrentCulture),
        dataType: "ST",
        resultType: new CodedElement(
          local: new Coded("CASTS", "Casts present", $"NATA{LabInfo.NATAAccNumber}"),
          international: new Coded("24124-0", "Casts", "LN")),
        value: Creator.Element("None seen"),
        status: "F")
      {
        Units = "Casts/hpf",
        ReferenceRange = "None seen",
        AbnormalFlag = "N",
        ObservationDateTime = ObservationDateTime,
      });

      //Crystals
      ResultList.Add(new Result(
        setId: (ResultList.Count + 1).ToString(CultureInfo.CurrentCulture),
        dataType: "ST",
        resultType: new CodedElement(
          local: new Coded("CRYST", "Crystals", $"NATA{LabInfo.NATAAccNumber}"),
          international: new Coded("49755-2", "Crystals", "LN")),
        value: Creator.Element("None seen"),
        status: "F")
      {
        Units = "Crystals/hpf",
        ReferenceRange = "None seen",
        AbnormalFlag = "N",
        ObservationDateTime = ObservationDateTime,
      });

      var CultureOneSens = new List<Result>();
      //Culture
      ResultList.Add(new Result(
        setId: (ResultList.Count + 1).ToString(CultureInfo.CurrentCulture),
        dataType: "ST",
        resultType: new CodedElement(
          local: new Coded("CRYST", "Culture result/Organism", $"NATA{LabInfo.NATAAccNumber}"),
          international: new Coded("630-4", "Bacteria identified", "LN")),
        value: Creator.Element("Escherichia coli 20,000 cfu/mL"),
        status: "F")
      {
        SubId = "1",
        Units = null,
        ReferenceRange = "No growth",
        AbnormalFlag = "N",
        ObservationDateTime = ObservationDateTime,
        ResultList = CultureOneSens
      });      

      //Culture One Sens: Amikacin
      CultureOneSens.Add(new Result(
        setId: (ResultList.Count + CultureOneSens .Count + 1).ToString(CultureInfo.CurrentCulture),
        dataType: "ST",
        resultType: new CodedElement(
          local: new Coded("Amikacin", "Amikacin", $"NATA{LabInfo.NATAAccNumber}"),
          international: new Coded("18860-7", "Amikacin", "LN")),
        value: Creator.Element("I"),
        status: "F")
      {
        SubId = "1.1",
        Units = null,
        ReferenceRange = null,
        AbnormalFlag = "I",
        ObservationDateTime = ObservationDateTime
      });

      //Culture One Sens: Amoxicillin
      CultureOneSens.Add(new Result(
        setId: (ResultList.Count + CultureOneSens.Count + 1).ToString(CultureInfo.CurrentCulture),
        dataType: "ST",
        resultType: new CodedElement(
          local: new Coded("Amoxicillin", "Amoxicillin", $"NATA{LabInfo.NATAAccNumber}"),
          international: new Coded("18861-5", "Amoxicillin", "LN")),
        value: Creator.Element("I"),
        status: "F")
      {
        SubId = "1.1",
        Units = null,
        ReferenceRange = null,
        AbnormalFlag = "I",
        ObservationDateTime = ObservationDateTime
      });

      //Culture One Sens: Cefuroxime
      CultureOneSens.Add(new Result(
        setId: (ResultList.Count + CultureOneSens.Count + 1).ToString(CultureInfo.CurrentCulture),
        dataType: "ST",
        resultType: new CodedElement(
          local: new Coded("Cefuroxime", "Cefuroxime", $"NATA{LabInfo.NATAAccNumber}"),
          international: new Coded("18862-3", "Cefuroxime", "LN")),
        value: Creator.Element("R"),
        status: "F")
      {
        SubId = "1.1",
        Units = null,
        ReferenceRange = null,
        AbnormalFlag = "R",
        ObservationDateTime = ObservationDateTime
      });

      //Culture One Sens: Gentamicin
      CultureOneSens.Add(new Result(
        setId: (ResultList.Count + CultureOneSens.Count + 1).ToString(CultureInfo.CurrentCulture),
        dataType: "ST",
        resultType: new CodedElement(
          local: new Coded("Gentamicin", "Gentamicin", $"NATA{LabInfo.NATAAccNumber}"),
          international: new Coded("18928-2", "Gentamicin", "LN")),
        value: Creator.Element("S"),
        status: "F")
      {
        SubId = "1.1",
        Units = null,
        ReferenceRange = null,
        AbnormalFlag = "S",
        ObservationDateTime = ObservationDateTime
      });

      //Culture One Sens: Co-trimoxazole
      CultureOneSens.Add(new Result(
        setId: (ResultList.Count + CultureOneSens.Count + 1).ToString(CultureInfo.CurrentCulture),
        dataType: "ST",
        resultType: new CodedElement(
          local: new Coded("Co-trimoxazole", "Co-trimoxazole", $"NATA{LabInfo.NATAAccNumber}"),
          international: new Coded("18998-5", "Co-trimoxazole", "LN")),
        value: Creator.Element("R"),
        status: "F")
      {
        SubId = "1.1",
        Units = null,
        ReferenceRange = null,
        AbnormalFlag = "R",
        ObservationDateTime = ObservationDateTime
      });

      //Culture One Sens: Trimethoprim
      CultureOneSens.Add(new Result(
        setId: (ResultList.Count + CultureOneSens.Count + 1).ToString(CultureInfo.CurrentCulture),
        dataType: "ST",
        resultType: new CodedElement(
          local: new Coded("Trimethoprim", "Trimethoprim", $"NATA{LabInfo.NATAAccNumber}"),
          international: new Coded("18997-7", "Trimethoprim", "LN")),
        value: Creator.Element("I"),
        status: "F")
      {
        SubId = "1.1",
        Units = null,
        ReferenceRange = null,
        AbnormalFlag = "I",
        ObservationDateTime = ObservationDateTime
      });

      //Interpretation
      ResultList.Add(new Result(
        setId: (ResultList.Count + CultureOneSens.Count + 1).ToString(CultureInfo.CurrentCulture),
        dataType: "ST",
        resultType: new CodedElement(
          local: new Coded("INTERP", "Interpretation", $"NATA{LabInfo.NATAAccNumber}"),
          international: null),
        value: Creator.Element("Previous treatment with trimethoprim ineffective for recurring UTI."),
        status: "F")
      {
        Units = null,
        ReferenceRange = null,
        AbnormalFlag = null,
        ObservationDateTime = ObservationDateTime,
      });

      ResultList.Add(PDFDisplaySegment(ResultList.Count + CultureOneSens.Count + 1, ObservationDateTime, PdfFileName));

      return ResultList;
    }
    public static IEnumerable<Result> GetProteinElectrophoresisResultList(DateTimeOffset AnalyserDateTime, string PdfFileName)
    {
      var ObservationDateTime = AnalyserDateTime;
      //var ProducerId = new CodedElement(new Coded("OtherPath", "1234", "AUSNATA"), null);
      var ResultList = new List<Result>();

      //Protein
      ResultList.Add(new Result(
        setId: (ResultList.Count + 1).ToString(CultureInfo.CurrentCulture),
        dataType: "NM",
        resultType: new CodedElement(
          local: new Coded("PROT", "Total Protein ", $"NATA{LabInfo.NATAAccNumber}"),
          international: new Coded("2885-2", "Total Protein", "LN")),
        value: Creator.Element("174"),
        status: "F")
      {
        Units = "g/L",
        ReferenceRange = "35-175",
        AbnormalFlag = "N",
        ObservationDateTime = ObservationDateTime,
        //ProducerId = ProducerId
      });

      //Albumin
      ResultList.Add(new Result(
        setId: (ResultList.Count + 1).ToString(CultureInfo.CurrentCulture),
        dataType: "NM",
        resultType: new CodedElement(
          local: new Coded("ALB", "Albumin", $"NATA{LabInfo.NATAAccNumber}"),
          international: new Coded("2862-1", "Albumin", "LN")),
        value: Creator.Element("30"),
        status: "F")
      {
        Units = "g/L",
        ReferenceRange = "30-42",
        AbnormalFlag = "N",
        ObservationDateTime = ObservationDateTime,
        //ProducerId = ProducerId
      });

      //Alpha 1
      ResultList.Add(new Result(
        setId: (ResultList.Count + 1).ToString(CultureInfo.CurrentCulture),
        dataType: "NM",
        resultType: new CodedElement(
          local: new Coded("ALPHA1", "Alpha-1 globulin", $"NATA{LabInfo.NATAAccNumber}"),
          international: new Coded("2865-4", "Alpha 1", "LN")),
        value: Creator.Element("3"),
        status: "F")
      {
        Units = "g/L",
        ReferenceRange = "2-4",
        AbnormalFlag = "N",
        ObservationDateTime = ObservationDateTime,
        //ProducerId = ProducerId
      });

      //Alpha 2
      ResultList.Add(new Result(
        setId: (ResultList.Count + 1).ToString(CultureInfo.CurrentCulture),
        dataType: "NM",
        resultType: new CodedElement(
          local: new Coded("ALPHA2", "Alpha-2 globulin", $"NATA{LabInfo.NATAAccNumber}"),
          international: new Coded("2868-8", "Alpha 2", "LN")),
        value: Creator.Element("8"),
        status: "F")
      {
        Units = "g/L",
        ReferenceRange = "4-9",
        AbnormalFlag = "N",
        ObservationDateTime = ObservationDateTime,
        //ProducerId = ProducerId
      });

      //Beta 1
      ResultList.Add(new Result(
        setId: (ResultList.Count + 1).ToString(CultureInfo.CurrentCulture),
        dataType: "NM",
        resultType: new CodedElement(
          local: new Coded("BETA1", "Beta-1 globulin", $"NATA{LabInfo.NATAAccNumber}"),
          international: new Coded("32730-4", "Beta 1", "LN")),
        value: Creator.Element("5"),
        status: "F")
      {
        Units = "g/L",
        ReferenceRange = "2-6",
        AbnormalFlag = "N",
        ObservationDateTime = ObservationDateTime,
        //ProducerId = ProducerId
      });

      //Beta 2
      ResultList.Add(new Result(
        setId: (ResultList.Count + 1).ToString(CultureInfo.CurrentCulture),
        dataType: "NM",
        resultType: new CodedElement(
          local: new Coded("BETA2", "Beta-2 globulin", $"NATA{LabInfo.NATAAccNumber}"),
          international: new Coded("32731-2", "Beta 2", "LN")),
        value: Creator.Element("5"),
        status: "F")
      {
        Units = "g/L",
        ReferenceRange = "2-6",
        AbnormalFlag = "N",
        ObservationDateTime = ObservationDateTime,
        //ProducerId = ProducerId
      });

      //Gamma
      ResultList.Add(new Result(
        setId: (ResultList.Count + 1).ToString(CultureInfo.CurrentCulture),
        dataType: "NM",
        resultType: new CodedElement(
          local: new Coded("GAMMA", "Gamma globulin", $"NATA{LabInfo.NATAAccNumber}"),
          international: new Coded("2874-6", "Gamma", "LN")),
        value: Creator.Element("7"),
        status: "F")
      {
        Units = "g/L",
        ReferenceRange = "6-15",
        AbnormalFlag = "N",
        ObservationDateTime = ObservationDateTime,
        //ProducerId = ProducerId
      });

      //Abnormal band 
      ResultList.Add(new Result(
        setId: (ResultList.Count + 1).ToString(CultureInfo.CurrentCulture),
        dataType: "NM",
        resultType: new CodedElement(
          local: new Coded("ABNBAN", "Abnormal band", $"NATA{LabInfo.NATAAccNumber}"),
          international: new Coded("53539-3", "Abnormal band", "LN")),
        value: Creator.Element("4"),
        status: "F")
      {
        Units = "g/L",
        ReferenceRange = "<0.1",
        AbnormalFlag = "H",
        ObservationDateTime = ObservationDateTime,
        //ProducerId = ProducerId
      });

      //Total Protein 
      ResultList.Add(new Result(
        setId: (ResultList.Count + 1).ToString(CultureInfo.CurrentCulture),
        dataType: "NM",
        resultType: new CodedElement(
          local: new Coded("TOTALPROT", "Total Protein", $"NATA{LabInfo.NATAAccNumber}"),
          international: new Coded("2885-2", "Total Protein ", "LN")),
        value: Creator.Element("62"),
        status: "F")
      {
        Units = "g/L",
        ReferenceRange = "61-78",
        AbnormalFlag = "H",
        ObservationDateTime = ObservationDateTime,
        //ProducerId = ProducerId
      });
      
      //Interpretation
      ResultList.Add(new Result(
        setId: (ResultList.Count + 1).ToString(CultureInfo.CurrentCulture),
        dataType: "ST",
        resultType: new CodedElement(
          local: new Coded("INTERP", "Protein electrophoresis pattern comments", $"NATA{LabInfo.NATAAccNumber}"),
          international: new Coded("49298-3", "Protein Fractions Nar [Interp]", "LN")),
        value: Creator.Element("No monoclonal free light chains (Bence Jones protein) detected. A sharp discrete band is present in the serum. Results are consistent with the presence of a plasma cell dyscrasia i.e. myeloma or monoclonal gammopathy of uncertain significance(MGUS)."),
        status: "F")
      {
        Units = null,
        ReferenceRange = null,
        AbnormalFlag = null,
        ObservationDateTime = ObservationDateTime,
      });

      ResultList.Add(PDFDisplaySegment(ResultList.Count + 1, ObservationDateTime, PdfFileName));

      return ResultList;
    }
    public static IEnumerable<Result> GetBloodGasArterialResultList(DateTimeOffset AnalyserDateTime, string PdfFileName)
    {
      var ObservationDateTime = AnalyserDateTime;
      //var ProducerId = new CodedElement(new Coded("OtherPath", "1234", "AUSNATA"), null);
      var ResultList = new List<Result>();

      //Oxygen inhaled
      ResultList.Add(new Result(
        setId: (ResultList.Count + 1).ToString(CultureInfo.CurrentCulture),
        dataType: "NM",
        resultType: new CodedElement(
          local: new Coded("OXYI", "Oxygen inhaled", $"NATA{LabInfo.NATAAccNumber}"),
          international: new Coded("3151-8", "Oxygen inhaled", "LN")),
        value: Creator.Element("0.50"),
        status: "F")
      {
        Units = "??",
        ReferenceRange = "????",
        AbnormalFlag = "N",
        ObservationDateTime = ObservationDateTime,
        //ProducerId = ProducerId
      });

      //Body temperature
      ResultList.Add(new Result(
        setId: (ResultList.Count + 1).ToString(CultureInfo.CurrentCulture),
        dataType: "NM",
        resultType: new CodedElement(
          local: new Coded("BTEMP", "Body temperature", $"NATA{LabInfo.NATAAccNumber}"),
          international: new Coded("8310-5", "Body temperature", "LN")),
        value: Creator.Element("37.2"),
        status: "F")
      {
        Units = "Cel",
        ReferenceRange = "36.5-37.5",
        AbnormalFlag = "N",
        ObservationDateTime = ObservationDateTime,
        //ProducerId = ProducerId
      });

      //pH arterial
      ResultList.Add(new Result(
        setId: (ResultList.Count + 1).ToString(CultureInfo.CurrentCulture),
        dataType: "NM",
        resultType: new CodedElement(
          local: new Coded("PHART", "pH arterial", $"NATA{LabInfo.NATAAccNumber}"),
          international: new Coded("2744-1", "pH arterial", "LN")),
        value: Creator.Element("7.50"),
        status: "F")
      {
        Units = "pH",
        ReferenceRange = "7.35-7.45",
        AbnormalFlag = "H",
        ObservationDateTime = ObservationDateTime,
        //ProducerId = ProducerId
      });

      //pCO2 arterial
      ResultList.Add(new Result(
        setId: (ResultList.Count + 1).ToString(CultureInfo.CurrentCulture),
        dataType: "NM",
        resultType: new CodedElement(
          local: new Coded("PCO2A", "pCO2 arterial", $"NATA{LabInfo.NATAAccNumber}"),
          international: new Coded("2019-8", "pCO2 arterial", "LN")),
        value: Creator.Element("30"),
        status: "F")
      {
        Units = "mmHg",
        ReferenceRange = "32-48",
        AbnormalFlag = "L",
        ObservationDateTime = ObservationDateTime,
        //ProducerId = ProducerId
      });

      //pO2 arterial
      ResultList.Add(new Result(
        setId: (ResultList.Count + 1).ToString(CultureInfo.CurrentCulture),
        dataType: "NM",
        resultType: new CodedElement(
          local: new Coded("PO2A", "pO2 arterial", $"NATA{LabInfo.NATAAccNumber}"),
          international: new Coded("2703-7", "pO2 arterial", "LN")),
        value: Creator.Element("61"),
        status: "F")
      {
        Units = "mmHg",
        ReferenceRange = "83-8",
        AbnormalFlag = "N",
        ObservationDateTime = ObservationDateTime,
        //ProducerId = ProducerId
      });

      //Oxygen content arterial
      ResultList.Add(new Result(
        setId: (ResultList.Count + 1).ToString(CultureInfo.CurrentCulture),
        dataType: "NM",
        resultType: new CodedElement(
          local: new Coded("OXYCA", "Oxygen content arterial", $"NATA{LabInfo.NATAAccNumber}"),
          international: new Coded("19218-7", "Oxygen content arterial", "LN")),
        value: Creator.Element("89"),
        status: "F")
      {
        Units = "mol/L",
        ReferenceRange = "94-98",
        AbnormalFlag = "L",
        ObservationDateTime = ObservationDateTime,
        //ProducerId = ProducerId
      });

      //Bicarbonate arterial
      ResultList.Add(new Result(
        setId: (ResultList.Count + 1).ToString(CultureInfo.CurrentCulture),
        dataType: "NM",
        resultType: new CodedElement(
          local: new Coded("BICA", "Bicarbonate arterial", $"NATA{LabInfo.NATAAccNumber}"),
          international: new Coded("1960-4", "Bicarbonate arterial", "LN")),
        value: Creator.Element("27"),
        status: "F")
      {
        Units = "mmol/L",
        ReferenceRange = "22-33",
        AbnormalFlag = "N",
        ObservationDateTime = ObservationDateTime,
        //ProducerId = ProducerId
      });

      //Base excess arterial 
      ResultList.Add(new Result(
        setId: (ResultList.Count + 1).ToString(CultureInfo.CurrentCulture),
        dataType: "NM",
        resultType: new CodedElement(
          local: new Coded("BASEEA", "Base excess arterial", $"NATA{LabInfo.NATAAccNumber}"),
          international: new Coded("1925-7", "Base excess arterial", "LN")),
        value: Creator.Element("4.1"),
        status: "F")
      {
        Units = "mmol/L",
        ReferenceRange = "-2.0-3.0",
        AbnormalFlag = "H",
        ObservationDateTime = ObservationDateTime,
        //ProducerId = ProducerId
      });

      //Oxygen saturation arterial
      ResultList.Add(new Result(
        setId: (ResultList.Count + 1).ToString(CultureInfo.CurrentCulture),
        dataType: "NM",
        resultType: new CodedElement(
          local: new Coded("OXYSA", "Oxygen saturation arterial", $"NATA{LabInfo.NATAAccNumber}"),
          international: new Coded("51733-4", "Oxygen saturation arterial", "LN")),
        value: Creator.Element("92"),
        status: "F")
      {
        Units = "%",
        ReferenceRange = "94-98",
        AbnormalFlag = "L",
        ObservationDateTime = ObservationDateTime,
        //ProducerId = ProducerId
      });

      //Sodium blood
      ResultList.Add(new Result(
        setId: (ResultList.Count + 1).ToString(CultureInfo.CurrentCulture),
        dataType: "NM",
        resultType: new CodedElement(
          local: new Coded("NAB", "Sodium blood", $"NATA{LabInfo.NATAAccNumber}"),
          international: new Coded("2947-0", "Sodium blood", "LN")),
        value: Creator.Element("137"),
        status: "F")
      {
        Units = "mmol/L",
        ReferenceRange = "135-145",
        AbnormalFlag = "N",
        ObservationDateTime = ObservationDateTime,
        //ProducerId = ProducerId
      });

      //Potassium blood
      ResultList.Add(new Result(
        setId: (ResultList.Count + 1).ToString(CultureInfo.CurrentCulture),
        dataType: "NM",
        resultType: new CodedElement(
          local: new Coded("KB", "Potassium blood", $"NATA{LabInfo.NATAAccNumber}"),
          international: new Coded("6298-4", "Potassium blood", "LN")),
        value: Creator.Element("3.5"),
        status: "F")
      {
        Units = "mmol/L",
        ReferenceRange = "3.5-5.2",
        AbnormalFlag = "N",
        ObservationDateTime = ObservationDateTime,
        //ProducerId = ProducerId
      });

      //Chloride blood
      ResultList.Add(new Result(
        setId: (ResultList.Count + 1).ToString(CultureInfo.CurrentCulture),
        dataType: "NM",
        resultType: new CodedElement(
          local: new Coded("CLB", "Chloride blood", $"NATA{LabInfo.NATAAccNumber}"),
          international: new Coded("2069-3", "Chloride blood", "LN")),
        value: Creator.Element("103"),
        status: "F")
      {
        Units = "mmol/L",
        ReferenceRange = "95-110",
        AbnormalFlag = "N",
        ObservationDateTime = ObservationDateTime,
        //ProducerId = ProducerId
      });

      //Glucose blood
      ResultList.Add(new Result(
        setId: (ResultList.Count + 1).ToString(CultureInfo.CurrentCulture),
        dataType: "NM",
        resultType: new CodedElement(
          local: new Coded("GLUCB", "Glucose blood", $"NATA{LabInfo.NATAAccNumber}"),
          international: new Coded("15074-8", "Glucose blood", "LN")),
        value: Creator.Element("9.1"),
        status: "F")
      {
        Units = "mmol/L",
        ReferenceRange = "3.0-7.8",
        AbnormalFlag = "H",
        ObservationDateTime = ObservationDateTime,
        //ProducerId = ProducerId
      });

      //Lactate blood
      ResultList.Add(new Result(
        setId: (ResultList.Count + 1).ToString(CultureInfo.CurrentCulture),
        dataType: "NM",
        resultType: new CodedElement(
          local: new Coded("LACTB", "Lactate blood", $"NATA{LabInfo.NATAAccNumber}"),
          international: new Coded("32693-4", "Lactate blood", "LN")),
        value: Creator.Element("0.8"),
        status: "F")
      {
        Units = "mmol/L",
        ReferenceRange = "<1.0",
        AbnormalFlag = "N",
        ObservationDateTime = ObservationDateTime,
        //ProducerId = ProducerId
      });

      //Anion gap blood
      ResultList.Add(new Result(
        setId: (ResultList.Count + 1).ToString(CultureInfo.CurrentCulture),
        dataType: "NM",
        resultType: new CodedElement(
          local: new Coded("AGB", "Anion gap blood", $"NATA{LabInfo.NATAAccNumber}"),
          international: new Coded("41276-7", "Anion gap blood", "LN")),
        value: Creator.Element("7"),
        status: "F")
      {
        Units = "mmol/L",
        ReferenceRange = "4-13",
        AbnormalFlag = "N",
        ObservationDateTime = ObservationDateTime,
        //ProducerId = ProducerId
      });

      //Urea blood
      ResultList.Add(new Result(
        setId: (ResultList.Count + 1).ToString(CultureInfo.CurrentCulture),
        dataType: "NM",
        resultType: new CodedElement(
          local: new Coded("AGB", "Urea blood", $"NATA{LabInfo.NATAAccNumber}"),
          international: new Coded("72903-8", "Urea blood", "LN")),
        value: Creator.Element("4.9"),
        status: "F")
      {
        Units = "mmol/L",
        ReferenceRange = "3.8-8.5",
        AbnormalFlag = "N",
        ObservationDateTime = ObservationDateTime,
        //ProducerId = ProducerId
      });

      //Calcium ionised blood
      ResultList.Add(new Result(
        setId: (ResultList.Count + 1).ToString(CultureInfo.CurrentCulture),
        dataType: "NM",
        resultType: new CodedElement(
          local: new Coded("CAIONB", "Calcium ionised blood", $"NATA{LabInfo.NATAAccNumber}"),
          international: new Coded("1994-3", "Calcium ionised blood", "LN")),
        value: Creator.Element("1.15"),
        status: "F")
      {
        Units = "mmol/L",
        ReferenceRange = "1.15-1.32",
        AbnormalFlag = "N",
        ObservationDateTime = ObservationDateTime,
        //ProducerId = ProducerId
      });

      //Carboxyhaemoglobin
      ResultList.Add(new Result(
        setId: (ResultList.Count + 1).ToString(CultureInfo.CurrentCulture),
        dataType: "SN",
        resultType: new CodedElement(
          local: new Coded("CARBOXHB", "Carboxyhaemoglobin", $"NATA{LabInfo.NATAAccNumber}"),
          international: new Coded("20563-3", "Carboxyhaemoglobin", "LN")),
        value: Creator.Element("<^0.2"),
        status: "F")
      {
        Units = "%",
        ReferenceRange = "<1.5",
        AbnormalFlag = "N",
        ObservationDateTime = ObservationDateTime,
        //ProducerId = ProducerId
      });

      //Oxyhaemoglobin arterial
      ResultList.Add(new Result(
        setId: (ResultList.Count + 1).ToString(CultureInfo.CurrentCulture),
        dataType: "NM",
        resultType: new CodedElement(
          local: new Coded("OXYHBA", "Oxyhaemoglobin arterial", $"NATA{LabInfo.NATAAccNumber}"),
          international: new Coded("2714-4", "Oxyhaemoglobin arterial", "LN")),
        value: Creator.Element("98"),
        status: "F")
      {
        Units = "%",
        ReferenceRange = "94-98",
        AbnormalFlag = "N",
        ObservationDateTime = ObservationDateTime,
        //ProducerId = ProducerId
      });

      //Methaemoglobin
      ResultList.Add(new Result(
        setId: (ResultList.Count + 1).ToString(CultureInfo.CurrentCulture),
        dataType: "NM",
        resultType: new CodedElement(
          local: new Coded("METHB", "Methaemoglobin", $"NATA{LabInfo.NATAAccNumber}"),
          international: new Coded("2614-6", "Methaemoglobin", "LN")),
        value: Creator.Element("0.2"),
        status: "F")
      {
        Units = "%",
        ReferenceRange = "<1.5",
        AbnormalFlag = "N",
        ObservationDateTime = ObservationDateTime,
        //ProducerId = ProducerId
      });

      //Haemoglobin
      ResultList.Add(new Result(
        setId: (ResultList.Count + 1).ToString(CultureInfo.CurrentCulture),
        dataType: "NM",
        resultType: new CodedElement(
          local: new Coded("HB", "Haemoglobin", $"NATA{LabInfo.NATAAccNumber}"),
          international: new Coded("20509-6", "Haemoglobin calculated", "LN")),
        value: Creator.Element("148"),
        status: "F")
      {
        Units = "g/L",
        ReferenceRange = "135-180",
        AbnormalFlag = "N",
        ObservationDateTime = ObservationDateTime,
        //ProducerId = ProducerId
      });

      //Interpretation
      ResultList.Add(new Result(
        setId: (ResultList.Count + 1).ToString(CultureInfo.CurrentCulture),
        dataType: "ST",
        resultType: new CodedElement(
          local: new Coded("INTERP", "Interpretation", $"NATA{LabInfo.NATAAccNumber}"),
          international: null),
        value: Creator.Element("Chemistry parameters are indicative of respiratory alkalosis."),
        status: "F")
      {
        Units = null,
        ReferenceRange = null,
        AbnormalFlag = null,
        ObservationDateTime = ObservationDateTime,
      });

      ResultList.Add(PDFDisplaySegment(ResultList.Count + 1, ObservationDateTime, PdfFileName));

      return ResultList;
    }
    public static IEnumerable<Result> GetSARSCoV2NATResultList(DateTimeOffset AnalyserDateTime, string PdfFileName)
    {
      var ObservationDateTime = AnalyserDateTime;
      //var ProducerId = new CodedElement(new Coded("OtherPath", "1234", "AUSNATA"), null);
      var ResultList = new List<Result>();

      //SARS-CoV-2 RNA
      ResultList.Add(new Result(
        setId: (ResultList.Count + 1).ToString(CultureInfo.CurrentCulture),
        dataType: "ST",        
        resultType: new CodedElement(
          local: new Coded("COVI2DRNA", "SARS-CoV-2 RNA", $"NATA{LabInfo.NATAAccNumber}"),
          international: new Coded("94309-2", "SARS-CoV-2 (COVID-19) RNA NAA+probe Ql (Unsp spec)", "LN")),
        value: Creator.Element("Not Detected"),
        status: "F")
      {
        Units = null,
        ReferenceRange = null,
        AbnormalFlag = "N",
        ObservationDateTime = ObservationDateTime,
        //ProducerId = ProducerId
      });

      //Interpretation
      ResultList.Add(new Result(
        setId: (ResultList.Count + 1).ToString(CultureInfo.CurrentCulture),
        dataType: "ST",
        resultType: new CodedElement(
          local: new Coded("INTERP", "Interpretation", $"NATA{LabInfo.NATAAccNumber}"),
          international: null),
        value: Creator.Element("Failure to detect virus specific nucleic acid does not always exclude SARS-CoV-2. Repeat testing including a sputum sample if available, may be indicated if clinical suspicion is high. This assay is designed to detect the E gene of SARS-CoV - 2, the causative agent of COVID - 19 using nucleic acid amplification.On occasions, a second assay targeting the N gene is also utilised."),
        status: "F")
      {
        Units = null,
        ReferenceRange = null,
        AbnormalFlag = null,
        ObservationDateTime = ObservationDateTime,
      });

      ResultList.Add(PDFDisplaySegment(ResultList.Count + 1, ObservationDateTime, PdfFileName));

      return ResultList;
    }    
    public static IEnumerable<Result> GetSARSCoV2SerologyResultList(DateTimeOffset AnalyserDateTime, string PdfFileName)
    {
      var ObservationDateTime = AnalyserDateTime;
      //var ProducerId = new CodedElement(new Coded("OtherPath", "1234", "AUSNATA"), null);
      var ResultList = new List<Result>();

      //SARS-CoV-2 IgA IF
      ResultList.Add(new Result(
        setId: (ResultList.Count + 1).ToString(CultureInfo.CurrentCulture),
        dataType: "NM",        
        resultType: new CodedElement(
          local: new Coded("COVID2IGA", "SARS-CoV-2 IgA IF", $"NATA{LabInfo.NATAAccNumber}"),
          international: new Coded("95427-1", "SARS-CoV-2 (COVID-19) IgA IF [Titer]", "LN")),
        value: Creator.Element("20"),
        status: "F")
      {
        //ToDo: Need Units, Ref-Range
        Units = "??",
        ReferenceRange = "????",
        AbnormalFlag = "N",
        ObservationDateTime = ObservationDateTime,
        //ProducerId = ProducerId
      });

      //SARS-CoV-2 IgA IF
      ResultList.Add(new Result(
        setId: (ResultList.Count + 1).ToString(CultureInfo.CurrentCulture),
        dataType: "NM",        
        resultType: new CodedElement(
          local: new Coded("COVID2IGM", "SARS-CoV-2 IgM IF", $"NATA{LabInfo.NATAAccNumber}"),
          international: new Coded("95429-7", "SARS-CoV-2 (COVID-19) IgG IF [Titer]", "LN")),
        value: Creator.Element("10"),
        status: "F")
      {
        //ToDo: Need Units, Ref-Range
        Units = "??",
        ReferenceRange = "????",
        AbnormalFlag = "N",
        ObservationDateTime = ObservationDateTime,
        //ProducerId = ProducerId
      });

      //SARS-CoV-2 IgG IF
      ResultList.Add(new Result(
        setId: (ResultList.Count + 1).ToString(CultureInfo.CurrentCulture),
        dataType: "NM",
        resultType: new CodedElement(
          local: new Coded("COVID2IGG", "SARS-CoV-2 IgG IF", $"NATA{LabInfo.NATAAccNumber}"),
          international: new Coded("95428-9", "SARS-CoV-2 (COVID-19) IgM IF [Titer]", "LN")),
        value: Creator.Element("320"),
        status: "F")
      {
        //ToDo: Need Units, Ref-Range
        Units = "??",
        ReferenceRange = "????",
        AbnormalFlag = "N",
        ObservationDateTime = ObservationDateTime,
        //ProducerId = ProducerId
      });

      List<string> CommentLineList = new List<string>
      {
        "These results suggest resolving SARS-CoV-2 infection. For further information please contact the medical virologist on(07) 5454 0387.",
        "",
        "virologist on (07) 5454 0387.",
        "",
        "N.B. SARS-Co-V-2 is the cause of coronavirus disease (CoVID-19).",
        "The Immunofluorescent tests used have been evaluated and validated but not yet registered with NATA.",
        "Results have been issued to the serious threat to public health from CoVID-19.",
        "These results have been electronically notified to the Ministry of Health."
      };

      //Interpretation
      ResultList.Add(new Result(
        setId: (ResultList.Count + 1).ToString(CultureInfo.CurrentCulture),
        dataType: "FT",
        resultType: new CodedElement(
          local: new Coded("INTERP", "Interpretation", $"NATA{LabInfo.NATAAccNumber}"),
          international: null),
        value: ResultFactory.GetFormatedTextElement(CommentLineList),
        status: "F")
      {
        Units = null,
        ReferenceRange = null,
        AbnormalFlag = null,
        ObservationDateTime = ObservationDateTime,
      });

      ResultList.Add(PDFDisplaySegment(ResultList.Count + 1, ObservationDateTime, PdfFileName));

      return ResultList;
    }
    private static Result PDFDisplaySegment(int SetId, DateTimeOffset AnalyserDateTime, string PdfFileName)
    {      
      string Base64PDF = null;
      if (PdfFileName is object)
      {
        System.IO.DirectoryInfo DirInfo = new System.IO.DirectoryInfo(@"C:\temp\SPIAMessages\PDF");
        if (DirInfo.Exists)
        {
          System.IO.FileInfo FileInfo = DirInfo.EnumerateFiles().SingleOrDefault(x => x.Name.Equals($"{PdfFileName}.pdf", StringComparison.CurrentCulture));
          if (FileInfo is object)
          {
            Base64PDF = PeterPiper.Hl7.V2.Support.Tools.Base64Tools.Encoder(System.IO.File.ReadAllBytes(FileInfo.FullName));
          }
          else
          {
            throw new ApplicationException($"Unable to locate PDF file which starts with : {PdfFileName}");
          }
        }
        else
        {
          throw new ApplicationException("");
        }
      }

      string ValueElementString = $"^application^pdf^Base64^[Base64 PDF content goes here]";
      if (PdfFileName is object)
      {
        ValueElementString = $"^application^pdf^Base64^{Base64PDF}";
      }

      //"^application^pdf^Base64^[Base64 PDF Content here]",
      //Interpretation    
      return new Result(
        setId: (SetId).ToString(CultureInfo.CurrentCulture),
        dataType: "ED",
        resultType: new CodedElement(
          local: null,
          international: new Coded("PDF", "Display Format in PDF", $"AUSPDI")),
        value: Creator.Element(ValueElementString),
        status: "F")
      {
        Units = null,
        ReferenceRange = null,
        AbnormalFlag = null,
        ObservationDateTime = AnalyserDateTime,
      };

    }

    private static IElement GetFormatedTextElement(List<string> CommentLineList)
    {
      if (CommentLineList is null)
      {
        throw new ArgumentNullException(nameof(CommentLineList));
      }
      if (CommentLineList.Count == 0)
      {
        throw new ApplicationException($"{nameof(CommentLineList)} is an empty list");
      }
      var CommentFormattedTextElement = Creator.Element();
      foreach (var line in CommentLineList)
      {
        if (!string.IsNullOrWhiteSpace(line))
        {
          CommentFormattedTextElement.Add(Creator.Content(line));
        }
        CommentFormattedTextElement.Add(Creator.Content(PeterPiper.Hl7.V2.Support.Standard.EscapeType.NewLine));
      }
      return CommentFormattedTextElement;
    }

  }
}
