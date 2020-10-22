using Spia.AusHl7v2Generation.Model.Logical;
using PeterPiper.Hl7.V2.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Spia.AusHl7v2Generation.Factory.HL7.v2
{
  public static class HL7v2MessageFactory
  {
    public static IMessage GetMessage(PathologyDocument pathologyDocument, MessageMetadata messageMetadata)
    {
      if (pathologyDocument is null)
        throw new ArgumentNullException(nameof(pathologyDocument));

      if (messageMetadata is null)
        throw new ArgumentNullException(nameof(messageMetadata));

      var HL7 = Creator.Message(MSHFactory.GetMSH(messageMetadata));

      HL7.Add(PIDFactory.GetPID(pathologyDocument.Patient));   
      HL7.Add(PV1Factory.GetPV1(patientClassCode : "N"));

      foreach (var Report in pathologyDocument.ReportList)
      {       
        //HL7.Add(ORCFactory.GetORC(pathologyDocument.Request, Report.ReportIdentifier));
        HL7.Add(OBRFactory.GetOBR(pathologyDocument.Request, Report));        
        foreach (var Result in Report.ResultList)
        {
          foreach(var OBX in OBXFactory.GetOBXList(Result))
          {
            HL7.Add(OBX);
          }
        }
      }
      return HL7;
    }
  }
}
