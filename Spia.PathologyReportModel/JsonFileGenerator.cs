using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Spia.PathologyReportModel.Model;

namespace Spia.PathologyReportModel
{
  public class JsonFileGenerator
  {
    public void Write(PathologyReportContainer PathologyReports, string OutputFilePath)
    {      
      string jsonString = JsonConvert.SerializeObject(PathologyReports, Formatting.Indented);
      File.WriteAllText(OutputFilePath, jsonString);
    }
  }
}
