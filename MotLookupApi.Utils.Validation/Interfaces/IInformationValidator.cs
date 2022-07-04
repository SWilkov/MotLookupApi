using MotLookupApi.Utils.Validation.Models;

namespace MotLookupApi.Utils.Validation.Interfaces
{
  public interface IInformationValidator<T> where T : class
  {
    ValidationInformation Validate(T item);
  }
}
