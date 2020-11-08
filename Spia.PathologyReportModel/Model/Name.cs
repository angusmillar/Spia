using Newtonsoft.Json;
using Spia.PathologyReportModel.CustomAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spia.PathologyReportModel.Model
{
  public class Name : PathologyModelBase
  {
    [JsonProperty(PropertyName = "family", Required = Required.Always)]
    [RequiredScope(ScopeType.Hl7v2, RequiredType.Mandatory)]
    [RequiredScope(ScopeType.Cda, RequiredType.Mandatory)]
    [RequiredScope(ScopeType.Fhir, RequiredType.Mandatory)]
    public string Family { get; set; }

    [JsonProperty(PropertyName = "given", Required = Required.AllowNull)]
    [RequiredScope(ScopeType.Hl7v2, RequiredType.Optional)]
    [RequiredScope(ScopeType.Cda, RequiredType.Optional)]
    [RequiredScope(ScopeType.Fhir, RequiredType.Optional)]
    public string Given { get; set; }

    [JsonProperty(PropertyName = "middle", Required = Required.AllowNull)]
    [RequiredScope(ScopeType.Hl7v2, RequiredType.Optional)]
    [RequiredScope(ScopeType.Cda, RequiredType.Optional)]
    [RequiredScope(ScopeType.Fhir, RequiredType.Optional)]
    public string Middle { get; set; }

    [JsonProperty(PropertyName = "title", Required = Required.Default)]
    [RequiredScope(ScopeType.Hl7v2, RequiredType.Optional)]
    [RequiredScope(ScopeType.Cda, RequiredType.Optional)]
    [RequiredScope(ScopeType.Fhir, RequiredType.Optional)]
    public string Title { get; set; }

  }
}
