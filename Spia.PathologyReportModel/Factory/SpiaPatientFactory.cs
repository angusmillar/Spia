using Spia.PathologyReportModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spia.PathologyReportModel.Factory
{
  public enum PatientType
  {
    TessaCITIZEN,
    GeorginaROSSLAND,    
    GlennFERNIE,
    GloriaNELSON,
    LarissaFERNIE,
    GregoryBLACKCOMB,
    GeorgeWHITEWATER,
    HaydenNORQUAY
  };

  public class SpiaPatientFactory
  {
    private Dictionary<PatientType, Patient> PatientDictionary = new Dictionary<PatientType, Patient>();
    public SpiaPatientFactory()
    {
      PatientDictionary.Add(PatientType.TessaCITIZEN, GetTessaCITIZEN());
      PatientDictionary.Add(PatientType.GeorginaROSSLAND, GetROSSLANDGeorgina());
      PatientDictionary.Add(PatientType.GlennFERNIE, GetGlennFERNIE());      
      PatientDictionary.Add(PatientType.GloriaNELSON, GetGloriaNELSON());
      PatientDictionary.Add(PatientType.LarissaFERNIE, GetLarissaFERNIE());
      PatientDictionary.Add(PatientType.GregoryBLACKCOMB, GetGregoryBLACKCOMB());
      PatientDictionary.Add(PatientType.GeorgeWHITEWATER, GetGeorgeWHITEWATER());
      PatientDictionary.Add(PatientType.HaydenNORQUAY, GetHaydenNORQUAY());      
    }

    public Patient GetPatient(PatientType patientType)
    {
      if (PatientDictionary.ContainsKey(patientType))
      {
        return PatientDictionary[patientType];
      }
      else
      {
        throw new ApplicationException($"Patient Type of {patientType.ToString()} not found in patient dictionary.");
      }
    }
    public Patient GetROSSLANDGeorgina()
    {
      var Patient = new Patient()
      {
        Name = new Name()
        {
          Family = "ROSSLAND",
          Given = "Georgina",
          Middle = "Noele",
          Title = null
        },
        DateOfBirth = new DateTime(1989, 07, 01),
        Gender =  GenderType.Female,
        AddressList = new List<Address>()
        {
          new Address()
          {
            LineOne = "123 Heston Court",
            LineTwo = null,
            City = "Brisbane",
            Country = "AUS",
            PostCode = "4571",
            State = StateType.QLD,
            Suburb = "Sunrise Beach",
            TypeCode =  AddressType.Home
          },
        },
        HomePhoneNumber = "+61492811778",
        IdentifierList = new List<Identifier>()
        {
          new Identifier()
          {
            Type = IdentifierType.IHI,
            Value = "8003603236836806"
          },
          new Identifier()
          {
            Type = IdentifierType.MRN,
            Value = "95429417",
            AssigningAuthority = "ACSH"
          }
        }
      };
      return Patient;
    }
    public Patient GetTessaCITIZEN()
    {
      var Patient = new Patient()
      {
        Name = new Name()
        {
          Family = "CITIZEN",
          Given = "Tessa",
          Middle = "Paige",
          Title = null
        },
        DateOfBirth = new DateTime(1987, 06, 30),
        Gender = GenderType.Female,
        AddressList = new List<Address>()
        {
          new Address()
          {
            LineOne = "123 Somewhere Place",
            LineTwo = null,
            Suburb = "Sunrise Beach",
            PostCode = "4571",
            City = "Brisbane",
            State = StateType.QLD,
            Country = "AUS",
            TypeCode =  AddressType.Home
          },
        },
        HomePhoneNumber = "+61499811044",
        IdentifierList = new List<Identifier>()
        {
          new Identifier()
          {
            Type = IdentifierType.IHI,
            Value = "8003606547651855"
          },
          new Identifier()
          {
            Type = IdentifierType.MRN,
            Value = "999909999",
            AssigningAuthority = "CoagulationThrombosisClinic"
          }
        }
      };
      return Patient;
    }
    public Patient GetGloriaNELSON()
    {
      var Patient = new Patient()
      {
        Name = new Name()
        {
          Family = "NELSON",
          Given = "Gloria",
          Middle = "Nannette",
          Title = null
        },
        DateOfBirth = new DateTime(1949, 04, 17),
        Gender = GenderType.Female,
        AddressList = new List<Address>()
        {
          new Address()
          {
            LineOne = "678 Oliver Close",
            LineTwo = null,
            Suburb = "Sunrise Beach",
            PostCode = "4571",
            City = "Brisbane",
            State = StateType.QLD,
            Country = "AUS",
            TypeCode =  AddressType.Home
          },
        },
        HomePhoneNumber = "+61491411377",
        IdentifierList = new List<Identifier>()
        {
          new Identifier()
          {
            Type = IdentifierType.IHI,
            Value = "8003600463320066"
          },
          new Identifier()
          {
            Type = IdentifierType.MRN,
            Value = "997907777",
            AssigningAuthority = "SunriseHospital"
          }
        }
      };
      return Patient;
    }
    private Patient GetLarissaFERNIE()
    {
      var Patient = new Patient()
      {
        Name = new Name()
        {
          Family = "FERNIE",
          Given = "Larissa",
          Middle = "Ellen",
          Title = null
        },
        DateOfBirth = new DateTime(1988, 05, 28),
        Gender = GenderType.Female,
        AddressList = new List<Address>()
        {
          new Address()
          {
           LineOne = "456 James Terrace",
           LineTwo = null,
           Suburb = "Sunrise Beach",
           PostCode = "4571",
           City = "Brisbane",
           State = StateType.QLD,
           Country = "AUS",
           TypeCode =  AddressType.Home
          },
        },
        HomePhoneNumber = "+61499811041",
        IdentifierList = new List<Identifier>()
        {
          new Identifier()
          {
            Type = IdentifierType.IHI,
            Value = "8003606361737731"
          },
          new Identifier()
          {
            Type = IdentifierType.MRN,
            Value = "719909917",
            AssigningAuthority = "InfertilityClinic"
          }
        }
      };
      return Patient;
    }
    private Patient GetGregoryBLACKCOMB()
    {
      var Patient = new Patient()
      {
        Name = new Name()
        {
          Family = "BLACKCOMB",
          Given = "Gregory",
          Middle = null,
          Title = null
        },
        DateOfBirth = new DateTime(1956, 01, 05),
        Gender = GenderType.Male,
        AddressList = new List<Address>()
        {
          new Address()
          {
            LineOne = "86 Ryan Square",
            LineTwo = null,
            Suburb = "Doonan",
            PostCode = "4571",
            City = "Brisbane",
            State = StateType.QLD,
            Country = "AUS",
            TypeCode =  AddressType.Home
          },
        },
        HomePhoneNumber = "+61447111384",
        IdentifierList = new List<Identifier>()
        {
          new Identifier()
          {
            Type = IdentifierType.IHI,
            Value = "8003601149087061"
          },
          new Identifier()
          {
            Type = IdentifierType.MRN,
            Value = "749909954",
            AssigningAuthority = "CardiologyClinic"
          }
        }
      };
      return Patient;
    }
    private Patient GetHaydenNORQUAY()
    {
      var Patient = new Patient()
      {
        Name = new Name()
        {
          Family = "NORQUAY",
          Given = "Hayden",
          Middle = "Rhys",
          Title = null
        },
        DateOfBirth = new DateTime(1993, 11, 02),
        Gender = GenderType.Male,
        AddressList = new List<Address>()
        {
          new Address()
          {
            LineOne = "197 Manu Court",
            LineTwo = null,
            Suburb = "Sunrise Beach",
            PostCode = "4571",
            City = "Brisbane",
            State = StateType.QLD,
            Country = "AUS",
            TypeCode =  AddressType.Home
          },
        },
        HomePhoneNumber = "+61492811776",
        IdentifierList = new List<Identifier>()
        {
          new Identifier()
          {
            Type = IdentifierType.IHI,
            Value = "8003602711331333"
          },
          new Identifier()
          {
            Type = IdentifierType.MRN,
            Value = "895429416",
            AssigningAuthority = "CardiologyClinic"
          }
        }
      };
      return Patient;
    }
    private Patient GetGeorgeWHITEWATER()
    {
      var Patient = new Patient()
      {
        Name = new Name()
        {
          Family = "WHITEWATER",
          Given = "George",
          Middle = "Neil",
          Title = null
        },
        DateOfBirth = new DateTime(1950, 08, 01),
        Gender = GenderType.Male,
        AddressList = new List<Address>()
        {
          new Address()
          {
            LineOne = "197 Ramsay Court",
            LineTwo = null,
            Suburb = "Sunrise Beach",
            PostCode = "4571",
            City = "Brisbane",
            State = StateType.QLD,
            Country = "AUS",
            TypeCode =  AddressType.Home
          },
        },
        HomePhoneNumber = "+61492813178",
        IdentifierList = new List<Identifier>()
        {
          new Identifier()
          {
            Type = IdentifierType.IHI,
            Value = "8003602145850890"
          },
          new Identifier()
          {
            Type = IdentifierType.MRN,
            Value = "95479412",
            AssigningAuthority = "RespiratoryClinicSunriseHospital"
          }
        }
      };
      return Patient;
    }
    private Patient GetGlennFERNIE()
    {
      var Patient = new Patient()
      {
        Name = new Name()
        {
          Family = "FERNIE",
          Given = "Glenn",
          Middle = "Neville",
          Title = null
        },
        DateOfBirth = new DateTime(1968, 05, 28),
        Gender = GenderType.Male,
        AddressList = new List<Address>()
        {
          new Address()
          {
             LineOne = "456 James Terrace",
             LineTwo = null,
             Suburb = "Sunrise Beach",
             PostCode = "4571",
             City = "Brisbane",
             State = StateType.QLD,
             Country = "AUS",
             TypeCode =  AddressType.Home
          },
        },
        HomePhoneNumber = "+61499811044",
        IdentifierList = new List<Identifier>()
        {
          new Identifier()
          {
            Type = IdentifierType.IHI,
            Value = "8003609414426781"
          },
          new Identifier()
          {
            Type = IdentifierType.MRN,
            Value = "719909917",
            AssigningAuthority = "DermatologyClinic"
          }
        }
      };
      return Patient;
    }
  }
}
