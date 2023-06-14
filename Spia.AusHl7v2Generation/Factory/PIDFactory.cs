using System;
using PeterPiper.Hl7.V2.Model;
using Spia.AusHl7v2Generation.Support;
using Spia.PathologyReportModel.Support;
using Spia.PathologyReportModel.Model;

namespace Spia.AusHl7v2Generation.Factory
{
  public static class PIDFactory
  {
    public static ISegment GetPID(Patient Patient)
    {
      if (Patient is null)
        throw new ArgumentNullException(nameof(Patient));

      //PID|1||2142363^^^ADHAHOSP^MR~61405230941^^^AUSHIC^MC~WA123456B^^^AUSDVA^DVG~8003608833357361^^^AUSHIC^NI||Smith^John^Brian^^Mr^^L~Smith^Johno^Bry^^Mr^^M||194506241031|M|||Unit 1^111 ADHA Street^Brisbane^QLD^4000^AUS^H~Unit 2^222 ADHATwo Street^Brisbane^QLD^4000^AUS^B||^PRN^PH^^^^93235615|^WPN^CP^^^^0414778341
      ISegment PID = Creator.Segment("PID");
      PID.Field(1).AsString = "1";

      foreach (var Id in Patient.IdentifierList)
      {
        var IdCode = HL7v2IdentifierSupport.GetIdentiferCode(Id);
        IField PatientIdField = Creator.Field();
        PatientIdField.Component(1).AsString = IdCode.Value.Replace(" ", string.Empty);
        PatientIdField.Component(4).AsString = IdCode.AssigingAuthority;
        PatientIdField.Component(5).AsString = IdCode.TypeCode;
        PID.Element(3).Add(PatientIdField);
      }

      
      IField Field = Creator.Field();
      Field.Component(1).AsString = Patient.Name.Family;
      Field.Component(2).AsString = Patient.Name.Given ?? "";
      Field.Component(3).AsString = Patient.Name.Middle ?? "";
      Field.Component(5).AsString = Patient.Name.Title ?? "";
      Field.Component(7).AsString = "L";
      PID.Element(5).Add(Field);
      

      //DOB
      PID.Field(7).Convert.DateTime.SetDateTimeOffset(new DateTimeOffset(Patient.DateOfBirth), false, PeterPiper.Hl7.V2.Support.Tools.DateTimeSupportTools.DateTimePrecision.Date);
      
      //Gender
      GenderTypeSupport GenderTypeSupport = new GenderTypeSupport();
      if (GenderTypeSupport.TryLookupByEnum(Patient.Gender, out string GenderCode))
      {        
        PID.Field(8).AsString = GenderCode;
      }
      else
      {
        throw new ApplicationException($"Unable to convert the {nameof(Patient.Gender)} of {Patient.Gender.ToString()} to a code for the HL7 PID-8 field.");
      }
      StateTypeSupport StateTypeSupport = new StateTypeSupport();

      AddressTypeSupport AddressTypeSupport = new AddressTypeSupport();
      foreach (var Add in Patient.AddressList)
      {
        
        IField AddressField = Creator.Field();
        AddressField.Component(1).AsString = Add.LineOne ?? "";
        AddressField.Component(2).AsString = Add.LineTwo ?? "";
        AddressField.Component(3).AsString = Add.City ?? "";
        if (StateTypeSupport.TryLookupByEnum(Add.State, out string StateCode))
        {
          AddressField.Component(4).AsString = StateCode;
        }
        else
        {
          throw new ApplicationException($"Unable to lookup the {nameof(StateType)} of {Add.State.ToString()}");
        }        
        AddressField.Component(5).AsString = Add.PostCode ?? "";
        AddressField.Component(6).AsString = Add.Country ?? "";
        if (AddressTypeSupport.TryLookupByEnum(Add.TypeCode, out string AddressTypeCode))
        {
          AddressField.Component(7).AsString = AddressTypeCode;
        }
        else
        {
          throw new ApplicationException($"Unable to locate the code required for the {nameof(AddressType)} enum of {Add.TypeCode.ToString()}");
        }
        
        PID.Element(11).Add(AddressField);
      }

      //^PRN^PH^^^^93235615
      if (!string.IsNullOrWhiteSpace(Patient.HomePhoneNumber))
      {
        IField HomePhone = Creator.Field();
        HomePhone.Component(2).AsString = "PRN";
        if (IsMobileNumber(Patient.HomePhoneNumber))
        {
          HomePhone.Component(3).AsString = "CP";
        }
        else
        {
          HomePhone.Component(3).AsString = "PH";
        }
        HomePhone.Component(7).AsString = Patient.HomePhoneNumber;
        PID.Element(13).Add(HomePhone);
      }

      //^WPN^CP^^^^0414778341
      //if (Patient.WorkPhone is object)
      //{
      //  IField WorkPhone = Creator.Field();
      //  WorkPhone.Component(2).AsString = "WPN";
      //  if (IsMobileNumber(patient.WorkPhone))
      //  {
      //    WorkPhone.Component(3).AsString = "CP";
      //  }
      //  else
      //  {
      //    WorkPhone.Component(3).AsString = "PH";
      //  }
      //  WorkPhone.Component(3).AsString = "CP";
      //  WorkPhone.Component(7).AsString = patient.WorkPhone;
      //  PID.Element(14).Add(WorkPhone);
      //}

      return PID;
    }
    
    private static bool IsMobileNumber(string value)
    {
      return value.StartsWith("+614", StringComparison.CurrentCulture) || value.StartsWith("614", StringComparison.CurrentCulture) || value.StartsWith("04", StringComparison.CurrentCulture);
    }
  }
}
