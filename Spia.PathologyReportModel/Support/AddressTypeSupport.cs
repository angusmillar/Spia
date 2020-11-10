using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spia.PathologyReportModel.Model;
namespace Spia.PathologyReportModel.Support
{
  public class AddressTypeSupport : CodeEnumSupport<AddressType>
  {   
    public AddressTypeSupport() : base(PrimeDictionary(), "HL7 Standard v2.4 Table 0190: Address type") {}

    private static Dictionary<string, AddressType> PrimeDictionary()
    {
      //This is the v2.4 Table 0190: Address type      
      return new Dictionary<string, AddressType>()
      {
        {"B",  AddressType.FirmBusiness},
        {"BA",  AddressType.BadAddress},
        {"BDL", AddressType.BirthDeliveryLocation},
        {"BR",  AddressType.ResidenceAtbirth},
        {"C",  AddressType.CurrentOrTemporary},
        {"F",  AddressType.CountryOfOrigin},
        {"H",  AddressType.Home},
        {"L",  AddressType.LegalAddress},
        {"M",  AddressType.Mailing},
        {"N",  AddressType.Birth},
        {"O",  AddressType.Office},
        {"P",  AddressType.Permanent},
        {"RH",  AddressType.RegistryHome},        
      };
    }    
  }
}
