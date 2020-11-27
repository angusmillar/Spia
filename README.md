

# Standards for Pathology Informatics in Australia (SPIA) Exemplar Reports #

[The Royal College of Pathologists of Australasia (RCPA)](https://www.rcpa.edu.au/Home) as part of their [Standards for Pathology Informatics in Australia (SPIA)](https://www.rcpa.edu.au/Library/Practising-Pathology/PTIS) working group has produced several pathology exemplar reports in a .pdf format. These reports are intended to showcase the desired elements and display format for pathology reports within Australia to help guide and inform the Australia Pathology sector. 

This .NET Framework solution produces a set accompanying electronic formats for the SPIA exemplar reports. The solution outputs the reports listed below is the following HL7 formats :

**HL7 Output Formats:**
* HL7 Version 2 ORU Messages as per the HL7 Australia profile: [HL7AUSD-STD-OO-ADRM-2018.1](https://confluence.hl7australia.com/display/OOADRM20181/Australian+Diagnostics+and+Referral+Messaging+-+Localisation+of+HL7+Version+2.4).
* HL7 FHIR Bundle as per the Australian Digital Health Agency (ADHA) Diagnostic Report profile (Still in development): [Diagnostic Report 1.0.0 (R4) June 2020](https://github.com/AuDigitalHealth/ci-fhir-r4/releases)
* HL7 CDA as per the Australian Digital Health Agency (ADHA) Pathology Report specifications: [eHealth Pathology Report v1.2.2](https://developer.digitalhealth.gov.au/specifications/clinical-documents/ep-2558-2017)

**SPIA Exemplar Pathology Report types:**

> Please note that the SPIA Exemplar Pathology Reports use completely fictitious patient details and results

* Exemplar Report Chlamydia trachomatis NAT v1.5.pdf
* Exemplar Report Chromosome studies v1.3.pdf
* Exemplar Report EPP v1.5.pdf
* Exemplar Report EUC v1.5.pdf
* Exemplar Report FBC v1.5.pdf
* Exemplar Report Haemochromatosis gene screening v1.4.pdf
* Exemplar Report HbsAb v1.4.pdf
* Exemplar Report Histo Colorectal SPRC v1.1.pdf
* Exemplar Report IgE v1.4.pdf
* Exemplar Report INR v1.6.pdf
* Exemplar Report SARS-CoV-2 Serology v0.3.pdf
* Exemplar Report SARS-CoV-2NAT v0.3.pdf
* SPIA Exemplar Report Blood Gases v1.2.pdf
* SPIA Exemplar Report Iron studies v0.2.pdf
* SPIA Exemplar Report Lipids v1.4.pdf
* SPIA Exemplar Report Urine MCS v1.5.pdf

> You can find the PDF attachments for each report of the above reports in the repository folder: `Spia\Spia.Runner\SpiaExemplarPdfReports`

# How to configure the solution #
The solution reads in a set of JSON formatted files where each contains all the required information for a single pathology report. This solution expects these files to be conformant to a custom format _(see the section below called The Pathology Logical Model (.json) file structure)_ that is agnostic to any of the formal health standards such as HL7 v2, CDA or FHIR. The custom JSON file is a logical model of a single Pathology report and is explain in detail below. Once you have created one of these files (the solution can output examples for you) the solution can read in that file, validate it is correct and provide feed back if it is not, and then output the pathology report in any of the supported formal Australian health standards as listed above in HL7 Output Formats.

To run the solution you can set the solution project named **Spia.Runner:** as the start-up project and configure how you would like it to operate by changing the properties found in the Program.cs file of that project. Here you will find a **SpiaFileGeneratorOptions** object which provides the following options:  

**SpiaFileGeneratorOptions property descriptions**
Option | Description
------------ | -------------
OutputDirectory | The parent folder path where the solution will outputted the generated content e.g. @"C:\temp\SPIAMessages\Output"
PdfAttachmentInputDirectory | The folder path where the solution will expect to find, by file name, the PDF attachments for each associated pathology report logical model JSON file. These are the full display PDFs for the pathology reports which will be encapsulate into the generated standards based output messages. e.g. @"C:\temp\SPIAMessages\PDF".  
LogicalModelInputDirectory | The folder path where the solution will read in a set of pathology report logical model JSON files e.g. @"C:\temp\SPIAMessages\LogicalModels" 
GenerateLogicalModels | A boolean (true or false) option which indicates weather the solution should generate the set of Pathology logical models (JSON files) that are hardcoded into the solution. These logical models are the RCPA's SPIA Exemplar reports as listed above. When true this setting forces the solution to output these logical model's files which you may then edit or change or use as a template to create your own completely new pathology reports. You may think of this setting as a primer to get you started with a set working examples as specified by the RCPA's working group. When this setting is true the destination folder must be empty to ensure the solution does not overwrite files you may have already manually edited.   
GenerateHL7Version2Messages | A boolean (true or false) option which indicates weather you want the solution to generate HL7 v2 messages for each pathology logical model file provided.  
GenerateFhirBundles | A boolean (true or false) option which indicates weather you want the solution to generate FHIR Bundles for each pathology logical model file provided.  
GenerateCdaDocuments | A boolean (true or false) option which indicates weather you want the solution to generate CDA Documents for each pathology logical model file provided.  
GenerateCdaPackages | A boolean (true or false) option which indicates weather you want the solution to generate CDA Packages for each pathology logical model file provided. A CDA Package is a ZIP file wrapper around a CDA document, PDF attachment and a digital signature file. This is the complete package as is expected when submitting CDA documents to the Australian My Health Record EHR XDS.b repository. Please note that when this option is true the following options are mandatory (NashCertificateSerial and CdaPackagerApproverPerson). You do not need to set GenerateCdaDocuments to true when setting this option to true.
NashCertificateSerial | When GenerateCdaPackages is true this option must be set to the serial number of the NASH Digital certificate which must be located in the windows certificate store of the same machine running the solution. The NASH certificate must be in the MyStore for the CurrentUser and not be expired. This is required to digitally sign the CDA package. 
CdaPackagerApproverPerson | When GenerateCdaPackages is true this option must be set to an instance of a `AdhaCdaPackageGeneration.Model.ApproverPerson` object which then requires the following 4 properties:  (FamilyName, GivenName, Title, Hpii), these are the details of the individual that is Approving the upload of the CDA package. A hpii or HPI-I is an Australian national identifier for a Healthcare provider individual (A registered Healthcare Provider).  


# How to run the solution #

As seen in the **Spia.Runner's** `Program.cs` file the `SpiaFileGeneratorOptions` object is passed into a newly instantiated `SpiaFileGenerator` object instance and the method `SpiaFileGenerator.Process()` is called to produce the required output.

Once run, the process will read in any *.json files found in the LogicalModelInputDirectory and validate they are in the format expected and contain all mandatory elements. If not, detailed errors message will be seen explaining the issues and the process will stop. If Validation passes the the solution will output the files as requested to the OutputDirectory. As required it will create folders within the OutputDirectory as follows: ('FHIR Bundles', 'HL7v2 Messages', 'CDA Documents', 'CDA Packages'). Each run of the process will delete all contents of these output folder before outputting new content.

# The Pathology Logical Model (.json) file structure #
Below is an full example of pathology report logical model JSON file for a 'SARS-CoV-2 Serology report' which has been annotated with comments above each property.
Please note that to use this file all the comments lines (e.g. ** This is a comment line) must be removed as JSON does not allow comments. 
> Remember you can set the **GenerateLogicalModels** option to true to have the solution generate a set of example Logical Model files like the one seen below.   
```json
{
  "pathologyReport": {
    ** The lab that is generating the report
    "performingLaboratory": {
      ** A human readable name for the Laboratory
      "facilityName": "PITUS Pathology",
      ** A computer readable code for the Laboratory
      "facilityCode": "PITUSPathology",
      ** The Laboratory's NATA Site NUmber, The National Association of Testing Authorities (NATA) accredits Laboratories for operations in Australia and issues a NATA Site number
      "nataSiteNumber": "9999",
      ** The Laboratory's National Organisation identifier - Healthcare Provider Identifier for Organisations (HPI-O) 
      "hpi-o": "8003 6299 0001 9338",
      ** The Laboratory's business phone number
      "businessPhoneNumber": "+61 2 8356 5858",
      ** A computer readable code that represents the Laboratory Information System (LIS) producing the report
      "laboratoryInformationSystemApplicationCode": "SuperLIS",
      ** The Laboratory's business address
      "address": {
        "lineOne": "Durham Hall",
        "lineTwo": "207 Albion Street",
        "suburb": "Surry Hills",
        "postCode": "2010",
        "city": null,
        "state": "NSW",
        "country": "AUS",
        "typeCode": "B"
      }
    },
    ** The pathology report's patient details
    "Patient": {
      ** The patient's name details
      "name": {
        "family": "WHITEWATER",
        "given": "George",
        "middle": "Neil",
        "title": "Mr"
      },
      ** The patient's Date of Birth
      "dateOfBirth": "1950-08-01T00:00:00",
      ** The patient's sex (A - Ambiguous, O - Other, U: Unknown, F - Female, M - Male)
      "gender": "M",
      ** The patient's addresses, many of
      "addressList": [
        {
          "lineOne": "197 Ramsay Court",
          "lineTwo": null,
          "suburb": "Sunrise Beach",
          "postCode": "4571",
          "city": "Brisbane",
          "state": "QLD",
          "country": "AUS",
          "typeCode": "H"
        }
      ],
      ** The patient's identifiers, many of
      "identifierList": [
        {
          ** The identifiers' value
          "value": "8003 6021 4585 0890",
          ** The type of identifier, can be one of (MedicareNumber, IHI, HPII, HPIO, MedicareProviderNumber, GUID, LocalToLab, MRN, DVA)
          "type": "IHI",
          ** The Assigning Authority code for the identifier, not required except for identifier of type MRN where it must be provided, as seen in  the next example.
          "assigningAuthority": null
        },
        {
          "value": "95479412",
          "type": "MRN",
          "assigningAuthority": "RespiratoryClinicSunriseHospital"
        }
      ],
      ** The patient's home phone number (Optional)
      "homePhoneNumber": "+61492813178"
    },
    ** The pathology reports request details (Mandatory)
    "Request": {
      ** The details of the facility that requested the pathology report (Mandatory)
      "requestingFacility": {
         ** The the facility's human readable name (Mandatory)
        "name": "Sunrise Hospital Respiratory Clinic",
        ** The the facility's identifier
        "identifier": {
          "value": "13A8DC14-A1E9-475C-9B4C-DA19866E020A",
          "type": "GUID",
          "assigningAuthority": null
        }
      },
      ** A code to represent the application that requested the pathology report, and to which it will return to (Mandatory)
      "requestingApplication": "Best Practice 1.8.5.743",
      ** The requesting doctor's details (Mandatory)
      "requestingProvider": {
        ** The requesting doctor's name details (Family name is Mandatory)
        "name": {
          "family": "Familydr",
          "given": "Trish",
          "middle": "",
          "title": "Dr"
        },
        ** The requesting doctor's identifier list (Mandatory)
        "identifierList": [
          ** (A HPI-I is Mandatory for CDA documents generation)
          {
            "value": "8003614242061461",
            "type": "HPII",
            "assigningAuthority": null
          },
          ** (A code local to the performing laboratory (Mandatory)
          {
            "value": "FAMTRI",
            "type": "LocalToLab",
            "assigningAuthority": null
          },
          ** (A Medicare Provider number (optional)
          {
            "value": "067709AH",
            "type": "MedicareProviderNumber",
            "assigningAuthority": null
          }
        ]
      },
      ** An order number from the system requesting the pathology report (Optional)
      "orderNumber": "00000016",
      ** Date & Time when the request was made by the requesting doctor (Mandatory)
      "requestedDateTime": "2020-07-25T00:00:00+10:00",
      ** Any Clinical notes provided by the requester, relevant to the Pathology request (Optional)
      "clinicalNotes": "Sore throat & cough, works in hospitality",
      ** Any individuals or organisations that a copy of the report has been sent to, aka 'Copy To' (Optional)
      "copyToList": [
        {
          "name": {
            "family": "My Physio",
            "given": null,
            "middle": null,
            "title": null
          },
          "identifierList": [
            {
              "value": "MYPHY",
              "type": "LocalToLab",
              "assigningAuthority": null
            }
          ]
        }
      ],
      ** A phone number to contact the requesting part on (Optional)
      "callBackPhoneNumber": null
    },
    ** The file name and extension of PDF attachment that is the pathology report to be read by the receiver (Mandatory), note this file must be found in the PdfAttachmentInputDirectory 
    "pdfFileName": "Exemplar Report SARS-CoV-2 Serology v0.3.pdf",
    ** The list of reports as found in the PDF attachment that are associated to a set of result values (Mandatory)
    ** There are unresolved issues with HL7 v2 messages which wish  to express many reports and yet only have a single PDF attachment.
    ** Do we duplicate the PDF in the message, we are not sure, at present this solution only support a single report when generating HL7 v2 messages.   
    "ReportList": [
      {
        ** The reports primary laboratory assigned identifier (Mandatory)
        "reportId": "1978881860",
        ** The reports specimen collection date & time (Mandatory)
        "collectionDateTime": "2020-07-25T18:20:00+10:00",
        ** The date & time that the specimens where Received into the laboratory (Mandatory)
        "specimenReceivedDateTime": "2020-07-25T19:32:00+10:00",
        ** The date & time that report was Released/Authorised/Finalised from the laboratory (Mandatory)
        "reportReleaseDateTime": "2020-07-26T10:36:00+10:00",
        ** The status of the report can be (F - Final, C- Correction, P - Preliminary, X - NoResultsAvailableOrderCanceled) (Mandatory)
        "reportStatus": "F",
        ** The report type details, what sort of report is it (Mandatory)
        "reportType": {
          ** The SNOMED term for the report type, preferably taken from the RCPA's SPIA Requesting Pathology terminology valueSet (Optional)
          "snomed": {
            "term": "1454651000168108",
            "description": "SARS-CoV-2 serology",            
            ** An OID for the system that the provided term comes from (Mandatory for CDA and only required on Local term types, so not required here for a SNOMED term) 
            "oid": null
          },
          ** The term that is local to the laboratory for the report type (Mandatory)
          "local": {
            "term": "COVID2SER",
            "description": "SARS-CoV-2 Serology",
            ** An OID for the system that the provided term comes from (Mandatory for CDA and only required on Local term types) 
            "oid": null
          }
        },
        ** A code for the department that the report is generated from in the laboratory as taken from 
        ** the HL7AUSD-STD-OO-ADRM-2018.1 standard HL7 Table 0074 - Diagnostic service section ID code system (Mandatory)
        "department": "SR",
        ** A reporting Pathologist's details, he who is responsible for the report from the  laboratory (Mandatory)
        "reportingPathologist": {
          ** A reporting Pathologist name details (Family name is Mandatory)
          "name": {
            "family": "Pathologist",
            "given": "Bella",
            "middle": "",
            "title": "Dr"
          },
          ** A reporting Pathologist HPI-I identifier (Mandatory for CDA Documents)
          "identifierList": [
            {
              "value": "8003 6111 0391 4531",
              "type": "HPII",
              "assigningAuthority": null
            },
            ** A reporting Pathologist laboratory local identifier (Mandatory)
            {
              "value": "BPATH",
              "type": "LocalToLab",
              "assigningAuthority": null
            }
          ]
        },
        ** The Panel/Battery of result values associated with the report, it is a list or result values (Mandatory to have at least one). 
        "panel": {
          ** The list of results for the Panel/Battery
          "resultList": [
            {
              ** The result type details, what sort of result is it (Mandatory)
              "resultType": {
                ** The report type's LONIC term, preferably taken from the RCPA's SPIA terminology value sets (Optional)
                "lonic": {
                  "term": "95427-1",
                  "description": "SARS-CoV-2 (COVID-19) IgA IF [Titer]",
                  "oid": null
                },
                ** The report type's term which is local to the laboratory (Mandatory)
                "local": {
                  "term": "COVID2IGA",
                  "description": "SARS-CoV-2 IgA IF",
                  "oid": null
                }
              },
              ** A data type for this result value. the data types have been taken from the HL7 v2 specification and the supported 
              ** types are (NM - Numeric, SN - Structured Numeric, NR - Numeric Range, ST - String, FT - Formatted Text) (Mandatory)
              "dataType": "NM",
              ** The result value which must align with the stated data type above (Optional). 
              ** This string must use HL7 v2 escaping which means you must replace the following values as shown
              ** | replace with \\F\\
              ** ^ replace with \\S\\ 
              ** & replace with \\T\\
              ** \ replace with \\E\\
              ** ~ replace with \\R\\
              ** You can use \\.br\\ to create a line break
              ** You can use \\H\\ to begin highlighting (turn bold on)
              ** You can use \\N\\ to end highlighting (turn bold off)
              "value": "20",
               ** The units for the result value, should be UCUM units (Optional)
              "units": null,
              ** The reference range for the result value, e.g '100 - 200' (Optional)
              "referenceRange": null,
              ** An abnormality flag for the result value, can be one of (L - Low, H - high, N - Normal, S - Susceptible, R - Resistant, I - Intermediate, A - Abnormal) (Optional)
              "abnormalFlag": null,
              ** The status for the result value, can be one of (F - Final, C- Correction, P - Preliminary, X - NoResultsAvailableOrderCanceled) (Mandatory)
              "status": "F",
              ** The date & time that the result value was generated by the laboratory's equipment (Optional)
              "observationDateTime": "2020-07-26T10:31:00+10:00",
              ** Some result values can contain children result values and even those children result values can contin there own children result values.
              ** This occurs in structured Microbiology reports such as a Urine MC&S where an identified organism will have that organism's sensitivity 
              ** results as children of the organism result. Here you can add a list of new results just like the one we have described (optional) 
              "childResultList": null
            },
            ** The next results in the Panel/Battery list, and on and on as described above.
            {
              "resultType": {
                "lonic": {
                  "term": "95429-7",
                  "description": "SARS-CoV-2 (COVID-19) IgG IF [Titer]",
                  "oid": null
                },
                "local": {
                  "term": "COVID2IGM",
                  "description": "SARS-CoV-2 IgM IF",
                  "oid": null
                }
              },
              "dataType": "NM",
              "value": "10",
              "units": null,
              "referenceRange": null,
              "abnormalFlag": null,
              "status": "F",
              "observationDateTime": "2020-07-26T10:31:00+10:00",
              "childResultList": null
            },
            {
              "resultType": {
                "lonic": {
                  "term": "95428-9",
                  "description": "SARS-CoV-2 (COVID-19) IgM IF [Titer]",
                  "oid": null
                },
                "local": {
                  "term": "COVID2IGG",
                  "description": "SARS-CoV-2 IgG IF",
                  "oid": null
                }
              },
              "dataType": "NM",
              "value": "320",
              "units": null,
              "referenceRange": null,
              "abnormalFlag": null,
              "status": "F",
              "observationDateTime": "2020-07-26T10:31:00+10:00",
              "childResultList": null
            },
            {
              "resultType": {
                "lonic": null,
                "local": {
                  "term": "INTERP",
                  "description": "Interpretation",
                  "oid": null
                }
              },
              "dataType": "FT",
              "value": "These results suggest resolving SARS-CoV-2 infection. For further information please contact the medical virologist on(07) 5454 0387.\\.br\\virologist on (07) 5454 0387.\\.br\\\\.br\\N.B. SARS-Co-V-2 is the cause of coronavirus disease (CoVID-19).\\.br\\The Immunofluorescent tests used have been evaluated and validated but not yet registered with NATA.\\.br\\Results have been issued to the serious threat to public health from CoVID-19.\\.br\\These results have been electronically notified to the Ministry of Health.\\.br\\",
              "units": null,
              "referenceRange": null,
              "abnormalFlag": null,
              "status": "F",
              "observationDateTime": "2020-07-26T10:31:00+10:00",
              "childResultList": null
            }
          ]
        }
      }
    ]
  }
}
```

# Solution Information #
The solution's projects are .NET Framework 4.7.2. 

*While I would have liked to use .NET Core, unfortunately, the ADHA CDA library dependencies are currently not compatible with .NET Core or .NET Standard.* 

The solution contains five projects as follows:

* **Spia.AdhaCdaGeneration:** The generation of the CDA Pathology Document as per the ADHA specifications for the My Health Record
* **Spia.AdhaCdaPackageGeneration:** The generation of the CDA Package as per the ADHA specifications for the My Health Record
* **Spia.AdhaFhirGeneration:** The generation of a pathology FHIR bundle as per the ADHA profiles which are still in development
* **Spia.AusHl7v2Generation:** The generation of HL7 v2 ORU messages as per the HL7 Australia profile: HL7AUSD-STD-OO-ADRM-2018.1
* **Spia.PathologyReportModel:** The logical Pathology report Model expressed in .json. 
* **Spia.Runner:** A console application that outputs all files to a given folder location

**Solution Nuget parent dependencies**
* Hl7.Fhir.R4
* Nehta.VendorLibrary.CDA.Generator.v3
* Nehta.VendorLibrary.CDAPackage
* PeterPiper

# Repo owner #
Angus Millar: angusbmillar@gmail.com
