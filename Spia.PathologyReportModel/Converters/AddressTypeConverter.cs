using Newtonsoft.Json;
using Spia.PathologyReportModel.Model;
using Spia.PathologyReportModel.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spia.PathologyReportModel.Converters
{
  public class AddressTypeConverter : EnumJsonConverterBase<AddressType>
  {
    public AddressTypeConverter()
      : base(new AddressTypeSupport()) { }
  }
}
