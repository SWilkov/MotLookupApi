using MotLookupApi.DataLayer.Interfaces;
using MotLookupApi.Framework.Enums;
using MotLookupApi.Framework.Models;
using MotLookupApi.Interfaces;
using MotLookupApi.Models;

namespace MotLookupApi.Queries
{
  public class GetVehicleByRegistration : IGetVehicle
  {
    private readonly IVehicleReadRepository _vehicleReadRepository;
    public GetVehicleByRegistration(IVehicleReadRepository vehicleReadRepository)
    {
      _vehicleReadRepository = vehicleReadRepository;
    }

    public async Task<Vehicle> Get(string input, SearchType searchType)
    {
      if (string.IsNullOrWhiteSpace(input))
        throw new ArgumentNullException(nameof(input));
      if (searchType != SearchType.Registration)
        throw new ArgumentException($"Invalid searchType");

      return await _vehicleReadRepository.Get(input);
    }
  }
}
