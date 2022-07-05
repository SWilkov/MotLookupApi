using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using MotLookupApi.DataLayer.Interfaces;

namespace MotLookupApi.Functions
{
  public class Warmer
  {
    private readonly ILogger _logger;
    private readonly IVehicleReadRepository _repository;

    public Warmer(ILoggerFactory loggerFactory, IVehicleReadRepository repository)
    {
      _logger = loggerFactory.CreateLogger<Warmer>();
      _repository = repository; 
    }

    /// <summary>
    /// Warmer function used by App Insights ping test to avoid cold starts of 30s +
    /// </summary>
    /// <param name="req"></param>
    /// <returns></returns>
    [Function("warmer")]
    public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData req)
    {
      _logger.LogInformation("C# HTTP trigger function processed a request.");

      var first = await _repository.GetFirst();

      if (first == null)
      {
        var nf = req.CreateResponse(HttpStatusCode.NotFound);
        return nf;
      }

      var response = req.CreateResponse(HttpStatusCode.OK);
      await response.WriteAsJsonAsync(new
      {
        Status = "success",
        Data = first
      });

      return response;
    }
  }
}
