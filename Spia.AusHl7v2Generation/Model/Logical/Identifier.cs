using System;
using System.Collections.Generic;
using System.Text;

namespace Spia.AusHl7v2Generation.Model.Logical
{
  public class Identifier
  {
    public Identifier(string value)
    {
      Value = value;
    }

    public string Value { get; set; }
    public string Type { get; set; }
    public string AssigingAuthority { get; set; }
  }
}
