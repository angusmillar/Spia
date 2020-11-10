

## Standards for Pathology Informatics in Australia (SPIA) Exemplar Reports ##

[The Royal College of Pathologists of Australasia (RCPA)](https://www.rcpa.edu.au/Home) as part of their [Standards for Pathology Informatics in Australia (SPIA)](https://www.rcpa.edu.au/Library/Practising-Pathology/PTIS) working group has produced several pathology exemplar reports in a .pdf format. These reports are intended to showcase the desired elements and display format for pathology reports within Australia to help guide and inform the Australia Pathology sector. 

The .NET Framework solution found in this GitHub repository seeks to produce a set accompanying electronic formats for the SPIA exemplar reports. The solution outputs the reports listed below is the following HL7 formats :

**HL7 Output Formats:**
* HL7 Version 2 ORU Messages as per the HL7 Australia profile: [HL7AUSD-STD-OO-ADRM-2018.1](https://confluence.hl7australia.com/display/OOADRM20181/Australian+Diagnostics+and+Referral+Messaging+-+Localisation+of+HL7+Version+2.4).
* HL7 FHIR Bundle as per the Australian Digital Health Agency (ADHA) Diagnostic Report profile (Still in development): [Diagnostic Report 1.0.0 (R4) June 2020](https://github.com/AuDigitalHealth/ci-fhir-r4/releases)
* HL7 CDA as per the Australian Digital Health Agency (ADHA) Pathology Report specifications: [eHealth Pathology Report v1.2.2](https://developer.digitalhealth.gov.au/specifications/clinical-documents/ep-2558-2017)

**SPIA Exemplar Pathology Report types:**

* Blood Gas Arterial & Venous v0.8.pdf
* Chlamydia trachomatis nucleic acid v1.6
* Electrolytes Urea Creatinine v1.5
* FBC v1.6
* Haemochromatosis genotyping v1.5
* HBsAb v1.5
* Immunoglobulin E v1.5
* INR v1.7
* Karyotyping v1.4
* Lipids v1.6
* MCS Urine v1.6
* Protein SPEP core v1.5
* SARS-CoV-2 NAT v0.4
* SARS-CoV-2 serology v0.3

> Please note that the SPIA Exemplar Pathology Reports use completely fictitious patient details and results

## Solution Information ##
The solution's projects are .NET Framework 4.7.2. 

*While I would have liked to use .NET Core, unfortunately, the ADHA CDA library dependencies are currently not compatible with .NET Core or .NET Standard.* 

The solution contains five projects as follows:

* **Spia.AdhaCdaGeneration:** The generation of the CDA Pathology Document as per the ADHA specifications for the My Health Record
* **Spia.AdhaCdaPackageGeneration:** The generation of the CDA Package as per the ADHA specifications for the My Health Record
* **Spia.AdhaFhirGeneration:** The generation of a pathology FHIR bundle as per the ADHA profiles which are still in development
* **Spia.AusHl7v2Generation:** The generation of HL7 v2 ORU messages as per the HL7 Australia profile: HL7AUSD-STD-OO-ADRM-2018.1
* **Spia.Runner:** A console application that outputs all files to a given folder location

**Solution Nuget parent dependencies**
* Hl7.Fhir.R4
* Nehta.VendorLibrary.CDA.Generator.v3
* Nehta.VendorLibrary.CDAPackage
* PeterPiper

## Repo owner ##
Angus Millar: angusbmillar@gmail.com
