using Newtonsoft.Json;
using Spia.PathologyReportModel.CustomAttribute;
using System;
using System.Collections.Generic;
using System.Text;

namespace Spia.PathologyReportModel.Model
{
  public class Address : PathologyModelBase
  {
    [JsonProperty(PropertyName = "lineOne", Required = Required.Always)]
    [RequiredScope(ScopeType.Hl7v2, RequiredType.Mandatory)]
    [RequiredScope(ScopeType.Cda, RequiredType.Mandatory)]
    [RequiredScope(ScopeType.Fhir, RequiredType.Mandatory)]
    public string LineOne { get; set; }

    [JsonProperty(PropertyName = "lineTwo", Required = Required.AllowNull)]
    [RequiredScope(ScopeType.Hl7v2, RequiredType.Optional)]
    [RequiredScope(ScopeType.Cda, RequiredType.Optional)]
    [RequiredScope(ScopeType.Fhir, RequiredType.Optional)]
    public string LineTwo { get; set; }

    [JsonProperty(PropertyName = "suburb", Required = Required.Always)]
    [RequiredScope(ScopeType.Hl7v2, RequiredType.Mandatory)]
    [RequiredScope(ScopeType.Cda, RequiredType.Mandatory)]
    [RequiredScope(ScopeType.Fhir, RequiredType.Mandatory)]
    public string Suburb { get; set; }

    [JsonProperty(PropertyName = "postCode", Required = Required.Always)]
    [RequiredScope(ScopeType.Hl7v2, RequiredType.Mandatory)]
    [RequiredScope(ScopeType.Cda, RequiredType.Mandatory)]
    [RequiredScope(ScopeType.Fhir, RequiredType.Mandatory)]
    public string PostCode { get; set; }

    [JsonProperty(PropertyName = "city", Required = Required.Always)]
    [RequiredScope(ScopeType.Hl7v2, RequiredType.Mandatory)]
    [RequiredScope(ScopeType.Cda, RequiredType.Mandatory)]
    [RequiredScope(ScopeType.Fhir, RequiredType.Mandatory)]
    public string City { get; set; }

    [JsonProperty(PropertyName = "state", Required = Required.Always)]
    [RequiredScope(ScopeType.Hl7v2, RequiredType.Mandatory)]
    [RequiredScope(ScopeType.Cda, RequiredType.Mandatory)]
    [RequiredScope(ScopeType.Fhir, RequiredType.Mandatory)]
    public string State { get; set; }

    [JsonProperty(PropertyName = "country", Required = Required.Always)]
    [RequiredScope(ScopeType.Hl7v2, RequiredType.Mandatory)]
    [RequiredScope(ScopeType.Cda, RequiredType.Mandatory)]
    [RequiredScope(ScopeType.Fhir, RequiredType.Mandatory)]
    public string Country { get; set; }

    [JsonProperty(PropertyName = "typeCode", Required = Required.Always)]
    [RequiredScope(ScopeType.Hl7v2, RequiredType.Mandatory)]
    [RequiredScope(ScopeType.Cda, RequiredType.Mandatory)]
    [RequiredScope(ScopeType.Fhir, RequiredType.Mandatory)]
    public string TypeCode { get; set; }
  }
}
