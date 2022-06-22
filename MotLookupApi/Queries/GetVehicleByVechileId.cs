using MotLookupApi.DataLayer.Interfaces;
using MotLookupApi.Framework.Enums;
using MotLookupApi.Framework.Models;
using MotLookupApi.Interfaces;
using MotLookupApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotLookupApi.Queries
{
  public class GetVehicleByVechileId : IGetVehicle
  {
    private readonly IVehicleReadRepository _vehicleReadRepository;
    public GetVehicleByVechileId(IVehicleReadRepository vehicleReadRepository)
    {
      _vehicleReadRepository = vehicleReadRepository;
    }

    public async Task<Vehicle> Get(string input, SearchType searchType)
    {
      if (string.IsNullOrWhiteSpace(input))
        throw new ArgumentNullException(nameof(input));
      if (searchType != SearchType.VehicleId)
        throw new ArgumentException($"Invalid searchType");

      return await _vehicleReadRepository.GetByUniqueVehcileId(input);
    }
  }
}
