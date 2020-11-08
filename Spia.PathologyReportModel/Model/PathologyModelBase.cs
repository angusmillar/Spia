using Spia.PathologyReportModel.CustomAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Spia.PathologyReportModel.Model
{
  public abstract class PathologyModelBase
  {
    public virtual bool ConditionalValidationIsValid(out List<string> ErrorMessageList, string Path)
    {
      ErrorMessageList = new List<string>();
      return true;
    }
    public bool IsValid(object Instance, ScopeType scopeType, out List<string> ErrorMessageList, string Path = "")
    {
      ErrorMessageList = new List<string>();
      foreach (PropertyInfo prop in Instance.GetType().GetProperties())
      {
        var TempErrorMessageList = new List<string>();
        string PropertyRootPath =$"{Path}{prop.Name}";
        var attrList = prop.GetCustomAttributes(typeof(RequiredScopeAttribute), true);
        if (attrList.Count() == 0)
        {
          throw new ApplicationException($"The property {Path}{prop.Name} of type {Instance.GetType().Name} has no {nameof(RequiredScopeAttribute)}.");
        }
        else
        {
          var RequiredScopeAttributeOfScopeType = attrList.Cast<RequiredScopeAttribute>().ToList().SingleOrDefault(x => x.Scope == scopeType);
          if (RequiredScopeAttributeOfScopeType != null)
          {
            if (RequiredScopeAttributeOfScopeType.Required == RequiredType.Mandatory)
            {
              var type = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
              if (type == typeof(Laboratory))
              {
                Laboratory Laboratory = prop.GetValue(Instance, null) as Laboratory;                                                
                if (!this.IsValid(Laboratory, scopeType, out TempErrorMessageList, PropertyRootPath))
                {
                  ErrorMessageList.AddRange(TempErrorMessageList);
                }
                if (!Laboratory.ConditionalValidationIsValid(out TempErrorMessageList, PropertyRootPath))
                {
                  ErrorMessageList.AddRange(TempErrorMessageList);
                  TempErrorMessageList.Clear();
                }
              }
              else if (type == typeof(Address))
              {
                Address Address = prop.GetValue(Instance, null) as Address;                
                if (!this.IsValid(Address, scopeType, out TempErrorMessageList, PropertyRootPath))
                {
                  ErrorMessageList.AddRange(TempErrorMessageList);
                }
                if (!Address.ConditionalValidationIsValid(out TempErrorMessageList, PropertyRootPath))
                {
                  ErrorMessageList.AddRange(TempErrorMessageList);
                  TempErrorMessageList.Clear();
                }                
              }
              else if (type == typeof(Code))
              {
                Code Code = prop.GetValue(Instance, null) as Code;                
                if (!this.IsValid(Code, scopeType, out TempErrorMessageList, PropertyRootPath))
                {
                  ErrorMessageList.AddRange(TempErrorMessageList);
                }
                if (!Code.ConditionalValidationIsValid(out TempErrorMessageList, PropertyRootPath))
                {
                  ErrorMessageList.AddRange(TempErrorMessageList);
                  TempErrorMessageList.Clear();
                }                
              }
              else if (type == typeof(Identifier))
              {
                Identifier Identifier = prop.GetValue(Instance, null) as Identifier;
                if (!this.IsValid(Identifier, scopeType, out TempErrorMessageList, PropertyRootPath))
                {
                  ErrorMessageList.AddRange(TempErrorMessageList);
                  TempErrorMessageList.Clear();
                }
                if (!Identifier.ConditionalValidationIsValid(out TempErrorMessageList, PropertyRootPath))
                {
                  ErrorMessageList.AddRange(TempErrorMessageList);
                  TempErrorMessageList.Clear();
                }              
              }
              else if (type == typeof(Name))
              {
                Name Name = prop.GetValue(Instance, null) as Name;                
                if (!this.IsValid(Name, scopeType, out TempErrorMessageList, PropertyRootPath))
                {
                  ErrorMessageList.AddRange(TempErrorMessageList);
                }
                if (!Name.ConditionalValidationIsValid(out TempErrorMessageList, PropertyRootPath))
                {
                  ErrorMessageList.AddRange(TempErrorMessageList);
                  TempErrorMessageList.Clear();
                }                
              }
              else if (type == typeof(Organisation))
              {
                Organisation Organisation = prop.GetValue(Instance, null) as Organisation;                
                if (!this.IsValid(Organisation, scopeType, out TempErrorMessageList, PropertyRootPath))
                {
                  ErrorMessageList.AddRange(TempErrorMessageList);
                }
                if (!Organisation.ConditionalValidationIsValid(out TempErrorMessageList, PropertyRootPath))
                {
                  ErrorMessageList.AddRange(TempErrorMessageList);
                  TempErrorMessageList.Clear();
                }
              }
              else if (type == typeof(Panel))
              {
                Panel Panel = prop.GetValue(Instance, null) as Panel;                
                if (!this.IsValid(Panel, scopeType, out TempErrorMessageList, PropertyRootPath))
                {
                  ErrorMessageList.AddRange(TempErrorMessageList);
                }
                if (!Panel.ConditionalValidationIsValid(out TempErrorMessageList, PropertyRootPath))
                {
                  ErrorMessageList.AddRange(TempErrorMessageList);
                  TempErrorMessageList.Clear();
                }
              }
              else if (type == typeof(PathologyReport))
              {
                PathologyReport PathologyReport = prop.GetValue(Instance, null) as PathologyReport;
                if (!this.IsValid(PathologyReport, scopeType, out TempErrorMessageList, PropertyRootPath))
                {
                  ErrorMessageList.AddRange(TempErrorMessageList);
                }
                if (!PathologyReport.ConditionalValidationIsValid(out TempErrorMessageList, PropertyRootPath))
                {
                  ErrorMessageList.AddRange(TempErrorMessageList);
                  TempErrorMessageList.Clear();
                }
              }
              else if (type == typeof(PathologyReportContainer))
              {
                PathologyReportContainer PathologyReportContainer = prop.GetValue(Instance, null) as PathologyReportContainer;
                if (!this.IsValid(PathologyReportContainer, scopeType, out TempErrorMessageList, PropertyRootPath))
                {
                  ErrorMessageList.AddRange(TempErrorMessageList);
                }
                if (!PathologyReportContainer.ConditionalValidationIsValid(out TempErrorMessageList, PropertyRootPath))
                {
                  ErrorMessageList.AddRange(TempErrorMessageList);
                  TempErrorMessageList.Clear();
                }
              }
              else if (type == typeof(Patient))
              {
                Patient Patient = prop.GetValue(Instance, null) as Patient;
                if (!this.IsValid(Patient, scopeType, out TempErrorMessageList, PropertyRootPath))
                {
                  ErrorMessageList.AddRange(TempErrorMessageList);
                }
                if (!Patient.ConditionalValidationIsValid(out TempErrorMessageList, PropertyRootPath))
                {
                  ErrorMessageList.AddRange(TempErrorMessageList);
                  TempErrorMessageList.Clear();
                }
              }
              else if (type == typeof(Provider))
              {
                Provider Provider = prop.GetValue(Instance, null) as Provider;
                if (!this.IsValid(Provider, scopeType, out TempErrorMessageList, PropertyRootPath))
                {
                  ErrorMessageList.AddRange(TempErrorMessageList);
                }
                if (!Provider.ConditionalValidationIsValid(out TempErrorMessageList, PropertyRootPath))
                {
                  ErrorMessageList.AddRange(TempErrorMessageList);
                  TempErrorMessageList.Clear();
                }
              }
              else if (type == typeof(Report))
              {
                Report Report = prop.GetValue(Instance, null) as Report;
                if (!this.IsValid(Report, scopeType, out TempErrorMessageList, PropertyRootPath))
                {
                  ErrorMessageList.AddRange(TempErrorMessageList);
                }
                if (!Report.ConditionalValidationIsValid(out TempErrorMessageList, PropertyRootPath))
                {
                  ErrorMessageList.AddRange(TempErrorMessageList);
                  TempErrorMessageList.Clear();
                }
              }
              else if (type == typeof(ReportType))
              {
                ReportType ReportType = prop.GetValue(Instance, null) as ReportType;
                if (!this.IsValid(ReportType, scopeType, out TempErrorMessageList, PropertyRootPath))
                {
                  ErrorMessageList.AddRange(TempErrorMessageList);
                }
                if (!ReportType.ConditionalValidationIsValid(out TempErrorMessageList, PropertyRootPath))
                {
                  ErrorMessageList.AddRange(TempErrorMessageList);
                  TempErrorMessageList.Clear();
                }
              }
              else if (type == typeof(Request))
              {
                Request Request = prop.GetValue(Instance, null) as Request;
                if (!this.IsValid(Request, scopeType, out TempErrorMessageList, PropertyRootPath))
                {
                  ErrorMessageList.AddRange(TempErrorMessageList);
                }
                if (!Request.ConditionalValidationIsValid(out TempErrorMessageList, PropertyRootPath))
                {
                  ErrorMessageList.AddRange(TempErrorMessageList);
                  TempErrorMessageList.Clear();
                }
              }
              else if (type == typeof(Result))
              {
                Result Result = prop.GetValue(Instance, null) as Result;
                if (!this.IsValid(Result, scopeType, out TempErrorMessageList, PropertyRootPath))
                {
                  ErrorMessageList.AddRange(TempErrorMessageList);
                }
                if (!Result.ConditionalValidationIsValid(out TempErrorMessageList, PropertyRootPath))
                {
                  ErrorMessageList.AddRange(TempErrorMessageList);
                  TempErrorMessageList.Clear();
                }
              }
              else if (type == typeof(ResultType))
              {
                ResultType ResultType = prop.GetValue(Instance, null) as ResultType;
                if (!this.IsValid(ResultType, scopeType, out TempErrorMessageList, PropertyRootPath))
                {
                  ErrorMessageList.AddRange(TempErrorMessageList);
                }
                if (!ResultType.ConditionalValidationIsValid(out TempErrorMessageList, PropertyRootPath))
                {
                  ErrorMessageList.AddRange(TempErrorMessageList);
                  TempErrorMessageList.Clear();
                }
              }
              else if (type == typeof(string))
              {
                string Value = prop.GetValue(Instance, null) as string;
                if (string.IsNullOrWhiteSpace(Value))
                {
                  ErrorMessageList.Add($"{Path}{prop.Name} is {RequiredScopeAttributeOfScopeType.Required.ToString()} for {scopeType.ToString()}");
                  return false;
                }
              }
              else if (type == typeof(DateTime))
              {
                string DateTimeString = prop.GetValue(Instance, null).ToString();
                if (string.IsNullOrWhiteSpace(DateTimeString))
                {
                  ErrorMessageList.Add($"{Path}{prop.Name} is {RequiredScopeAttributeOfScopeType.Required.ToString()} for {scopeType.ToString()}");
                  return false;
                }               
              }
              else if (type == typeof(DateTimeOffset))
              {
                string DateTimeString = prop.GetValue(Instance, null).ToString();
                if (string.IsNullOrWhiteSpace(DateTimeString))
                {
                  ErrorMessageList.Add($"{Path}{prop.Name} is {RequiredScopeAttributeOfScopeType.Required.ToString()} for {scopeType.ToString()}");
                  return false;
                }
              }
              else if (type.IsEnum)
              {
                string EnumValueString = prop.GetValue(Instance, null).ToString();
                if (string.IsNullOrWhiteSpace(EnumValueString))
                {
                  ErrorMessageList.Add($"{Path}{prop.Name} is {RequiredScopeAttributeOfScopeType.Required.ToString()} for {scopeType.ToString()}");
                  return false;
                }
              }
              else if (type.GetTypeInfo().GenericTypeArguments[0] == typeof(Address))
              {
                List<Address> AddressList = prop.GetValue(Instance, null) as List<Address>;
                for (int i = 0; i < AddressList.Count; i++)
                {
                  if (!this.IsValid(AddressList[i], scopeType, out TempErrorMessageList, $"{Path}{prop.Name}[{i.ToString()}]."))
                  {
                    ErrorMessageList.AddRange(TempErrorMessageList);
                    TempErrorMessageList.Clear();
                  }
                  if (!AddressList[i].ConditionalValidationIsValid(out TempErrorMessageList, $"{PropertyRootPath}[{i.ToString()}]."))
                  {
                    ErrorMessageList.AddRange(TempErrorMessageList);
                    TempErrorMessageList.Clear();
                  }
                }                
              }
              else if (type.GetTypeInfo().GenericTypeArguments[0] == typeof(Identifier))
              {
                List<Identifier> IdentifierList = prop.GetValue(Instance, null) as List<Identifier>;
                for (int i = 0; i < IdentifierList.Count; i++)
                {
                  if (!this.IsValid(IdentifierList[i], scopeType, out ErrorMessageList, $"{PropertyRootPath}[{i.ToString()}]."))
                  {
                    ErrorMessageList.AddRange(TempErrorMessageList);
                    TempErrorMessageList.Clear();
                  }
                  if (!IdentifierList[i].ConditionalValidationIsValid(out TempErrorMessageList, $"{PropertyRootPath}[{i.ToString()}]."))
                  {
                    ErrorMessageList.AddRange(TempErrorMessageList);
                    TempErrorMessageList.Clear();
                  }
                }
              }
              else if (type.GetTypeInfo().GenericTypeArguments[0] == typeof(Report))
              {
                List<Report> ReportList = prop.GetValue(Instance, null) as List<Report>;
                for (int i = 0; i < ReportList.Count; i++)
                {
                  if (!this.IsValid(ReportList[i], scopeType, out ErrorMessageList, $"{Path}{prop.Name}[{i.ToString()}]."))
                  {
                    ErrorMessageList.AddRange(TempErrorMessageList);
                    TempErrorMessageList.Clear();
                  }
                  if (!ReportList[i].ConditionalValidationIsValid(out TempErrorMessageList, $"{PropertyRootPath}[{i.ToString()}]."))
                  {
                    ErrorMessageList.AddRange(TempErrorMessageList);
                    TempErrorMessageList.Clear();
                  }
                }
              }
              else if (type.GetTypeInfo().GenericTypeArguments[0] == typeof(Result))
              {
                List<Result> ResultList = prop.GetValue(Instance, null) as List<Result>;
                for (int i = 0; i < ResultList.Count; i++)
                {
                  if (!this.IsValid(ResultList[i], scopeType, out ErrorMessageList, $"{Path}{prop.Name}[{i.ToString()}]."))
                  {
                    ErrorMessageList.AddRange(TempErrorMessageList);
                    TempErrorMessageList.Clear();
                  }
                  if (!ResultList[i].ConditionalValidationIsValid(out TempErrorMessageList, $"{PropertyRootPath}[{i.ToString()}]."))
                  {
                    ErrorMessageList.AddRange(TempErrorMessageList);
                    TempErrorMessageList.Clear();
                  }
                }
              }
              else
              {
                throw new ApplicationException($"Type of {type.Name} has not validation routine.");
              }
            }
          }
          else
          {
            throw new ApplicationException($"The property {Path}{prop.Name} of type {Instance.GetType().Name} has no {nameof(RequiredScopeAttribute)} of the {nameof(ScopeType)} of {scopeType.ToString()}.");
          }
        }
      }
      return ErrorMessageList.Count == 0;
    }

    
  }
}
