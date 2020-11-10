using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spia.PathologyReportModel.Model;
namespace Spia.PathologyReportModel.Support
{
  public class StateTypeSupport : CodeEnumSupport<StateType>
  {   
    public StateTypeSupport() : base(PrimeDictionary(), "Australian states & territory codes") {}

    private static Dictionary<string, StateType> PrimeDictionary()
    {
      return new Dictionary<string, StateType>()
      {
        {"ACT",  StateType.ACT},
        {"NSW",  StateType.NSW},
        {"NT", StateType.NT},
        {"QLD",  StateType.QLD},
        {"SA", StateType.SA},
        {"TAZ", StateType.TAZ},
        {"VIC", StateType.VIC},
        {"WA", StateType.WA},
      };
    }    
  }
}
