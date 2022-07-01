using Microsoft.Extensions.DependencyInjection;

namespace MotLookupApi.Dvla.Extensions
{
  public static class ServiceCollectionExtensions
  {
    public static void AddDvlaGovData(this IServiceCollection services)
    {
      services.AddScoped<Interfaces.IDvlaDataMapper, Mappers.DvlaDataMapper>();
    }
  }
}
