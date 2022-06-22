using Microsoft.Extensions.Logging;
using MotLookupApi.DataLayer.MySQL.Interfaces;
using MotLookupApi.Framework.Enums;
using MotLookupApi.Framework.Models;
using MotLookupApi.Gov.Uk.Interfaces;
using MotLookupApi.Gov.Uk.Models;
using MotLookupApi.Models;
using Newtonsoft.Json;

namespace MotLookupApi.Services
{
  public class MotGovService
  {
    private readonly HttpClient _client;
    private readonly IVehicleGovMapper _vehicleGovMapper;
    private readonly ILogger _logger;
    public MotGovService(HttpClient client,
       IVehicleGovMapper vehicleGovMapper,
       ILogger<MotGovService> logger)
    {
      _client = client;
      _vehicleGovMapper = vehicleGovMapper;
      _logger = logger;
    }

    public async Task<Vehicle> Get(string input, SearchType searchType = SearchType.Registration)
    {
      if (string.IsNullOrEmpty(input))
        throw new ArgumentNullException(nameof(input));

      var url = searchType == SearchType.Registration ? $"?registration={input}" : $"?vehicleId={input}";

      var response = await _client.GetAsync($"{url}");
      if (!response.IsSuccessStatusCode)
      {
        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
          return null;

        //log as MOT Gov is sending other failures        
        _logger.LogInformation($"Something has gone wrong with MotGovService: {response.RequestMessage}");
      }

      response.EnsureSuccessStatusCode();

      var body = await response.Content.ReadAsStringAsync();
      if (string.IsNullOrWhiteSpace(body))
      {
        //TODO log
        throw new ArgumentException($"Somethings gone wrong getting the Vehicle from the Gov api for input: {input}");
      }

      var govModel = JsonConvert.DeserializeObject<IEnumerable<VehicleGovModel>>(body);      
        
      if (govModel == null || !govModel.Any())
      {
        _logger.LogInformation($"no gov data for input : ${input}");
        return null;
      }

      return _vehicleGovMapper.Map(govModel.FirstOrDefault());
    }
  }
}
