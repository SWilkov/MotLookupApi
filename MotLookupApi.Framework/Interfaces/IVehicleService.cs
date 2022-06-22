using MotLookupApi.Framework.Enums;
using MotLookupApi.Framework.Models;
using System.Threading.Tasks;

namespace MotLookupApi.Framework.Interfaces
{
  public interface IVehicleService
  {
    Task<Vehicle> Get(string input, SearchType searchType = SearchType.Registration);
    Task<Vehicle> Save(Vehicle vehicle);
    bool IsNewCar(Vehicle vehicle);
    //Task DeleteTests(Vehicle vehicle);
  }
}
