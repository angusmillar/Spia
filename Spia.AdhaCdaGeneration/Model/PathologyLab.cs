using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spia.AdhaCdaGeneration.Model
{
  public class PathologyLab : Organisation
  {
    public string NataSiteNumber { get; set; }
    public List<Pathologist> ReportingPathologistsList { get; set; }
  }
}
