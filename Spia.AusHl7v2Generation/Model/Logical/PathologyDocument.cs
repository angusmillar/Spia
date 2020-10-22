using System;
using System.Collections.Generic;
using System.Text;

namespace Spia.AusHl7v2Generation.Model.Logical
{
  public class PathologyDocument
  {
    public PathologyDocument(Patient patient, Request request)
    {
      Patient = patient;
      Request = request;
      ReportList = new List<Report>();
    }

    public Patient Patient { get; set; }
    public Request Request { get; set; }
    public List<Report> ReportList { get; }
  }
}
