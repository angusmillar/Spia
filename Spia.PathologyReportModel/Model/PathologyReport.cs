using Newtonsoft.Json;
using Spia.PathologyReportModel.CustomAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spia.PathologyReportModel.Model
{
  public class PathologyReport : PathologyModelBase
  {
    [JsonProperty(PropertyName = "performingLaboratory", Required = Required.Always)]
    [RequiredScope(ScopeType.Hl7v2, RequiredType.Mandatory)]
    [RequiredScope(ScopeType.Cda, RequiredType.Mandatory)]
    [RequiredScope(ScopeType.Fhir, RequiredType.Mandatory)]
    public Laboratory PerformingLaboratory { get; set; }

    [JsonProperty(PropertyName = "Patient", Required = Required.Always)]
    [RequiredScope(ScopeType.Hl7v2, RequiredType.Mandatory)]
    [RequiredScope(ScopeType.Cda, RequiredType.Mandatory)]
    [RequiredScope(ScopeType.Fhir, RequiredType.Mandatory)]
    public Patient Patient { get; set; }

    [JsonProperty(PropertyName = "Request", Required = Required.Always)]
    [RequiredScope(ScopeType.Hl7v2, RequiredType.Mandatory)]
    [RequiredScope(ScopeType.Cda, RequiredType.Mandatory)]
    [RequiredScope(ScopeType.Fhir, RequiredType.Mandatory)]
    public Request Request { get; set; }

    [JsonProperty(PropertyName = "pdfFileName", Required = Required.Always)]
    [RequiredScope(ScopeType.Hl7v2, RequiredType.Mandatory)]
    [RequiredScope(ScopeType.Cda, RequiredType.Mandatory)]
    [RequiredScope(ScopeType.Fhir, RequiredType.Mandatory)]
    public string PdfFileName { get; set; }

    [JsonProperty(PropertyName = "ReportList", Required = Required.Always)]
    [RequiredScope(ScopeType.Hl7v2, RequiredType.Mandatory)]
    [RequiredScope(ScopeType.Cda, RequiredType.Mandatory)]
    [RequiredScope(ScopeType.Fhir, RequiredType.Mandatory)]
    public IList<Report> ReportList { get; set; }

    public DateTimeOffset GetOldestReportReleaseDateTime()
    {
      IEnumerable<DateTimeOffset> ReportReleaseDateTimeList = this.ReportList.Select(x => x.ReportReleaseDateTime);
      return GetOldesDateTime(ReportReleaseDateTimeList);
    }

    private DateTimeOffset GetOldesDateTime(IEnumerable<DateTimeOffset> DateList)
    {
      if (DateList is null)
      {
        throw new ArgumentNullException(nameof(DateList));
      }

      if (DateList.Count() == 0)
      {
        throw new ApplicationException("An empty date list was provided to the method.");
      }

      DateTimeOffset OldestReportReleaseDate = DateTimeOffset.MinValue;
      foreach (var ThisReportsReleaseDate in DateList)
      {
        if (OldestReportReleaseDate < ThisReportsReleaseDate)
        {
          OldestReportReleaseDate = ThisReportsReleaseDate;
        }
      }
      return OldestReportReleaseDate;
    }
  }
}
