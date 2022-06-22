using MotLookupApi.Framework.Enums;
using MotLookupApi.Framework.Models;
using MotLookupApi.Models;

namespace MotLookupApi.Interfaces
{
  public interface IGetVehicle
  {
    Task<Vehicle> Get(string input, SearchType searchType);
  }
}
