using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spia.PathologyReportModel.Model
{
  public class Panel
  {
    [JsonProperty(PropertyName = "resultList", Required = Required.Always)]
    public IList<Result> ResultList { get; set; }

    

  }
}
