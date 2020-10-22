using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spia.AdhaCdaPackageGeneration.Model
{
  public class ApproverPerson
  {
    public string FamilyName { get; set; }
    public string GivenName { get; set; }
    public string Title { get; set; }

    private string _Hpii;
    public string Hpii
    {
      get { return _Hpii.Replace(" ", string.Empty); }
      set { _Hpii = value; }
    }
  }
}
