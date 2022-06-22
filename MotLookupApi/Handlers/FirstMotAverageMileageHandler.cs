using AW.Utilities.Validation.Enums;
using AW.Utilities.Validation.Interfaces;
using MediatR;
using MotLookupApi.Events;
using MotLookupApi.Framework.Interfaces;
using MotLookupApi.Framework.Models;

namespace MotLookupApi.Handlers
{
  public class FirstMotAverageMileageHandler : INotificationHandler<VehicleRetrievedEvent>
  {
    private readonly IInformationValidator<Vehicle> _validator;
    private readonly IVehicleService _vehicleService;
    public FirstMotAverageMileageHandler(IInformationValidator<Vehicle> validator,
      IVehicleService vehicleService)
    {
      _validator = validator;
      _vehicleService = vehicleService;
    }

    public async Task Handle(VehicleRetrievedEvent notification, 
      CancellationToken cancellationToken)
    {
      if (notification == null)
        throw new ArgumentNullException(nameof(notification));

      var validation = _validator.Validate(notification.Vehicle);
      if (validation.Result == Result.Invalid)
        return;

      if (_vehicleService.IsNewCar(notification.Vehicle))
        return;

      var vehicle = notification.Vehicle;
      var orderTests = vehicle.MotTests.OrderByDescending(x => x.CompletedDate);

      var years = orderTests.Last().CompletedDate.Year - vehicle.FirstUsedDate.Year;
      notification.AverageMilesFirstMot = orderTests.Last().Mileage / years;
      notification.YearsToFirstMot = orderTests.LastOrDefault().CompletedDate.Year - vehicle.FirstUsedDate.Year;
    }
  }
}
