using Newtonsoft.Json;
using Spia.PathologyReportModel.CustomAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spia.PathologyReportModel.Model
{
  public class Code : PathologyModelBase
  {
    [JsonProperty(PropertyName = "term", Required = Required.Always)]
    [RequiredScope(ScopeType.Hl7v2, RequiredType.Mandatory)]
    [RequiredScope(ScopeType.Cda, RequiredType.Mandatory)]
    [RequiredScope(ScopeType.Fhir, RequiredType.Mandatory)]
    public string Term { get; set; }

    [JsonProperty(PropertyName = "description", Required = Required.Always)]
    [RequiredScope(ScopeType.Hl7v2, RequiredType.Mandatory)]
    [RequiredScope(ScopeType.Cda, RequiredType.Mandatory)]
    [RequiredScope(ScopeType.Fhir, RequiredType.Mandatory)]
    public string Description { get; set; }

    [JsonProperty(PropertyName = "oid", Required = Required.AllowNull)]
    [RequiredScope(ScopeType.Hl7v2, RequiredType.NotRequired)]
    [RequiredScope(ScopeType.Cda, RequiredType.Optional)]
    [RequiredScope(ScopeType.Fhir, RequiredType.NotRequired)]
    public string Oid { get; set; }
  }
}
