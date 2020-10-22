using System;
using System.Collections.Generic;
using System.Text;

namespace Spia.AusHl7v2Generation.Factory.LogicalModel
{
  public class AusStandardHL7v2
  {
    public AusStandardHL7v2()
    {
      this.MedicareNumber = new IdentifierCode(assigingAuthority: "AUSHIC", typeCode: "MC");
      this.Ihi = new IdentifierCode(assigingAuthority: "AUSHIC", typeCode: "NI");
      this.Mrn = new IdentifierCode(assigingAuthority: "", typeCode: "MR");
    }
    public IdentifierCode MedicareNumber { get; private set; }
    public IdentifierCode Ihi { get; private set; }
    public IdentifierCode Mrn { get; private set; }

  }

  public class IdentifierCode
  {
    public IdentifierCode(string assigingAuthority, string typeCode)
    {
      AssigingAuthority = assigingAuthority;
      TypeCode = typeCode;
    }

    public string AssigingAuthority { get; private set; }
    public string TypeCode { get; private set; }
  }
}
