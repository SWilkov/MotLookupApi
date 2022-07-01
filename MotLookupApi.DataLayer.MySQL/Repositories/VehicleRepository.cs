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
      if (vehicle.Id == default(int))
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
        var existing = _context.Vehicles.FirstOrDefault(x => x.Id == vehicle.Id);
        if (existing is not null)
        {
          _context.Entry(existing).CurrentValues.SetValues(dm);
          var rows = await _context.SaveChangesAsync();
          if (rows == default(int))
          {
            //TODO log
          }

          return _vehicleMapper.Map(dm);
        } 
        else
        {
          return vehicle;
        }
        
      }
    }    
  }
}
