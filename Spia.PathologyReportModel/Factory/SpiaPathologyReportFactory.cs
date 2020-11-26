using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Spia.PathologyReportModel.Factory.SpiaReports;
using Spia.PathologyReportModel.Model;
using Spia.PathologyReportModel.Support;

namespace Spia.PathologyReportModel.Factory
{
  public class SpiaPathologyReportFactory
  {
    private SpiaPatientFactory PatientFactory;
    private SpiaProviderFactory ProviderFactory;
    private SpiaLaboratoryFactory LaboratoryFactory;
    public SpiaPathologyReportFactory()
    {
      PatientFactory = new SpiaPatientFactory();
      ProviderFactory = new SpiaProviderFactory();
      LaboratoryFactory = new SpiaLaboratoryFactory();
    }

    public List<PathologyReportContainer> GetAll()
    {
      var Result = new List<PathologyReportContainer>();      
      IEnumerable<Type> IReportTypeList = GetTypesWithInterface(System.Reflection.Assembly.GetExecutingAssembly());
      foreach(Type IReportType in IReportTypeList)
      {       
        IReportFactory ReportFactory = (IReportFactory)Activator.CreateInstance(IReportType, PatientFactory, ProviderFactory, LaboratoryFactory);
        Result.Add(ReportFactory.GetReport());
      }
      return Result;      
    }

    private IEnumerable<Type> GetTypesWithInterface(Assembly assembly)
    {
      var InterfaceType = typeof(IReportFactory);
      return assembly.GetLoadableTypes().Where(InterfaceType.IsAssignableFrom).Where(x => x.IsClass).ToList();
    }

  }
}

