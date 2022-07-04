using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MotLookupApi.DataLayer.Interfaces;
using MotLookupApi.DataLayer.MySQL.Extensions;
using MotLookupApi.Events;
using MotLookupApi.Factories;
using MotLookupApi.Framework.Enums;
using MotLookupApi.Framework.Interfaces;
using MotLookupApi.Framework.Models;
using MotLookupApi.Gov.Uk.Extensions;
using MotLookupApi.Gov.Uk.Models;
using MotLookupApi.Interfaces;
using MotLookupApi.Models;
using MotLookupApi.Queries;
using MotLookupApi.Services;
using MotLookupApi.Utils.Validation.Interfaces;
using MotLookupApi.Validators;
using MotLookupApi.Wrappers;
using System.Reflection;
using System.Text.Json;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices(services =>
    {
      services.Configure<JsonSerializerOptions>(options =>
      {
        options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;

      });

      services.AddMySQLServer(Environment.GetEnvironmentVariable("MYSQL_CONN_STR") ?? "");
      
      services.AddOptions<GovSettings>()
        .Configure<IConfiguration>((settings, config) =>
        {
          config.GetSection(nameof(GovSettings)).Bind(settings);
        });

      services.AddMediatR(Assembly.GetExecutingAssembly());

      #region HttpClients
      services.AddHttpClient<MotGovService>(s =>
      {
        s.BaseAddress = new Uri(Environment.GetEnvironmentVariable("GOV_BASE_URL") ?? throw new ArgumentException("Invalid base url!"));
        s.DefaultRequestHeaders.Add("x-api-key", Environment.GetEnvironmentVariable("GOV_API_KEY") ?? throw new ArgumentException("Invalid api key"));
        s.DefaultRequestHeaders.Add("Accept", "application/json+v6");
      });
      #endregion

      #region Factories
      services.AddScoped<IVehicleSearchTypeFactory, VehicleSearchTypeFactory>();
      #endregion

      #region Services
      services.AddMotLookupApiGov();

      services.AddScoped<IVehicleService, VehicleService>();
      services.AddScoped<IVehicleQueryService, VehicleQueryService>();
      services.AddScoped<IMileageStatisticsService, MileageStatisticsService>();

      services.AddSingleton<IGetVehicle>(sp =>
      {
        using (var scope = sp.CreateScope())
        {
          var dict = new Dictionary<SearchType, IGetVehicle>
          {
            {
              SearchType.Registration,
              new GetVehicleByRegistration(scope.ServiceProvider.GetRequiredService<IVehicleReadRepository>())
            },
            {
              SearchType.VehicleId,
              new GetVehicleByVechileId(scope.ServiceProvider.GetRequiredService<IVehicleReadRepository>())
            }
          };
          return new GetVehicleComposite(dict);
        }        
      });
      #endregion

      #region Validators
      services.AddScoped<IInformationValidator<Vehicle>, VehicleValidator>();
      services.AddScoped<IInformationValidator<VehicleGovModel>, VehicleGovValidator>();
      services.AddScoped<IInformationValidator<VehicleRetrievedEvent>, MileageStatisticsValidator>();
      #endregion

      #region Wrappers
      services.AddScoped<IMotGovServiceWrapper, MotGovServiceWrapper>();
      #endregion
    })
    .Build();

host.Run();