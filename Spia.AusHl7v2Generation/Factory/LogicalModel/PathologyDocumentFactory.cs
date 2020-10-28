using Spia.AusHl7v2Generation.Model.Logical;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Spia.AusHl7v2Generation.Factory.LogicalModel
{
  public static class PathologyDocumentFactory
  {
    public static PathologyDocument GetChlamydia(string PdfFileName)
    {
      var Patient = PatientFactory.GetPatient(PatientFactory.PateintType.GeorginaROSSLAND);
      var Request = RequestFactory.GetChlamydiaRequest();
      var PathologyDocument = new PathologyDocument(Patient, Request);
      Report Report = ReportFactory.GetChlamydiaReport();
      PathologyDocument.ReportList.Add(Report);
      Report.ResultList.AddRange(ResultFactory.GetChlamydiaResultList(Report.ReportReleaseDateTime.Subtract(TimeSpan.FromMinutes(5)), PdfFileName));
      return PathologyDocument;
    }

    public static PathologyDocument GetEUC(string PdfFileName)
    {
      var Patient = PatientFactory.GetPatient(PatientFactory.PateintType.GlennFERNIE);
      var Request = RequestFactory.GetEUC();
      var PathologyDocument = new PathologyDocument(Patient, Request);
      Report Report = ReportFactory.GetEUCReport();
      PathologyDocument.ReportList.Add(Report);
      Report.ResultList.AddRange(ResultFactory.GetEUCResultList(Report.ReportReleaseDateTime.Subtract(TimeSpan.FromMinutes(5)), PdfFileName));
      return PathologyDocument;
    }

    public static PathologyDocument GetFBC(string PdfFileName)
    {
      var Patient = PatientFactory.GetPatient(PatientFactory.PateintType.TessaCITIZEN);
      var Request = RequestFactory.GetFBC1Request();
      var PathologyDocument = new PathologyDocument(Patient, Request);
      Report Report = ReportFactory.GetFBC();
      PathologyDocument.ReportList.Add(Report);
      Report.ResultList.AddRange(ResultFactory.GetFBCResultList(Report.ReportReleaseDateTime.Subtract(TimeSpan.FromMinutes(5)), PdfFileName));
      return PathologyDocument;
    }

    public static PathologyDocument GetHFE(string PdfFileName)
    {
      var Patient = PatientFactory.GetPatient(PatientFactory.PateintType.GlennFERNIE);
      var Request = RequestFactory.GetHFERequest();
      var PathologyDocument = new PathologyDocument(Patient, Request);
      Report Report = ReportFactory.GetHFEReport();
      PathologyDocument.ReportList.Add(Report);
      Report.ResultList.AddRange(ResultFactory.GetHFEResultList(Report.ReportReleaseDateTime.Subtract(TimeSpan.FromMinutes(5)), PdfFileName));
      return PathologyDocument;
    }
    public static PathologyDocument GetHepBsAb(string PdfFileName)
    {
      var Patient = PatientFactory.GetPatient(PatientFactory.PateintType.GeorginaROSSLAND);
      var Request = RequestFactory.GetHepBsAbRequest();
      var PathologyDocument = new PathologyDocument(Patient, Request);
      Report Report = ReportFactory.GetHepBsAbReport();
      PathologyDocument.ReportList.Add(Report);
      Report.ResultList.AddRange(ResultFactory.GetHepBsAbResultList(Report.ReportReleaseDateTime.Subtract(TimeSpan.FromMinutes(5)), PdfFileName));
      return PathologyDocument;
    }

    public static PathologyDocument GetImmunoglobulinE(string PdfFileName)
    {
      var Patient = PatientFactory.GetPatient(PatientFactory.PateintType.GloriaNELSON);
      var Request = RequestFactory.GetImmunoglobulinERequest();
      var PathologyDocument = new PathologyDocument(Patient, Request);
      Report Report = ReportFactory.GetmmunoglobulinE();
      PathologyDocument.ReportList.Add(Report);
      Report.ResultList.AddRange(ResultFactory.GetImmunoglobulinEResultList(Report.ReportReleaseDateTime.Subtract(TimeSpan.FromMinutes(5)), PdfFileName));
      return PathologyDocument;
    }

    public static PathologyDocument GetINRAndFBC(string PdfFileName)
    {
      var Patient = PatientFactory.GetPatient(PatientFactory.PateintType.TessaCITIZEN);
      var Request = RequestFactory.GetInrFbc();
      var PathologyDocument = new PathologyDocument(Patient, Request);

      Report INRReport = ReportFactory.GetINR();
      INRReport.ResultList.AddRange(ResultFactory.GetINRResultList(INRReport.ReportReleaseDateTime.Subtract(TimeSpan.FromMinutes(5)), PdfFileName));
      PathologyDocument.ReportList.Add(INRReport);

      Report FBCReport = ReportFactory.GetFBC();
      FBCReport.ResultList.AddRange(ResultFactory.GetFBCResultList(FBCReport.ReportReleaseDateTime.Subtract(TimeSpan.FromMinutes(5)), PdfFileName));
      PathologyDocument.ReportList.Add(FBCReport);

      return PathologyDocument;
    }
    public static PathologyDocument GetINR(string PdfFileName)
    {
      var Patient = PatientFactory.GetPatient(PatientFactory.PateintType.TessaCITIZEN);
      var Request = RequestFactory.GetInr();
      var PathologyDocument = new PathologyDocument(Patient, Request);

      Report INRReport = ReportFactory.GetINR();
      INRReport.ResultList.AddRange(ResultFactory.GetINRResultList(INRReport.ReportReleaseDateTime.Subtract(TimeSpan.FromMinutes(5)), PdfFileName));
      PathologyDocument.ReportList.Add(INRReport);

      return PathologyDocument;
    }

    public static PathologyDocument GetMSU(string PdfFileName)
    {
      var Patient = PatientFactory.GetPatient(PatientFactory.PateintType.GloriaNELSON);
      var Request = RequestFactory.GetMSURequest();
      var PathologyDocument = new PathologyDocument(Patient, Request);
      Report Report = ReportFactory.GetMSUReport();
      PathologyDocument.ReportList.Add(Report);
      Report.ResultList.AddRange(ResultFactory.GetMSUResultList(Report.ReportReleaseDateTime.Subtract(TimeSpan.FromMinutes(5)), PdfFileName));
      return PathologyDocument;
    }

    public static PathologyDocument GetKaryotyping(string PdfFileName)
    {
      var Patient = PatientFactory.GetPatient(PatientFactory.PateintType.LarissaFERNIE);
      var Request = RequestFactory.GetKaryotypingRequest();
      var PathologyDocument = new PathologyDocument(Patient, Request);
      Report Report = ReportFactory.GetKaryotyping();
      PathologyDocument.ReportList.Add(Report);
      Report.ResultList.AddRange(ResultFactory.GetKaryotypingResultList(Report.ReportReleaseDateTime.Subtract(TimeSpan.FromMinutes(5)), PdfFileName));
      return PathologyDocument;
    }

    public static PathologyDocument GetLipids(string PdfFileName)
    {
      var Patient = PatientFactory.GetPatient(PatientFactory.PateintType.GregoryBLACKCOMB);
      var Request = RequestFactory.GetLipids();
      var PathologyDocument = new PathologyDocument(Patient, Request);
      Report Report = ReportFactory.GetLipids();
      PathologyDocument.ReportList.Add(Report);
      Report.ResultList.AddRange(ResultFactory.GetLipidsResultList(Report.ReportReleaseDateTime.Subtract(TimeSpan.FromMinutes(5)), PdfFileName));
      return PathologyDocument;
    }

    public static PathologyDocument GetProteinElectrophoresis(string PdfFileName)
    {
      var Patient = PatientFactory.GetPatient(PatientFactory.PateintType.GlennFERNIE);
      var Request = RequestFactory.GetProteinElectrophoresis();
      var PathologyDocument = new PathologyDocument(Patient, Request);
      Report Report = ReportFactory.GetProteinElectrophoresis();
      PathologyDocument.ReportList.Add(Report);
      Report.ResultList.AddRange(ResultFactory.GetProteinElectrophoresisResultList(Report.ReportReleaseDateTime.Subtract(TimeSpan.FromMinutes(5)), PdfFileName));
      return PathologyDocument;
    }

    public static PathologyDocument GetBloodGasArterial(string PdfFileName)
    {
      var Patient = PatientFactory.GetPatient(PatientFactory.PateintType.GlennFERNIE);
      var Request = RequestFactory.GetBloodGasArterial();
      var PathologyDocument = new PathologyDocument(Patient, Request);
      Report Report = ReportFactory.GetBloodGasArterial();
      PathologyDocument.ReportList.Add(Report);
      Report.ResultList.AddRange(ResultFactory.GetBloodGasArterialResultList(Report.ReportReleaseDateTime.Subtract(TimeSpan.FromMinutes(5)), PdfFileName));
      return PathologyDocument;
    }

    public static PathologyDocument GetSARSCoV2NAT(string PdfFileName)
    {
      var Patient = PatientFactory.GetPatient(PatientFactory.PateintType.HaydenNORQUAY);
      var Request = RequestFactory.GetSARSCoV2NAT();
      var PathologyDocument = new PathologyDocument(Patient, Request);
      Report Report = ReportFactory.GetSARSCoV2NAT();
      PathologyDocument.ReportList.Add(Report);
      Report.ResultList.AddRange(ResultFactory.GetSARSCoV2NATResultList(Report.ReportReleaseDateTime.Subtract(TimeSpan.FromMinutes(5)), PdfFileName));
      return PathologyDocument;
    }

    public static PathologyDocument GetSARSCoV2Serology(string PdfFileName)
    {
      var Patient = PatientFactory.GetPatient(PatientFactory.PateintType.GregoryBLACKCOMB);
      var Request = RequestFactory.GetSARSCoV2Serology();
      var PathologyDocument = new PathologyDocument(Patient, Request);
      Report Report = ReportFactory.GetSARSCoV2Serology();
      PathologyDocument.ReportList.Add(Report);
      Report.ResultList.AddRange(ResultFactory.GetSARSCoV2SerologyResultList(Report.ReportReleaseDateTime.Subtract(TimeSpan.FromMinutes(5)), PdfFileName));
      return PathologyDocument;
    }

  }
}
