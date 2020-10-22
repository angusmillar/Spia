using System;
using System.Collections.Generic;
using System.Text;

namespace Spia.AusHl7v2Generation.Model.Logical
{
  public class Provider
  {
    public Provider(Name name)
    {
      Name = name;
      IdentifierList = new List<Identifier>();
    }

    public Name Name { get; set; }
    public List<Identifier> IdentifierList { get; }
  }
}
