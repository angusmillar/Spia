using System;
using System.Collections.Generic;
using System.Text;

namespace Spia.AusHl7v2Generation.Model.Logical
{
  public class HierarchicDesignator
  {
    public HierarchicDesignator(string namespaceId)
    {
      NamespaceId = namespaceId;
    }

    public string NamespaceId { get; set; }
    public string UniversalId { get; set; }
    public string UniversalIdType { get; set; }
  }
}
