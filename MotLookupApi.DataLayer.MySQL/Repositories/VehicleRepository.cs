using MotLookupApi.DataLayer.Interfaces;
using MotLookupApi.DataLayer.MySQL.Data;
using MotLookupApi.DataLayer.MySQL.DataModels;
using MotLookupApi.DataLayer.MySQL.Interfaces;
using MotLookupApi.Framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotLookupApi.DataLayer.MySQL.Repositories
{
  public class VehicleRepository : IRepository<Vehicle>
  {
    private readonly IVehicleMapper _vehicleMapper;
    private readonly VehicleDataContext _context;
    private readonly IVehicleReadRepository _vehicleReadRepository;
    public VehicleRepository(IVehicleMapper vehicleMapper,      
      VehicleDataContext context, IVehicleReadRepository vehicleReadRepository)
    {
      _vehicleMapper = vehicleMapper;
      _context = context;
      _vehicleReadRepository = vehicleReadRepository;
    }

    public Task Delete(Vehicle item)
    {
      throw new NotImplementedException();
    }

    public async Task<Vehicle> Save(Vehicle vehicle)
    {
      if (vehicle == null)
        throw new ArgumentNullException(nameof(vehicle));

      var dm = _vehicleMapper.Map(vehicle);
      var exists = await _vehicleReadRepository.Exists(vehicle.Registration, vehicle.UniqueVehicleId);
      if (vehicle.Id == default(int) || !exists)
      {
        dm.CreatedAt = DateTime.Now;
        var entity = await _context.Vehicles.AddAsync(dm);
        if (entity.State != Microsoft.EntityFrameworkCore.EntityState.Added)
        {
          //TODO log
        }

        await _context.SaveChangesAsync();
        vehicle.Id = entity.Entity.Id;

        return vehicle;
      }
      else
      {
        var updated =  _context.Vehicles.Update(dm);
        var rows = await _context.SaveChangesAsync();
        if (rows == default(int))
        {
          //TODO log
        }

        return _vehicleMapper.Map(updated.Entity);
      }
    }    
  }
}
