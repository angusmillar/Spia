using System;
using System.Collections.Generic;
using System.Text;
using PeterPiper.Hl7.V2.Model;
using Spia.PathologyReportModel.Model;
using Spia.PathologyReportModel.Support;

namespace Spia.AusHl7v2Generation.Factory
{
  public class OBXFactory
  {
    private int _SetIdCounter;

    public List<ISegment> GetOBXList(string nataSiteNumber, IList<Result> resultList, int SetIdCounter = 1, int SubId = 1, bool IsChildSubIdSet = false)
    {
      _SetIdCounter = SetIdCounter;
      if (resultList is null)
        throw new ArgumentNullException(nameof(resultList));

      var OBXList = new List<ISegment>();
      int ChildSubId = 1;
      foreach (var Result in resultList)
      {
        if (Result.ChildResultList != null)
        {
          OBXList.Add(GetOBX(Result, nataSiteNumber, _SetIdCounter, SubId.ToString()));
          _SetIdCounter++;
          OBXList.AddRange(GetOBXList(nataSiteNumber, Result.ChildResultList, _SetIdCounter, SubId, IsChildSubIdSet: true));
          SubId++;
          ChildSubId = 1;
        }
        else
        {
          if (IsChildSubIdSet)
          {
            string ChildSubIdString = $"{SubId.ToString()}.{ChildSubId.ToString()}";
            OBXList.Add(GetOBX(Result, nataSiteNumber, _SetIdCounter, ChildSubIdString));
            _SetIdCounter++;
            ChildSubId++;
          }
          else
          {
            OBXList.Add(GetOBX(Result, nataSiteNumber, _SetIdCounter));
            _SetIdCounter++;
          }
        }
      }
      return OBXList;
    }

    private ISegment GetOBX(Result result, string nataSiteNumber, int SetId, string SubId = null)
    {
      if (result is null)
        throw new ArgumentNullException(nameof(result));

      var OBX = Creator.Segment("OBX");
      OBX.Field(1).AsString = SetId.ToString();
      OBX.Field(2).AsString = result.DataType;
      if (result.Type.Lonic != null)
      {
        OBX.Field(3).Component(1).AsString = result.Type.Lonic.Term;
        OBX.Field(3).Component(2).AsString = result.Type.Lonic.Description;
        OBX.Field(3).Component(3).AsString = "LN";
      }
      OBX.Field(3).Component(4).AsString = result.Type.Local.Term;
      OBX.Field(3).Component(5).AsString = result.Type.Local.Description;
      OBX.Field(3).Component(6).AsString = $"NATA{nataSiteNumber}";
      if (!string.IsNullOrWhiteSpace(SubId))
      {
        OBX.Field(4).AsString = SubId;
      }
      OBX.Element(5).AsStringRaw = result.Value ?? "";
      OBX.Field(6).AsString = result.Units ?? ""; ;
      OBX.Field(7).AsString = result.ReferenceRange ?? "";


      ResultStatusTypeSupport ResultStatusTypeSupport = new ResultStatusTypeSupport();
      if (ResultStatusTypeSupport.TryLookupByEnum(result.Status, out string ResultStatus))
      {
        OBX.Field(11).AsString = ResultStatus;
      }
      else
      {
        throw new ApplicationException($"Unable to convert the {nameof(result.Status)} of {result.Status.ToString()} to a code for the HL7 OBR-25 field.");
      }


      if (result.ObservationDateTime.HasValue)
      {
        OBX.Field(14).Convert.DateTime.SetDateTimeOffset(result.ObservationDateTime.Value, true);
      }

      OBX.Field(15).AsString = "\"\"";

      return OBX;

    }

    public ISegment GetPdfOBX(string PdfDirectoryPath, string PdfFileName, int SetId, string Status)
    {
      var OBX = Creator.Segment("OBX");
      OBX.Field(1).AsString = SetId.ToString();
      OBX.Field(2).AsString = "ED";
      //"PDF", "Display Format in PDF", $"AUSPDI"
      OBX.Field(3).Component(1).AsString = "PDF";
      OBX.Field(3).Component(2).AsString = "Display Format in PDF";
      OBX.Field(3).Component(3).AsString = "AUSPDI";

      //string ValueElementString = $"^application^pdf^Base64^[Base64 PDF content goes here]"
      OBX.Field(5).Component(2).AsString = "application";
      OBX.Field(5).Component(3).AsString = "pdf";
      OBX.Field(5).Component(4).AsString = "Base64";
      OBX.Field(5).Component(5).AsString = GetPdfBase64Content(PdfDirectoryPath, PdfFileName);
      OBX.Field(11).AsString = Status;

      OBX.Field(15).AsString = "\"\"";

      return OBX;
    }

    private string GetPdfBase64Content(string PdfDirectoryPath, string PdfFileName)
    {
      if (string.IsNullOrWhiteSpace(PdfDirectoryPath))
      {
        throw new ApplicationException($"The {nameof(PdfDirectoryPath)} is empty or null");
      }

      if (string.IsNullOrWhiteSpace(PdfFileName))
      {
        throw new ApplicationException($"The {nameof(PdfFileName)} is empty or null");
      }

      System.IO.FileInfo PdfFileinfo = new System.IO.FileInfo(System.IO.Path.Combine(PdfDirectoryPath, PdfFileName));
      if (PdfFileinfo.Exists)
      {
        return PeterPiper.Hl7.V2.Support.Tools.Base64Tools.Encoder(System.IO.File.ReadAllBytes(PdfFileinfo.FullName));
      }
      else
      {
        throw new ApplicationException($"Unable to locate the PDF attachment file at the following file path: {PdfFileinfo.FullName}");
      }
    }

  }
}
