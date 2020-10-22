using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spia.Runner
{
  class Program
  {
    static void Main(string[] args)
    {
      string RootSpiaDirectoryPath = @"C:\temp\SPIAMessages";
      SpiaFileGenerator SpiaFileGenerator = new SpiaFileGenerator();
      SpiaFileGenerator.Process(RootSpiaDirectoryPath);
    }
  }
}
