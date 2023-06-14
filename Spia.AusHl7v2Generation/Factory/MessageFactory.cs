using PeterPiper.Hl7.V2.Model;
using System;
using System.Collections.Generic;
using System.Text;
using Spia.PathologyReportModel.Model;

namespace Spia.AusHl7v2Generation.Factory
{
  public static class MessageFactory
  {
    public static string GetMessage(PathologyReport PathologyReport, string PDFDirectoryPath)
    {
      if (PathologyReport is null)
        throw new ArgumentNullException(nameof(PathologyReport));

      DateTimeOffset MessageCreatedDateTime = PathologyReport.GetOldestReportReleaseDateTime().AddMinutes(2);
      var HL7 = Creator.Message(MSHFactory.GetMSH(Guid.NewGuid().ToString().ToUpper(), MessageCreatedDateTime, PathologyReport.PerformingLaboratory, PathologyReport.Request.RequestingApplication, PathologyReport.Request.RequestingFacility));

      HL7.Add(PIDFactory.GetPID(PathologyReport.Patient));
      HL7.Add(PV1Factory.GetPV1(patientClassCode: "N", referringProvider: PathologyReport.Request.RequestingProvider, performingLabNataSiteNumber: PathologyReport.PerformingLaboratory.NataSiteNumber));

      OBXFactory OBXFactory = new OBXFactory();
      foreach (var Report in PathologyReport.ReportList)
      {        
        HL7.Add(OBRFactory.GetOBR(PathologyReport.Request, Report, PathologyReport.PerformingLaboratory));
        foreach (var OBX in OBXFactory.GetOBXList(PathologyReport.PerformingLaboratory.NataSiteNumber, Report.Panel.ResultList))
        {
          HL7.Add(OBX);
        }
        HL7.Add(OBXFactory.GetPdfOBX(PDFDirectoryPath, PathologyReport.PdfFileName, HL7.SegmentCount("OBX") + 1, Report.ReportStatus));

      }
      return HL7.AsStringRaw;

    }
  }
}
