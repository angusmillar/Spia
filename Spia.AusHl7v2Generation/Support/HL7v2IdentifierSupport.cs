using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spia.PathologyReportModel.Model;

namespace Spia.AusHl7v2Generation.Support
{
  public static class HL7v2IdentifierSupport
  {
    public static IdentifierCode GetIdentiferCode(Identifier identifier, string LocalToLabAssigingAuthorityCode = null)
    {
      switch (identifier.Type)
      {
        case IdentifierType.MedicareNumber:
          return new IdentifierCode(assigingAuthority: "AUSHIC", typeCode: "MC", value: identifier.Value);
        case IdentifierType.IHI:
          return new IdentifierCode(assigingAuthority: "AUSHIC", typeCode: "NI", value: identifier.Value);
        case IdentifierType.HPII:
          return new IdentifierCode(assigingAuthority: "AUSHIC", typeCode: "NPI", value: identifier.Value);
        case IdentifierType.HPIO:
          return new IdentifierCode(assigingAuthority: "AUSHIC", typeCode: "NOI", value: identifier.Value);
        case IdentifierType.MedicareProviderNumber:          
          return new IdentifierCode(assigingAuthority: "AUSHICPR", typeCode: "", value: identifier.Value);
        case IdentifierType.GUID:
          return new IdentifierCode(assigingAuthority: "", typeCode: "GUID", value: identifier.Value);
        case IdentifierType.LocalToLab:
          return new IdentifierCode(assigingAuthority: LocalToLabAssigingAuthorityCode, typeCode: "", value: identifier.Value);
        case IdentifierType.MRN:
          return new IdentifierCode(assigingAuthority: identifier.AssigningAuthority, typeCode: "MR", value: identifier.Value);
        default:
          throw new ApplicationException($"No HL7 v2 mapping for the identifier type of : {identifier.ToString()}");
      }
    }

    public class IdentifierCode
    {
      public IdentifierCode(string assigingAuthority, string typeCode, string value)
      {
        AssigingAuthority = assigingAuthority;
        TypeCode = typeCode;
        Value = value;
      }
      public string Value { get; private set; }
      public string AssigingAuthority { get; private set; }
      public string TypeCode { get; private set; }
    }
  }

}
