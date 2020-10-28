using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spia.PathologyReportModel.Model
{
  public class Code
  {
    [JsonProperty(PropertyName = "term", Required = Required.Always)]
    public string Term { get; set; }

    [JsonProperty(PropertyName = "description", Required = Required.Always)]
    public string Description { get; set; }
  }
}
