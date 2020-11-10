using Newtonsoft.Json;
using Spia.PathologyReportModel.CustomAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spia.PathologyReportModel.Model
{
  public class PathologyReport : PathologyModelBase
  {
    [JsonProperty(PropertyName = "performingLaboratory", Required = Required.Always)]
    [RequiredScope(ScopeType.Hl7v2, RequiredType.Mandatory)]
    [RequiredScope(ScopeType.Cda, RequiredType.Mandatory)]
    [RequiredScope(ScopeType.Fhir, RequiredType.Mandatory)]
    public Laboratory PerformingLaboratory { get; set; }

    [JsonProperty(PropertyName = "Patient", Required = Required.Always)]
    [RequiredScope(ScopeType.Hl7v2, RequiredType.Mandatory)]
    [RequiredScope(ScopeType.Cda, RequiredType.Mandatory)]
    [RequiredScope(ScopeType.Fhir, RequiredType.Mandatory)]
    public Patient Patient { get; set; }

    [JsonProperty(PropertyName = "Request", Required = Required.Always)]
    [RequiredScope(ScopeType.Hl7v2, RequiredType.Mandatory)]
    [RequiredScope(ScopeType.Cda, RequiredType.Mandatory)]
    [RequiredScope(ScopeType.Fhir, RequiredType.Mandatory)]
    public Request Request { get; set; }

    [JsonProperty(PropertyName = "pdfFileName", Required = Required.Always)]
    [RequiredScope(ScopeType.Hl7v2, RequiredType.Mandatory)]
    [RequiredScope(ScopeType.Cda, RequiredType.Mandatory)]
    [RequiredScope(ScopeType.Fhir, RequiredType.Mandatory)]
    public string PdfFileName { get; set; }

    [JsonProperty(PropertyName = "ReportList", Required = Required.Always)]
    [RequiredScope(ScopeType.Hl7v2, RequiredType.Mandatory)]
    [RequiredScope(ScopeType.Cda, RequiredType.Mandatory)]
    [RequiredScope(ScopeType.Fhir, RequiredType.Mandatory)]
    public IList<Report> ReportList { get; set; }

    public DateTimeOffset GetOldestReportReleaseDateTime()
    {
      IEnumerable<DateTimeOffset> ReportReleaseDateTimeList = this.ReportList.Select(x => x.ReportReleaseDateTime);
      return GetOldesDateTime(ReportReleaseDateTimeList);
    }

    public ResultStatusType GetRolledUpReportStatus()
    {
      var V2ReportStatusList = this.ReportList.Select(x => x.ReportStatus);
      if (V2ReportStatusList.Any(x => x == ResultStatusType.NoResultsAvailableOrderCanceled))
      {
        throw new ApplicationException($"Unable to roll-up a report status of {ResultStatusType.NoResultsAvailableOrderCanceled.ToString()}");
      }
      else if (V2ReportStatusList.Any(x => x == ResultStatusType.Correction))
      {
        return ResultStatusType.Correction;
      }
      else if (V2ReportStatusList.Any(x => x == ResultStatusType.Preliminary))
      {
        return ResultStatusType.Preliminary;
      }
      else if (V2ReportStatusList.Any(x => x == ResultStatusType.Final))
      {
        return ResultStatusType.Final;
      }
      else
      {
        throw new ApplicationException($"Unable to locate any report status to roll-up.");
      }
    }

    private DateTimeOffset GetOldesDateTime(IEnumerable<DateTimeOffset> DateList)
    {
      if (DateList is null)
      {
        throw new ArgumentNullException(nameof(DateList));
      }

      if (DateList.Count() == 0)
      {
        throw new ApplicationException("An empty date list was provided to the method.");
      }

      DateTimeOffset OldestReportReleaseDate = DateTimeOffset.MinValue;
      foreach (var ThisReportsReleaseDate in DateList)
      {
        if (OldestReportReleaseDate < ThisReportsReleaseDate)
        {
          OldestReportReleaseDate = ThisReportsReleaseDate;
        }
      }
      return OldestReportReleaseDate;
    }


    protected override bool IsValidConditionalValidation(ScopeType scopeType, List<string> ErrorMessageList, string Path)
    {
      if (scopeType == ScopeType.Cda)
      {
        //Get the first ReportingPathologist's HPII ad then check all the others also have that HPII in their identifier list
        Identifier FirstReportingPathologistHpii = this.ReportList.First().ReportingPathologist.IdentifierList.SingleOrDefault(x => x.Type == IdentifierType.HPII);
        if (FirstReportingPathologistHpii is object)
        {
          if (this.ReportList.Any(x => !x.ReportingPathologist.IdentifierList.Any(y => y.Type == IdentifierType.HPII && y.Value == FirstReportingPathologistHpii.Value)))
          {
            ErrorMessageList.Add($"For CDA creation all {Path}.ReportingPathologist must be the same individual for each report. This is validated by checking they all have the same HPI-I within their identifier list.");
          }
        }  
        if (this.ReportList.Any(x => !x.ReportingPathologist.IdentifierList.Any(y => y.Type == IdentifierType.HPII)))
        {
          ErrorMessageList.Add($"For CDA creation all {Path}.ReportingPathologist must have an identifier of type HPI-I.");
        }
      }
      return ErrorMessageList.Count() == 0; 
    }
  }
}
