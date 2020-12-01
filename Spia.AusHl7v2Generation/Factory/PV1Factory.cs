using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using PeterPiper.Hl7.V2.Model;
using Spia.AusHl7v2Generation.Support;

namespace Spia.AusHl7v2Generation.Factory
{
  public static class PV1Factory
  {
    //PV1|1|O|Ward1^RoomE8^Bed10^ADHAHOSP&2.16.840.1.113883.19.5&ISO||||ABCB^AttendingOmar^Muhammad^^^Dr^^^SUPER-LIS~123456^AttendingOmar^Muhammad^^^Dr^^^ADHAHOSP~2304227F^AttendingOmar^Muhammad^^^Dr^^^AUSHICPR|HIJK^ReferringWilliams^Simon^^^Dr^^^SUPER-LIS~858595^ReferringWilliams^Simon^^^Dr^^^ADHAHOSP~2929016F^ReferringWilliams^Simon^^^Dr^^^AUSHICPR
    public static ISegment GetPV1(string patientClassCode, Spia.PathologyReportModel.Model.Provider referringProvider, string performingLabNataSiteNumber)
    {
      ISegment PV1 = Creator.Segment("PV1");
      PV1.Field(1).AsString = "1";
      PV1.Field(2).AsString = patientClassCode;

      //IField PatientLocation = Creator.Field();
      //PatientLocation.Component(1).AsString = "Ward1";
      //PatientLocation.Component(2).AsString = "RoomE8";
      //PatientLocation.Component(3).AsString = "Bed10";
      //PatientLocation.Component(4).SubComponent(1).AsString = "ADHAHOSP";
      //PatientLocation.Component(5).SubComponent(2).AsString = "2.16.840.1.113883.19.5";
      //PatientLocation.Component(5).SubComponent(3).AsString = "ISO";
      //PV1.Element(3).Add(PatientLocation);

      
      foreach (var Id in referringProvider.IdentifierList)
      {
        var IdentiferTypeInfo = HL7v2IdentifierSupport.GetIdentiferCode(Id, $"NATA{performingLabNataSiteNumber}");
        IField Field = Creator.Field();
        Field.Component(1).AsString = IdentiferTypeInfo.Value;
        Field.Component(2).AsString = referringProvider.Name.Family;
        Field.Component(3).AsString = referringProvider.Name.Given ?? "";
        Field.Component(6).AsString = referringProvider.Name.Title ?? "";
        Field.Component(9).AsString = IdentiferTypeInfo.AssigingAuthority;
        Field.Component(10).AsString = "L";
        Field.Component(13).AsString = IdentiferTypeInfo.TypeCode ?? "";

        //PV1-8: Definition: This field contains the referring physician information. Multiple names and identifiers for the same 
        //physician may be sent. The field sequences are not used to indicate multiple referring doctors. The legal name must be sent 
        //in the first sequence. If the legal name is not sent, then a repeat delimiter must be sent in the first sequence. Depending on 
        //local agreements, either the ID or the name may be absent from this field. Refer to User-defined Table 0010 - Physician ID for suggested values
        PV1.Element(8).Add(Field);

        //https://confluence.hl7australia.com/display/OOADRM20181/2+Patient+Administration+for+Pathology#id-2PatientAdministrationforPathology-PV1-92.2.2.9PV1-9Consultingdoctor(XCN)00139
        //PV1-9: In the Australian setting this field is used to identify the target provider for this message. 
        //A location specific ID is used and the field should not repeat as each message is unique for the target provider. 
        //Where available the Medicare provider number is used as this provides for a location specific identifier. Messages 
        //should be routed based on this field and only the first repeat is used.
        if (Id.Type == PathologyReportModel.Model.IdentifierType.MedicareProviderNumber)
        {
          PV1.Element(9).Add(Field.Clone());
        }               
      }

      return PV1;
    }
  }
}
