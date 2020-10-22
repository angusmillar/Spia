using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spia.AusHl7v2Generation.Model.Logical;
using PeterPiper.Hl7.V2.Model;

namespace Spia.AusHl7v2Generation.Factory.HL7.v2
{
  public static class OBRFactory
  {
    public static ISegment GetOBR(Request request, Report report)
    {
      //OBR|1|112233^PathologyOrder^2.16.840.1.113883.19.4.1.5^ISO|15P000005-123456^SUPER-LIS^2.16.840.1.113883.19.1.2^ISO|26604007^Complete blood count^SCT^FBE^Full Blood Count^SUPER-LIS|||201504100930+1000||||||Patient has a history of severe gout caused by rhubarb.|201504101100+1000||DFTR^OrderingRankin^Jeff^^^Dr^^^SUPER-LIS~958678^OrderingRankin^Jeff^^^Dr^^^ADHAHOSP~4322581B^OrderingRankin^Jeff^^^Dr^^^AUSHICPR~8003614899999997^OrderingRankin^Jeff^^^Dr^^^AUSHIC^^^^^NPI|^WPN^PH^^^^0893412041|||CP=N,DR=4322581B||201504101115+1000||HM|F||^^^201504100800+1000^^RT|2304227F^CopyDoctorRyan^Paul^^^Dr^^^AUSHICPR~0813266H^CopyDoctorArnet^Claire^^^Dr^^^AUSHICPR~4628361B^CopyDoctorTeller^Sally^^^Dr^^^AUSHICPR||||DRPRIM&PathologistThompson&Harry&&&DR&&&SUPER-LIS
      if (request is null)
        throw new ArgumentNullException(nameof(request));
      if (report is null)
        throw new ArgumentNullException(nameof(report));

      ISegment OBR = Creator.Segment("OBR");      
      OBR.Field(1).AsString = "1";

      IField PlacerOrderNumber = Creator.Field();
      PlacerOrderNumber.Component(1).AsString = request.OrderNumber.Value;
      PlacerOrderNumber.Component(2).AsString = request.OrderNumber.NamespaceId;
      PlacerOrderNumber.Component(3).AsString = request.OrderNumber.UniversalId ?? "";
      PlacerOrderNumber.Component(4).AsString = request.OrderNumber.UniversalIdType ?? "";
      OBR.Element(2).Add(PlacerOrderNumber);

      IField FillerOrderNumber = Creator.Field();
      FillerOrderNumber.Component(1).AsString = report.ReportIdentifier.Value;
      FillerOrderNumber.Component(2).AsString = report.ReportIdentifier.NamespaceId;
      FillerOrderNumber.Component(3).AsString = report.ReportIdentifier.UniversalId ?? "";
      FillerOrderNumber.Component(4).AsString = report.ReportIdentifier.UniversalIdType ?? "";
      OBR.Element(3).Add(FillerOrderNumber);

      //26604007^Complete blood count^SCT^FBE^Full Blood Count^SUPER-LIS
      IField UniversalServiceIdentifier = Creator.Field();
      UniversalServiceIdentifier.Component(1).AsString = report.ReportType?.International?.Value ?? "";
      UniversalServiceIdentifier.Component(2).AsString = report.ReportType?.International?.Description ?? "";
      UniversalServiceIdentifier.Component(3).AsString = report.ReportType?.International?.System ?? "";
      UniversalServiceIdentifier.Component(4).AsString = report.ReportType?.Local?.Value ?? "";
      UniversalServiceIdentifier.Component(5).AsString = report.ReportType?.Local?.Description ?? "";
      UniversalServiceIdentifier.Component(6).AsString = report.ReportType?.Local?.System ?? ""; ;
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
        IField Field = Creator.Field();
        Field.Component(1).AsString = Id.Value;
        Field.Component(2).AsString = request.RequestingProvider.Name.Family;
        Field.Component(3).AsString = request.RequestingProvider.Name.Given;
        Field.Component(6).AsString = request.RequestingProvider.Name.Title;
        Field.Component(9).AsString = Id.AssigingAuthority ?? "";
        Field.Component(10).AsString = request.RequestingProvider.Name.TypeCode ?? "";
        Field.Component(13).AsString = Id.Type ?? "";
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

      var OrderingDrProviderNumber = request.RequestingProvider.IdentifierList.SingleOrDefault(x => x.AssigingAuthority == "AUSHICPR");
      if (OrderingDrProviderNumber is object)
      {
        //Copy = No, directed at the DR=ProviderNumber
        OBR.Field(20).AsString = $"CP=N,DR={OrderingDrProviderNumber.Value}";
      }

      //Report Status Change DateTime
      OBR.Field(22).Convert.DateTime.SetDateTimeOffset(report.ReportReleaseDateTime, true);

      //Service Section Id
      OBR.Field(24).AsString = report.DiagnosticServiceSectionId ?? "";

      //Report Status
      OBR.Field(25).AsString = report.Status.ToString();

      //Quantity/Timing (Collection Date Time)
      IField QuantityTiming = Creator.Field();
      QuantityTiming.Component(4).Convert.DateTime.SetDateTimeOffset(request.RequestedDateTime, true);
      QuantityTiming.Component(6).AsString = "RT";
      OBR.Element(27).Add(QuantityTiming);

      //Copy Doctors
      foreach (var CopyDr in request.CopyProviderList)
      {
        foreach (var Id in CopyDr.IdentifierList)
        {
          IField Field = Creator.Field();
          Field.Component(1).AsString = Id.Value;
          Field.Component(2).AsString = CopyDr.Name.Family;
          Field.Component(3).AsString = CopyDr.Name.Given;
          Field.Component(6).AsString = CopyDr.Name.Title ?? "";
          Field.Component(9).AsString = Id.AssigingAuthority ?? "";
          Field.Component(10).AsString = CopyDr.Name.TypeCode ?? "";
          Field.Component(13).AsString = Id.Type ?? "";
          OBR.Element(28).Add(Field);
        }
      }

      //Principle Result Interpretor
      //DRPRIM&PathologistThompson&Harry&&&DR&&&SUPER-LIS
      IField PrincipleResultInterpretor = Creator.Field();
      PrincipleResultInterpretor.Component(1).SubComponent(1).AsString = report.ReportingPathologist.IdentifierList[0]?.Value ?? "";
      PrincipleResultInterpretor.Component(1).SubComponent(2).AsString = report.ReportingPathologist.Name.Family;
      PrincipleResultInterpretor.Component(1).SubComponent(3).AsString = report.ReportingPathologist.Name.Given;
      PrincipleResultInterpretor.Component(1).SubComponent(6).AsString = report.ReportingPathologist.Name.Title ?? "";
      PrincipleResultInterpretor.Component(1).SubComponent(9).AsString = report.ReportingPathologist.IdentifierList[0]?.AssigingAuthority ?? "";
      OBR.Element(32).Add(PrincipleResultInterpretor);

      return OBR;
    }

  }
}
