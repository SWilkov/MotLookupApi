using Microsoft.Extensions.Logging;
using MotLookupApi.DataLayer.Interfaces;
using MotLookupApi.Framework.Interfaces;
using MotLookupApi.Framework.Models;

namespace MotLookupApi.Services
{
  public class VehicleQueryService :
    IVehicleQueryService
  {
    private readonly IRepository<VehicleQuery> _vehicleQueryRepository;
    private readonly ILogger _logger;
    public VehicleQueryService(IRepository<VehicleQuery> vehicleQueryRepository,      
      ILogger<VehicleQueryService> logger)
    {
      _vehicleQueryRepository = vehicleQueryRepository;
      _logger = logger;
    }

    public async Task<VehicleQuery> Save(VehicleQuery item)
    {
      if (item == null)
        throw new ArgumentNullException(nameof(item));

      var savedQuery = await _vehicleQueryRepository.Save(item);
      if (savedQuery == null || savedQuery.Id == default(int))
        _logger.LogInformation($"Error saving vehicleQuery");

      return savedQuery;
    }
  }
}
