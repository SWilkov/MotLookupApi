using MotLookupApi.DataLayer.Interfaces;
using MotLookupApi.DataLayer.MySQL.DataModels;
using MotLookupApi.Framework.Models;

namespace MotLookupApi.DataLayer.MySQL.Interfaces
{
  public interface IVehicleMapper :
    IMapper<VehicleDataModel, Vehicle>,
    IMapper<Vehicle, VehicleDataModel>
  {

  }
}
