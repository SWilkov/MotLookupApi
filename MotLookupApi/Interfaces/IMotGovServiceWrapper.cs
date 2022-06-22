using MotLookupApi.Framework.Enums;
using MotLookupApi.Framework.Models;
using MotLookupApi.Models;

namespace MotLookupApi.Interfaces
{
  public interface IMotGovServiceWrapper
  {
    Task<Vehicle> Get(string input, SearchType searchType = SearchType.Registration);
  }
}