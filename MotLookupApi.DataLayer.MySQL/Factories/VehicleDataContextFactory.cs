using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using MotLookupApi.DataLayer.MySQL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotLookupApi.DataLayer.MySQL.Factories
{
  public class VehicleDataContextFactory : IDesignTimeDbContextFactory<VehicleDataContext>
  {
    public VehicleDataContext CreateDbContext(string[] args)
    {
      var connectionString = Environment.GetEnvironmentVariable("MYSQL_CONN_STR");

      var optionsBuilder = new DbContextOptionsBuilder<VehicleDataContext>();
      optionsBuilder.UseMySql("Server=localhost; Port=3307; Uid=root; Pwd=secret; database=vehiclestats;",
        new MySqlServerVersion(new Version(5, 7, 32)),
        options =>
        {
          options.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
        });

      return new VehicleDataContext(optionsBuilder.Options);
    }
  }
}
