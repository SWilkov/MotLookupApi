using MotLookupApi.DataLayer.Interfaces;
using MotLookupApi.Framework.Enums;
using MotLookupApi.Framework.Interfaces;
using MotLookupApi.Framework.Models;
using MotLookupApi.Models;

namespace MotLookupApi.Services
{
  public class VehicleService : IVehicleService
  {
    private readonly IVehicleReadRepository _vehicleReadRepository;
    private readonly IRepository<Vehicle> _vehicleRepository;
    public VehicleService(IVehicleReadRepository vehicleReadRepository,
      IRepository<Vehicle> vehicleRepository)
    {
      _vehicleReadRepository = vehicleReadRepository;
      _vehicleRepository = vehicleRepository;
    }

    public async Task<Vehicle> Get(string input, SearchType searchType = SearchType.Registration)
    {
      if (string.IsNullOrEmpty(input))
        throw new ArgumentNullException(nameof(input));

      var vehicle = searchType == SearchType.Registration ? await _vehicleReadRepository.Get(input) 
        : await _vehicleReadRepository.GetByUniqueVehcileId(input);

      return vehicle;
    }

    public async Task<Vehicle> Save(Vehicle vehicle)
    {
      if (vehicle == null)
        throw new ArgumentNullException(nameof(vehicle));

      return await _vehicleRepository.Save(vehicle); 
    }

    public bool IsNewCar(Vehicle vehicle)
    {
      if (vehicle.MotTestDueDate > DateTime.MinValue)
      {
        var expiryYear = vehicle.MotTestDueDate.Year;
        if (expiryYear > DateTime.Now.Year)
          return true;
      }

      return false;
    }

    //public async Task DeleteTests(Vehicle vehicle)
    //{
    //  if (vehicle.MotTests == null || !vehicle.MotTests.Any())
    //    return;

    //  foreach(var test in vehicle.MotTests)
    //  {
    //    if (test.Id == default(int))
    //      continue;

    //    await _testRepository.Delete(test);
    //  }
    //}    
  }
}
