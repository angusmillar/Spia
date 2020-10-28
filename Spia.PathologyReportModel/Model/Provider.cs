using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spia.PathologyReportModel.Model
{
  public class Provider
  {
    [JsonProperty(PropertyName = "name", Required = Required.Always)]
    public Name Name { get; set; }

    [JsonProperty(PropertyName = "identifierList", Required = Required.Always)]
    public IList<Identifier> IdentifierList { get; set; }
  }
}
