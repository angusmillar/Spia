using Spia.AusHl7v2Generation.Model.Logical;
using System;
using System.Collections.Generic;
using System.Text;

namespace Spia.AusHl7v2Generation.Factory.LogicalModel
{
  public static class MessageMetaFactory
  {
    public static MessageMetadata GetMessageMetadata(Report report, Request request, string messageControlId)
    {
      if (report is null)
        throw new ArgumentNullException(nameof(report));
      if (request is null)
        throw new ArgumentNullException(nameof(request));
      if (string.IsNullOrWhiteSpace(messageControlId))
        throw new ArgumentNullException(nameof(messageControlId));

      var MessageDateTime = report.ReportReleaseDateTime.AddMinutes(5);
      var MessageType = "ORU";
      var MessageEvent = "R01";
      var MessageStructure = $"{MessageType}_{MessageEvent}";
      var MessageControlId = messageControlId;
      var ProcessingId = "T";
      var VersionId = PeterPiper.Hl7.V2.Model.Creator.Field("2.4^AUS&Australia&ISO3166_1^HL7AU-OO-201701&&L");      
      var AcceptAck = "AL";
      var ApplicationAck = "AL";
      var CountryCode = "AUS";
      var CharacterSet = "8859/1";
      var PrincipalLanguageOfMessage = PeterPiper.Hl7.V2.Model.Creator.Field("en^English^ISO639");       

      return new MessageMetadata(report.SendingApplication, report.SendingFacility, request.ReceivingApplication, request.ReceivingFacility,
        MessageDateTime, MessageType, MessageEvent, MessageStructure, MessageControlId, ProcessingId, VersionId,
        AcceptAck, ApplicationAck, CountryCode, CharacterSet, PrincipalLanguageOfMessage);        
    }
  }
}
