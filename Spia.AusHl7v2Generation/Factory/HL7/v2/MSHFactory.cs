using System;
using System.Collections.Generic;
using System.Text;
using Spia.AusHl7v2Generation.Model.Logical;
using PeterPiper.Hl7.V2.Model;

namespace Spia.AusHl7v2Generation.Factory.HL7.v2
{
  public static class MSHFactory
  {
    //MSH|^~\&|SUPER-LIS^2.16.840.1.113883.19.1^ISO|ADHAPath^4321^AUSNATA|Rhubarb-EMR^2.16.840.1.113883.19.4.2^ISO|ADHAHOSP^2.16.840.1.113883.19.5^ISO|20150410083015+1000||ORU^R01^ORU_R01|P0000051504102331070|P|2.4|||AL|NE|AUS|8859/1
    public static ISegment GetMSH(MessageMetadata messageMetadata)
    {
      if (messageMetadata is null)
        throw new ArgumentNullException(nameof(messageMetadata));

      IMessage Msg = Creator.Message("2.4", messageMetadata.MessageType, messageMetadata.MessageEvent, messageMetadata.MessageControlId, messageMetadata.MessageStructure);
      var MSH = Msg.Segment("MSH");

      IField SendingApplication = Creator.Field();
      SendingApplication.Component(1).AsString = messageMetadata.SendingApplication.NamespaceId;
      SendingApplication.Component(2).AsString = messageMetadata.SendingApplication.UniversalId ?? "";
      SendingApplication.Component(3).AsString = messageMetadata.SendingApplication.UniversalIdType ?? "";
      MSH.Element(3).Add(SendingApplication);

      IField SendingFacility = Creator.Field();
      SendingFacility.Component(1).AsString = messageMetadata.SendingFacility.NamespaceId;
      SendingFacility.Component(2).AsString = messageMetadata.SendingFacility.UniversalId ?? "";
      SendingFacility.Component(3).AsString = messageMetadata.SendingFacility.UniversalIdType ?? "";
      MSH.Element(4).Add(SendingFacility);

      IField ReceivingApplication = Creator.Field();
      ReceivingApplication.Component(1).AsString = messageMetadata.ReceivingApplication.NamespaceId;
      ReceivingApplication.Component(2).AsString = messageMetadata.ReceivingApplication.UniversalId ?? "";
      ReceivingApplication.Component(3).AsString = messageMetadata.ReceivingApplication.UniversalIdType ?? "";
      MSH.Element(5).Add(ReceivingApplication);

      IField ReceivingFacility = Creator.Field();
      ReceivingFacility.Component(1).AsString = messageMetadata.ReceivingFacility.NamespaceId;
      ReceivingFacility.Component(2).AsString = messageMetadata.ReceivingFacility.UniversalId;
      ReceivingFacility.Component(3).AsString = messageMetadata.ReceivingFacility.UniversalIdType;
      MSH.Element(6).Add(ReceivingFacility);

      //20150410083015+1000
      MSH.Field(7).Convert.DateTime.SetDateTimeOffset(messageMetadata.MessageDateTime, true);

      //Message Type
      MSH.Field(9).Component(1).AsString = messageMetadata.MessageType;
      MSH.Field(9).Component(2).AsString = messageMetadata.MessageEvent;
      MSH.Field(9).Component(3).AsString = messageMetadata.MessageStructure;

      //Message Control Id
      MSH.Field(10).AsString = messageMetadata.MessageControlId; ;

      //Production
      MSH.Field(11).AsString = messageMetadata.ProcessingId;

      //HL7 Version
      MSH.Element(12).ClearAll();
      MSH.Element(12).Add(messageMetadata.VersionId.Clone());      

      //Accept Ack
      MSH.Field(15).AsString = messageMetadata.AcceptAck;
      
      //Application Ack
      MSH.Field(16).AsString = messageMetadata.ApplicationAck;

      //Application Ack
      MSH.Field(17).AsString = messageMetadata.CountryCode;

      //Application Ack
      MSH.Field(18).AsString = messageMetadata.CharacterSet;

      //Application Ack
      MSH.Element(19).ClearAll();
      MSH.Element(19).Add(messageMetadata.PrincipalLanguageOfMessage.Clone());
      
      return MSH.Clone();

    }
  }
}
