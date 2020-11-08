using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Spia.PathologyReportModel.CustomAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spia.PathologyReportModel.Model
{
  public enum IdentifierType { MedicareNumber, IHI, HPII, HPIO, MedicareProviderNumber, GUID, LocalToLab, MRN};
  public class Identifier : PathologyModelBase
  {
    [JsonProperty(PropertyName = "value", Required = Required.Always)]
    [RequiredScope(ScopeType.Hl7v2, RequiredType.Mandatory)]
    [RequiredScope(ScopeType.Cda, RequiredType.Mandatory)]
    [RequiredScope(ScopeType.Fhir, RequiredType.Mandatory)]
    public string Value { get; set; }

    [JsonProperty(PropertyName = "type", Required = Required.Always)]
    [JsonConverter(typeof(StringEnumConverter))]
    [RequiredScope(ScopeType.Hl7v2, RequiredType.Mandatory)]
    [RequiredScope(ScopeType.Cda, RequiredType.Mandatory)]
    [RequiredScope(ScopeType.Fhir, RequiredType.Mandatory)]
    public IdentifierType Type { get; set; }

    [JsonProperty(PropertyName = "assigningAuthority", Required = Required.AllowNull)]
    [RequiredScope(ScopeType.Hl7v2, RequiredType.Conditional)]
    [RequiredScope(ScopeType.Cda, RequiredType.Conditional)]
    [RequiredScope(ScopeType.Fhir, RequiredType.Conditional)]
    public string AssigningAuthority { get; set; }

    public override bool ConditionalValidationIsValid(out List<string> ErrorMessageList, string Path)
    {
      ErrorMessageList = new List<string>();
      if (this.Type == IdentifierType.MRN)
      {
        if (string.IsNullOrWhiteSpace(this.AssigningAuthority))
        {
          ErrorMessageList.Add($"It is a conditional requirement that is the {nameof(this.AssigningAuthority)} must be set to a value when the {nameof(this.Type)} is set to {this.Type.ToString()}. The {nameof(this.AssigningAuthority)} was found to be null or empty at the following path:  {Path}");
        }
      }
      return ErrorMessageList.Count == 0;      
    }
  }
}
