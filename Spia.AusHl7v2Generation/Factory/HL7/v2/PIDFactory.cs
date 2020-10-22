using System;
using Spia.AusHl7v2Generation.Model.Logical;
using PeterPiper.Hl7.V2.Model;

namespace Spia.AusHl7v2Generation.Factory.HL7.v2
{
  public static class PIDFactory
  {    
    public static ISegment GetPID(Patient patient)
    {
      if (patient is null)
        throw new ArgumentNullException(nameof(patient));

      //PID|1||2142363^^^ADHAHOSP^MR~61405230941^^^AUSHIC^MC~WA123456B^^^AUSDVA^DVG~8003608833357361^^^AUSHIC^NI||Smith^John^Brian^^Mr^^L~Smith^Johno^Bry^^Mr^^M||194506241031|M|||Unit 1^111 ADHA Street^Brisbane^QLD^4000^AUS^H~Unit 2^222 ADHATwo Street^Brisbane^QLD^4000^AUS^B||^PRN^PH^^^^93235615|^WPN^CP^^^^0414778341
      ISegment PID = Creator.Segment("PID");
      PID.Field(1).AsString = "1";
      
      foreach(var Id in patient.IdentifierList)
      {
        IField Field = Creator.Field();
        Field.Component(1).AsString = Id.Value;
        Field.Component(4).AsString = Id.AssigingAuthority;
        Field.Component(5).AsString = Id.Type;
        PID.Element(3).Add(Field);
      }      

      foreach(var Name in patient.NameList)
      {
        IField Field = Creator.Field();
        Field.Component(1).AsString = Name.Family;
        Field.Component(2).AsString = Name.Given;
        Field.Component(3).AsString = Name.Middle ?? "";
        Field.Component(5).AsString = Name.Title ?? "";
        Field.Component(7).AsString = Name.TypeCode ?? "";
        PID.Element(5).Add(Field);
      }

      //DOB
      PID.Field(7).Convert.DateTime.SetDateTimeOffset(new DateTimeOffset(patient.DateOfBirth), false, PeterPiper.Hl7.V2.Support.Tools.DateTimeSupportTools.DateTimePrecision.Date);
      //Gender
      PID.Field(8).AsString = patient.Gender.ToString();

      foreach(var Add in patient.AddressList)
      {
        IField Field = Creator.Field();
        Field.Component(1).AsString = Add.LineOne ?? "";
        Field.Component(2).AsString = Add.LineTwo ?? "";
        Field.Component(3).AsString = Add.City ?? "";
        Field.Component(4).AsString = Add.State ?? "";
        Field.Component(5).AsString = Add.PostCode ?? "";
        Field.Component(6).AsString = Add.Country ?? "";
        Field.Component(7).AsString = Add.TypeCode ?? "";
        PID.Element(11).Add(Field);
      }

      //^PRN^PH^^^^93235615
      if (patient.HomePhone is object) 
      {
        IField HomePhone = Creator.Field();
        HomePhone.Component(2).AsString = "PRN";
        if (IsMobileNumber(patient.HomePhone))
        {
          HomePhone.Component(3).AsString = "CP";
        }
        else
        {
          HomePhone.Component(3).AsString = "PH";
        }
        HomePhone.Component(7).AsString = patient.HomePhone;
        PID.Element(13).Add(HomePhone);
      }

      //^WPN^CP^^^^0414778341
      if (patient.WorkPhone is object)
      {
        IField WorkPhone = Creator.Field();
        WorkPhone.Component(2).AsString = "WPN";
        if (IsMobileNumber(patient.WorkPhone))
        {
          WorkPhone.Component(3).AsString = "CP";
        }
        else
        {
          WorkPhone.Component(3).AsString = "PH";
        }
        WorkPhone.Component(3).AsString = "CP";
        WorkPhone.Component(7).AsString = patient.WorkPhone;
        PID.Element(14).Add(WorkPhone);
      }
      
      return PID;
    }

    private static bool IsMobileNumber(string value)
    {
      return value.StartsWith("+614", StringComparison.CurrentCulture) || value.StartsWith("614", StringComparison.CurrentCulture) || value.StartsWith("04", StringComparison.CurrentCulture);
    }
  }
}
