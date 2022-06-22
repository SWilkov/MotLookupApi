using Microsoft.EntityFrameworkCore;
using MotLookupApi.DataLayer.Interfaces;
using MotLookupApi.DataLayer.MySQL.Data;
using MotLookupApi.DataLayer.MySQL.DataModels;
using MotLookupApi.DataLayer.MySQL.Interfaces;
using MotLookupApi.Framework.Models;

namespace MotLookupApi.DataLayer.MySQL.Repositories
{
  public class VehicleReadRepository : IVehicleReadRepository
  {
    private readonly IVehicleMapper _vehicleMapper;
    private readonly VehicleDataContext _context;
    public VehicleReadRepository(IVehicleMapper mapper,
      VehicleDataContext context)
    {
      _context = context;
      _vehicleMapper = mapper;
    }

    public async Task<Vehicle> Get(string registration)
    {
      if (string.IsNullOrEmpty(registration))
        throw new ArgumentNullException(nameof(registration));

      var vehicle = await _context.Vehicles.Include(x => x.MotTests).ThenInclude(x => x.Comments)
                                           .FirstOrDefaultAsync(x => x.Registration.ToLower() == registration.ToLower());

      if (vehicle == null)
        return null;

      return _vehicleMapper.Map(vehicle);
    }

    public async Task<Vehicle> GetByUniqueVehcileId(string uniqueVehicleId)
    {
      if (string.IsNullOrEmpty(uniqueVehicleId))
        throw new ArgumentNullException(nameof(uniqueVehicleId));

      var vehicle = await _context.Vehicles.Include(x => x.MotTests).ThenInclude(x => x.Comments)
                                           .FirstOrDefaultAsync(x => x.UniqueVehicleId.ToLower() == uniqueVehicleId.ToLower());

      if (vehicle == null)
        return null;

      return _vehicleMapper.Map(vehicle);
    }

    public async Task<bool> Exists(string registration, string vehicleId)
    {
      if (string.IsNullOrWhiteSpace(registration) && string.IsNullOrWhiteSpace(vehicleId)) throw new ArgumentNullException("No registration or vehicleid params!");
      var formattedRegistration = registration.Trim().ToLower();

      var exists = await _context.Vehicles.FirstOrDefaultAsync(x => x.Registration.ToLower() == formattedRegistration ||
        x.UniqueVehicleId == vehicleId);

      return exists != null && exists.Id > 0;
    }
  }
}
