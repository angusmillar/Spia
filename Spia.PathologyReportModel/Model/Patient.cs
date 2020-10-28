using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spia.PathologyReportModel.Model
{
  public class Patient
  {
    [JsonProperty(PropertyName = "name", Required = Required.Always)]
    public Name Name { get; set; }

    [JsonProperty(PropertyName = "dateOfBirth", Required = Required.Always)]
    public DateTime DateOfBirth { get; set; }

    [JsonProperty(PropertyName = "gender", Required = Required.Always)]
    public string Gender { get; set; }

    [JsonProperty(PropertyName = "addressList", Required = Required.Always)]
    public IList<Address> AddressList { get; set; }

    [JsonProperty(PropertyName = "identifierList", Required = Required.Always)]
    public List<Identifier> IdentifierList { get; set; }

    [JsonProperty(PropertyName = "homePhoneNumber", Required = Required.Always)]
    public string HomePhoneNumber { get; set; }

  }
}
