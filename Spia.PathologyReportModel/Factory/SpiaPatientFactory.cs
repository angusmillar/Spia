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
    GraceROSSLAND,    
    GlennFERNIE,
    GloriaNELSON,
    LarissaFERNIE,
    GregoryBLACKCOMB,
    GeorgeWHITEWATER,
    HaydenNORQUAY,
    IrisREVELSTOKE
  };

  public class SpiaPatientFactory
  {
    private Dictionary<PatientType, Patient> PatientDictionary = new Dictionary<PatientType, Patient>();
    public SpiaPatientFactory()
    {
      PatientDictionary.Add(PatientType.TessaCITIZEN, GetTessaCITIZEN());
      PatientDictionary.Add(PatientType.GraceROSSLAND, GetGraceROSSLAND());
      PatientDictionary.Add(PatientType.GlennFERNIE, GetGlennFERNIE());      
      PatientDictionary.Add(PatientType.GloriaNELSON, GetGloriaNELSON());
      PatientDictionary.Add(PatientType.LarissaFERNIE, GetLarissaFERNIE());
      PatientDictionary.Add(PatientType.GregoryBLACKCOMB, GetGregoryBLACKCOMB());
      PatientDictionary.Add(PatientType.GeorgeWHITEWATER, GetGeorgeWHITEWATER());
      PatientDictionary.Add(PatientType.HaydenNORQUAY, GetHaydenNORQUAY());
      PatientDictionary.Add(PatientType.IrisREVELSTOKE, GetIrisREVELSTOKE());
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
    public Patient GetGraceROSSLAND()
    {
      var Patient = new Patient()
      {
        Name = new Name()
        {
          Family = "ROSSLAND",
          Given = "Grace",
          Middle = "Mia",
          Title = "Ms"
        },
        DateOfBirth = new DateTime(1989, 07, 01),
        Gender =  GenderType.Female,
        AddressList = new List<Address>()
        {
          new Address()
          {
            LineOne = "123 Heston Court",
            LineTwo = null,                                                
            Suburb = "Sunrise Beach",
            PostCode = "4571",
            State = StateType.QLD,
            City = null,
            Country = "AUS",
            TypeCode =  AddressType.Home
          },
        },
        HomePhoneNumber = null,
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
            Value = "954629417",
            AssigningAuthority = "SunshineHospital"
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
          Title = "Mrs"
        },
        DateOfBirth = new DateTime(1987, 06, 30),
        Gender = GenderType.Female,
        AddressList = new List<Address>()
        {
          new Address()
          {
            LineOne = "123 Shames Avenue",
            LineTwo = null,
            Suburb = "Sunrise Bay",
            PostCode = "4573",
            City = null,
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
          Title = "Mrs"
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
    public Patient GetLarissaFERNIE()
    {
      var Patient = new Patient()
      {
        Name = new Name()
        {
          Family = "FERNIE",
          Given = "Larissa",
          Middle = "Ellen",
          Title = "Mrs"
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
           City = null,
           State = StateType.QLD,
           Country = "AUS",
           TypeCode =  AddressType.Home
          },
        },
        HomePhoneNumber = null,
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
            AssigningAuthority = "SunshineHospital"
          }
        }
      };
      return Patient;
    }
    public Patient GetGregoryBLACKCOMB()
    {
      var Patient = new Patient()
      {
        Name = new Name()
        {
          Family = "BLACKCOMB",
          Given = "Gregory",
          Middle = null,
          Title = "Mr"
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
    public Patient GetHaydenNORQUAY()
    {
      var Patient = new Patient()
      {
        Name = new Name()
        {
          Family = "NORQUAY",
          Given = "Hayden",
          Middle = "Rhys",
          Title = "Mr"
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
    public Patient GetGeorgeWHITEWATER()
    {
      var Patient = new Patient()
      {
        Name = new Name()
        {
          Family = "WHITEWATER",
          Given = "George",
          Middle = "Neil",
          Title = "Mr"
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
    public Patient GetGlennFERNIE()
    {
      var Patient = new Patient()
      {
        Name = new Name()
        {
          Family = "FERNIE",
          Given = "Glenn",
          Middle = "Neville",
          Title = "Mr"
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
    public Patient GetIrisREVELSTOKE()
    {
      var Patient = new Patient()
      {
        Name = new Name()
        {
          Family = "REVELSTOKE",
          Given = "Iris",
          Middle = "Mabel",
          Title = "Mrs"
        },
        DateOfBirth = new DateTime(1958, 07, 26),
        Gender = GenderType.Female,
        AddressList = new List<Address>()
        {
          new Address()
          {
             LineOne = "76 Panorama Place",
             LineTwo = null,
             Suburb = "Sunrise Bay",
             PostCode = "4573",
             City = null,
             State = StateType.QLD,
             Country = "AUS",
             TypeCode =  AddressType.Home
          },
        },
        HomePhoneNumber = null,
        IdentifierList = new List<Identifier>()
        {
          new Identifier()
          {
            Type = IdentifierType.IHI,
            Value = "8003600490464671"
          },
          new Identifier()
          {
            Type = IdentifierType.MRN,
            Value = "724409912",
            AssigningAuthority = "SunriseHospital"
          }
        }
      };
      return Patient;
    }
    public Patient GetEricaPURCELL()
    {
      var Patient = new Patient()
      {
        Name = new Name()
        {
          Family = "PURCELL",
          Given = "Erica",
          Middle = "Lynne",
          Title = "Mrs"
        },
        DateOfBirth = new DateTime(1956, 05, 08),
        Gender = GenderType.Female,
        AddressList = new List<Address>()
        {
          new Address()
          {
             LineOne = "58 Golden Circuit",
             LineTwo = null,
             Suburb = "Sunrise Bay",
             PostCode = "4573",
             City = null,
             State = StateType.QLD,
             Country = "AUS",
             TypeCode =  AddressType.Home
          },
        },
        HomePhoneNumber = null,
        IdentifierList = new List<Identifier>()
        {
          new Identifier()
          {
            Type = IdentifierType.IHI,
            Value = "8003600490464671"
          },
          new Identifier()
          {
            Type = IdentifierType.MRN,
            Value = "719909800",
            AssigningAuthority = "SunriseHospital"
          }
        }
      };
      return Patient;
    }
    public Patient GetGordonSEYMOUR()
    {
      var Patient = new Patient()
      {
        Name = new Name()
        {
          Family = "SEYMOUR",
          Given = "Gordon",
          Middle = "Peter",
          Title = "Mr"
        },
        DateOfBirth = new DateTime(1956, 05, 08),
        Gender = GenderType.Male,
        AddressList = new List<Address>()
        {
          new Address()
          {
             LineOne = "456 Stellar Street",
             LineTwo = null,
             Suburb = "Sunrise Bay",
             PostCode = "4573",
             City = null,
             State = StateType.QLD,
             Country = "AUS",
             TypeCode =  AddressType.Home
          },
        },
        HomePhoneNumber = null,
        IdentifierList = new List<Identifier>()
        {
          new Identifier()
          {
            Type = IdentifierType.IHI,
            Value = "8003 6026 6124 5921"
          },
          new Identifier()
          {
            Type = IdentifierType.MRN,
            Value = "719909914",
            AssigningAuthority = "SunriseHospital"
          }
        }
      };
      return Patient;
    }
    public Patient GetNormanMANNING()
    {
      var Patient = new Patient()
      {
        Name = new Name()
        {
          Family = "MANNING",
          Given = "Norman",
          Middle = "Lyle",
          Title = "Mr"
        },
        DateOfBirth = new DateTime(1968, 05, 23),
        Gender = GenderType.Male,
        AddressList = new List<Address>()
        {
          new Address()
          {
             LineOne = "84 Nakiska Court",
             LineTwo = null,
             Suburb = "Sunrise Bay",
             PostCode = "4573",
             City = null,
             State = StateType.QLD,
             Country = "AUS",
             TypeCode =  AddressType.Home
          },
        },
        HomePhoneNumber = null,
        IdentifierList = new List<Identifier>()
        {
          new Identifier()
          {
            Type = IdentifierType.IHI,
            Value = "8003 6028 4716 0044"
          },
          new Identifier()
          {
            Type = IdentifierType.MRN,
            Value = "719909938",
            AssigningAuthority = "SunriseHospital"
          }
        }
      };
      return Patient;
    }
  }
}
