using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spia.PathologyReportModel.Model;
namespace Spia.PathologyReportModel.Support
{
  public class GenderTypeSupport : CodeEnumSupport<GenderType>
  {   
    public GenderTypeSupport() : base(PrimeDictionary(), "HL7AUSD-STD-OO-ADRM-2018.1 standard v2.4 Table 0001: Administrative sex") {}

    private static Dictionary<string, GenderType> PrimeDictionary()
    {
      //This is the User-defined Table 0001 Administrative sex 
      //Taken from the  HL7AUSD-STD-OO-ADRM-2018.1 standard : https://confluence.hl7australia.com/display/OOADRM20181/2+Patient+Administration+for+Pathology#id-2PatientAdministrationforPathology-PID-82.2.1.8PID-8Administrativesex(IS)00111
      return new Dictionary<string, GenderType>()
      {
        {"N",  GenderType.NotApplicable},
        {"A",  GenderType.Ambiguous},
        {"O", GenderType.Other},
        {"U",  GenderType.Unknown},
        {"F", GenderType.Female},
        {"M", GenderType.Male},
      };
    }    
  }
}
