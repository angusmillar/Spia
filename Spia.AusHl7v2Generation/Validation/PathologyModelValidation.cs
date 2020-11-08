using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Spia.PathologyReportModel.CustomAttribute;
using Spia.PathologyReportModel.Model;

namespace Spia.AusHl7v2Generation.Validation
{
  public class PathologyModelValidation
  {
    public bool IsValid(PathologyReportContainer PathologyReportContainer, out List<string> ErrorList)
    {            
      return PathologyReportContainer.IsValid(PathologyReportContainer, ScopeType.Hl7v2, out ErrorList);      
    }
    
  }

}
