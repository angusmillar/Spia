using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spia.PathologyReportModel.Model;
namespace Spia.PathologyReportModel.Support
{
  public class DiagnosticServiceSectionIdSupport : CodeEnumSupport<DiagnosticService>
  {
    public DiagnosticServiceSectionIdSupport() : base(PrimeDictionary(), "HL7AUSD-STD-OO-ADRM-2018.1 standard HL7 Table 0074 - Diagnostic service section ID") {}

    private static Dictionary<string, DiagnosticService> PrimeDictionary()
    {
      //This is the HL7 Table 0074 - Diagnostic service section ID 
      //Taken from the  HL7AUSD-STD-OO-ADRM-2018.1 standard : https://confluence.hl7australia.com/display/OOADRM20181/4+Observation+Reporting#id-4ObservationReporting-4.4.1.24OBR-24DiagnosticservsectID(ID)00257
      return new Dictionary<string, DiagnosticService>()
      {
        {"AU", DiagnosticService.Audiology},
        {"BG",  DiagnosticService.BloodGases},
        {"BLB", DiagnosticService.BloodBank},
        {"CG",  DiagnosticService.Cytogenetics},
        {"CUS", DiagnosticService.CardiacUltrasound},
        {"CTH", DiagnosticService.CardiacCatheterization},
        {"CT",  DiagnosticService.CATScan},
        {"CH",  DiagnosticService.Chemistry},
        {"CP",  DiagnosticService.Cytopathology},
        {"EC",  DiagnosticService.Electrocardiac},
        {"EN",  DiagnosticService.Electroneuro},
        {"GE",  DiagnosticService.Genetics},
        {"HM",  DiagnosticService.Hematology},
        {"ICU", DiagnosticService.BedsideICUMonitoring},
        {"IMM", DiagnosticService.Immunology},
        {"LAB", DiagnosticService.Laboratory},
        {"MB",  DiagnosticService.Microbiology},
        {"MCB", DiagnosticService.Mycobacteriology},
        {"MYC", DiagnosticService.Mycology},
        {"NMR", DiagnosticService.NuclearMagneticResonance},
        {"NMS", DiagnosticService.NuclearMedicineScan},
        {"NRS", DiagnosticService.NursingServiceMeasures},
        {"OUS", DiagnosticService.OBUltrasound},
        {"OT",  DiagnosticService.OccupationalTherapy},
        {"OTH", DiagnosticService.Other},
        {"OSL", DiagnosticService.OutsideLab},
        {"PHR", DiagnosticService.Pharmacy},
        {"PT",  DiagnosticService.PhysicalTherapy},
        {"PHY", DiagnosticService.Physician},
        {"PF",  DiagnosticService.PulmonaryFunction},
        {"RAD", DiagnosticService.Radiology},
        {"RUS", DiagnosticService.RadiologyUltrasound},
        {"RC",  DiagnosticService.RespiratoryCare},
        {"RT",  DiagnosticService.RadiationTherapy},
        {"RX",  DiagnosticService.Radiograph},
        {"SR",  DiagnosticService.Serology},
        {"SP",  DiagnosticService.HistologyAndAnatomicalPathology},
        {"TX",  DiagnosticService.Toxicology},
        {"VUS", DiagnosticService.VascularUltrasound},
        {"VR",  DiagnosticService.Virology},
        {"XRC", DiagnosticService.Cineradiograph}
      };
    }    
  }
}
