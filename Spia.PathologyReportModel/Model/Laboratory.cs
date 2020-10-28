using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spia.PathologyReportModel.Model
{
  public class Laboratory
  {   
    [JsonProperty(PropertyName = "facilityCode", Required = Required.Always)]
    public string FacilityCode { get; set; }

    [JsonProperty(PropertyName = "nataSiteNumber", Required = Required.Always)]
    public string NataSiteNumber { get; set; }
  }
}
