using Spia.AusHl7v2Generation.Factory.LogicalModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PeterPiper.Hl7.V2.Model;
using Spia.AusHl7v2Generation.Model.Logical;

namespace Spia.AusHl7v2Generation.Factory.HL7.v2
{
  public delegate void LogEventMessage(string Message);
  public class Hl7v2FileGenerator
  {
    public LogEventMessage LogEventMessageDelegate;
    public void Process(string OutputPath)
    {
      this.Log("----------------------------------------------------------------------");
      this.Log("SPIA HL7 v2 Messages");                 
      //1. Chlamydia Report
      string PdfFileName = "SPIA Exemplar Report Chlamydia trachomatis nucleic acid v1.6";
      PathologyDocument PathologyDocument = PathologyDocumentFactory.GetChlamydia(PdfFileName);
      MessageMetadata MessageMetadata = MessageMetaFactory.GetMessageMetadata(PathologyDocument.ReportList.First(), PathologyDocument.Request, messageControlId: "230ABB41-932A-41C5-AFB8-BB5E90F587CF");
      IMessage HL7 = HL7v2MessageFactory.GetMessage(PathologyDocument, MessageMetadata);
      string Message = HL7.AsStringRaw;
      System.IO.File.WriteAllText($@"{OutputPath}\1 {PdfFileName}.hl7", Message);
      this.Log($"Generate: {PdfFileName}.hl7");

      //2. Colorectal Cancer Report
      //Do not currently have content for this message

      //3. Electrolytes Urea Creatinine Report
      PdfFileName = "SPIA Exemplar Report Electrolytes Urea Creatinine v1.5";
      PathologyDocument = PathologyDocumentFactory.GetEUC(PdfFileName);
      MessageMetadata = MessageMetaFactory.GetMessageMetadata(PathologyDocument.ReportList.First(), PathologyDocument.Request, messageControlId: "E48CFF2E-CF87-486B-B679-10101034AC29");
      HL7 = HL7v2MessageFactory.GetMessage(PathologyDocument, MessageMetadata);
      Message = HL7.AsStringRaw;
      System.IO.File.WriteAllText($@"{OutputPath}\3 {PdfFileName}.hl7", Message);      
      this.Log($"Generate: {PdfFileName}.hl7");

      

      //4. Full Blood Count Report
      PdfFileName = "SPIA Exemplar Report FBC v1.6";
      PathologyDocument = PathologyDocumentFactory.GetFBC(PdfFileName);
      MessageMetadata = MessageMetaFactory.GetMessageMetadata(PathologyDocument.ReportList.First(), PathologyDocument.Request, messageControlId: "43DF357B-7D41-4012-A5D0-91632F94DEC7");
      HL7 = HL7v2MessageFactory.GetMessage(PathologyDocument, MessageMetadata);
      Message = HL7.AsStringRaw;
      System.IO.File.WriteAllText($@"{OutputPath}\4 {PdfFileName}.hl7", Message);
      this.Log($"Generate: {PdfFileName}.hl7");

      //5. Haemochromatosis genotyping Report
      PdfFileName = "SPIA Exemplar Report Haemochromatosis genotyping v1.5";
      PathologyDocument = PathologyDocumentFactory.GetHFE(PdfFileName);
      MessageMetadata = MessageMetaFactory.GetMessageMetadata(PathologyDocument.ReportList.First(), PathologyDocument.Request, messageControlId: "D4358B79-1F3D-4584-97AE-0E29942F6329");
      HL7 = HL7v2MessageFactory.GetMessage(PathologyDocument, MessageMetadata);
      Message = HL7.AsStringRaw;
      System.IO.File.WriteAllText($@"{OutputPath}\5 {PdfFileName}.hl7", Message);
      this.Log($"Generate: {PdfFileName}.hl7");

      //6. HBsAb Report
      PdfFileName = "SPIA Exemplar Report HBsAb v1.5";
      PathologyDocument = PathologyDocumentFactory.GetHepBsAb(PdfFileName);
      MessageMetadata = MessageMetaFactory.GetMessageMetadata(PathologyDocument.ReportList.First(), PathologyDocument.Request, messageControlId: "140C19F2-EF6A-4992-AB27-D59BD95AB53C");
      HL7 = HL7v2MessageFactory.GetMessage(PathologyDocument, MessageMetadata);
      Message = HL7.AsStringRaw;
      System.IO.File.WriteAllText($@"{OutputPath}\6 {PdfFileName}.hl7", Message);
      this.Log($"Generate: {PdfFileName}.hl7");

      //7. Immunoglobulin E Report
      PdfFileName = "SPIA Exemplar Report Immunoglobulin E v1.5";
      PathologyDocument = PathologyDocumentFactory.GetImmunoglobulinE(PdfFileName);
      MessageMetadata = MessageMetaFactory.GetMessageMetadata(PathologyDocument.ReportList.First(), PathologyDocument.Request, messageControlId: "3FFC4EE3-528D-441E-8EF7-F4FD04036C12");
      HL7 = HL7v2MessageFactory.GetMessage(PathologyDocument, MessageMetadata);
      Message = HL7.AsStringRaw;
      System.IO.File.WriteAllText($@"{OutputPath}\7 {PdfFileName}.hl7", Message);
      this.Log($"Generate: {PdfFileName}.hl7");

      //8. INR and FBC Report
      //PdfFileName = "SPIA Exemplar Report INR and FBC v1.2";
      //PathologyDocument = PathologyDocumentFactory.GetINRAndFBC(PdfFileName);
      //MessageMetadata = MessageMetaFactory.GetMessageMetadata(PathologyDocument.ReportList.First(), PathologyDocument.Request, messageControlId: "72C9D9DB-E356-42B8-BB4E-0F9AC3354608");
      //HL7 = HL7v2MessageFactory.GetMessage(PathologyDocument, MessageMetadata);
      //Message = HL7.AsStringRaw;
      //System.IO.File.WriteAllText($@"{OutputPath}\8 {PdfFileName}.hl7", Message);

      //9. INR Report
      PdfFileName = "SPIA Exemplar Report INR v1.7";
      PathologyDocument = PathologyDocumentFactory.GetINR(PdfFileName);
      MessageMetadata = MessageMetaFactory.GetMessageMetadata(PathologyDocument.ReportList.First(), PathologyDocument.Request, messageControlId: "944A5BD3-82E5-47B2-B39F-035A05C776A9");
      HL7 = HL7v2MessageFactory.GetMessage(PathologyDocument, MessageMetadata);
      Message = HL7.AsStringRaw;
      System.IO.File.WriteAllText($@"{OutputPath}\9 {PdfFileName}.hl7", Message);
      this.Log($"Generate: {PdfFileName}.hl7");

      //10. Karyotyping Report
      PdfFileName = "SPIA Exemplar Report Karyotyping v1.4";
      PathologyDocument = PathologyDocumentFactory.GetKaryotyping(PdfFileName);
      MessageMetadata = MessageMetaFactory.GetMessageMetadata(PathologyDocument.ReportList.First(), PathologyDocument.Request, messageControlId: "FA1BC49F-8EB1-4B40-AB9F-4A333E64991F");
      HL7 = HL7v2MessageFactory.GetMessage(PathologyDocument, MessageMetadata);
      Message = HL7.AsStringRaw;
      System.IO.File.WriteAllText($@"{OutputPath}\10 {PdfFileName}.hl7", Message);
      this.Log($"Generate: {PdfFileName}.hl7");

      //11. Lipids Report
      PdfFileName = "SPIA Exemplar Report Lipids v1.6";
      PathologyDocument = PathologyDocumentFactory.GetLipids(PdfFileName);
      MessageMetadata = MessageMetaFactory.GetMessageMetadata(PathologyDocument.ReportList.First(), PathologyDocument.Request, messageControlId: "7C290AF6-8360-4062-A578-90F6E206AD32");
      HL7 = HL7v2MessageFactory.GetMessage(PathologyDocument, MessageMetadata);
      Message = HL7.AsStringRaw;
      System.IO.File.WriteAllText($@"{OutputPath}\11 {PdfFileName}.hl7", Message);
      this.Log($"Generate: {PdfFileName}.hl7");

      //12. MCS Urine Report
      PdfFileName = "SPIA Exemplar Report MCS Urine v1.6";
      PathologyDocument = PathologyDocumentFactory.GetMSU(PdfFileName);
      MessageMetadata = MessageMetaFactory.GetMessageMetadata(PathologyDocument.ReportList.First(), PathologyDocument.Request, messageControlId: "69444795-3145-4ADC-8FE9-AB25A7E6BECA");
      HL7 = HL7v2MessageFactory.GetMessage(PathologyDocument, MessageMetadata);
      Message = HL7.AsStringRaw;
      System.IO.File.WriteAllText($@"{OutputPath}\12 {PdfFileName}.hl7", Message);
      this.Log($"Generate: {PdfFileName}.hl7");

      //13. Protein SPEP core Report
      PdfFileName = "SPIA Exemplar Report Protein SPEP core v1.5";
      PathologyDocument = PathologyDocumentFactory.GetProteinElectrophoresis(PdfFileName);
      MessageMetadata = MessageMetaFactory.GetMessageMetadata(PathologyDocument.ReportList.First(), PathologyDocument.Request, messageControlId: "0FC48495-2F73-4854-80A6-145111A68B28");
      HL7 = HL7v2MessageFactory.GetMessage(PathologyDocument, MessageMetadata);
      Message = HL7.AsStringRaw;
      System.IO.File.WriteAllText($@"{OutputPath}\13 {PdfFileName}.hl7", Message);
      this.Log($"Generate: {PdfFileName}.hl7");

      //14. Blood Gas Arterial & Venous Report
      PdfFileName = "SPIA Exemplar Report Blood Gas Arterial & Venous v0.8";
      PathologyDocument = PathologyDocumentFactory.GetBloodGasArterial(PdfFileName);
      MessageMetadata = MessageMetaFactory.GetMessageMetadata(PathologyDocument.ReportList.First(), PathologyDocument.Request, messageControlId: "4D568DF9-B697-454E-B601-32E5B708827C");
      HL7 = HL7v2MessageFactory.GetMessage(PathologyDocument, MessageMetadata);
      Message = HL7.AsStringRaw;
      System.IO.File.WriteAllText($@"{OutputPath}\14 {PdfFileName}.hl7", Message);
      this.Log($"Generate: {PdfFileName}.hl7");

      //15. SARS-CoV-2 NAT Report 
      PdfFileName = "SPIA Exemplar Report SARS-CoV-2 NAT v0.4";
      PathologyDocument = PathologyDocumentFactory.GetSARSCoV2NAT(PdfFileName);
      MessageMetadata = MessageMetaFactory.GetMessageMetadata(PathologyDocument.ReportList.First(), PathologyDocument.Request, messageControlId: "C0B60050-4A7B-425F-B156-CC07EBC3EE80");
      HL7 = HL7v2MessageFactory.GetMessage(PathologyDocument, MessageMetadata);
      Message = HL7.AsStringRaw;
      System.IO.File.WriteAllText($@"{OutputPath}\15 {PdfFileName}.hl7", Message);
      this.Log($"Generate: {PdfFileName}.hl7");

      //16. SARS-CoV-2 serology Report 
      PdfFileName = "SPIA Exemplar Report SARS-CoV-2 serology v0.3";
      PathologyDocument = PathologyDocumentFactory.GetSARSCoV2Serology(PdfFileName);
      MessageMetadata = MessageMetaFactory.GetMessageMetadata(PathologyDocument.ReportList.First(), PathologyDocument.Request, messageControlId: "4D568DF9-B697-454E-B601-32E5B708827C");
      HL7 = HL7v2MessageFactory.GetMessage(PathologyDocument, MessageMetadata);
      Message = HL7.AsStringRaw;
      System.IO.File.WriteAllText($@"{OutputPath}\16 {PdfFileName}.hl7", Message);
      this.Log($"Generate: {PdfFileName}.hl7");
    }

    private void Log(string messagae)
    {
      if (this.LogEventMessageDelegate != null)
      {
        LogEventMessageDelegate.Invoke(messagae);
      }
    }
  }
}
