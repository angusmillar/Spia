using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spia.PathologyReportModel.Model
{
  public class Report
  {
    [JsonProperty(PropertyName = "performingLaboratory", Required = Required.Always)]
    public Laboratory PerformingLaboratory { get; set; }

    [JsonProperty(PropertyName = "reportId", Required = Required.Always)]
    public string  ReportId { get; set; }

    [JsonProperty(PropertyName = "collectionDateTime", Required = Required.Always)]
    public DateTimeOffset CollectionDateTime { get; set; }

    [JsonProperty(PropertyName = "specimenReceivedDateTime", Required = Required.Always)]
    public DateTimeOffset SpecimenReceivedDateTime { get; set; }

    [JsonProperty(PropertyName = "reportReleaseDateTime", Required = Required.Always)]
    public DateTimeOffset ReportReleaseDateTime { get; set; }

    [JsonProperty(PropertyName = "reportStatus", Required = Required.Always)]
    public Char ReportStatus { get; set; }

    [JsonProperty(PropertyName = "reportType", Required = Required.Always)]
    public ReportType ReportType { get; set; }

    [JsonProperty(PropertyName = "department", Required = Required.Always)]
    public string Department { get; set; }

    [JsonProperty(PropertyName = "reportingPathologist", Required = Required.Always)]
    public Provider ReportingPathologist { get; set; }

    [JsonProperty(PropertyName = "panel", Required = Required.Always)]
    public Panel Panel { get; set; }

  }
}
