using AW.Utilities.Validation.Enums;
using AW.Utilities.Validation.Interfaces;
using MediatR;
using MotLookupApi.Events;
using MotLookupApi.Framework.Extensions;
using MotLookupApi.Framework.Interfaces;
using MotLookupApi.Framework.Models;

namespace MotLookupApi.Handlers
{
  public class TotalAverageMileageHandler : INotificationHandler<VehicleRetrievedEvent>
  {
    private readonly IInformationValidator<Vehicle> _validator;
    private readonly IVehicleService _vehicleService;
    public TotalAverageMileageHandler(IInformationValidator<Vehicle> validator,
      IVehicleService vehicleService)
    {
      _validator = validator;
      _vehicleService = vehicleService;
    }

    public async Task Handle(VehicleRetrievedEvent notification, CancellationToken cancellationToken)
    {
      if (notification == null)
        throw new ArgumentNullException(nameof(notification));

      var validation = _validator.Validate(notification.Vehicle);
      if (validation.Result == Result.Invalid)
        return;

      if (_vehicleService.IsNewCar(notification.Vehicle))
        return;

      var vehicle = notification.Vehicle;
      var orderedTests = vehicle.MotTests.LatestRelevant()
                                         .OrderByDescending(x => x.CompletedDate);
      var latestTest = orderedTests.FirstOrDefault();
      if (latestTest == null)
        return;
      
      var latestYear = latestTest.CompletedDate.Year;
      var years = latestYear - vehicle.FirstUsedDate.Year;

      notification.AverageMilesPerYear = latestTest.Mileage / years;
    }
  }
}
