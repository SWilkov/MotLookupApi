using MotLookupApi.Events;
using MotLookupApi.Framework.Extensions;
using MotLookupApi.Framework.Models;
using MotLookupApi.Interfaces;
using MotLookupApi.Utils.Validation.Enums;
using MotLookupApi.Utils.Validation.Interfaces;

namespace MotLookupApi.Services
{
  public class MileageStatisticsService : IMileageStatisticsService
  {
    private readonly IInformationValidator<VehicleRetrievedEvent> _validator;
    public MileageStatisticsService(IInformationValidator<VehicleRetrievedEvent> validator)
    {
      _validator = validator;
    }

    public ICollection<MileageStatistics> Create(
      Events.VehicleRetrievedEvent vehicleRetrievedEvent)
    {
      var stats = new List<MileageStatistics>();
      var validationInformation = _validator.Validate(vehicleRetrievedEvent);
      if (validationInformation.Result == Result.Invalid)
      {
        //TODO Log
        return stats;
      }

      var tests = vehicleRetrievedEvent.Vehicle.MotTests;

      //Order by Oldest Test first
      var orderedTests = tests.LatestRelevant()
                              .OrderBy(x => x.CompletedDate);

      if (!orderedTests.Any())
        return stats;
      
      var startYear = vehicleRetrievedEvent.Vehicle.FirstUsedDate.Year;
      var firstMotEndYear = startYear + vehicleRetrievedEvent.YearsToFirstMot;

      //First Mot Data
      stats.Add(new MileageStatistics
      {
        StartYear = startYear,
        EndYear = firstMotEndYear,
        Mileage = vehicleRetrievedEvent.AverageMilesFirstMot
      });

      if (orderedTests.Count() == 1)
        return stats;

      var laterMotTests = orderedTests.Where(x => x.CompletedDate.Year > firstMotEndYear);
      if (laterMotTests == null || !laterMotTests.Any())
      {
        //TODO Log
        return stats;
      }

      var lastYearsMileage = orderedTests.FirstOrDefault().Mileage;

      foreach(var laterTest in laterMotTests)
      {
        stats.Add(new MileageStatistics
        {
          StartYear = laterTest.CompletedDate.Year - 1,
          EndYear = laterTest.CompletedDate.Year,
          Mileage = laterTest.Mileage - lastYearsMileage
        });

        lastYearsMileage = laterTest.Mileage;
      }
      return stats;
    }
  }
}
