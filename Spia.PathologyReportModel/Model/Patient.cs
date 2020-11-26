using Newtonsoft.Json;
using Spia.PathologyReportModel.Converters;
using Spia.PathologyReportModel.CustomAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spia.PathologyReportModel.Model
{
  
  public class Patient : PathologyModelBase
  {
    [JsonProperty(PropertyName = "name", Required = Required.Always)]
    [RequiredScope(ScopeType.Hl7v2, RequiredType.Mandatory)]
    [RequiredScope(ScopeType.Cda, RequiredType.Mandatory)]
    [RequiredScope(ScopeType.Fhir, RequiredType.Mandatory)]
    public Name Name { get; set; }

    [JsonProperty(PropertyName = "dateOfBirth", Required = Required.Always)]
    [RequiredScope(ScopeType.Hl7v2, RequiredType.Mandatory)]
    [RequiredScope(ScopeType.Cda, RequiredType.Mandatory)]
    [RequiredScope(ScopeType.Fhir, RequiredType.Mandatory)]
    public DateTime DateOfBirth { get; set; }

    [JsonProperty(PropertyName = "gender", Required = Required.Always)]
    [JsonConverter(typeof(GenderTypeConverter))]
    [RequiredScope(ScopeType.Hl7v2, RequiredType.Mandatory)]
    [RequiredScope(ScopeType.Cda, RequiredType.Mandatory)]
    [RequiredScope(ScopeType.Fhir, RequiredType.Mandatory)]
    public GenderType Gender { get; set; }

    [JsonProperty(PropertyName = "addressList", Required = Required.Always)]
    [RequiredScope(ScopeType.Hl7v2, RequiredType.Mandatory)]
    [RequiredScope(ScopeType.Cda, RequiredType.Mandatory)]
    [RequiredScope(ScopeType.Fhir, RequiredType.Mandatory)]
    public IList<Address> AddressList { get; set; }

    [JsonProperty(PropertyName = "identifierList", Required = Required.Always)]
    [RequiredScope(ScopeType.Hl7v2, RequiredType.Mandatory)]
    [RequiredScope(ScopeType.Cda, RequiredType.Mandatory)]
    [RequiredScope(ScopeType.Fhir, RequiredType.Mandatory)]
    public List<Identifier> IdentifierList { get; set; }

    [JsonProperty(PropertyName = "homePhoneNumber", Required = Required.AllowNull)]
    [RequiredScope(ScopeType.Hl7v2, RequiredType.Optional)]
    [RequiredScope(ScopeType.Cda, RequiredType.Optional)]
    [RequiredScope(ScopeType.Fhir, RequiredType.Optional)]
    public string HomePhoneNumber { get; set; }

  }
}
