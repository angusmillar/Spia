using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PeterPiper.Hl7.V2.Model;
using Spia.AdhaCdaGeneration.Model;

namespace Spia.AdhaCdaGeneration.Factory
{
  public class CdaCreatorInput
  {
    public bool IsMandatoryCDAElementsOnly { get; set;  }
    public IMessage Message { get; set;  }
    public PathologyCdaMetadata CdaMetadata { get; set; }
    public string FilePathToPdfReport { get; set; }
    public byte[] CdaDocuemntLogoImageBytes { get; set; }
    public string CdaDocumentOutPutFilePath { get; set; }
    


  }
}
