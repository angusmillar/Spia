using Spia.AusHl7v2Generation.Model.Logical;
using System;
using System.Collections.Generic;
using System.Text;

namespace Spia.AusHl7v2Generation.Factory.LogicalModel
{
  public static class RequestFactory
  {
    public static Request GetChlamydiaRequest()
    {
      string ReceivingFacilityNamespaceId = "Antenatal Clinic Sunrise Hospital";
      string RequestingDoctorsMedicareProviderNumber = "8203015Y";

      var RequestingProvider = RequestingDoctorTrishFamilyDr(RequestingDoctorsMedicareProviderNumber);

      var OrderNumber = GetOrderNumberEI("00000001");

      var RequestedDateTime = new DateTimeOffset(2019, 09, 23, 00, 00, 00, TimeSpan.FromHours(10));

      var ReceivingApplication = new HierarchicDesignator("Best Practice 1.8.5.743");
      var ReceivingFacility = new HierarchicDesignator(ReceivingFacilityNamespaceId) { UniversalId = "143569C9-8AFC-4BBD-A663-95079AE10B57", UniversalIdType = "GUID" };

      var Request = new Request(ReceivingApplication, ReceivingFacility, RequestingProvider, OrderNumber, RequestedDateTime);
      Request.GroupOrderNumber = new EntityIdentifier("20000001", "PathologyGroupOrder");

      Request.ClinicalNotes = "First trimester antenatal screen, ~ 10 weeks pregnant (G1P0)";

      //Copy Doctors
      var CopyProviderOne = new Provider(new Name("Midwife", "Bianca") { TypeCode = "L" });
      CopyProviderOne.IdentifierList.Add(new Identifier("MIDBIA") { AssigingAuthority = $"NATA{LabInfo.NATAAccNumber}" });
      Request.CopyProviderList.Add(CopyProviderOne);

      //Call the person that made the order
      Request.CallBackPhoneNumber = "07302308594";

      return Request;
    }
    public static Request GetEUC()
    {
      string ReceivingFacilityNamespaceId = "Dermatology Clinic";
      string RequestingDoctorsMedicareProviderNumber = "9885728J";

      var RequestingProvider = RequestingDoctorTrishFamilyDr(RequestingDoctorsMedicareProviderNumber);

      var OrderNumber = GetOrderNumberEI("00000002");

      var RequestedDateTime = new DateTimeOffset(2019, 11, 30, 00, 00, 00, TimeSpan.FromHours(10));

      var ReceivingApplication = new HierarchicDesignator("Best Practice 1.8.5.743");
      var ReceivingFacility = new HierarchicDesignator(ReceivingFacilityNamespaceId) { UniversalId = "D101F20B-1453-47A1-AD3F-A2845964A84E", UniversalIdType = "GUID" };

      var Request = new Request(ReceivingApplication, ReceivingFacility, RequestingProvider, OrderNumber, RequestedDateTime)
      {
        GroupOrderNumber = new EntityIdentifier("20000002", "PathologyGroupOrder"),
        ClinicalNotes = "Recent fungal infection R toes"
      };

      //Copy Doctors
      var CopyProviderOne = new Provider(new Name("Dermatology Clinic", "") { TypeCode = "L" });
      CopyProviderOne.IdentifierList.Add(new Identifier("DERMCLI") { AssigingAuthority = $"NATA{LabInfo.NATAAccNumber}" });
      Request.CopyProviderList.Add(CopyProviderOne);

      var CopyProviderTwo = new Provider(new Name("Dermatologist", "Brendan") { Title = "Dr", TypeCode = "L" });
      CopyProviderTwo.IdentifierList.Add(new Identifier("DERMBRE") { AssigingAuthority = $"NATA{LabInfo.NATAAccNumber}" });
      Request.CopyProviderList.Add(CopyProviderTwo);

      //Call the person that made the order
      Request.CallBackPhoneNumber = "07302308594";

      return Request;
    }
    public static Request GetFBC1Request()
    {
      string ReceivingFacilityNamespaceId = "Coagulation & Thrombosis Clinic";
      string RequestingDoctorsMedicareProviderNumber = "283530KX";

      var RequestingProvider = RequestingDoctorTrishFamilyDr(RequestingDoctorsMedicareProviderNumber);

      var OrderNumber = GetOrderNumberEI("00000004");
      var RequestedDateTime = new DateTimeOffset(2019, 07, 29, 00, 00, 00, TimeSpan.FromHours(10));

      var ReceivingApplication = new HierarchicDesignator("Best Practice 1.8.5.743");
      var ReceivingFacility = new HierarchicDesignator(ReceivingFacilityNamespaceId) { UniversalId = "264C8EF6-868F-49B0-A532-B47D03F1A8D7", UniversalIdType = "GUID" };

      var Request = new Request(ReceivingApplication, ReceivingFacility, RequestingProvider, OrderNumber, RequestedDateTime);
      Request.GroupOrderNumber = new EntityIdentifier("20000004", "PathologyGroupOrder");

      Request.ClinicalNotes = "Warfarin 6mg per day, Family Hx Diabetes";

      //Copy Doctors
      var CopyProviderOne = new Provider(new Name("Coagulation & Thrombosis Clinic", "") { TypeCode = "L" });
      CopyProviderOne.IdentifierList.Add(new Identifier("COAGTHROMCL") { AssigingAuthority = $"NATA{LabInfo.NATAAccNumber}" });
      Request.CopyProviderList.Add(CopyProviderOne);

      var CopyProviderTwo = new Provider(new Name("Cardiologist", "Bill") { Title = "Dr", TypeCode = "L" });
      CopyProviderTwo.IdentifierList.Add(new Identifier("RHEUREB") { AssigingAuthority = $"NATA{LabInfo.NATAAccNumber}" });
      CopyProviderTwo.IdentifierList.Add(new Identifier("243089UJ") { AssigingAuthority = "AUSHICPR" });
      Request.CopyProviderList.Add(CopyProviderTwo);

      //Call the person that made the order
      Request.CallBackPhoneNumber = "07302308594";

      return Request;
    }
    public static Request GetHFERequest()
    {
      string ReceivingFacilityNamespaceId = "Dermatology Clinic";
      string RequestingDoctorsMedicareProviderNumber = "9885728J";

      var RequestingProvider = RequestingDoctorTrishFamilyDr(RequestingDoctorsMedicareProviderNumber);

      var OrderNumber = GetOrderNumberEI("00000005");

      var RequestedDateTime = new DateTimeOffset(2019, 10, 29, 00, 00, 00, TimeSpan.FromHours(10));

      var ReceivingApplication = new HierarchicDesignator("Best Practice 1.8.5.743");
      var ReceivingFacility = new HierarchicDesignator(ReceivingFacilityNamespaceId) { UniversalId = "D101F20B-1453-47A1-AD3F-A2845964A84E", UniversalIdType = "GUID" };

      var Request = new Request(ReceivingApplication, ReceivingFacility, RequestingProvider, OrderNumber, RequestedDateTime);
      Request.GroupOrderNumber = new EntityIdentifier("20000005", "PathologyGroupOrder");

      Request.ClinicalNotes = "? Hereditary haemochromatosis. Arthritis, increased iron stores.";

      //Copy Doctors
      var CopyProviderOne = new Provider(new Name("Genetics Clinic", "") { TypeCode = "L" });
      CopyProviderOne.IdentifierList.Add(new Identifier("GENCLI") { AssigingAuthority = $"NATA{LabInfo.NATAAccNumber}" });
      Request.CopyProviderList.Add(CopyProviderOne);

      var CopyProviderTwo = new Provider(new Name("Genetic Counsellor", "Bjorn ") { Title = "Dr", TypeCode = "L" });
      CopyProviderTwo.IdentifierList.Add(new Identifier("BJGC") { AssigingAuthority = $"NATA{LabInfo.NATAAccNumber}" });
      Request.CopyProviderList.Add(CopyProviderTwo);

      //Call the person that made the order
      Request.CallBackPhoneNumber = "07302308594";

      return Request;
    }
    public static Request GetHepBsAbRequest()
    {
      string ReceivingFacilityNamespaceId = "Antenatal Clinic Sunrise Hospital ";
      string RequestingDoctorsMedicareProviderNumber = "8203015Y";

      var RequestingProvider = RequestingDoctorTrishFamilyDr(RequestingDoctorsMedicareProviderNumber);

      var OrderNumber = GetOrderNumberEI("00000006");

      var RequestedDateTime = new DateTimeOffset(2019, 09, 23, 00, 00, 00, TimeSpan.FromHours(10));

      var ReceivingApplication = new HierarchicDesignator("Best Practice 1.8.5.743");
      var ReceivingFacility = new HierarchicDesignator(ReceivingFacilityNamespaceId) { UniversalId = "143569C9-8AFC-4BBD-A663-95079AE10B57", UniversalIdType = "GUID" };

      var Request = new Request(ReceivingApplication, ReceivingFacility, RequestingProvider, OrderNumber, RequestedDateTime);
      Request.GroupOrderNumber = new EntityIdentifier("20000006", "PathologyGroupOrder");

      Request.ClinicalNotes = "First trimester antenatal screen, ~ 10 weeks pregnant (G1P0).";

      //Copy Doctors
      var CopyProviderOne = new Provider(new Name("GPMidwife", "Bianca") { Title = "DR", TypeCode = "L" });
      CopyProviderOne.IdentifierList.Add(new Identifier("GPMBIA") { AssigingAuthority = $"NATA{LabInfo.NATAAccNumber}" });
      Request.CopyProviderList.Add(CopyProviderOne);

      //Call the person that made the order
      Request.CallBackPhoneNumber = "07302308594";

      return Request;
    }
    public static Request GetImmunoglobulinERequest()
    {
      string ReceivingFacilityNamespaceId = "Sunrise Hospital Medical Records";
      string RequestingDoctorsMedicareProviderNumber = "1783879L";

      var RequestingProvider = RequestingDoctorTrishFamilyDr(RequestingDoctorsMedicareProviderNumber);

      var OrderNumber = GetOrderNumberEI("00000007");

      var RequestedDateTime = new DateTimeOffset(2019, 11, 23, 00, 00, 00, TimeSpan.FromHours(10));

      var ReceivingApplication = new HierarchicDesignator("Best Practice 1.8.5.743");
      var ReceivingFacility = new HierarchicDesignator(ReceivingFacilityNamespaceId) { UniversalId = "0FECFC6C-C98F-4625-B58B-ECB27063DAF1", UniversalIdType = "GUID" };

      var Request = new Request(ReceivingApplication, ReceivingFacility, RequestingProvider, OrderNumber, RequestedDateTime);
      Request.GroupOrderNumber = new EntityIdentifier("20000007", "PathologyGroupOrder");

      Request.ClinicalNotes = "Mild reaction to bee sting. Asthma.";

      //Copy Doctors
      var CopyProviderOne = new Provider(new Name("Allergy Clinic", "") { TypeCode = "L" });
      CopyProviderOne.IdentifierList.Add(new Identifier("ALLERGYCLI") { AssigingAuthority = "SUPER-LIS" });
      Request.CopyProviderList.Add(CopyProviderOne);

      var CopyProviderTwo = new Provider(new Name("Immunologist", "Beula") { Title = "Dr", TypeCode = "L" });
      CopyProviderTwo.IdentifierList.Add(new Identifier("IMMUNBE") { AssigingAuthority = "SUPER-LIS" });
      CopyProviderTwo.IdentifierList.Add(new Identifier("823290QF") { AssigingAuthority = "AUSHICPR" });

      Request.CopyProviderList.Add(CopyProviderTwo);

      //Call the person that made the order
      Request.CallBackPhoneNumber = "07302308594";

      return Request;
    }
    public static Request GetInrFbc()
    {
      string ReceivingFacilityNamespaceId = "Coagulation & Thrombosis Clinic";
      string RequestingDoctorsMedicareProviderNumber = "283530KX";

      var RequestingProvider = RequestingDoctorTrishFamilyDr(RequestingDoctorsMedicareProviderNumber);

      var OrderNumber = GetOrderNumberEI("00000008");

      var RequestedDateTime = new DateTimeOffset(2019, 07, 29, 06, 00, 00, TimeSpan.FromHours(10));

      var ReceivingApplication = new HierarchicDesignator("Best Practice 1.8.5.743");
      var ReceivingFacility = new HierarchicDesignator(ReceivingFacilityNamespaceId) { UniversalId = "264C8EF6-868F-49B0-A532-B47D03F1A8D7", UniversalIdType = "GUID" };

      var Request = new Request(ReceivingApplication, ReceivingFacility, RequestingProvider, OrderNumber, RequestedDateTime);
      Request.GroupOrderNumber = new EntityIdentifier("20000008", "PathologyGroupOrder");

      Request.ClinicalNotes = "Warfarin 6mg per day, Family Hx Diabetes.";

      //Copy Doctors     
      var CopyProviderOne = new Provider(new Name("Coagulation & Thrombosis Clinic", "") { TypeCode = "L" });
      CopyProviderOne.IdentifierList.Add(new Identifier("COAGTHRCL") { AssigingAuthority = $"NATA{LabInfo.NATAAccNumber}" });
      Request.CopyProviderList.Add(CopyProviderOne);

      var CopyProviderTwo = new Provider(new Name("Cardiologist", "Bill") { Title = "Dr", TypeCode = "L" });
      CopyProviderTwo.IdentifierList.Add(new Identifier("RHEUREB") { AssigingAuthority = $"NATA{LabInfo.NATAAccNumber}" });
      CopyProviderTwo.IdentifierList.Add(new Identifier("243089UJ") { AssigingAuthority = "AUSHICPR" });
      Request.CopyProviderList.Add(CopyProviderTwo);

      //Call the person that made the order
      Request.CallBackPhoneNumber = "07302308594";

      return Request;
    }
    public static Request GetInr()
    {
      string ReceivingFacilityNamespaceId = "Coagulation & Thrombosis Clinic";
      string RequestingDoctorsMedicareProviderNumber = "283530KX";

      var RequestingProvider = RequestingDoctorTrishFamilyDr(RequestingDoctorsMedicareProviderNumber);

      var OrderNumber = GetOrderNumberEI("00000009");

      var RequestedDateTime = new DateTimeOffset(2019, 07, 29, 00, 00, 00, TimeSpan.FromHours(10));

      var ReceivingApplication = new HierarchicDesignator("Best Practice 1.8.5.743");
      var ReceivingFacility = new HierarchicDesignator(ReceivingFacilityNamespaceId) { UniversalId = "ACA30A38-811E-4E0D-B55C-774D38B8E171", UniversalIdType = "GUID" };

      var Request = new Request(ReceivingApplication, ReceivingFacility, RequestingProvider, OrderNumber, RequestedDateTime);
      Request.GroupOrderNumber = new EntityIdentifier("20000009", "PathologyGroupOrder");

      Request.ClinicalNotes = "Warfarin 6mg per day, Family Hx Diabetes.";

      //Copy Doctors     
      var CopyProviderOne = new Provider(new Name("Coagulation & Thrombosis Clinic", "") { TypeCode = "L" });
      CopyProviderOne.IdentifierList.Add(new Identifier("COAGTHRCL") { AssigingAuthority = $"NATA{LabInfo.NATAAccNumber}" });
      Request.CopyProviderList.Add(CopyProviderOne);

      var CopyProviderTwo = new Provider(new Name("Cardiologist", "Bill") { Title = "Dr", TypeCode = "L" });
      CopyProviderTwo.IdentifierList.Add(new Identifier("RHEUREB") { AssigingAuthority = $"NATA{LabInfo.NATAAccNumber}" });
      CopyProviderTwo.IdentifierList.Add(new Identifier("243089UJ") { AssigingAuthority = "AUSHICPR" });
      Request.CopyProviderList.Add(CopyProviderTwo);

      //Call the person that made the order
      Request.CallBackPhoneNumber = "07302308594";

      return Request;
    }
    public static Request GetKaryotypingRequest()
    {
      string ReceivingFacilityNamespaceId = "Infertility Clinic";
      string RequestingDoctorsMedicareProviderNumber = "951577QT";

      var RequestingProvider = RequestingDoctorTrishFamilyDr(RequestingDoctorsMedicareProviderNumber);

      var OrderNumber = GetOrderNumberEI("00000010");

      var RequestedDateTime = new DateTimeOffset(2019, 11, 20, 00, 00, 00, TimeSpan.FromHours(10));

      var ReceivingApplication = new HierarchicDesignator("Best Practice 1.8.5.743");
      var ReceivingFacility = new HierarchicDesignator(ReceivingFacilityNamespaceId) { UniversalId = "501970A0-1E1F-40D8-9656-61899527203F", UniversalIdType = "GUID" };

      var Request = new Request(ReceivingApplication, ReceivingFacility, RequestingProvider, OrderNumber, RequestedDateTime);
      Request.GroupOrderNumber = new EntityIdentifier("20000010", "PathologyGroupOrder");

      Request.ClinicalNotes = "Primary infertility";

      //Copy Doctors
      var CopyProviderOne = new Provider(new Name("Genetic Counselling Clinic coordinator", "") { TypeCode = "L" });
      CopyProviderOne.IdentifierList.Add(new Identifier("GCCC") { AssigingAuthority = $"NATA{LabInfo.NATAAccNumber}" });
      Request.CopyProviderList.Add(CopyProviderOne);

      var CopyProviderTwo = new Provider(new Name("Sarsgaard", "Genetic Counsellor") { Title = "Dr", TypeCode = "L" });
      CopyProviderTwo.IdentifierList.Add(new Identifier("SGC") { AssigingAuthority = $"NATA{LabInfo.NATAAccNumber}" });
      CopyProviderTwo.IdentifierList.Add(new Identifier("7771139A") { AssigingAuthority = "AUSHICPR" });
      CopyProviderTwo.IdentifierList.Add(new Identifier("8003619113297912") { AssigingAuthority = "AUSHIC", Type = "NPI" });
      Request.CopyProviderList.Add(CopyProviderTwo);

      //Call the person that made the order
      Request.CallBackPhoneNumber = "07302308594";

      return Request;
    }
    public static Request GetLipids()
    {
      string ReceivingFacilityNamespaceId = "Cardiology Clinic";
      string RequestingDoctorsMedicareProviderNumber = "3243890Y";

      var RequestingProvider = RequestingDoctorTrishFamilyDr(RequestingDoctorsMedicareProviderNumber);

      var OrderNumber = GetOrderNumberEI("00000011");

      var RequestedDateTime = new DateTimeOffset(2019, 11, 09, 00, 00, 00, TimeSpan.FromHours(10));

      var ReceivingApplication = new HierarchicDesignator("Best Practice 1.8.5.743");
      var ReceivingFacility = new HierarchicDesignator(ReceivingFacilityNamespaceId) { UniversalId = "55023B97-61F8-4445-8590-4F18AD68E9AD", UniversalIdType = "GUID" };

      var Request = new Request(ReceivingApplication, ReceivingFacility, RequestingProvider, OrderNumber, RequestedDateTime);
      Request.GroupOrderNumber = new EntityIdentifier("20000011", "PathologyGroupOrder");

      Request.ClinicalNotes = "Knee surgery 12-Sept-19. Weight gain becoming problematic.";

      //Copy Doctors
      var CopyProviderOne = new Provider(new Name("Cardiology Clinic, Sunshine Hospital", "") { TypeCode = "L" });
      CopyProviderOne.IdentifierList.Add(new Identifier("CCSH") { AssigingAuthority = $"NATA{LabInfo.NATAAccNumber}" });
      Request.CopyProviderList.Add(CopyProviderOne);

      //ToDo: Should this doctor have a full given name?
      var CopyProviderTwo = new Provider(new Name("Bastien", "G") { Title = "Dr", TypeCode = "L" });
      CopyProviderTwo.IdentifierList.Add(new Identifier("BASTG") { AssigingAuthority = $"NATA{LabInfo.NATAAccNumber}" });
      CopyProviderTwo.IdentifierList.Add(new Identifier("1582964K") { AssigingAuthority = "AUSHICPR" });
      CopyProviderTwo.IdentifierList.Add(new Identifier("8003617895300367") { AssigingAuthority = "AUSHIC", Type = "NPI" });
      Request.CopyProviderList.Add(CopyProviderTwo);

      //Call the person that made the order
      Request.CallBackPhoneNumber = "07302308594";

      return Request;
    }
    public static Request GetMSURequest()
    {
      string ReceivingFacilityNamespaceId = "Doctor In The House Surgery";
      string RequestingDoctorsMedicareProviderNumber = "8264815W";

      var RequestingProvider = RequestingDoctorTrishFamilyDr(RequestingDoctorsMedicareProviderNumber);

      var OrderNumber = GetOrderNumberEI("00000012");

      var RequestedDateTime = new DateTimeOffset(2020, 01, 22, 00, 00, 00, TimeSpan.FromHours(10));

      var ReceivingApplication = new HierarchicDesignator("Best Practice 1.8.5.743");
      var ReceivingFacility = new HierarchicDesignator(ReceivingFacilityNamespaceId) { UniversalId = "191394B8-6AB0-4EF5-BC7D-19A5B37FA60F", UniversalIdType = "GUID" };

      var Request = new Request(ReceivingApplication, ReceivingFacility, RequestingProvider, OrderNumber, RequestedDateTime);
      Request.GroupOrderNumber = new EntityIdentifier("20000012", "PathologyGroupOrder");

      Request.ClinicalNotes = "Diabetic with recurring UTI. Last course Trimethoprim (100 mg bd) completed 27 Nov 2019.";

      //Copy Doctors
      var CopyProviderOne = new Provider(new Name("Rheumatology Clinic", "") { TypeCode = "L" });
      CopyProviderOne.IdentifierList.Add(new Identifier("RHEUCL") { AssigingAuthority = $"NATA{LabInfo.NATAAccNumber}" });
      Request.CopyProviderList.Add(CopyProviderOne);

      var CopyProviderTwo = new Provider(new Name("Rheumatologist", "Rebecca") { Title = "Dr", TypeCode = "L" });
      CopyProviderTwo.IdentifierList.Add(new Identifier("RHEUREB") { AssigingAuthority = $"NATA{LabInfo.NATAAccNumber}" });
      CopyProviderTwo.IdentifierList.Add(new Identifier("582859VL") { AssigingAuthority = "AUSHICPR" });
      CopyProviderTwo.IdentifierList.Add(new Identifier("8003616866307674") { AssigingAuthority = "AUSHIC", Type = "NPI" });
      Request.CopyProviderList.Add(CopyProviderTwo);

      //Call the person that made the order
      Request.CallBackPhoneNumber = "07302308594";

      return Request;
    }
    public static Request GetProteinElectrophoresis()
    {
      string ReceivingFacilityNamespaceId = "Immunology Clinic";
      string RequestingDoctorsMedicareProviderNumber = "8264815W";

      var RequestingProvider = RequestingDoctorTrishFamilyDr(RequestingDoctorsMedicareProviderNumber);

      var OrderNumber = GetOrderNumberEI("00000013");

      var RequestedDateTime = new DateTimeOffset(2019, 11, 3, 00, 00, 00, TimeSpan.FromHours(10));

      var ReceivingApplication = new HierarchicDesignator("Best Practice 1.8.5.743");
      var ReceivingFacility = new HierarchicDesignator(ReceivingFacilityNamespaceId) { UniversalId = "D220F4D1-D62C-4EE3-8356-F5DA1484370E", UniversalIdType = "GUID" };

      var Request = new Request(ReceivingApplication, ReceivingFacility, RequestingProvider, OrderNumber, RequestedDateTime);
      Request.GroupOrderNumber = new EntityIdentifier("20000013", "PathologyGroupOrder");

      Request.ClinicalNotes = "? Multiple myeloma";

      //Copy Doctors
      var CopyProviderOne = new Provider(new Name("Immunology Clinic", ""));
      CopyProviderOne.IdentifierList.Add(new Identifier("IMMC") { AssigingAuthority = $"NATA{LabInfo.NATAAccNumber}" });
      Request.CopyProviderList.Add(CopyProviderOne);

      var CopyProviderTwo = new Provider(new Name("Immunologist", "Armande") { Title = "Dr", TypeCode = "L" });
      CopyProviderTwo.IdentifierList.Add(new Identifier("IMMARM") { AssigingAuthority = $"NATA{LabInfo.NATAAccNumber}" });
      CopyProviderTwo.IdentifierList.Add(new Identifier("477425AX") { AssigingAuthority = "AUSHICPR" });
      CopyProviderTwo.IdentifierList.Add(new Identifier("8003611874467495") { AssigingAuthority = "AUSHIC", Type = "NPI" });
      Request.CopyProviderList.Add(CopyProviderTwo);

      //Call the person that made the order
      Request.CallBackPhoneNumber = "07302308594";

      return Request;
    }
    public static Request GetBloodGasArterial()
    {
      string ReceivingFacilityNamespaceId = "Sunshine Hospital Emergency Dept";
      string RequestingDoctorsMedicareProviderNumber = "873721DH";

      var RequestingProvider = RequestingDoctorTrishFamilyDr(RequestingDoctorsMedicareProviderNumber);

      var OrderNumber = GetOrderNumberEI("00000014");

      var RequestedDateTime = new DateTimeOffset(2019, 11, 09, 00, 00, 00, TimeSpan.FromHours(10));

      //ToDo: Is this correct?
      var ReceivingApplication = new HierarchicDesignator("Best Practice 1.8.5.743");
      var ReceivingFacility = new HierarchicDesignator(ReceivingFacilityNamespaceId) { UniversalId = "171E5425-175A-4AEB-8D55-7161148DE6F0", UniversalIdType = "GUID" };

      var Request = new Request(ReceivingApplication, ReceivingFacility, RequestingProvider, OrderNumber, RequestedDateTime);
      Request.GroupOrderNumber = new EntityIdentifier("20000014", "PathologyGroupOrder");

      Request.ClinicalNotes = "Recent history uncontrolled diabetes; Shortness of breath";

      //Copy Doctors
      var CopyProviderOne = new Provider(new Name("Diabetes Clinic Sunshine Hospital", ""));
      CopyProviderOne.IdentifierList.Add(new Identifier("DCSH") { AssigingAuthority = $"NATA{LabInfo.NATAAccNumber}" });
      Request.CopyProviderList.Add(CopyProviderOne);
     
      var CopyProviderTwo = RequestingDoctorTrishFamilyDr("930481AT");
      
      //Should Be: Dr Trish FamilyDr, 97 Piazza Parade, Sunrise Beach
      Request.CopyProviderList.Add(CopyProviderTwo);

      //Call the person that made the order
      //Request.CallBackPhoneNumber = "07302308594";

      return Request;
    }
    public static Request GetSARSCoV2NAT()
    {
      string ReceivingFacilityNamespaceId = "Fever Clinic Sunrise Hospital";
      string RequestingDoctorsMedicareProviderNumber = "603107KW";

      var RequestingProvider = RequestingDoctorTrishFamilyDr(RequestingDoctorsMedicareProviderNumber);

      var OrderNumber = GetOrderNumberEI("00000015");

      var RequestedDateTime = new DateTimeOffset(2020, 08, 20, 00, 00, 00, TimeSpan.FromHours(10));
      
      var ReceivingApplication = new HierarchicDesignator("Best Practice 1.8.5.743"); 
      var ReceivingFacility = new HierarchicDesignator(ReceivingFacilityNamespaceId) { UniversalId = "3659F40F-8BDF-4CD6-BF46-38257CA6BB97", UniversalIdType = "GUID" };

      var Request = new Request(ReceivingApplication, ReceivingFacility, RequestingProvider, OrderNumber, RequestedDateTime);
      Request.GroupOrderNumber = new EntityIdentifier("20000015", "PathologyGroupOrder");

      Request.ClinicalNotes = "Sore throat & cough, works in hospitality industry";

      //Copy Doctors
      var CopyProviderOne = new Provider(new Name("My Specialist", ""));
      CopyProviderOne.IdentifierList.Add(new Identifier("MYSPEC") { AssigingAuthority = $"NATA{LabInfo.NATAAccNumber}" });
      Request.CopyProviderList.Add(CopyProviderOne);            

      //Call the person that made the order
      //Request.CallBackPhoneNumber = "07302308594";

      return Request;
    }
    public static Request GetSARSCoV2Serology()
    {
      string ReceivingFacilityNamespaceId = "Respiratory Clinic Sunrise Hospital";
      string RequestingDoctorsMedicareProviderNumber = "067709AH";

      var RequestingProvider = RequestingDoctorTrishFamilyDr(RequestingDoctorsMedicareProviderNumber);

      var OrderNumber = GetOrderNumberEI("00000016");
      
      var RequestedDateTime = new DateTimeOffset(2020, 7, 24, 00, 00, 00, TimeSpan.FromHours(10));
      
      var ReceivingApplication = new HierarchicDesignator("Best Practice 1.8.5.743");
      var ReceivingFacility = new HierarchicDesignator(ReceivingFacilityNamespaceId) { UniversalId = "13A8DC14-A1E9-475C-9B4C-DA19866E020A", UniversalIdType = "GUID" };

      var Request = new Request(ReceivingApplication, ReceivingFacility, RequestingProvider, OrderNumber, RequestedDateTime);
      Request.GroupOrderNumber = new EntityIdentifier("20000016", "PathologyGroupOrder");

      Request.ClinicalNotes = "Shortness of breath, sore throat";

      //Copy Doctors
      var CopyProviderOne = new Provider(new Name("My Physio", ""));
      CopyProviderOne.IdentifierList.Add(new Identifier("MYPHY") { AssigingAuthority = $"NATA{LabInfo.NATAAccNumber}" });
      Request.CopyProviderList.Add(CopyProviderOne);

      //Call the person that made the order
      //Request.CallBackPhoneNumber = "07302308594";

      return Request;
    }
    

    private static Provider RequestingDoctorTrishFamilyDr(string MedicareProviderNumber)
    {
      var RequestingProvider = new Provider(new Name("Familydr", "Trish") { Title = "Dr", TypeCode = "L" });
      RequestingProvider.IdentifierList.Add(new Identifier("FAMTRI") { AssigingAuthority = $"NATA{LabInfo.NATAAccNumber}" });
      RequestingProvider.IdentifierList.Add(new Identifier(MedicareProviderNumber) { AssigingAuthority = "AUSHICPR" });
      RequestingProvider.IdentifierList.Add(new Identifier("8003614242061461") { AssigingAuthority = "AUSHIC", Type = "NPI" });
      return RequestingProvider;
    }
    private static EntityIdentifier GetOrderNumberEI(string OrderNumberValue)
    {
      var OrderNumber = new EntityIdentifier(OrderNumberValue, "PathologyOrder");
      OrderNumber.UniversalId = LabInfo.NATAAccNumber;
      OrderNumber.UniversalIdType = "AUSNATA";
      return OrderNumber;
    }


  }
}
