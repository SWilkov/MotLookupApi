using AW.Utilities.Validation.Enums;
using AW.Utilities.Validation.Interfaces;
using AW.Utilities.Validation.Models;
using MotLookupApi.Framework.Interfaces;
using MotLookupApi.Framework.Models;

namespace MotLookupApi.Validators
{
  public class VehicleValidator : IInformationValidator<Vehicle>
  {
    private readonly IVehicleService _vehicleService;
    public VehicleValidator(IVehicleService vehicleService)
    {
      _vehicleService = vehicleService;
    }
        
    public ValidationInformation Validate(Vehicle instance)
    {
      if (instance == null)
        throw new ArgumentNullException(nameof(instance));

      var info = new ValidationInformation
      {
        Result = Result.Valid,
        Message = string.Empty
      };

      //Not a new car so check has MOT tests
      if (!_vehicleService.IsNewCar(instance))
      {
        if (instance.MotTests == null || !instance.MotTests.Any())
        {
          info.Result = Result.Invalid;
          info.Message = $"No MOT tests found for {instance.Registration}. ";
        }
      }

      if (string.IsNullOrEmpty(instance.Registration))
      {
        info.Result = Result.Invalid;
        info.Message += $"No registration found. ";
      }

      return info;
    }
  }
}
