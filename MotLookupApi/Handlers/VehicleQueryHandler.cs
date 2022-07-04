using MediatR;
using Microsoft.Extensions.Logging;
using MotLookupApi.Events;
using MotLookupApi.Framework.Interfaces;
using MotLookupApi.Framework.Models;
using MotLookupApi.Utils.Validation.Enums;
using MotLookupApi.Utils.Validation.Interfaces;

namespace MotLookupApi.Handlers
{
  public class VehicleQueryHandler :
    INotificationHandler<VehicleRetrievedEvent>
  {
    private readonly IVehicleQueryService _vehicleQueryService;
    private readonly IInformationValidator<Vehicle> _validator;
    private readonly ILogger _logger;
    public VehicleQueryHandler(IVehicleQueryService vehicleQueryService,
      IInformationValidator<Vehicle> validator, ILogger<VehicleQueryHandler> logger)
    {
      _vehicleQueryService = vehicleQueryService;
      _validator = validator;
      _logger = logger;
    }

    public async Task Handle(VehicleRetrievedEvent notification, CancellationToken cancellationToken)
    {
      if (notification == null)
        throw new ArgumentNullException(nameof(notification));

      var validation = _validator.Validate(notification.Vehicle);
      if (validation.Result == Result.Invalid)
      {
        _logger.LogInformation($"VehicleQueryHandler failed validation for vehicle: {notification.Vehicle.Registration}");        
        return;
      }

      var query = new VehicleQuery
      {
        VehicleId = notification.Vehicle.Id
      };

      var savedQuery = await _vehicleQueryService.Save(query);
    }
  }
}
