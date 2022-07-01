using MotLookupApi.Dvla.Models;
using MotLookupApi.Framework.Models;

namespace MotLookupApi.Dvla.Interfaces
{
  public interface IDvlaDataMapper
  {
    DvlaData Map(DvlaGovDataModel source);
  }
}