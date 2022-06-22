using MotLookupApi.Framework.Enums;
using MotLookupApi.Framework.Models;
using MotLookupApi.Interfaces;
using MotLookupApi.Models;

namespace MotLookupApi.Factories
{
  public class VehicleSearchTypeFactory : IVehicleSearchTypeFactory
  {
    public SearchQuery Get(MotRequest request)
    {
      if (request == null)
        throw new ArgumentNullException(nameof(request));
      if (string.IsNullOrWhiteSpace(request.Registration) && string.IsNullOrWhiteSpace(request.VehicleId))
        throw new ArgumentNullException("Both Registration & VechileId are missing!");

      if (string.IsNullOrWhiteSpace(request.VehicleId.Trim()) && !string.IsNullOrWhiteSpace(request.Registration.Trim()))
        return new SearchQuery(request.Registration, SearchType.Registration);
      if (!string.IsNullOrWhiteSpace(request.VehicleId.Trim()) && string.IsNullOrWhiteSpace(request.Registration.Trim()))
        return new SearchQuery(request.VehicleId, SearchType.VehicleId);

      throw new ArgumentOutOfRangeException(nameof(request));
    }
  }
}
