using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spia.PathologyReportModel.Model;
namespace Spia.PathologyReportModel.Support
{
  public class ResultStatusTypeSupport : CodeEnumSupport<ResultStatusType>
  {   
    public ResultStatusTypeSupport() : base(PrimeDictionary(), "HL7AUSD-STD-OO-ADRM-2018.1 standard v2.4 HL7 Table 0123 - Result status for valid OBR Result Status entries") {}

    private static Dictionary<string, ResultStatusType> PrimeDictionary()
    {
      //This is the HL7 Table 0123 - Result status for valid OBR Result Status entries 
      //Taken from the  HL7AUSD-STD-OO-ADRM-2018.1 standard : https://confluence.hl7australia.com/display/OOADRM20181/4+Observation+Reporting#id-4ObservationReporting-4.4.1.25OBR-25Resultstatus(ID)00258
      return new Dictionary<string, ResultStatusType>()
      {
        {"C",  ResultStatusType.Correction},
        {"F",  ResultStatusType.Final},
        {"X", ResultStatusType.NoResultsAvailableOrderCanceled},
        {"P",  ResultStatusType.Preliminary},        
      };
    }    
  }
}
