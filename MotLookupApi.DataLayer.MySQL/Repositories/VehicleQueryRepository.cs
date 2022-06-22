using MotLookupApi.DataLayer.Interfaces;
using MotLookupApi.DataLayer.MySQL.Data;
using MotLookupApi.DataLayer.MySQL.DataModels;
using MotLookupApi.Framework.Models;

namespace MotLookupApi.DataLayer.MySQL.Repositories
{
  public class VehicleQueryRepository : IRepository<VehicleQuery>
  {
    private readonly VehicleDataContext _context;
    public VehicleQueryRepository(VehicleDataContext context)
    {
      _context = context;
    }

    public Task Delete(VehicleQuery item)
    {
      throw new NotImplementedException();
    }

    public async Task<VehicleQuery> Save(VehicleQuery item)
    {
      if (item == null)
        throw new ArgumentNullException(nameof(item));
      if (item.VehicleId == default(int))
        throw new ArgumentException("Invalid VehicleId");

      var dm = new VehicleQueryDataModel
      {  
        Id = 0,
        CreatedAt = DateTime.Now,
        VehicleId = item.VehicleId
      };

      var entry = await _context.AddAsync(dm);
      var rows = await _context.SaveChangesAsync();

      if (rows == default(int))
      {
        //TODO log
      }

      return new VehicleQuery
      {
        Id = dm.Id,
        CreatedAt = dm.CreatedAt,
        VehicleId = dm.VehicleId
      };
    }

    public Task<VehicleQuery> Save(VehicleQuery item, int parentId)
    {
      throw new NotImplementedException();
    }
  }
}
