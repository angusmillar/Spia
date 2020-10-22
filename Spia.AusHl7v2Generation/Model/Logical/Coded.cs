using System;
using System.Collections.Generic;
using System.Text;

namespace Spia.AusHl7v2Generation.Model.Logical
{
  public class Coded
  {
    public Coded(string value, string description, string system)
    {
      Value = value;
      Description = description;
      System = system;
    }

    public string Value { get; set; }
    public string Description { get; set; }
    public string System { get; set; }
  }
}
