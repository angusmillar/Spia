using Spia.PathologyReportModel.CustomAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Spia.PathologyReportModel.Model
{
  public abstract class PathologyModelBase
  {
    protected virtual bool IsValidConditionalValidation(ScopeType scopeType, List<string> ErrorMessageList, string Path)
    {      
      return true;
    }
    public bool IsValid(object Instance, ScopeType scopeType, List<string> ErrorMessageList, string Path = "")
    {
      string ErrorMessageTemplate = "{0} is {1} for {2} and must not be null or empty.";
      foreach (PropertyInfo prop in Instance.GetType().GetProperties())
      {        
        string PropertyRootPath =$"{Path}.{prop.Name}";
        if (PropertyRootPath.StartsWith("."))
        {
          PropertyRootPath = PropertyRootPath.Remove(0, 1);
        }
          var attrList = prop.GetCustomAttributes(typeof(RequiredScopeAttribute), true);
        if (attrList.Count() == 0)
        {
          throw new ApplicationException($"The property {PropertyRootPath} of type {Instance.GetType().Name} has no {nameof(RequiredScopeAttribute)}.");
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
                this.IsValid(Laboratory, scopeType, ErrorMessageList, PropertyRootPath);
                Laboratory.IsValidConditionalValidation(scopeType, ErrorMessageList, PropertyRootPath);
              }
              else if (type == typeof(Address))
              {
                Address Address = prop.GetValue(Instance, null) as Address;
                this.IsValid(Address, scopeType, ErrorMessageList, PropertyRootPath);
                Address.IsValidConditionalValidation(scopeType, ErrorMessageList, PropertyRootPath);
              }
              else if (type == typeof(Code))
              {
                Code Code = prop.GetValue(Instance, null) as Code;
                this.IsValid(Code, scopeType, ErrorMessageList, PropertyRootPath);
                Code.IsValidConditionalValidation(scopeType, ErrorMessageList, PropertyRootPath);
              }
              else if (type == typeof(Identifier))
              {
                Identifier Identifier = prop.GetValue(Instance, null) as Identifier;
                this.IsValid(Identifier, scopeType, ErrorMessageList, PropertyRootPath);
                Identifier.IsValidConditionalValidation(scopeType, ErrorMessageList, PropertyRootPath);
              }
              else if (type == typeof(Name))
              {
                Name Name = prop.GetValue(Instance, null) as Name;
                this.IsValid(Name, scopeType, ErrorMessageList, PropertyRootPath);
                Name.IsValidConditionalValidation(scopeType, ErrorMessageList, PropertyRootPath);
              }
              else if (type == typeof(Organisation))
              {
                Organisation Organisation = prop.GetValue(Instance, null) as Organisation;
                this.IsValid(Organisation, scopeType, ErrorMessageList, PropertyRootPath);
                Organisation.IsValidConditionalValidation(scopeType, ErrorMessageList, PropertyRootPath);
              }
              else if (type == typeof(Panel))
              {
                Panel Panel = prop.GetValue(Instance, null) as Panel;
                this.IsValid(Panel, scopeType, ErrorMessageList, PropertyRootPath);
                Panel.IsValidConditionalValidation(scopeType, ErrorMessageList, PropertyRootPath);
              }
              else if (type == typeof(PathologyReport))
              {
                PathologyReport PathologyReport = prop.GetValue(Instance, null) as PathologyReport;
                this.IsValid(PathologyReport, scopeType, ErrorMessageList, PropertyRootPath);
                PathologyReport.IsValidConditionalValidation(scopeType, ErrorMessageList, PropertyRootPath);
              }
              else if (type == typeof(PathologyReportContainer))
              {
                PathologyReportContainer PathologyReportContainer = prop.GetValue(Instance, null) as PathologyReportContainer;
                this.IsValid(PathologyReportContainer, scopeType, ErrorMessageList, PropertyRootPath);
                PathologyReportContainer.IsValidConditionalValidation(scopeType, ErrorMessageList, PropertyRootPath);
              }
              else if (type == typeof(Patient))
              {
                Patient Patient = prop.GetValue(Instance, null) as Patient;
                this.IsValid(Patient, scopeType, ErrorMessageList, PropertyRootPath);
                Patient.IsValidConditionalValidation(scopeType, ErrorMessageList, PropertyRootPath);
              }
              else if (type == typeof(Provider))
              {
                Provider Provider = prop.GetValue(Instance, null) as Provider;
                this.IsValid(Provider, scopeType, ErrorMessageList, PropertyRootPath);
                Provider.IsValidConditionalValidation(scopeType, ErrorMessageList, PropertyRootPath);
              }
              else if (type == typeof(Report))
              {
                Report Report = prop.GetValue(Instance, null) as Report;
                this.IsValid(Report, scopeType, ErrorMessageList, PropertyRootPath);
                Report.IsValidConditionalValidation(scopeType, ErrorMessageList, PropertyRootPath);
              }
              else if (type == typeof(ReportType))
              {
                ReportType ReportType = prop.GetValue(Instance, null) as ReportType;
                this.IsValid(ReportType, scopeType, ErrorMessageList, PropertyRootPath);
                ReportType.IsValidConditionalValidation(scopeType, ErrorMessageList, PropertyRootPath);
              }
              else if (type == typeof(Request))
              {
                Request Request = prop.GetValue(Instance, null) as Request;
                this.IsValid(Request, scopeType, ErrorMessageList, PropertyRootPath);
                Request.IsValidConditionalValidation(scopeType, ErrorMessageList, PropertyRootPath);
              }
              else if (type == typeof(Result))
              {
                Result Result = prop.GetValue(Instance, null) as Result;
                this.IsValid(Result, scopeType, ErrorMessageList, PropertyRootPath);
                Result.IsValidConditionalValidation(scopeType, ErrorMessageList, PropertyRootPath);
              }
              else if (type == typeof(ResultType))
              {
                ResultType ResultType = prop.GetValue(Instance, null) as ResultType;
                this.IsValid(ResultType, scopeType, ErrorMessageList, PropertyRootPath);
                ResultType.IsValidConditionalValidation(scopeType, ErrorMessageList, PropertyRootPath);
              }
              else if (type == typeof(string))
              {
                
                string Value = prop.GetValue(Instance, null) as string;
                if (string.IsNullOrWhiteSpace(Value))
                {
                  ErrorMessageList.Add(String.Format(ErrorMessageTemplate, PropertyRootPath, RequiredScopeAttributeOfScopeType.Required.ToString().ToLower(), scopeType.ToString()));                  
                }
              }
              else if (type == typeof(DateTime))
              {
                string DateTimeString = prop.GetValue(Instance, null).ToString();
                if (string.IsNullOrWhiteSpace(DateTimeString))
                {
                  ErrorMessageList.Add(String.Format(ErrorMessageTemplate, PropertyRootPath, RequiredScopeAttributeOfScopeType.Required.ToString().ToLower(), scopeType.ToString()));
                }               
              }
              else if (type == typeof(DateTimeOffset))
              {
                string DateTimeString = prop.GetValue(Instance, null).ToString();
                if (string.IsNullOrWhiteSpace(DateTimeString))
                {
                  ErrorMessageList.Add(String.Format(ErrorMessageTemplate, PropertyRootPath, RequiredScopeAttributeOfScopeType.Required.ToString().ToLower(), scopeType.ToString()));
                }
              }
              else if (type.IsEnum)
              {
                string EnumValueString = prop.GetValue(Instance, null).ToString();
                if (string.IsNullOrWhiteSpace(EnumValueString))
                {
                  ErrorMessageList.Add(String.Format(ErrorMessageTemplate, PropertyRootPath, RequiredScopeAttributeOfScopeType.Required.ToString().ToLower(), scopeType.ToString()));
                }
              }
              else if (type.GetTypeInfo().GenericTypeArguments[0] == typeof(Address))
              {
                List<Address> AddressList = prop.GetValue(Instance, null) as List<Address>;
                for (int i = 0; i < AddressList.Count; i++)
                {
                  string LoopPropertyRootPath = $"{PropertyRootPath}[{i.ToString()}]";
                  this.IsValid(AddressList[i], scopeType, ErrorMessageList, LoopPropertyRootPath);
                  AddressList[i].IsValidConditionalValidation(scopeType, ErrorMessageList, LoopPropertyRootPath);                  
                }                
              }
              else if (type.GetTypeInfo().GenericTypeArguments[0] == typeof(Identifier))
              {
                List<Identifier> IdentifierList = prop.GetValue(Instance, null) as List<Identifier>;
                for (int i = 0; i < IdentifierList.Count; i++)
                {
                  string LoopPropertyRootPath = $"{PropertyRootPath}[{i.ToString()}]";
                  this.IsValid(IdentifierList[i], scopeType, ErrorMessageList, LoopPropertyRootPath);
                  IdentifierList[i].IsValidConditionalValidation(scopeType, ErrorMessageList, LoopPropertyRootPath);                  
                }
              }
              else if (type.GetTypeInfo().GenericTypeArguments[0] == typeof(Report))
              {
                List<Report> ReportList = prop.GetValue(Instance, null) as List<Report>;
                for (int i = 0; i < ReportList.Count; i++)
                {
                  string LoopPropertyRootPath = $"{PropertyRootPath}[{i.ToString()}]";
                  this.IsValid(ReportList[i], scopeType, ErrorMessageList, LoopPropertyRootPath);
                  ReportList[i].IsValidConditionalValidation(scopeType, ErrorMessageList, LoopPropertyRootPath);                 
                }
              }
              else if (type.GetTypeInfo().GenericTypeArguments[0] == typeof(Result))
              {
                List<Result> ResultList = prop.GetValue(Instance, null) as List<Result>;
                for (int i = 0; i < ResultList.Count; i++)
                {
                  string LoopPropertyRootPath = $"{PropertyRootPath}[{i.ToString()}]";
                  this.IsValid(ResultList[i], scopeType, ErrorMessageList, LoopPropertyRootPath);
                  ResultList[i].IsValidConditionalValidation(scopeType, ErrorMessageList, LoopPropertyRootPath);                  
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
