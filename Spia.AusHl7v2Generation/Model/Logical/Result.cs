using System;
using System.Collections.Generic;
using System.Text;

namespace Spia.AusHl7v2Generation.Model.Logical
{
  public class Result
  {
    public Result(string setId, string dataType, CodedElement resultType, PeterPiper.Hl7.V2.Model.IElement value, string status)
    {
      SetId = setId;
      DataType = dataType;
      ResultType = resultType;
      Value = value;
      Status = status;      
    }

    public string SetId { get; set; }
    public string DataType { get; set; }
    public CodedElement ResultType { get; set; }
    public string SubId { get; set; }
    public PeterPiper.Hl7.V2.Model.IElement Value { get; set; }
    public string Units { get; set; }
    public string ReferenceRange { get; set; }
    public string AbnormalFlag { get; set; }    
    public string Status { get; set; }
    public DateTimeOffset? ObservationDateTime { get; set; }
    public CodedElement ProducerId { get; set; }
    public List<Result> ResultList { get; set; }
  }
}
