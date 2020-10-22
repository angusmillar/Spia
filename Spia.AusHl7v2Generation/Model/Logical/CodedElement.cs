using System;
using System.Collections.Generic;
using System.Text;

namespace Spia.AusHl7v2Generation.Model.Logical
{
  public class CodedElement
  {
    public CodedElement(Coded local, Coded international)
    {
      Local = local;
      International = international;
    }

    public Coded Local { get; set; }
    public Coded International { get; set; }   
  }
}
