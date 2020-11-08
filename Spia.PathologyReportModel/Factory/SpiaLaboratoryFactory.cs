using Spia.PathologyReportModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spia.PathologyReportModel.Factory
{
  public class SpiaLaboratoryFactory
  {
    public Laboratory GetPITUSLaboratory()
    {
      return new Laboratory()
      {
        FacilityName = "PITUS Pathology",
        FacilityCode = "PITUSPathology",
        NataSiteNumber = "9999",
        Hpio = "8003 6299 0001 9338",
        BusinessPhoneNumber = "+61 2 8356 5858",
        LaboratoryInformationSystemApplicationCode = "SuperLIS"
      };
    }
  }
}
