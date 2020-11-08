using Newtonsoft.Json;
using Spia.PathologyReportModel.CustomAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spia.PathologyReportModel.Model
{
  public class PathologyReportContainer: PathologyModelBase
  {
    [JsonProperty(PropertyName = "pathologyReport", Required = Required.Always)]
    [RequiredScope(ScopeType.Hl7v2, RequiredType.Mandatory)]
    [RequiredScope(ScopeType.Cda, RequiredType.Mandatory)]
    [RequiredScope(ScopeType.Fhir, RequiredType.Mandatory)]
    public PathologyReport PathologyReport { get; set; }

  }
}
