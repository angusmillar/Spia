using Newtonsoft.Json;
using Spia.PathologyReportModel.CustomAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spia.PathologyReportModel.Model
{
  public class Request : PathologyModelBase
  {    
    [JsonProperty(PropertyName = "requestingFacility", Required = Required.Always)]
    [RequiredScope(ScopeType.Hl7v2, RequiredType.Mandatory)]
    [RequiredScope(ScopeType.Cda, RequiredType.Mandatory)]
    [RequiredScope(ScopeType.Fhir, RequiredType.Mandatory)]
    public Organisation RequestingFacility { get; set; }

    [JsonProperty(PropertyName = "requestingApplication", Required = Required.Always)]
    [RequiredScope(ScopeType.Hl7v2, RequiredType.Mandatory)]
    [RequiredScope(ScopeType.Cda, RequiredType.Mandatory)]
    [RequiredScope(ScopeType.Fhir, RequiredType.Mandatory)]
    public string RequestingApplication { get; set; }

    [JsonProperty(PropertyName = "requestingProvider", Required = Required.Always)]
    [RequiredScope(ScopeType.Hl7v2, RequiredType.Mandatory)]
    [RequiredScope(ScopeType.Cda, RequiredType.Mandatory)]
    [RequiredScope(ScopeType.Fhir, RequiredType.Mandatory)]
    public Provider RequestingProvider { get; set; }

    [JsonProperty(PropertyName = "orderNumber", Required = Required.AllowNull)]
    [RequiredScope(ScopeType.Hl7v2, RequiredType.Mandatory)]
    [RequiredScope(ScopeType.Cda, RequiredType.Mandatory)]
    [RequiredScope(ScopeType.Fhir, RequiredType.Mandatory)]
    public string OrderNumber { get; set; }

    [JsonProperty(PropertyName = "requestedDateTime", Required = Required.Always)]
    [RequiredScope(ScopeType.Hl7v2, RequiredType.Mandatory)]
    [RequiredScope(ScopeType.Cda, RequiredType.Mandatory)]
    [RequiredScope(ScopeType.Fhir, RequiredType.Mandatory)]
    public DateTimeOffset RequestedDate { get; set; }

    [JsonProperty(PropertyName = "clinicalNotes", Required = Required.AllowNull)]
    [RequiredScope(ScopeType.Hl7v2, RequiredType.Optional)]
    [RequiredScope(ScopeType.Cda, RequiredType.Optional)]
    [RequiredScope(ScopeType.Fhir, RequiredType.Optional)]
    public string ClinicalNotes { get; set; }

    [JsonProperty(PropertyName = "copyToList", Required = Required.AllowNull)]
    [RequiredScope(ScopeType.Hl7v2, RequiredType.Optional)]
    [RequiredScope(ScopeType.Cda, RequiredType.Optional)]
    [RequiredScope(ScopeType.Fhir, RequiredType.Optional)]
    public IList<Provider> CopyToList { get; set; }

    [JsonProperty(PropertyName = "callBackPhoneNumber", Required = Required.AllowNull)]
    [RequiredScope(ScopeType.Hl7v2, RequiredType.Optional)]
    [RequiredScope(ScopeType.Cda, RequiredType.Optional)]
    [RequiredScope(ScopeType.Fhir, RequiredType.Optional)]
    public string CallBackPhoneNumber { get; set; }

  }
}
