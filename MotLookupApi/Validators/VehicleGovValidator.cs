using AW.Utilities.Validation.Enums;
using AW.Utilities.Validation.Interfaces;
using AW.Utilities.Validation.Models;
using MotLookupApi.Gov.Uk.Models;

namespace MotLookupApi.Validators
{
  public class VehicleGovValidator : IInformationValidator<VehicleGovModel>
  {
    public ValidationInformation Validate(VehicleGovModel instance)
    {
      if (instance == null)
        throw new ArgumentNullException(nameof(instance));

      var info = new ValidationInformation
      {
        Result = Result.Valid,
        Message = string.Empty
      };

      if (string.IsNullOrEmpty(instance.Registration))
      {
        info.Result = Result.Invalid;
        info.Message = "Error Vehicle must have a registration";
      }
      
      return info;
    }
  }
}
