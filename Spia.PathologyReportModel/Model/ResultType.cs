using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spia.PathologyReportModel.Model
{
  public class ResultType
  {
    [JsonProperty(PropertyName = "lonic", Required = Required.AllowNull)]
    public Code Lonic { get; set; }

    [JsonProperty(PropertyName = "local", Required = Required.Always)]
    public Code Local { get; set; }
  }
}
