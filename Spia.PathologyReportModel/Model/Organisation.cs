using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spia.PathologyReportModel.Model
{
  public class Organisation
  {
    [JsonProperty(PropertyName = "name", Required = Required.Always)]
    public string Name { get; set; }

    [JsonProperty(PropertyName = "identifier", Required = Required.Always)]
    public Identifier Identifier { get; set; }

  }
}
