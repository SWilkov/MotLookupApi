using System.Threading.Tasks;
using MotLookupApi.Framework.Models;

namespace MotLookupApi.DataLayer.Interfaces
{
  public interface IVehicleReadRepository
  {
    Task<IEnumerable<Vehicle>> GetAll();
    Task<Vehicle> Get(string registration);
    Task<Vehicle> GetByUniqueVehcileId(string uniqueVehicleId);
    Task<bool> Exists(string registration, string vehicleId);

    //TODO remove
    Task<Vehicle> Test();
    Task<Vehicle> GetFirst();
  }
}