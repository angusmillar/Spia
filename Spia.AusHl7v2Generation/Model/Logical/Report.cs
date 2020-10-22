using System;
using System.Collections.Generic;
using System.Text;

namespace Spia.AusHl7v2Generation.Model.Logical
{
  public class Report
  {
    public Report(EntityIdentifier reportIdentifier, 
      DateTimeOffset collectionDateTime, 
      DateTimeOffset specimenReceivedDateTime,
      DateTimeOffset reportReleaseDateTime, 
      CodedElement reportType, 
      char status, 
      string diagnosticServiceSectionId, 
      Provider reportingPathologist,
      HierarchicDesignator sendingApplication,
      HierarchicDesignator sendingFacility)
    {
      ReportIdentifier = reportIdentifier;
      CollectionDateTime = collectionDateTime;
      SpecimenReceivedDateTime = specimenReceivedDateTime;
      ReportReleaseDateTime = reportReleaseDateTime;
      ReportType = reportType;
      Status = status;
      DiagnosticServiceSectionId = diagnosticServiceSectionId;
      ReportingPathologist = reportingPathologist;
      SendingApplication = sendingApplication;
      SendingFacility = sendingFacility;
      ResultList = new List<Result>();
    }

    public EntityIdentifier ReportIdentifier { get; set; }
    public DateTimeOffset CollectionDateTime { get; set; }
    public DateTimeOffset SpecimenReceivedDateTime { get; set; }
    public DateTimeOffset ReportReleaseDateTime { get; set; }    
    public CodedElement ReportType { get; set; }
    public string DiagnosticServiceSectionId { get; set; }
    public char Status { get; set; }    
    public Provider ReportingPathologist { get; set; }
    public List<Result> ResultList { get; }
    public HierarchicDesignator SendingApplication { get; set; }
    public HierarchicDesignator SendingFacility { get; set; }    
  }
}
