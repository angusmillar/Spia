using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spia.PathologyReportModel.Model
{
  public enum IdentifierType { MedicareNumber, IHI, HPII, HPIO, MedicareProviderNumber, GUID, LocalToLab, MRN};
  public class Identifier
  {
    [JsonProperty(PropertyName = "value", Required = Required.Always)]
    public string Value { get; set; }

    [JsonProperty(PropertyName = "type", Required = Required.Always)]
    [JsonConverter(typeof(StringEnumConverter))]
    public IdentifierType Type { get; set; }

    [JsonProperty(PropertyName = "assigningAuthority", Required = Required.AllowNull)]
    public string AssigningAuthority { get; set; }
  }
}
