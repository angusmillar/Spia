using System;
using System.Collections.Generic;
using System.Text;
using Spia.AusHl7v2Generation.Model.Logical;
using PeterPiper.Hl7.V2.Model;

namespace Spia.AusHl7v2Generation.Factory.HL7.v2
{
  public static class OBXFactory
  {

    public static List<ISegment> GetOBXList(Result result)
    {
      if (result is null)
        throw new ArgumentNullException(nameof(result));

      var OBXList = new List<ISegment>();
      OBXList.Add(GetOBX(result));
      if (result.ResultList != null)
      {
        foreach(var Child in result.ResultList)
        {
          OBXList.AddRange(GetOBXList(Child));
        }
      }
      return OBXList;
    }

    public static ISegment GetOBX(Result result)
    {
      if (result is null)
        throw new ArgumentNullException(nameof(result));

      var OBX = Creator.Segment("OBX");
      OBX.Field(1).AsString = result.SetId;
      OBX.Field(2).AsString = result.DataType;
      OBX.Field(3).Component(1).AsString = result.ResultType.International?.Value ?? "";
      OBX.Field(3).Component(2).AsString = result.ResultType.International?.Description ?? "";
      OBX.Field(3).Component(3).AsString = result.ResultType.International?.System ?? "";
      OBX.Field(3).Component(4).AsString = result.ResultType.Local?.Value ?? "";
      OBX.Field(3).Component(5).AsString = result.ResultType.Local?.Description ?? "";
      OBX.Field(3).Component(6).AsString = result.ResultType.Local?.System ?? "";
      OBX.Field(4).AsString = result.SubId ?? ""; ;
      OBX.Insert(5, result.Value);
      OBX.Field(6).AsString = result.Units ?? ""; ;
      OBX.Field(7).AsString = result.ReferenceRange ?? ""; ;
      OBX.Field(11).AsString = result.Status;
      if (result.ObservationDateTime.HasValue)
      {
        OBX.Field(14).Convert.DateTime.SetDateTimeOffset(result.ObservationDateTime.Value, true);
      }
      if (result.ProducerId is null)
      {
        OBX.Field(15).AsString = "\"\"";
      }
      else
      {
        OBX.Field(15).Component(1).AsString = result.ProducerId?.Local?.Value ?? "";
        OBX.Field(15).Component(2).AsString = result.ProducerId?.Local?.Description ?? "";
        OBX.Field(15).Component(3).AsString = result.ProducerId?.Local?.System ?? "";
        OBX.Field(15).Component(4).AsString = result.ProducerId?.International?.Value ?? "";
        OBX.Field(15).Component(5).AsString = result.ProducerId?.International?.Description ?? "";
        OBX.Field(15).Component(6).AsString = result.ProducerId?.International?.System ?? "";
      }


      return OBX;

    }
  }
}
