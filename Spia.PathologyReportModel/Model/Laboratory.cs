using Newtonsoft.Json;
using Spia.PathologyReportModel.CustomAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Spia.PathologyReportModel.Model
{
  public class Laboratory : PathologyModelBase
  {
    
    [JsonProperty(PropertyName = "facilityName", Required = Required.Always)]
    [RequiredScope(ScopeType.Hl7v2, RequiredType.NotRequired)]
    [RequiredScope(ScopeType.Cda, RequiredType.Mandatory)]
    [RequiredScope(ScopeType.Fhir, RequiredType.NotRequired)]
    public string FacilityName { get; set; }

    [JsonProperty(PropertyName = "facilityCode", Required = Required.Always)]
    [RequiredScope(ScopeType.Hl7v2, RequiredType.Mandatory)]
    [RequiredScope(ScopeType.Cda, RequiredType.NotRequired)]
    [RequiredScope(ScopeType.Fhir, RequiredType.NotRequired)]
    public string FacilityCode { get; set; }

    [JsonProperty(PropertyName = "nataSiteNumber", Required = Required.Always)]
    [RequiredScope(ScopeType.Hl7v2, RequiredType.Mandatory)]
    [RequiredScope(ScopeType.Cda, RequiredType.NotRequired)]
    [RequiredScope(ScopeType.Fhir, RequiredType.NotRequired)]
    public string NataSiteNumber { get; set; }

    [JsonProperty(PropertyName = "hpi-o", Required = Required.AllowNull)]
    [RequiredScope(ScopeType.Hl7v2, RequiredType.NotRequired)]
    [RequiredScope(ScopeType.Cda, RequiredType.NotRequired)]
    [RequiredScope(ScopeType.Fhir, RequiredType.NotRequired)]
    public string Hpio { get; set; }

    [JsonProperty(PropertyName = "businessPhoneNumber", Required = Required.AllowNull)]
    [RequiredScope(ScopeType.Hl7v2, RequiredType.NotRequired)]
    [RequiredScope(ScopeType.Cda, RequiredType.NotRequired)]
    [RequiredScope(ScopeType.Fhir, RequiredType.NotRequired)]
    public string BusinessPhoneNumber { get; set; }

    [JsonProperty(PropertyName = "laboratoryInformationSystemApplicationCode", Required = Required.Always)]
    [RequiredScope(ScopeType.Hl7v2, RequiredType.Mandatory)]
    [RequiredScope(ScopeType.Cda, RequiredType.NotRequired)]
    [RequiredScope(ScopeType.Fhir, RequiredType.NotRequired)]
    public string LaboratoryInformationSystemApplicationCode { get; set; }


    

    
  }
}
