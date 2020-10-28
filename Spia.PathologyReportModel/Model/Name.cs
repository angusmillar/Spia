using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spia.PathologyReportModel.Model
{
  public class Name
  {
    [JsonProperty(PropertyName = "family", Required = Required.Always)]
    public string Family { get; set; }
    [JsonProperty(PropertyName = "given", Required = Required.AllowNull)]
    public string Given { get; set; }
    [JsonProperty(PropertyName = "middle", Required = Required.AllowNull)]
    public string Middle { get; set; }
    [JsonProperty(PropertyName = "title", Required = Required.AllowNull)]
    public string Title { get; set; }

  }
}
