using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spia.PathologyReportModel.Model;

namespace Spia.PathologyReportModel
{
  public class JsonFileReader
  {
    public PathologyReportContainer ReadPathologyReports(string FilePath)
    {
      if (!File.Exists(FilePath))
        throw new FileNotFoundException($"Unable to locate file at : {FilePath}");

      using (StreamReader file = File.OpenText(FilePath))
      {

        JsonSerializer serializer = new JsonSerializer();
        try
        {
          PathologyReportContainer PathologyReports = (PathologyReportContainer)serializer.Deserialize(file, typeof(PathologyReportContainer));
          return PathologyReports;
        } 
        catch(Exception exec)
        {
          string msg = exec.Message;
          return null;
        }
        
      }
    }
  }
}
