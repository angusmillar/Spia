using System;
using System.Collections.Generic;
using System.Text;

namespace Spia.AusHl7v2Generation.Model.Logical
{
  public class Name
  {
    public Name(string family, string given)
    {
      Family = family;
      Given = given;
    }

    public string Family { get; set; }
    public string Given { get; set; }
    public string Middle { get; set; }
    public string Title { get; set; }
    public string TypeCode { get; set; }
  }
}
