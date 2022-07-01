using MotLookupApi.DataLayer.MySQL.DataModels;
using MotLookupApi.DataLayer.MySQL.Interfaces;
using MotLookupApi.Framework.Models;

namespace MotLookupApi.DataLayer.MySQL.Mappers
{
  public class VehicleMapper : IVehicleMapper
  {
    private readonly IMotTestMapper _motMapper;
    public VehicleMapper(IMotTestMapper motTestMapper)
    {
      _motMapper = motTestMapper;
    }
    public VehicleDataModel Map(Vehicle vehicle)
    {
      if (vehicle == null)
        return null;

      var dm = new VehicleDataModel
      {
        Id = vehicle.Id,
        FirstUsedDate = vehicle.FirstUsedDate,
        FuelType = vehicle.FuelType,
        Make = vehicle.Make,
        Model = vehicle.Model,
        PrimaryColour = vehicle.PrimaryColour,
        Registration = vehicle.Registration,
        EngineSize = vehicle.EngineSize,
        RegistrationDate = vehicle.RegistrationDate,
        UniqueVehicleId = vehicle.UniqueVehicleId,
        MotTestDueDate = vehicle.MotTestDueDate,
        DvlaId = vehicle.DvlaId.HasValue ? vehicle.DvlaId.Value : 0,
        ManufactureDate = vehicle.ManufactureDate,
        MotTests = vehicle.MotTests == null || !vehicle.MotTests.Any() ? null 
        : vehicle.MotTests.Select(x => _motMapper.Map(x)).ToList()
      };

      return dm;
    }

    public Vehicle Map(VehicleDataModel source)
    {
      if (source == null)
        throw new ArgumentNullException(nameof(source));

      return new Vehicle
      {
        Id = source.Id,
        FirstUsedDate = source.FirstUsedDate,
        PrimaryColour = source.PrimaryColour,
        FuelType = source.FuelType,
        Make = source.Make,
        Model = source.Model,
        Registration = source.Registration,
        MotTestDueDate = source.MotTestDueDate,
        RegistrationDate = source.RegistrationDate,
        EngineSize=source.EngineSize, 
        UniqueVehicleId = source.UniqueVehicleId,
        DvlaId = source.DvlaId,
        ManufactureDate = source.ManufactureDate,
        MotTests = source.MotTests == null || !source.MotTests.Any() ? null 
          : source.MotTests.Select(x => _motMapper.Map(x)).ToList()
      };
    }
  }
}
