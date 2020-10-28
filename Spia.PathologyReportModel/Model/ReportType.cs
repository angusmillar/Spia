using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spia.PathologyReportModel.Model
{
  public class ReportType
  {
    [JsonProperty(PropertyName = "snomed", Required = Required.AllowNull)]
    public Code Snomed { get; set; }

    [JsonProperty(PropertyName = "local", Required = Required.Always)]
    public Code Local { get; set; }
  }
}
