using System;
using PeterPiper.Hl7.V2.Model;
using System.Collections.Generic;
using System.Text;

namespace Spia.AusHl7v2Generation.Model.Logical
{
  public class MessageMetadata
  {
    public MessageMetadata(HierarchicDesignator sendingApplication, 
      HierarchicDesignator sendingFacility, 
      HierarchicDesignator receivingApplication, 
      HierarchicDesignator receivingFacility, 
      DateTimeOffset messageDateTime, 
      string messageType, 
      string messageEvent, 
      string messageStructure, 
      string messageControlId, 
      string processingId,
      IField versionId,       
      string acceptAck, 
      string applicationAck, 
      string countryCode, 
      string characterSet,
      IField principalLanguageOfMessage)
    {
      SendingApplication = sendingApplication ?? throw new ArgumentNullException(nameof(sendingApplication));
      SendingFacility = sendingFacility ?? throw new ArgumentNullException(nameof(sendingFacility));
      ReceivingApplication = receivingApplication ?? throw new ArgumentNullException(nameof(receivingApplication));
      ReceivingFacility = receivingFacility ?? throw new ArgumentNullException(nameof(receivingFacility));
      MessageDateTime = messageDateTime;
      MessageType = messageType ?? throw new ArgumentNullException(nameof(messageType));
      MessageEvent = messageEvent ?? throw new ArgumentNullException(nameof(messageEvent));
      MessageStructure = messageStructure ?? throw new ArgumentNullException(nameof(messageStructure));
      MessageControlId = messageControlId ?? throw new ArgumentNullException(nameof(messageControlId));
      ProcessingId = processingId ?? throw new ArgumentNullException(nameof(processingId));
      VersionId = versionId ?? throw new ArgumentNullException(nameof(versionId));     
      AcceptAck = acceptAck ?? throw new ArgumentNullException(nameof(acceptAck));
      ApplicationAck = applicationAck ?? throw new ArgumentNullException(nameof(applicationAck));
      CountryCode = countryCode ?? throw new ArgumentNullException(nameof(countryCode));
      CharacterSet = characterSet ?? throw new ArgumentNullException(nameof(characterSet));
      PrincipalLanguageOfMessage = principalLanguageOfMessage ?? throw new ArgumentNullException(nameof(principalLanguageOfMessage));
    }

    public HierarchicDesignator SendingApplication { get; set; }
    public HierarchicDesignator SendingFacility { get; set; }
    public HierarchicDesignator ReceivingApplication { get; set; }
    public HierarchicDesignator ReceivingFacility { get; set; }
    public DateTimeOffset MessageDateTime { get; set; }
    public string MessageType { get; set; }
    public string MessageEvent { get; set; }
    public string MessageStructure { get; set; }
    public string MessageControlId { get; set; }
    public string ProcessingId { get; set; }
    public IField VersionId { get; set; }
    public string InternationalizationCode { get; set; }    
    public string InternationalVersionId { get; set; }
    public string AcceptAck { get; set; }
    public string ApplicationAck { get; set; }
    public string CountryCode { get; set; }    
    public string CharacterSet { get; set; }
    public IField PrincipalLanguageOfMessage { get; set; }

  }
}
