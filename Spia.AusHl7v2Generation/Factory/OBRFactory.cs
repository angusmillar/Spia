using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PeterPiper.Hl7.V2.Model;
using Spia.AusHl7v2Generation.Support;
using Spia.PathologyReportModel.Support;

namespace Spia.AusHl7v2Generation.Factory
{
  public static class OBRFactory
  {
    public static ISegment GetOBR(Spia.PathologyReportModel.Model.Request request, Spia.PathologyReportModel.Model.Report report, Spia.PathologyReportModel.Model.Laboratory performingLab)
    {
      //OBR|1|112233^PathologyOrder^2.16.840.1.113883.19.4.1.5^ISO|15P000005-123456^SUPER-LIS^2.16.840.1.113883.19.1.2^ISO|26604007^Complete blood count^SCT^FBE^Full Blood Count^SUPER-LIS|||201504100930+1000||||||Patient has a history of severe gout caused by rhubarb.|201504101100+1000||DFTR^OrderingRankin^Jeff^^^Dr^^^SUPER-LIS~958678^OrderingRankin^Jeff^^^Dr^^^ADHAHOSP~4322581B^OrderingRankin^Jeff^^^Dr^^^AUSHICPR~8003614899999997^OrderingRankin^Jeff^^^Dr^^^AUSHIC^^^^^NPI|^WPN^PH^^^^0893412041|||CP=N,DR=4322581B||201504101115+1000||HM|F||^^^201504100800+1000^^RT|2304227F^CopyDoctorRyan^Paul^^^Dr^^^AUSHICPR~0813266H^CopyDoctorArnet^Claire^^^Dr^^^AUSHICPR~4628361B^CopyDoctorTeller^Sally^^^Dr^^^AUSHICPR||||DRPRIM&PathologistThompson&Harry&&&DR&&&SUPER-LIS
      if (request is null)
        throw new ArgumentNullException(nameof(request));
      if (report is null)
        throw new ArgumentNullException(nameof(report));

      ISegment OBR = Creator.Segment("OBR");
      OBR.Field(1).AsString = "1";

      IField PlacerOrderNumber = Creator.Field();
      PlacerOrderNumber.Component(1).AsString = request.OrderNumber ?? "";
      PlacerOrderNumber.Component(2).AsString = "PathologyOrder";
      PlacerOrderNumber.Component(3).AsString = performingLab.NataSiteNumber;
      PlacerOrderNumber.Component(4).AsString = "AUSNATA";
      OBR.Element(2).Add(PlacerOrderNumber);

      IField FillerOrderNumber = Creator.Field();
      FillerOrderNumber.Component(1).AsString = report.ReportId;
      FillerOrderNumber.Component(2).AsString = performingLab.FacilityCode;
      FillerOrderNumber.Component(3).AsString = performingLab.NataSiteNumber;
      FillerOrderNumber.Component(4).AsString = "AUSNATA";
      OBR.Element(3).Add(FillerOrderNumber);

      //26604007^Complete blood count^SCT^FBE^Full Blood Count^SUPER-LIS
      IField UniversalServiceIdentifier = Creator.Field();
      UniversalServiceIdentifier.Component(1).AsString = report.ReportType.Snomed?.Term ?? "";
      UniversalServiceIdentifier.Component(2).AsString = report.ReportType.Snomed?.Description ?? "";
      if (!UniversalServiceIdentifier.Component(1).IsEmpty)
      {
        UniversalServiceIdentifier.Component(3).AsString = "SCT";
      }
      UniversalServiceIdentifier.Component(4).AsString = report.ReportType.Local.Term;
      UniversalServiceIdentifier.Component(5).AsString = report.ReportType.Local.Description;
      UniversalServiceIdentifier.Component(6).AsString = $"NATA{performingLab.NataSiteNumber}";
      OBR.Element(4).Add(UniversalServiceIdentifier);

      //Observation Date/Time (Collection DateTime)      
      OBR.Field(7).Convert.DateTime.SetDateTimeOffset(report.CollectionDateTime, true);

      //Clinical Note
      OBR.Field(13).AsString = request.ClinicalNotes ?? "";

      //Specimen received date/time    
      //Definition: For observations requiring a specimen, the specimen received date/time is the actual login time at the diagnostic service. 
      //This field must contain a value when the order is accompanied 
      //by a specimen, or when the observation required a specimen and the message is a report.
      OBR.Field(14).Convert.DateTime.SetDateTimeOffset(report.SpecimenReceivedDateTime, true);

      //Ordering Doctor
      foreach (var Id in request.RequestingProvider.IdentifierList)
      {
        var IdentiferTypeInfo = HL7v2IdentifierSupport.GetIdentiferCode(Id, $"NATA{performingLab.NataSiteNumber}");
        IField Field = Creator.Field();
        Field.Component(1).AsString = IdentiferTypeInfo.Value;
        Field.Component(2).AsString = request.RequestingProvider.Name.Family;
        Field.Component(3).AsString = request.RequestingProvider.Name.Given;
        Field.Component(6).AsString = request.RequestingProvider.Name.Title;
        Field.Component(9).AsString = IdentiferTypeInfo.AssigingAuthority;
        Field.Component(10).AsString = "L";
        Field.Component(13).AsString = IdentiferTypeInfo.TypeCode;
        OBR.Element(16).Add(Field);
      }

      if (request.CallBackPhoneNumber is object)
      {
        IField OrderCallBackNumber = Creator.Field();
        OrderCallBackNumber.Component(2).AsString = "WPN";
        OrderCallBackNumber.Component(3).AsString = "PH";
        OrderCallBackNumber.Component(7).AsString = request.CallBackPhoneNumber;
        OBR.Element(17).Add(OrderCallBackNumber);
      }

      var OrderingDrProviderNumber = request.RequestingProvider.IdentifierList.SingleOrDefault(x => x.Type == PathologyReportModel.Model.IdentifierType.MedicareProviderNumber);
      if (OrderingDrProviderNumber is object)
      {
        //Copy = No, directed at the DR=ProviderNumber
        OBR.Field(20).AsString = $"CP=N,DR={OrderingDrProviderNumber.Value}";
      }

      //Report Status Change DateTime
      OBR.Field(22).Convert.DateTime.SetDateTimeOffset(report.ReportReleaseDateTime, true);

      //Service Section Id      
      DiagnosticServiceSectionIdSupport DiagnosticServiceSectionIdSupport = new DiagnosticServiceSectionIdSupport();
      if (DiagnosticServiceSectionIdSupport.TryLookupByEnum(report.Department, out string DiagnosticServiceSectionIdCode))
      {
        OBR.Field(24).AsString = DiagnosticServiceSectionIdCode;        
      }
      else
      {
        throw new ApplicationException($"Unable to convert the {nameof(report.Department)} of {report.Department.ToString()} to a code for the HL7 OBR-24 field.");
      }      

      //Report Status
      ResultStatusTypeSupport ResultStatusTypeSupport = new ResultStatusTypeSupport();
      if (ResultStatusTypeSupport.TryLookupByEnum(report.ReportStatus, out string ReportStatus))
      {
        OBR.Field(25).AsString = ReportStatus;
      }
      else
      {
        throw new ApplicationException($"Unable to convert the {nameof(report.ReportStatus)} of {report.ReportStatus.ToString()} to a code for the HL7 OBR-25 field.");
      }      

      //Quantity/Timing (Collection Date Time)
      IField QuantityTiming = Creator.Field();
      QuantityTiming.Component(4).Convert.DateTime.SetDateTimeOffset(request.RequestedDate, true);
      QuantityTiming.Component(6).AsString = "RT";
      OBR.Element(27).Add(QuantityTiming);

      //Copy Doctors
      if (request.CopyToList is object)
      {
        foreach (var CopyDr in request.CopyToList)
        {
          var IdenitferToUserForCopyToDoctor = CopyDr.IdentifierList.SingleOrDefault(x => x.Type == PathologyReportModel.Model.IdentifierType.MedicareProviderNumber);
          if (IdenitferToUserForCopyToDoctor == null)
          {
            IdenitferToUserForCopyToDoctor = CopyDr.IdentifierList.SingleOrDefault(x => x.Type == PathologyReportModel.Model.IdentifierType.LocalToLab);
          }
          if (IdenitferToUserForCopyToDoctor == null)
          {
            throw new ApplicationException($"All CopyTo doctors must have a Medicare Provider Number (preferred) or a Local Lab ID. The CopyTo entry of {CopyDr.Name.Title ?? ""} {CopyDr.Name.Family} {CopyDr.Name.Given ?? ""} did not have a either.");
          }

          var IdentiferTypeInfo = HL7v2IdentifierSupport.GetIdentiferCode(IdenitferToUserForCopyToDoctor, $"NATA{performingLab.NataSiteNumber}");
          IField Field = Creator.Field();
          Field.Component(1).AsString = IdentiferTypeInfo.Value;
          Field.Component(2).AsString = CopyDr.Name.Family;
          Field.Component(3).AsString = CopyDr.Name.Given ?? "";
          Field.Component(6).AsString = CopyDr.Name.Title ?? "";
          Field.Component(9).AsString = IdentiferTypeInfo.AssigingAuthority;
          Field.Component(10).AsString = "L";
          Field.Component(13).AsString = IdentiferTypeInfo.TypeCode;
          OBR.Element(28).Add(Field);
        }
      }
      //Principle Result Interpretor
      //DRPRIM&PathologistThompson&Harry&&&DR&&&SUPER-LIS
      var PrincipleResultInterpretorLocalLabCode = report.ReportingPathologist.IdentifierList.SingleOrDefault(x => x.Type == PathologyReportModel.Model.IdentifierType.LocalToLab);
      if (PrincipleResultInterpretorLocalLabCode != null)
      {
        IField PrincipleResultInterpretor = Creator.Field();
        PrincipleResultInterpretor.Component(1).SubComponent(1).AsString = PrincipleResultInterpretorLocalLabCode.Value;
        PrincipleResultInterpretor.Component(1).SubComponent(2).AsString = report.ReportingPathologist.Name.Family;
        PrincipleResultInterpretor.Component(1).SubComponent(3).AsString = report.ReportingPathologist.Name.Given;
        PrincipleResultInterpretor.Component(1).SubComponent(6).AsString = report.ReportingPathologist.Name.Title ?? "";
        PrincipleResultInterpretor.Component(1).SubComponent(9).AsString = $"NATA{performingLab.NataSiteNumber}";
        OBR.Element(32).Add(PrincipleResultInterpretor);
      }
      else
      {
        throw new ApplicationException($"Unable to locate a {PathologyReportModel.Model.IdentifierType.LocalToLab.ToString()} identifier for the Reporting Pathologist");
      }

      return OBR;
    }

  }
}
