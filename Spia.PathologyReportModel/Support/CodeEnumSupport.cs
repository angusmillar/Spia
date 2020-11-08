using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spia.PathologyReportModel.Model;
namespace Spia.PathologyReportModel.Support
{
  public class CodeEnumSupport<EnumType> where EnumType : Enum
  {
    private Dictionary<string, EnumType> CodeEnumDictionary;
    private Dictionary<EnumType,string> EnumCodeDictionary;
    private string DisplayNameOfCodeSystem;
    public CodeEnumSupport(Dictionary<string, EnumType> CodeEnumDictionary, string  displayNameOfCodeSystem)
    {
      this.CodeEnumDictionary = CodeEnumDictionary;
      this.EnumCodeDictionary = null;
      this.DisplayNameOfCodeSystem = displayNameOfCodeSystem;
    }
    public Dictionary<string, EnumType> LookupByCode()
    {
      return CodeEnumDictionary;
    }

    /// <summary>
    /// Try and lookup a Enum value based on the Code value
    /// </summary>
    /// <param name="value"></param>
    /// <param name="result"></param>
    /// <returns></returns>
    public bool TryLookupByCode(string value, out EnumType result)
    {      
      if (this.CodeEnumDictionary.ContainsKey(value))
      {
        result = this.CodeEnumDictionary[value];
        return true;
      }
      //This assignment below is required as we can not set an enum to null
      //however the caller must ignore the return result when TryLookup returns false so the 
      //assignment  is not relevant. 
      result = this.CodeEnumDictionary.First().Value;
      return false;
    }
    /// <summary>
    /// Try and lookup a Code value based on the Enum value
    /// </summary>
    /// <param name="value"></param>
    /// <param name="code"></param>
    /// <returns></returns>
    public bool TryLookupByEnum(EnumType value, out string code)
    {
      this.PrimeReverseDictionary();
      if (this.EnumCodeDictionary.ContainsKey(value))
      {
        code = this.EnumCodeDictionary[value];
        return true;
      }      
      code = null;
      return false;      
    }
    /// <summary>
    /// Get a list of the Enum values only
    /// </summary>
    /// <returns></returns>
    public List<EnumType> GetEnumList()
    {
      return CodeEnumDictionary.Values.ToList();
    }
    /// <summary>
    /// Get a list of the Code values only
    /// </summary>
    /// <returns></returns>
    public List<string> GetCodeList()
    {      
      return CodeEnumDictionary.Keys.ToList();
    }
    /// <summary>
    /// Get a list of Code and Description items
    /// </summary>
    /// <returns></returns>
    public List<CodeDescription> GetCodeAndDescriptionList()
    {      
      var result = new List<CodeDescription>();
      foreach(var item in this.CodeEnumDictionary)
      {
        result.Add(new CodeDescription(item.Key, item.Value.ToString()));
      }
      return result;
    }
    /// <summary>
    /// Get a human display name of the code system being represented 
    /// </summary>
    /// <returns></returns>
    public string GetDisplayNameOfCodeSystem()
    {
      return this.DisplayNameOfCodeSystem;
    }
    private void PrimeReverseDictionary()
    {
      if (this.EnumCodeDictionary is null)
      {
        this.EnumCodeDictionary = new Dictionary<EnumType, string>();
        foreach (var item in this.CodeEnumDictionary)
        {
          EnumCodeDictionary.Add(item.Value, item.Key);
        }
      }      
    }

    public class CodeDescription
    {
      public CodeDescription(string code, string description)
      {
        this.Code = code;
        this.Description = description;
      }
      public string Code {private set; get;}
      public string Description { private set; get; }
    }

  }
}
