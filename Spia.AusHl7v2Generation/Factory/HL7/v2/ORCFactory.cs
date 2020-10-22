using PeterPiper.Hl7.V2.Model;
using System;
using System.Collections.Generic;
using Spia.AusHl7v2Generation.Model.Logical;

namespace Spia.AusHl7v2Generation.Factory.HL7.v2
{

  public static class ORCFactory
  {
    //ORC|RE|112233^PathologyOrder^2.16.840.1.113883.19.4.1.5^ISO|15P000005-123456^SUPER-LIS^2.16.840.1.113883.19.1.2^ISO|44556677^PathologyOrderGroupID^2.16.840.1.113883.19.4.1.4^ISO|CM||||201504100800+1000|||DFTR^OrderingRankin^Jeff^^^Dr^^^SUPER-LIS~958678^OrderingRankin^Jeff^^^Dr^^^ADHAHOSP~4322581B^OrderingRankin^Jeff^^^Dr^^^AUSHICPR~8003614899999997^OrderingRankin^Jeff^^^Dr^^^AUSHIC^^^^^NPI||^WPN^PH^^^^0893412041|201504100730+1000    
    public static ISegment GetORC(Request request, EntityIdentifier reportIdentifier)
    {
      if (request is null)
        throw new ArgumentNullException(nameof(request));
      if (reportIdentifier is null)
        throw new ArgumentNullException(nameof(reportIdentifier));
      
      ISegment ORC = Creator.Segment("ORC");      
      ORC.Field(1).AsString = "RE";

      IField PlacerOrderNumber = Creator.Field();
      PlacerOrderNumber.Component(1).AsString = request.OrderNumber.Value;
      PlacerOrderNumber.Component(2).AsString = request.OrderNumber.NamespaceId;
      PlacerOrderNumber.Component(3).AsString = request.OrderNumber.UniversalId ?? "";
      PlacerOrderNumber.Component(4).AsString = request.OrderNumber.UniversalIdType ?? ""; ;
      ORC.Element(2).Add(PlacerOrderNumber);

      IField FillerOrderNumber = Creator.Field();
      FillerOrderNumber.Component(1).AsString = reportIdentifier.Value;
      FillerOrderNumber.Component(2).AsString = reportIdentifier.NamespaceId;
      FillerOrderNumber.Component(3).AsString = reportIdentifier.UniversalId ?? "";
      FillerOrderNumber.Component(4).AsString = reportIdentifier.UniversalIdType ?? "";
      ORC.Element(3).Add(FillerOrderNumber);

      if (request.GroupOrderNumber is object)
      {
        IField GroupOrderNumber = Creator.Field();
        GroupOrderNumber.Component(1).AsString = request.GroupOrderNumber.Value;
        GroupOrderNumber.Component(2).AsString = request.GroupOrderNumber.NamespaceId;
        GroupOrderNumber.Component(3).AsString = request.GroupOrderNumber.UniversalId ?? "";
        GroupOrderNumber.Component(4).AsString = request.GroupOrderNumber.UniversalIdType ?? "";
        ORC.Element(4).Add(GroupOrderNumber);
      }

      ORC.Field(5).AsString = "CM";

      //This field contains the date and time of the event that initiated the current transaction as reflected in ORC-1 Order Control Code
      ORC.Field(9).Convert.DateTime.SetDateTimeOffset(request.RequestedDateTime, true, PeterPiper.Hl7.V2.Support.Tools.DateTimeSupportTools.DateTimePrecision.Date);

      foreach (var Id in request.RequestingProvider.IdentifierList)
      {
        IField Field = Creator.Field();
        Field.Component(1).AsString = Id.Value;
        Field.Component(2).AsString = request.RequestingProvider.Name.Family;
        Field.Component(3).AsString = request.RequestingProvider.Name.Given;
        Field.Component(6).AsString = request.RequestingProvider.Name.Title;
        Field.Component(9).AsString = Id.AssigingAuthority ?? "";
        Field.Component(14).AsString = Id.Type ?? "";
        ORC.Element(12).Add(Field);
      }
      return ORC;
    }
  }
}
