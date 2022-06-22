using MotLookupApi.Framework.Models;

namespace MotLookupApi.Models
{
  public class MotUpdaterResponse
  {
    public bool Success { get; private set; }
    public Vehicle Vehicle { get; private set; }
    public MotUpdaterResponse(Vehicle vehicle, bool success = false)
    {
      this.Success = success;
      this.Vehicle = vehicle;
    }
  }
}
