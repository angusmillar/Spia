using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spia.AdhaCdaGeneration.Model
{
  public class Pathologist
  {
    public string Name { get; set; }
    public string LocalCode { get; set; }    
    
    private string _Hpii;
    public string Hpii
    {
      get { return _Hpii.Replace(" ", string.Empty); }
      set { _Hpii = value; }
    }
  }
}
