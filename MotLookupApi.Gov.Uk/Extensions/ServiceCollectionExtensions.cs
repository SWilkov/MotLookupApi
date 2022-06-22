using Microsoft.Extensions.DependencyInjection;

namespace MotLookupApi.Gov.Uk.Extensions
{
  public static class ServiceCollectionExtensions
  {
    public static void AddMotLookupApiGov(this IServiceCollection services)
    {
      #region Mappers
      services.AddScoped<Interfaces.IVehicleGovMapper, Mappers.VehicleGovMapper>();
      #endregion
    }
  }
}
