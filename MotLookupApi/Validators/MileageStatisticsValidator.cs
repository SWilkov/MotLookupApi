using MotLookupApi.Events;
using MotLookupApi.Utils.Validation.Enums;
using MotLookupApi.Utils.Validation.Interfaces;
using MotLookupApi.Utils.Validation.Models;

namespace MotLookupApi.Validators
{
  public class MileageStatisticsValidator
    : IInformationValidator<VehicleRetrievedEvent>
  {
    public MileageStatisticsValidator()
    {
     
    }

    public ValidationInformation Validate(VehicleRetrievedEvent instance)
    {
      if (instance == null)
        throw new ArgumentNullException(nameof(instance));

      var validationInformation = new ValidationInformation
      {
        Result = Result.Valid,
        Message = string.Empty
      };

      if (instance.Vehicle == null)
      {
        validationInformation.Message = "Error Vehicle is null";
        validationInformation.Result = Result.Invalid;
        return validationInformation;
      }

      if (instance.Vehicle.MotTests == null)
      {
        validationInformation.Message = $"Error Vehicle MotTests is null for {instance.Vehicle.Registration}";
        validationInformation.Result = Result.Invalid;
        return validationInformation;
      }

      var firstUsedPlus = instance.Vehicle.FirstUsedDate.AddYears(instance.YearsToFirstMot);
      if (DateTimeOffset.Now < firstUsedPlus && !instance.Vehicle.MotTests.Any())
      {
        validationInformation.Message = $"Failed Validation. Car {instance.Vehicle.Registration} is too new for any MOT mileage data";
        validationInformation.Result = Result.Invalid;
      }

      return validationInformation;
    }
  }
}
