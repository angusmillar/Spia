using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Spia.PathologyReportModel.CustomAttribute
{
  public static class RequiredScopeAttributeExtention
  {
    public static RequiredType? RequiredScope<T>(this T obj, Expression<Func<T, string>> value, ScopeType scopeType)
    {
      var memberExpression = value.Body as MemberExpression;
      var attrList = memberExpression.Member.GetCustomAttributes(typeof(RequiredScopeAttribute), true);
      if (attrList.Count() == 0)
      {
        return null;
      }
      else
      {
        var RequiredScopeAttributeOfScopeType = attrList.Cast<RequiredScopeAttribute>().ToList().SingleOrDefault(x => x.Scope == scopeType);
        if (RequiredScopeAttributeOfScopeType != null)
        {
          return RequiredScopeAttributeOfScopeType.Required;
        }
        else
        {
          return null;
        }
      }
    }

  }

}
