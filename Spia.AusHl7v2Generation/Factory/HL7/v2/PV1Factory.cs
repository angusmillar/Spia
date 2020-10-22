using System;
using System.Collections.Generic;
using System.Text;
using PeterPiper.Hl7.V2.Model;

namespace Spia.AusHl7v2Generation.Factory.HL7.v2
{
  public static class PV1Factory
  {
    //PV1|1|O|Ward1^RoomE8^Bed10^ADHAHOSP&2.16.840.1.113883.19.5&ISO||||ABCB^AttendingOmar^Muhammad^^^Dr^^^SUPER-LIS~123456^AttendingOmar^Muhammad^^^Dr^^^ADHAHOSP~2304227F^AttendingOmar^Muhammad^^^Dr^^^AUSHICPR|HIJK^ReferringWilliams^Simon^^^Dr^^^SUPER-LIS~858595^ReferringWilliams^Simon^^^Dr^^^ADHAHOSP~2929016F^ReferringWilliams^Simon^^^Dr^^^AUSHICPR
    public static ISegment GetPV1(string patientClassCode)
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

      //IField AttendingDoctorLISId = Creator.Field();
      //AttendingDoctorLISId.Component(1).AsString = "ABCB";
      //AttendingDoctorLISId.Component(2).AsString = "AttendingOmar";
      //AttendingDoctorLISId.Component(3).AsString = "Muhammad";
      //AttendingDoctorLISId.Component(6).SubComponent(1).AsString = "Dr";
      //AttendingDoctorLISId.Component(9).SubComponent(2).AsString = "SUPER-LIS";      
      //PV1.Element(7).Add(AttendingDoctorLISId);

      //IField AttendingDoctorHospitalId = Creator.Field();
      //AttendingDoctorHospitalId.Component(1).AsString = "123456";
      //AttendingDoctorHospitalId.Component(2).AsString = "AttendingOmar";
      //AttendingDoctorHospitalId.Component(3).AsString = "Muhammad";
      //AttendingDoctorHospitalId.Component(6).SubComponent(1).AsString = "Dr";
      //AttendingDoctorHospitalId.Component(9).SubComponent(2).AsString = "ADHAHOSP";
      //PV1.Element(7).Add(AttendingDoctorHospitalId);

      //IField AttendingDoctorMedicareProviderNumber = Creator.Field();
      //AttendingDoctorMedicareProviderNumber.Component(1).AsString = "2304227F";
      //AttendingDoctorMedicareProviderNumber.Component(2).AsString = "AttendingOmar";
      //AttendingDoctorMedicareProviderNumber.Component(3).AsString = "Muhammad";
      //AttendingDoctorMedicareProviderNumber.Component(6).SubComponent(1).AsString = "Dr";
      //AttendingDoctorMedicareProviderNumber.Component(9).SubComponent(2).AsString = "AUSHICPR";
      //PV1.Element(7).Add(AttendingDoctorMedicareProviderNumber);

      ////2929016F^ReferringWilliams^Simon^^^Dr^^^AUSHICPR
      //IField ReferringDoctorLISId = Creator.Field();
      //ReferringDoctorLISId.Component(1).AsString = "HIJK";
      //ReferringDoctorLISId.Component(2).AsString = "ReferringWilliams";
      //ReferringDoctorLISId.Component(3).AsString = "Simon";
      //ReferringDoctorLISId.Component(6).SubComponent(1).AsString = "Dr";
      //ReferringDoctorLISId.Component(9).SubComponent(2).AsString = "SUPER-LIS";
      //PV1.Element(8).Add(ReferringDoctorLISId);

      //IField ReferringDoctorHospitalId = Creator.Field();
      //ReferringDoctorHospitalId.Component(1).AsString = "858595";
      //ReferringDoctorHospitalId.Component(2).AsString = "ReferringWilliams";
      //ReferringDoctorHospitalId.Component(3).AsString = "Simon";
      //ReferringDoctorHospitalId.Component(6).SubComponent(1).AsString = "Dr";
      //ReferringDoctorHospitalId.Component(9).SubComponent(2).AsString = "ADHAHOSP";
      //PV1.Element(8).Add(ReferringDoctorHospitalId);

      //IField ReferringDoctorMedicareProviderNumber = Creator.Field();
      //ReferringDoctorMedicareProviderNumber.Component(1).AsString = "2929016F";
      //ReferringDoctorMedicareProviderNumber.Component(2).AsString = "ReferringWilliams";
      //ReferringDoctorMedicareProviderNumber.Component(3).AsString = "Simon";
      //ReferringDoctorMedicareProviderNumber.Component(6).SubComponent(1).AsString = "Dr";
      //ReferringDoctorMedicareProviderNumber.Component(9).SubComponent(2).AsString = "AUSHICPR";
      //PV1.Element(8).Add(ReferringDoctorMedicareProviderNumber);

      return PV1;
    }
  }
}
