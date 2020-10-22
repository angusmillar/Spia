using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spia.AdhaFhirGeneration.Factory
{
  public class AdhaFhirFileGenerator
  {
    public delegate void LogEventMessage(string Message);

    public LogEventMessage LogEventMessageDelegate;
    public void Process(string RootHl7v2DirectoryPath, string outputPath)
    {
      this.Log("----------------------------------------------------------------------");
      this.Log("SPIA FHIR Bundles");

      string[] FilePathArray = Directory.GetFiles(RootHl7v2DirectoryPath, "*.hl7");

      DirectoryInfo FhirOutputDir = new DirectoryInfo(outputPath);
      foreach (FileInfo file in FhirOutputDir.GetFiles())
      {
        file.Delete();
      }

      PathologyFactory PathologyFactory = new PathologyFactory();
      foreach (string FilePath in FilePathArray)
      {
        string Resource = PathologyFactory.CreateJson(FilePath);
        FileInfo fi = new FileInfo(FilePath);
        File.WriteAllText(Path.Combine(FhirOutputDir.FullName, fi.Name.Replace(fi.Extension, ".json")), Resource);
        this.Log($"Generate: {fi.Name.Replace(fi.Extension, ".json")}");        
      }      
    }

    private void Log(string messagae)
    {
      if (this.LogEventMessageDelegate != null)
      {
        LogEventMessageDelegate.Invoke(messagae);
      }
    }
  }
}
