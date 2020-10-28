using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spia.PathologyReportModel.Model
{
  public class PathologyReport
  {    
    [JsonProperty(PropertyName = "Patient", Required = Required.Always)]
    public Patient Patient { get; set; }

    [JsonProperty(PropertyName = "Request", Required = Required.Always)]
    public Request Request { get; set; }

    [JsonProperty(PropertyName = "pdfFileName", Required = Required.Always)]
    public string PdfFileName { get; set; }

    [JsonProperty(PropertyName = "ReportList", Required = Required.Always)]
    public IList<Report> ReportList { get; set; }
  }
}
