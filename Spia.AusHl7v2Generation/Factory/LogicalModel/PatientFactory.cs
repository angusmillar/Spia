using Spia.AusHl7v2Generation.Model.Logical;
using System;
using System.Collections.Generic;
using System.Text;

namespace Spia.AusHl7v2Generation.Factory.LogicalModel
{
  public static class PatientFactory
  {
    public enum PateintType { TessaCITIZEN, 
      GeorginaROSSLAND, GlennFERNIE, GloriaNELSON, LarissaFERNIE, GregoryBLACKCOMB, GeorgeWHITEWATER, HaydenNORQUAY
    };
    public static Patient GetPatient(PatientFactory.PateintType patientType)
    {
      var AusStandardHL7v2 = new AusStandardHL7v2();
      switch (patientType)
      {
        case PateintType.TessaCITIZEN:
          return TessaCITIZEN();
        case PateintType.GeorginaROSSLAND:
          return GeorginaROSSLAND();        
        case PateintType.GlennFERNIE:
          return GlennFERNIE();
        case PateintType.GloriaNELSON:
          return GloriaNELSON();
        case PateintType.LarissaFERNIE:
          return LarissaFERNIE();
        case PateintType.GregoryBLACKCOMB:
          return GregoryBLACKCOMB();
        case PateintType.GeorgeWHITEWATER:
          return GeorgeWHITEWATER();
        case PateintType.HaydenNORQUAY:
          return HaydenNORQUAY();
        default:
          throw new ApplicationException($"Unknown Patient {patientType.ToString()} ");          
      }
    }
    private static Patient TessaCITIZEN()
    {
      var NameList = new List<Name>() { new Name("CITIZEN", "Tessa")
      {
        Middle = "Paige",
        TypeCode = "L"
      }};

      var Gender = 'F';
      var DateOfBirth = new DateTime(1987, 06, 30);

      var Pat = new Patient(NameList, Gender, DateOfBirth)
      {
        HomePhone = "+61499811044",
        //WorkPhone = "+61499811045"
      };

      Pat.AddressList.Add(new Address()
      {
        LineOne = "123 Somewhere Place",
        LineTwo = "",
        Suburb = "Sunrise Beach",
        PostCode = "4571",
        City = "Brisbane",
        State = "QLD",
        Country = "AUS",
        TypeCode = "H"
      });

      Pat.IdentifierList.Add(GetMrn("999909999", "CoagulationThrombosisClinic"));      
      //Pat.IdentifierList.Add(GetMedicareNumberId("61405230941"));
      Pat.IdentifierList.Add(GetIhiId("8003606547651855"));

      //Pat.IdentifierList.Add(new Identifier("WA123456B")
      //{
      //  Type = "DVG",
      //  AssigingAuthority = "AUSDVA"
      //});

      return Pat;
    }
    private static Patient GeorginaROSSLAND()
    {
      var NameList = new List<Name>() { new Name("ROSSLAND", "Georgina")
      {
        Middle = "Noele",
        TypeCode = "L"
      }};

      var Gender = 'F';
      var DateOfBirth = new DateTime(1989, 07, 01);

      var Pat = new Patient(NameList, Gender, DateOfBirth)
      {
        HomePhone = "+61492811778",
        //WorkPhone = "+61499811045"
      };

      Pat.AddressList.Add(new Address()
      {
        LineOne = "123 Heston Court",
        LineTwo = "",
        Suburb = "Sunrise Beach",
        PostCode = "4571",
        City = "Brisbane",
        State = "QLD",
        Country = "AUS",
        TypeCode = "H"
      });

      Pat.IdentifierList.Add(GetMrn("95429417", "ACSH"));
      //Pat.IdentifierList.Add(GetMedicareNumberId("32233460748"));
      Pat.IdentifierList.Add(GetIhiId("8003603236836806"));

      return Pat;
    }    
    private static Patient GlennFERNIE()
    {
      var NameList = new List<Name>() { new Name("FERNIE", "Glenn")
      {
        Middle = "Neville",
        TypeCode = "L"
      }};

      var Gender = 'M';
      var DateOfBirth = new DateTime(1968, 05, 28);

      var Pat = new Patient(NameList, Gender, DateOfBirth)
      {
        HomePhone = "+61499811044",
        //WorkPhone = "+61499811045"
      };

      Pat.AddressList.Add(new Address()
      {
        LineOne = "456 James Terrace",
        LineTwo = "",
        Suburb = "Sunrise Beach",
        PostCode = "4571",
        City = "Brisbane",
        State = "QLD",
        Country = "AUS",
        TypeCode = "H"
      });

      Pat.IdentifierList.Add(GetMrn("719909917", "DermatologyClinic"));
      //Pat.IdentifierList.Add(GetMedicareNumberId("28125278357"));
      Pat.IdentifierList.Add(GetIhiId("8003609414426781"));

      return Pat;
    }
    private static Patient GloriaNELSON()
    {
      var NameList = new List<Name>() { new Name("NELSON", "Gloria")
      {
        Middle = "Nannette",
        TypeCode = "L"
      }};

      var Gender = 'F';
      var DateOfBirth = new DateTime(1949, 04, 17);

      var Pat = new Patient(NameList, Gender, DateOfBirth)
      {
        HomePhone = "+61491411377",
        //WorkPhone = "+61499811045"
      };

      Pat.AddressList.Add(new Address()
      {
        LineOne = "678 Oliver Close",
        LineTwo = "",
        Suburb = "Sunrise Beach",
        PostCode = "4571",
        City = "Brisbane",
        State = "QLD",
        Country = "AUS",
        TypeCode = "H"
      });

      Pat.IdentifierList.Add(GetMrn("997907777", "SunriseHospital"));
      //Pat.IdentifierList.Add(GetMedicareNumberId("50271648132"));
      Pat.IdentifierList.Add(GetIhiId("8003600463320066"));

      return Pat;
    }
    private static Patient LarissaFERNIE()
    {
      var NameList = new List<Name>() { new Name("FERNIE", "Larissa")
      {
        Middle = "Ellen",
        TypeCode = "L"
      }};

      var Gender = 'F';
      var DateOfBirth = new DateTime(1988, 05, 28);

      var Pat = new Patient(NameList, Gender, DateOfBirth)
      {
        HomePhone = "+61499811041",
        //WorkPhone = "+61499811045"
      };

      Pat.AddressList.Add(new Address()
      {
        LineOne = "456 James Terrace",
        LineTwo = "",
        Suburb = "Sunrise Beach",
        PostCode = "4571",
        City = "Brisbane",
        State = "QLD",
        Country = "AUS",
        TypeCode = "H"
      });

      Pat.IdentifierList.Add(GetMrn("??", "InfertilityClinic"));
      //Pat.IdentifierList.Add(GetMedicareNumberId("42330987451"));
      Pat.IdentifierList.Add(GetIhiId("8003606361737731"));

      return Pat;
    }    
    private static Patient GregoryBLACKCOMB()
    {
      var NameList = new List<Name>() { new Name("BLACKCOMB", "Gregory")
      {
        Middle = null,
        TypeCode = "L"
      }};

      var Gender = 'M';
      var DateOfBirth = new DateTime(1956, 01, 05);

      var Pat = new Patient(NameList, Gender, DateOfBirth)
      {
        HomePhone = "+61447111384",
        //WorkPhone = "+61499811045"
      };

      Pat.AddressList.Add(new Address()
      {
        LineOne = "86 Ryan Square",
        LineTwo = "",
        Suburb = "Doonan",
        PostCode = "4571",
        City = "Brisbane",
        State = "QLD",
        Country = "AUS",
        TypeCode = "H"
      });

      Pat.IdentifierList.Add(GetMrn("749909954", "CardiologyClinic"));
      //Pat.IdentifierList.Add(GetMedicareNumberId("42330987451"));
      Pat.IdentifierList.Add(GetIhiId("8003601149087061"));

      return Pat;
    }
    private static Patient HaydenNORQUAY()
    {
      var NameList = new List<Name>() { new Name("NORQUAY", "Hayden")
      {
        Middle = "Rhys",
        TypeCode = "L"
      }};

      var Gender = 'M';
      var DateOfBirth = new DateTime(1993, 11, 02);

      var Pat = new Patient(NameList, Gender, DateOfBirth)
      {
        HomePhone = "+61 4 9281 1776"        
      };

      Pat.AddressList.Add(new Address()
      {
        LineOne = "197 Manu Court",
        LineTwo = "",
        Suburb = "Sunrise Beach",
        PostCode = "4571",
        City = "Brisbane",
        State = "QLD",
        Country = "AUS",
        TypeCode = "H"
      });

      Pat.IdentifierList.Add(GetMrn("895429416", "CardiologyClinic"));
      //Pat.IdentifierList.Add(GetMedicareNumberId("42330987451"));
      Pat.IdentifierList.Add(GetIhiId("8003602711331333"));

      return Pat;
    }
    private static Patient GeorgeWHITEWATER()
    {
      var NameList = new List<Name>() { new Name("WHITEWATER", "George")
      {
        Middle = "Neil",
        TypeCode = "L"
      }};

      var Gender = 'M';
      var DateOfBirth = new DateTime(1950, 08, 01);

      var Pat = new Patient(NameList, Gender, DateOfBirth)
      {
        HomePhone = "+61492813178"      
      };

      Pat.AddressList.Add(new Address()
      {
        LineOne = "197 Ramsay Court",
        LineTwo = "",
        Suburb = "Sunrise Beach",
        PostCode = "4571",
        City = "Brisbane",
        State = "QLD",
        Country = "AUS",
        TypeCode = "H"
      });

      Pat.IdentifierList.Add(GetMrn("95479412", "RespiratoryClinicSunriseHospital"));
      //Pat.IdentifierList.Add(GetMedicareNumberId("42330987451"));
      Pat.IdentifierList.Add(GetIhiId("8003602145850890"));

      return Pat;
    }



    private static Identifier GetMedicareNumberId(string value)
    {
      var AusStandardHL7v2 = new AusStandardHL7v2();
      return new Identifier(value) { AssigingAuthority = AusStandardHL7v2.MedicareNumber.AssigingAuthority, Type = AusStandardHL7v2.MedicareNumber.TypeCode };
    }
    private static Identifier GetIhiId(string value)
    {
      var AusStandardHL7v2 = new AusStandardHL7v2();
      return new Identifier(value) { AssigingAuthority = AusStandardHL7v2.Ihi.AssigingAuthority, Type = AusStandardHL7v2.Ihi.TypeCode };
    }
    private static Identifier GetMrn(string value, string assigingAuthority)
    {
      var AusStandardHL7v2 = new AusStandardHL7v2();
      return new Identifier(value) { AssigingAuthority = assigingAuthority, Type = AusStandardHL7v2.Mrn.TypeCode };
    }

  }
}
