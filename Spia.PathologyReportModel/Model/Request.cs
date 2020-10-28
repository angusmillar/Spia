using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spia.PathologyReportModel.Model
{
  public class Request
  {    
    [JsonProperty(PropertyName = "requestingFacility", Required = Required.Always)]
    public Organisation RequestingFacility { get; set; }

    [JsonProperty(PropertyName = "requestingApplication", Required = Required.Always)]
    public string RequestingApplication { get; set; }

    [JsonProperty(PropertyName = "requestingProvider", Required = Required.Always)]
    public Provider RequestingProvider { get; set; }

    [JsonProperty(PropertyName = "orderNumber", Required = Required.AllowNull)]
    public string OrderNumber { get; set; }

    [JsonProperty(PropertyName = "requestedDateTime", Required = Required.Always)]
    public DateTimeOffset RequestedDate { get; set; }

    [JsonProperty(PropertyName = "clinicalNotes", Required = Required.AllowNull)]
    public string ClinicalNotes { get; set; }

    [JsonProperty(PropertyName = "copyToList", Required = Required.AllowNull)]
    public IList<Provider> CopyToList { get; set; }

    [JsonProperty(PropertyName = "callBackPhoneNumber", Required = Required.AllowNull)]
    public string CallBackPhoneNumber { get; set; }

  }
}
