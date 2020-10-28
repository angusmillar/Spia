using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spia.PathologyReportModel.Model
{
  public class Result
  {    
    [JsonProperty(PropertyName = "resultType", Required = Required.Always)]
    public ResultType Type { get; set; }

    [JsonProperty(PropertyName = "dataType", Required = Required.Always)]
    public string DataType { get; set; }

    [JsonProperty(PropertyName = "value", Required = Required.Always)]
    public string Value { get; set; }

    [JsonProperty(PropertyName = "units", Required = Required.AllowNull)]
    public string Units { get; set; }

    [JsonProperty(PropertyName = "referenceRange", Required = Required.AllowNull)]
    public string ReferenceRange { get; set; }

    [JsonProperty(PropertyName = "abnormalFlag", Required = Required.AllowNull)]
    public string AbnormalFlag { get; set; }

    [JsonProperty(PropertyName = "status", Required = Required.AllowNull)]
    public string Status { get; set; }

    [JsonProperty(PropertyName = "observationDateTime", Required = Required.AllowNull)]
    public DateTimeOffset? ObservationDateTime { get; set; }

    [JsonProperty(PropertyName = "childResultList", Required = Required.AllowNull)]
    public IList<Result> ChildResultList { get; set; }

  }
}
