using System;
using System.Collections.Generic;
using System.Text;

namespace Spia.AusHl7v2Generation.Model.Logical
{ 
  public class Patient
  {
    public Patient(List<Name> nameList, char gender, DateTime dateOfBirth )
    {
      this.NameList = nameList;
      this.Gender = gender;
      this.DateOfBirth = dateOfBirth;
      this.AddressList = new List<Address>();
      this.IdentifierList = new List<Identifier>();
    }
    public List<Name> NameList { get; }    
    public DateTime DateOfBirth { get; set; }
    public char Gender { get; set; }
    public List<Identifier> IdentifierList { get; }
    public List<Address> AddressList { get; }
    public string HomePhone { get; set; }
    public string WorkPhone { get; set; }
  }
}
