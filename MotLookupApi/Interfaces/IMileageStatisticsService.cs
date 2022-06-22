using MotLookupApi.Events;
using MotLookupApi.Framework.Models;

namespace MotLookupApi.Interfaces
{
  public interface IMileageStatisticsService
  {
    ICollection<MileageStatistics> Create(
      VehicleRetrievedEvent vehicleRetrievedEvent);
  }
}