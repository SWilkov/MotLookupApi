using MotLookupApi.Framework.Enums;
using MotLookupApi.Framework.Models;
using MotLookupApi.Interfaces;
using MotLookupApi.Models;
using MotLookupApi.Services;

namespace MotLookupApi.Wrappers
{
  public class MotGovServiceWrapper : IMotGovServiceWrapper
  {
    private readonly MotGovService _motGovService;
    public MotGovServiceWrapper(MotGovService motGovService)
    {
      _motGovService = motGovService;
    }

    public async Task<Vehicle> Get(string input, SearchType searchType = SearchType.Registration)
    {
      if (string.IsNullOrEmpty(input))
        throw new ArgumentNullException(nameof(input));

      return await _motGovService.Get(input, searchType);
    }
  }
}
