using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hl7.Fhir.Model;

namespace Spia.AdhaFhirGeneration.Factory
{
  public static class FhirDataTypeFactory
  {
    
    public static Identifier GetIdentifier(string system, string value, CodeableConcept type = null)
    {
      var Id = new Hl7.Fhir.Model.Identifier();
      if (!string.IsNullOrWhiteSpace(system))
      {
        if (Uri.TryCreate(system, UriKind.Absolute, out Uri uri))
        {
          Id.System = uri.OriginalString;
        }
        else
        {
          throw new FormatException($"The Identifier system provided could not be parsed as an absolute uri."); 
        }
      }
      else
      {
        throw new FormatException($"The Identifier system provided can not be null or empty string.");
      }
      if (!string.IsNullOrWhiteSpace(value))
      {
        Id.Value = value;
      }
      else
      {
        throw new FormatException($"The Identifier value provided can not be null or empty string.");
      }

      if(type is object)
      {
        Id.Type = type;
      }
      return Id;            
    }
    public static HumanName GetHumanName(string family, string given, string middle, string title)
    {
      if (string.IsNullOrWhiteSpace(family))
        throw new ApplicationException("The family name in mandatory for a FHIR HumanName datatype");

      var HumanName = new HumanName();
      HumanName.Family = family;
      
      if (!string.IsNullOrWhiteSpace(title))
      {
        HumanName.Prefix = new List<string>() { title };
      }
      if (!string.IsNullOrWhiteSpace(given))
      {
        HumanName.Given = new List<string>() { given };
        if (!string.IsNullOrWhiteSpace(middle))
        {
          HumanName.Given.ToList().Add(middle);
        }
      }
      SetHumanNameText(HumanName);
      return HumanName;
    }

    private static void SetHumanNameText(HumanName humanName)
    {
      string Fullname = string.Empty;
      if (humanName.Family != "")
      {
        Fullname = humanName.Family.ToUpper();
      }

      if (humanName.Given.Count() > 0)
      {
        Fullname += $", ";
        foreach (var Given in humanName.Given)
        {
          Fullname += $"{Given} ";
        }
        Fullname.TrimEnd(' ');
      }

      if (humanName.Prefix.Count() > 0)
      {
        foreach (var Prefix in humanName.Prefix)
        {
          Fullname += $"{Prefix}, ";
        }
        Fullname.TrimEnd(',');
      }
      humanName.Text = Fullname;
    }
  }
}
