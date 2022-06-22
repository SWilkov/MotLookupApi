using MotLookupApi.Framework.Models;
using MotLookupApi.Models;

namespace MotLookupApi.Interfaces
{
  public interface IVehicleSearchTypeFactory
  {
    SearchQuery Get(MotRequest request);
  }
}
