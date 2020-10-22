using System;
using System.Collections.Generic;
using System.Text;

namespace Spia.AusHl7v2Generation.Model.Logical
{
  public class Address
  {
    public string LineOne { get; set; }
    public string LineTwo { get; set; }
    public string Suburb { get; set; }
    public string PostCode { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Country { get; set; }
    public string TypeCode { get; set; }
  }
}
