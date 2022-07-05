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

    public async Task<IEnumerable<Vehicle>> GetAll()
    {
      var all = await _context.Vehicles.ToListAsync();
      if (all is null) return null;

      return all.Select(x => _vehicleMapper.Map(x));
    }

    public async Task<Vehicle> GetFirst()
    {
      var first = await _context.Vehicles.FirstOrDefaultAsync();
      if (first is null) return null;

      return _vehicleMapper.Map(first);
    }

    public async Task<Vehicle> Get(string registration)
    {
      if (string.IsNullOrEmpty(registration))
        throw new ArgumentNullException(nameof(registration));

      var vehicle = await _context.Vehicles.Include(x => x.MotTests).ThenInclude(x => x.Comments)
                                           .FirstOrDefaultAsync(x => x.Registration.ToLower() == registration.ToLower());

      if (vehicle == null)
        return null;

      vehicle.MotTests = vehicle.MotTests.OrderBy(x => x.CompletedDate).ToList();

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

    public async Task<Vehicle> Test()
    {
      var v = await _context.Vehicles.Include(x => x.MotTests)
                                     .FirstOrDefaultAsync(x => (x.MotTestDueDate != DateTime.MinValue && 
                                     DateTime.Now >= x.MotTestDueDate.AddDays(-60)) || (x.MotTests != null && x.MotTests.Any() 
                                     && DateTime.Now > x.MotTests.First().ExpiryDate.AddDays(-60)));

      if (v == null) return null;

      return _vehicleMapper.Map(v);
    }

    //public async Task<Vehicle> GetByDate(DateTime date)
    //{
    //  var vehicle = await _context.Vehicles.Include(x => x.MotTests).ThenInclude(x => x.Comments)
    //                                       .Where(x => x.ManufactureDate is not null ? DateTime.UtcNow : DateTime.MinValue)

    //  if (vehicle == null)
    //    return null;

    //  return _vehicleMapper.Map(vehicle);
    //}

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
