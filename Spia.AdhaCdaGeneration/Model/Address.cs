using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spia.AdhaCdaGeneration.Model
{

  public class Address
  {
    public string LineOne { get; set; }
    public string LineTwo { get; set; }
    public string Suburb { get; set; }
    public string PostCode { get; set; }
    public StateType State { get; set; }

    public enum StateType { WA, NT, QLD, ACT, NSW, VIC, SA, TAZ};
  }
}
