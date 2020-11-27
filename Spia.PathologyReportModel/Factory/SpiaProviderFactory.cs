using Spia.PathologyReportModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spia.PathologyReportModel.Factory
{
  public class SpiaProviderFactory
  {
    // Reporting Pathologist Providers    
    public Provider GetBellaPathologist()
    {
      return new Provider()
      {
        Name = new Name()
        {
          Family = "Pathologist",
          Given = "Bella",
          Middle = "",
          Title = "Dr"
        },
        IdentifierList = new List<Identifier>()
        {
         new Identifier()
         {
            Value = "8003 6111 0391 4531",
            Type =  IdentifierType.HPII
         },
         new Identifier()
         {
           Value = "BPATH",
           Type = IdentifierType.LocalToLab
         }         
        }
      };
    }
    public Provider GetBertramPathologist()
    {
      return new Provider()
      {
        Name = new Name()
        {
          Family = "Pathologist",
          Given = "Bertram",
          Middle = "",
          Title = "Dr"
        },
        IdentifierList = new List<Identifier>()
        {
         new Identifier()
         {
            Value = "8003 6136 8407 5880",
            Type =  IdentifierType.HPII
         },
         new Identifier()
         {
           Value = "BI",
           Type = IdentifierType.LocalToLab
         }
        }
      };
    }
    public Provider GetMarioPathologist()
    {
      return new Provider()
      {
        Name = new Name()
        {
          Family = "Pathologist",
          Given = "Mario",
          Middle = "",
          Title = "Dr"
        },
        IdentifierList = new List<Identifier>()
        {
         new Identifier()
         {
            Value = "8003 6150 4939 6513",
            Type =  IdentifierType.HPII
         },
         new Identifier()
         {
           Value = "MARP",
           Type = IdentifierType.LocalToLab
         }
        }
      };
    }
    public Provider GetManuelDelPathologist()
    {
      return new Provider()
      {
        Name = new Name()
        {
          Family = "Pathologist",
          Given = "Manuel",
          Middle = "del",
          Title = "Dr"
        },
        IdentifierList = new List<Identifier>()
        {
         new Identifier()
         {
            Value = "8003 6115 7353 3100",
            Type =  IdentifierType.HPII
         },
         new Identifier()
         {
           Value = "DPM",
           Type = IdentifierType.LocalToLab
         }
        }
      };
    }
    public Provider GetMarissaPathologist()
    {
      return new Provider()
      {
        Name = new Name()
        {
          Family = "Pathologist",
          Given = "Marissa",
          Middle = "",
          Title = "Dr"
        },
        IdentifierList = new List<Identifier>()
        {
         new Identifier()
         {
            Value = "8003 6143 4432 9915",
            Type =  IdentifierType.HPII
         },
         new Identifier()
         {
           Value = "MP",
           Type = IdentifierType.LocalToLab
         }
        }
      };
    }
    public Provider GetKondoPathologist()
    {
      return new Provider()
      {
        Name = new Name()
        {
          Family = "Pathologist",
          Given = "Kondo",
          Middle = "",
          Title = "Dr"
        },
        IdentifierList = new List<Identifier>()
        {
         new Identifier()
         {
            Value = "8003 6168 4816 8954",
            Type =  IdentifierType.HPII
         },
         new Identifier()
         {
           Value = "KUP",
           Type = IdentifierType.LocalToLab
         }
        }
      };
    }
    public Provider GetEvannaPathologist()
    {
      return new Provider()
      {
        Name = new Name()
        {
          Family = "Pathologist",
          Given = "Evanna",
          Middle = "",
          Title = "Dr"
        },
        IdentifierList = new List<Identifier>()
        {
         new Identifier()
         {
            Value = "8003 6169 0858 7986",
            Type =  IdentifierType.HPII
         },
         new Identifier()
         {
           Value = "EP",
           Type = IdentifierType.LocalToLab
         }
        }
      };
    }
    public Provider GetStanleyVirologist()
    {
      return new Provider()
      {
        Name = new Name()
        {
          Family = "Virologist",
          Given = "Stanley",
          Middle = null,
          Title = "Dr"
        },
        IdentifierList = new List<Identifier>()
        {
          new Identifier()
          {
            Value = "8003 6100 3427 0591",
            Type = IdentifierType.HPII
          },
          new Identifier()
          {
            Value = "SVIRO",
            Type = IdentifierType.LocalToLab
          }
        }
      };
    }
    public Provider GetArturoPathologist()
    {
      return new Provider()
      {
        Name = new Name()
        {
          Family = "Pathologist",
          Given = "Arturo",
          Middle = null,
          Title = "Dr"
        },
        IdentifierList = new List<Identifier>()
        {
          new Identifier()
          {
            Value = "8003 6168 0685 7986",
            Type = IdentifierType.HPII
          },
          new Identifier()
          {
            Value = "ARTPATH",
            Type = IdentifierType.LocalToLab
          }
        }
      };
    }

    //Requesting Providers
    public Provider GetTrishFamilyDr(string MedicareProviderNumber)
    {
      return new Provider()
      {
        Name = new Name()
        {
          Family = "Familydr",
          Given = "Trish",
          Middle = "",
          Title = "Dr"
        },
        IdentifierList = new List<Identifier>()
        {
         new Identifier()
         {
            Value = "8003614242061461",
            Type =  IdentifierType.HPII
         },
         new Identifier()
         {
           Value = "FAMTRI",
           Type = IdentifierType.LocalToLab
         },
         new Identifier()
         {
           Value = MedicareProviderNumber,
           Type = IdentifierType.MedicareProviderNumber
         }
        }
      };
    }

    // CopyTo Providers
    public Provider GetBiancaMidwife()
    {
      return new Provider()
      {
        Name = new Name()
        {
          Family = "Midwife",
          Given = "Bianca",
          Middle = null,
          Title = "Dr"
        },
        IdentifierList = new List<Identifier>()
        {
          new Identifier()
          {
            Value = "MIDBIA",
            Type = IdentifierType.LocalToLab
          }
        }
      };
    }
    public Provider GetDermatologyClinic()
    {
      return new Provider()
      {
        Name = new Name()
        {
          Family = "Dermatology Clinic",
          Given = "",
          Middle = null,
          Title = null
        },
        IdentifierList = new List<Identifier>()
        {
          new Identifier()
          {
            Value = "DERMCLI",
            Type = IdentifierType.LocalToLab
          }
        }
      };
    }
    public Provider GetBrendanDermatologist()
    {
      return new Provider()
      {
        Name = new Name()
        {
          Family = "Dermatologist",
          Given = "Brendan",
          Middle = null,
          Title = "Dr"
        },
        IdentifierList = new List<Identifier>()
        {
          new Identifier()
          {
            Value = "DERMBRE",
            Type = IdentifierType.LocalToLab
          }
        }
      };
    }
    public Provider GetCoagulationAndThrombosisClinic()
    {
      return new Provider()
      {
        Name = new Name()
        {
          Family = "Coagulation & Thrombosis Clinic",
          Given = "",
          Middle = null,
          Title = null
        },
        IdentifierList = new List<Identifier>()
        {
          new Identifier()
          {
            Value = "COAGTHROMCL",
            Type = IdentifierType.LocalToLab
          }
        }
      };
    }
    public Provider GetBillCardiologist()
    {
      return new Provider()
      {
        Name = new Name()
        {
          Family = "Cardiologist",
          Given = "Bill",
          Middle = null,
          Title = "Dr"
        },
        IdentifierList = new List<Identifier>()
        {
          new Identifier()
          {
            Value = "RHEUREB",
            Type = IdentifierType.LocalToLab
          },
          new Identifier()
          {
            Value = "243089UJ",
            Type = IdentifierType.MedicareProviderNumber
          }
        }
      };
    }
    public Provider GetBillHaematologist()
    {
      return new Provider()
      {
        Name = new Name()
        {
          Family = "Haematologist",
          Given = "Bill",
          Middle = null,
          Title = "Dr"
        },
        IdentifierList = new List<Identifier>()
        {
          new Identifier()
          {
            Value = "BHAEMA",
            Type = IdentifierType.LocalToLab
          },
          new Identifier()
          {
            Value = "847489RK",
            Type = IdentifierType.MedicareProviderNumber
          }
        }
      };
    }
    public Provider GetGeneticsClinic()
    {
      return new Provider()
      {
        Name = new Name()
        {
          Family = "Genetics Clinic",
          Given = "",
          Middle = null,
          Title = null
        },
        IdentifierList = new List<Identifier>()
        {
          new Identifier()
          {
            Value = "GENCLI",
            Type = IdentifierType.LocalToLab
          }
        }
      };
    }
    public Provider GetGeneticCounsellingClinicCoordinator()
    {
      return new Provider()
      {
        Name = new Name()
        {
          Family = "Genetic Counselling Clinic coordinator",
          Given = null,
          Middle = null,
          Title = null
        },
        IdentifierList = new List<Identifier>()
        {
          new Identifier()
          {
            Value = "GCCC",
            Type = IdentifierType.LocalToLab
          }
        }
      };
    }
    public Provider GetBjornGeneticCounsellor()
    {
      return new Provider()
      {
        Name = new Name()
        {
          Family = "Genetic Counsellor",
          Given = "Bjorn",
          Middle = null,
          Title = null
        },
        IdentifierList = new List<Identifier>()
        {
          new Identifier()
          {
            Value = "BJGC",
            Type = IdentifierType.LocalToLab
          }
        }
      };
    }
    public Provider GetSarsgaardGeneticCounsellor()
    {
      return new Provider()
      {
        Name = new Name()
        {
          Family = "Sarsgaard",
          Given = "Genetic Counsellor",
          Middle = null,
          Title = "Dr"
        },
        IdentifierList = new List<Identifier>()
        {
          new Identifier()
          {
            Value = "SGC",
            Type = IdentifierType.LocalToLab
          },
          new Identifier()
          {
            Value = "7771139A",
            Type = IdentifierType.MedicareProviderNumber
          },
          new Identifier()
          {
            Value = "8003619113297912",
            Type = IdentifierType.HPII
          }
        }
      };
    }    
    public Provider GetAllergyClinic()
    {
      return new Provider()
      {
        Name = new Name()
        {
          Family = "Allergy Clinic",
          Given = "",
          Middle = null,
          Title = null
        },
        IdentifierList = new List<Identifier>()
        {
          new Identifier()
          {
            Value = "ALLERGYCLI",
            Type = IdentifierType.LocalToLab
          }
        }
      };
    }
    public Provider GetBeulaImmunologist()
    {
      return new Provider()
      {
        Name = new Name()
        {
          Family = "Immunologist",
          Given = "Beula",
          Middle = null,
          Title = "Dr"
        },
        IdentifierList = new List<Identifier>()
        {
          new Identifier()
          {
            Value = "IMMUNBE",
            Type = IdentifierType.LocalToLab
          },
          new Identifier()
          {
            Value = "823290QF",
            Type = IdentifierType.MedicareProviderNumber
          }
        }
      };
    }
    public Provider GetRebeccaGP()
    {
      return new Provider()
      {
        Name = new Name()
        {
          Family = "GP",
          Given = "Rebecca",
          Middle = null,
          Title = "Dr"
        },
        IdentifierList = new List<Identifier>()
        {
          new Identifier()
          {
            Value = "REBGP",
            Type = IdentifierType.LocalToLab
          },
          new Identifier()
          {
            Value = "582859VL",
            Type = IdentifierType.MedicareProviderNumber
          }          
        }
      };
    }
    public Provider GetRheumatologyClinic()
    {
      return new Provider()
      {
        Name = new Name()
        {
          Family = "Rheumatology Clinic",
          Given = "",
          Middle = null,
          Title = "Dr"
        },
        IdentifierList = new List<Identifier>()
        {
          new Identifier()
          {
            Value = "RHEUCL",
            Type = IdentifierType.LocalToLab
          }
        }
      };
    }
    public Provider GetCardiologyClinicSunshineHospital()
    {
      return new Provider()
      {
        Name = new Name()
        {
          Family = "Cardiology Clinic",
          Given = "Sunshine Hospital",
          Middle = null,
          Title = null
        },
        IdentifierList = new List<Identifier>()
        {
          new Identifier()
          {
            Value = "CCSH",
            Type = IdentifierType.LocalToLab
          }
        }
      };
    }
    public Provider GetGBastien()
    {
      return new Provider()
      {
        Name = new Name()
        {
          Family = "Bastien",
          Given = "G",
          Middle = null,
          Title = "Dr"
        },
        IdentifierList = new List<Identifier>()
        {
          new Identifier()
          {
            Value = "BASTG",
            Type = IdentifierType.LocalToLab
          },
          new Identifier()
          {
            Value = "1582964K",
            Type = IdentifierType.MedicareProviderNumber
          },
          new Identifier()
          {
            Value = "8003617895300367",
            Type = IdentifierType.HPII
          }
        }
      };
    }
    public Provider GetImmunologyClinic()
    {
      return new Provider()
      {
        Name = new Name()
        {
          Family = "Immunology Clinic",
          Given = null,
          Middle = null,
          Title = null
        },
        IdentifierList = new List<Identifier>()
        {
          new Identifier()
          {
            Value = "IMMC",
            Type = IdentifierType.LocalToLab
          }
        }
      };
    }
    public Provider GetInfusionClinic()
    {
      return new Provider()
      {
        Name = new Name()
        {
          Family = "Infusion Clinic, Sunshine Hospital",
          Given = null,
          Middle = null,
          Title = null
        },
        IdentifierList = new List<Identifier>()
        {
          new Identifier()
          {
            Value = "INFCSH",
            Type = IdentifierType.LocalToLab
          }
        }
      };
    }
    public Provider GetArmandeImmunologist()
    {
      return new Provider()
      {
        Name = new Name()
        {
          Family = "Immunologist",
          Given = "Armande",
          Middle = null,
          Title = "Dr"
        },
        IdentifierList = new List<Identifier>()
        {
          new Identifier()
          {
            Value = "IMMARM",
            Type = IdentifierType.LocalToLab
          },
          new Identifier()
          {
            Value = "477425AX",
            Type = IdentifierType.MedicareProviderNumber
          },
          new Identifier()
          {
            Value = "8003611874467495",
            Type = IdentifierType.HPII
          }
        }
      };
    }
    public Provider GetDiabetesClinicSunshineHospital()
    {
      return new Provider()
      {
        Name = new Name()
        {
          Family = "Diabetes Clinic Sunshine Hospital",
          Given = null,
          Middle = null,
          Title = null
        },
        IdentifierList = new List<Identifier>()
        {
          new Identifier()
          {
            Value = "DCSH",
            Type = IdentifierType.LocalToLab
          },          
        }
      };
    }
    public Provider GetMySpecialist()
    {
      return new Provider()
      {
        Name = new Name()
        {
          Family = "My Specialist",
          Given = null,
          Middle = null,
          Title = null
        },
        IdentifierList = new List<Identifier>()
        {
          new Identifier()
          {
            Value = "MYSPEC",
            Type = IdentifierType.LocalToLab
          }          
        }
      };
    }
    public Provider GetMyPhysio()
    {
      return new Provider()
      {
        Name = new Name()
        {
          Family = "My Physio",
          Given = null,
          Middle = null,
          Title = null,
        },
        IdentifierList = new List<Identifier>()
        {
          new Identifier()
          {
            Value = "MYPHY",
            Type = IdentifierType.LocalToLab
          }
        }
      };
    }
  }
}
