using MotLookupApi.Utils.Validation.Enums;

namespace MotLookupApi.Utils.Validation.Interfaces
{
  public interface IValidator<T> where T : class
  {
    Result Validate(T item);
  }
}
