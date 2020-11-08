using System;
using System.Collections.Generic;
using System.Text;
using PeterPiper.Hl7.V2.Model;
using Spia.PathologyReportModel.Model;

namespace Spia.AusHl7v2Generation.Factory
{
  public static class MSHFactory
  {       
    public static ISegment GetMSH(string MessageControlId, DateTimeOffset MessageDateTime, Laboratory PerformingLaboratory, string ReceivingApplicationNamespaceId, Organisation ReceivingFacilityOrganisation)
    {      
      IMessage Msg = Creator.Message("2.4", "ORU", "R01", MessageControlId, "ORU_R01");
      var MSH = Msg.Segment("MSH");

      IField SendingApplication = Creator.Field();
      SendingApplication.Component(1).AsString = PerformingLaboratory.LaboratoryInformationSystemApplicationCode;
      //SendingApplication.Component(2).AsString = messageMetadata.SendingApplication.UniversalId ?? "";
      //SendingApplication.Component(3).AsString = messageMetadata.SendingApplication.UniversalIdType ?? "";
      MSH.Element(3).Add(SendingApplication);

      IField SendingFacility = Creator.Field();
      SendingFacility.Component(1).AsString = PerformingLaboratory.FacilityCode;
      SendingFacility.Component(2).AsString = PerformingLaboratory.NataSiteNumber;
      SendingFacility.Component(3).AsString = "AUSNATA";
      MSH.Element(4).Add(SendingFacility);

      IField ReceivingApplication = Creator.Field();
      ReceivingApplication.Component(1).AsString = ReceivingApplicationNamespaceId;
      //ReceivingApplication.Component(2).AsString = messageMetadata.ReceivingApplication.UniversalId ?? "";
      //ReceivingApplication.Component(3).AsString = messageMetadata.ReceivingApplication.UniversalIdType ?? "";
      MSH.Element(5).Add(ReceivingApplication);

      IField ReceivingFacility = Creator.Field();
      ReceivingFacility.Component(1).AsString = ReceivingFacilityOrganisation.Name;
      ReceivingFacility.Component(2).AsString = ReceivingFacilityOrganisation.Identifier.Value;
      ReceivingFacility.Component(3).AsString = ReceivingFacilityOrganisation.Identifier.Type.ToString();
      MSH.Element(6).Add(ReceivingFacility);

      //20150410083015+1000
      MSH.Field(7).Convert.DateTime.SetDateTimeOffset(MessageDateTime, true);

      //Message Type
      //MSH.Field(9).Component(1).AsString = messageMetadata.MessageType;
      //MSH.Field(9).Component(2).AsString = messageMetadata.MessageEvent;
      //MSH.Field(9).Component(3).AsString = messageMetadata.MessageStructure;

      //Message Control Id
      MSH.Field(10).AsString = MessageControlId; ;

      //Production
      MSH.Field(11).AsString = "T";

      //HL7 Version
      //|2.4^AUS&Australia&ISO3166_1^HL7AU-OO-201701&&L|
      MSH.Element(12).ClearAll();
      MSH.Element(12).Component(1).AsString = "2.4";
      MSH.Element(12).Component(2).SubComponent(1).AsString = "AUS";
      MSH.Element(12).Component(2).SubComponent(2).AsString = "Australia";
      MSH.Element(12).Component(2).SubComponent(3).AsString = "ISO3166_1";
      MSH.Element(12).Component(3).SubComponent(1).AsString = "HL7AU-OO-201701";
      MSH.Element(12).Component(3).SubComponent(3).AsString = "L";

      //Accept Ack
      MSH.Field(15).AsString = "AL";

      //Application Ack
      MSH.Field(16).AsString = "AL";

      //CountryCode
      MSH.Field(17).AsString = "AUS";

      //CharacterSet
      MSH.Field(18).AsString = "ASCII";

      //PrincipalLanguageOfMessage
      //"en^English^ISO639"
      MSH.Element(19).ClearAll();
      MSH.Element(19).Component(1).AsString = "en";
      MSH.Element(19).Component(2).AsString = "English";
      MSH.Element(19).Component(3).AsString = "ISO639";

      return MSH.Clone();

    }    
  }
}
