using MotLookupApi.Framework.Enums;
using MotLookupApi.Framework.Models;
using MotLookupApi.Interfaces;
using MotLookupApi.Models;

namespace MotLookupApi.Queries
{
  public class GetVehicleComposite : IGetVehicle
  {
    private readonly Dictionary<SearchType, IGetVehicle> _services;
    public GetVehicleComposite(Dictionary<SearchType, IGetVehicle> services)
    {
      _services = services;
    }
    public Task<Vehicle> Get(string input, SearchType searchType) =>
      _services[searchType].Get(input, searchType);
  }
}
