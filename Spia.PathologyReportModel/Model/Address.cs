using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Spia.PathologyReportModel.Model
{
  public class Address
  {
    [JsonProperty(PropertyName = "lineOne", Required = Required.AllowNull)]
    public string LineOne { get; set; }
    [JsonProperty(PropertyName = "lineTwo", Required = Required.AllowNull)]
    public string LineTwo { get; set; }
    [JsonProperty(PropertyName = "suburb", Required = Required.AllowNull)]
    public string Suburb { get; set; }
    [JsonProperty(PropertyName = "postCode", Required = Required.AllowNull)]
    public string PostCode { get; set; }
    [JsonProperty(PropertyName = "city", Required = Required.AllowNull)]
    public string City { get; set; }
    [JsonProperty(PropertyName = "state", Required = Required.AllowNull)]
    public string State { get; set; }
    [JsonProperty(PropertyName = "country", Required = Required.AllowNull)]
    public string Country { get; set; }
    [JsonProperty(PropertyName = "typeCode", Required = Required.AllowNull)]
    public string TypeCode { get; set; }
  }
}
