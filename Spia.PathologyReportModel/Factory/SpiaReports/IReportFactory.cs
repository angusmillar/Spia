using Spia.PathologyReportModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spia.PathologyReportModel.Factory.SpiaReports
{
  public interface IReportFactory
  {
    PathologyReportContainer GetReport();
  }
}
