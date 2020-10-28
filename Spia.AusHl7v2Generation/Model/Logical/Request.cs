using System;
using System.Collections.Generic;
using System.Text;

namespace Spia.AusHl7v2Generation.Model.Logical
{
  public class Request
  {
    public Request(HierarchicDesignator receivingApplication, HierarchicDesignator receivingFacility, Provider requestingProvider, EntityIdentifier orderNumber, DateTimeOffset requestedDateTime)
    {
      ReceivingApplication = receivingApplication;
      ReceivingFacility = receivingFacility;
      RequestingProvider = requestingProvider;      
      OrderNumber = orderNumber;
      RequestedDateTime = requestedDateTime;
      CopyProviderList = new List<Provider>();
    }

    public HierarchicDesignator ReceivingApplication { get; set; }
    public HierarchicDesignator ReceivingFacility { get; set; }
    public Provider RequestingProvider { get; set; }
    public List<Provider> CopyProviderList { get; }
    public EntityIdentifier OrderNumber { get; set; }
    
    public DateTimeOffset RequestedDateTime { get; set; }
    public string ClinicalNotes { get; set; }
    public string CallBackPhoneNumber { get; set; }

  }
}
