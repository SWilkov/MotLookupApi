using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MotLookupApi.DataLayer.Interfaces;
using MotLookupApi.DataLayer.MySQL.Interfaces;
using MotLookupApi.Framework.Models;

namespace MotLookupApi.DataLayer.MySQL.Extensions
{
  public static class ServiceCollectionExtensions
  {
    public static void AddMySQLServer(this IServiceCollection services, string connectionString)
    {
      #region Database
      services.AddDbContext<Data.VehicleDataContext>(options =>
      {
        options.EnableSensitiveDataLogging(true);
        options.UseMySql(connectionString, new MySqlServerVersion(new Version(5, 7, 32)),
        mySqlOptions =>
        {
          mySqlOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
        });
      });
      #endregion           

      #region Repositories
      services.AddScoped<IRepository<Vehicle>, Repositories.VehicleRepository>();
      services.AddScoped<IRepository<VehicleQuery>, Repositories.VehicleQueryRepository>();
      services.AddScoped<IVehicleReadRepository, Repositories.VehicleReadRepository>();
      #endregion

      #region Mappers
      services.AddScoped<IVehicleMapper, Mappers.VehicleMapper>();
      services.AddScoped<IMotTestMapper, Mappers.MotTestMapper>();
      services.AddScoped<ICommentMapper, Mappers.CommentMapper>();
      #endregion
    }
  }
}
