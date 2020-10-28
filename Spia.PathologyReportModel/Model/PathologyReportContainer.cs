using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spia.PathologyReportModel.Model
{
  public class PathologyReportContainer
  {
    [JsonProperty(PropertyName = "pathologyReport", Required = Required.Always)]
    public PathologyReport PathologyReport { get; set; }

  }
}
