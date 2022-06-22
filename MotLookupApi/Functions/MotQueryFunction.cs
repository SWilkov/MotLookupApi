using System.Collections.Generic;
using System.Net;
using MediatR;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using MotLookupApi.Events;
using MotLookupApi.Framework.Interfaces;
using MotLookupApi.Framework.Models;
using MotLookupApi.Interfaces;
using MotLookupApi.Models;
using Newtonsoft.Json;

namespace MotLookupApi.Functions
{
  public class MotQueryFunction
  {
    private readonly ILogger _logger;
    private readonly IVehicleService _vehicleService;
    private readonly IVehicleSearchTypeFactory _vehicleSearchTypeFactory;
    private readonly IMotGovServiceWrapper _motGovServiceWrapper;
    private readonly IMileageStatisticsService _mileageStatisticsService;
    private readonly IMediator _mediator;

    public MotQueryFunction(ILoggerFactory loggerFactory, IVehicleService vehicleService,
      IVehicleSearchTypeFactory vehicleSearchTypeFactory,
      IMotGovServiceWrapper motGovServiceWrapper,
      IMileageStatisticsService mileageStatisticsService,
      IMediator mediator)
    {
      _logger = loggerFactory.CreateLogger<MotQueryFunction>();
      _vehicleService = vehicleService;
      _vehicleSearchTypeFactory = vehicleSearchTypeFactory;
      _motGovServiceWrapper = motGovServiceWrapper;
      _mileageStatisticsService = mileageStatisticsService;
      _mediator = mediator;
    }

    [Function("mot-query")]
    public async Task<HttpResponseData> Post([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequestData req)
    {
      _logger.LogInformation("C# HTTP trigger function processed a request.");
      var saved = new Vehicle();
      var data = req.FunctionContext.BindingContext.BindingData;
      if (data == null)
      {
        var bad = req.CreateResponse(HttpStatusCode.BadRequest);
        return bad;
      }

      var registration = req.FunctionContext.BindingContext.BindingData["registration"];
      var vehicleId = req.FunctionContext.BindingContext.BindingData["vehicleId"];

      var motRequest = new MotRequest
      {
        Registration = registration == null ? String.Empty : registration.ToString(),
        VehicleId = vehicleId == null ? String.Empty : vehicleId.ToString()
      };

      var query = _vehicleSearchTypeFactory.Get(motRequest);

      var motResults = await _motGovServiceWrapper.Get(query.Input, query.SearchType);
      var existing = await _vehicleService.Get(query.Input, query.SearchType);
      if (existing != null && existing.Id > 0)
      {
        if (motResults.MotTests.Count != existing.MotTests.Count)
        {
          existing.MotTests = motResults.MotTests;
        }

        saved = await _vehicleService.Save(motResults);
      }
      else
      {
        if (motResults == null)
        {
          var nf = req.CreateResponse(HttpStatusCode.NotFound);
          return nf;
        }

        saved = await _vehicleService.Save(motResults);
        if (saved == null)
        {
          //TODO problem saving to db
        }
      }

      //Events
      var @event = new VehicleRetrievedEvent
      {
        Vehicle = saved
      };
      await _mediator.Publish(@event);

      //Mileage info
      saved.MileageStatistics = _mileageStatisticsService.Create(@event);

      var response = req.CreateResponse(HttpStatusCode.OK);
      await response.WriteAsJsonAsync<MotUpdaterResponse>(new MotUpdaterResponse(saved, true));
      return response;
    }
  }
}
