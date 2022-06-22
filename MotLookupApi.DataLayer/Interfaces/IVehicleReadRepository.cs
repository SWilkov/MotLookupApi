using System.Threading.Tasks;
using MotLookupApi.Framework.Models;

namespace MotLookupApi.DataLayer.Interfaces
{
  public interface IVehicleReadRepository
  {
    Task<Vehicle> Get(string registration);
    Task<Vehicle> GetByUniqueVehcileId(string uniqueVehicleId);
    Task<bool> Exists(string registration, string vehicleId);
  }
}