using System;
using System.Collections.Generic;
using System.Text;

namespace Spia.AusHl7v2Generation.Model.Logical
{
  //112233^RhubarbOrders^2.16.840.1.113883.19.4.1.5^ISO
  public class EntityIdentifier
  {
    public EntityIdentifier(string value, string namespaceId)
    {
      Value = value;
      NamespaceId = namespaceId;
    }    
    public string Value { get; set; }
    public string NamespaceId { get; set; }
    public string UniversalId { get; set; }
    public string UniversalIdType { get; set; }
  }
}
