using Newtonsoft.Json;
using Spia.PathologyReportModel.CustomAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spia.PathologyReportModel.Model
{
  public class ResultType : PathologyModelBase
  {
    [JsonProperty(PropertyName = "lonic", Required = Required.AllowNull)]
    [RequiredScope(ScopeType.Hl7v2, RequiredType.Optional)]
    [RequiredScope(ScopeType.Cda, RequiredType.Optional)]
    [RequiredScope(ScopeType.Fhir, RequiredType.Optional)]
    public Code Lonic { get; set; }

    [JsonProperty(PropertyName = "local", Required = Required.Always)]
    [RequiredScope(ScopeType.Hl7v2, RequiredType.Mandatory)]
    [RequiredScope(ScopeType.Cda, RequiredType.Mandatory)]
    [RequiredScope(ScopeType.Fhir, RequiredType.Mandatory)]
    public Code Local { get; set; }
  }
}
