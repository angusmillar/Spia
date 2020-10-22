using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spia.AdhaCdaGeneration.Model
{
  public class Organisation
  {
    private string _Hpio;

    public string Hpio
    {
      get { return _Hpio.Replace(" ", string.Empty); }
      set { _Hpio = value; }
    }

    public string Name { get; set; }
    public Address Address { get; set; }
    public string Phone { get; set; }
  }
}
