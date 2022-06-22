using MotLookupApi.Framework.Models;
using MotLookupApi.Gov.Uk.Models;

namespace MotLookupApi.Gov.Uk.Interfaces
{
  public interface IVehicleGovMapper
  {
    Vehicle Map(VehicleGovModel vehicleGovModel);

  }
}
