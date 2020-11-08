using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spia.PathologyReportModel.CustomAttribute
{
  public enum ScopeType {Hl7v2, Cda, Fhir };
  public enum RequiredType { Mandatory, Optional, Conditional, NotRequired };

  [System.AttributeUsage(AttributeTargets.Property ,AllowMultiple = true, Inherited = false )]
  public class RequiredScopeAttribute : Attribute
  {
    public RequiredScopeAttribute(ScopeType scope, RequiredType required)
    {
      this.Scope = scope;
      this.Required = required;
    }

    public ScopeType Scope { get; }
    public RequiredType Required { get; }
  }
}
