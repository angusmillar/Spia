using Newtonsoft.Json;
using Spia.PathologyReportModel.Model;
using Spia.PathologyReportModel.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spia.PathologyReportModel.Converters
{  
  public class EnumJsonConverterBase<EnumType> : JsonConverter<EnumType> where EnumType : Enum
  {
    private CodeEnumSupport<EnumType> CodeEnumSupport;
    
    public EnumJsonConverterBase(CodeEnumSupport<EnumType> CodeEnumSupport)
    {
      this.CodeEnumSupport = CodeEnumSupport;           
    }

    public override EnumType ReadJson(JsonReader reader, Type objectType, EnumType existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
      if (reader.Value is string value)
      {
        EnumType result;
        if (CodeEnumSupport.TryLookupByCode(value.Trim().ToUpper(), out result))
        {
          return result;
        }
        else
        {
          string AllowedCodes = string.Empty;
          CodeEnumSupport.GetCodeAndDescriptionList().ForEach(Item => AllowedCodes = AllowedCodes + "\n " + Item.Code + " : " + Item.Description);
          throw new Exception($"Unable to convert the provided {objectType.FullName} code of '{value}' to an allowed value. \n" +
            $"The allowed values come from the code system: {this.CodeEnumSupport.GetDisplayNameOfCodeSystem()}\n " +
            $"The allowed values are: \n {AllowedCodes}");
        }
      }
      else
      {
        throw new Exception($"Unable to cast the value for {objectType.FullName} to a string.");
      }
    }

    public override void WriteJson(JsonWriter writer, EnumType value, JsonSerializer serializer)
    {
      string Code;
      if (CodeEnumSupport.TryLookupByEnum(value, out Code))
      {
        writer.WriteValue(Code);
      }
      else
      {
        throw new Exception($"Unable to locate a {nameof(EnumType)} value of {value.ToString()} from the code system : {this.CodeEnumSupport.GetDisplayNameOfCodeSystem()}.");
      }
    }
  }

}
